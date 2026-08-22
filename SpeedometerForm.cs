using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;


namespace BikeDB2024
{
    public enum BikeComputerModel
    {
        Unknown = 0,
        BC1612 = 0x15,
        BC1212 = 0x12,
        BC1612_STS = 0x16
    }

    public partial class SpeedometerForm : Form
    {
        public int SpeedometerID { get; set; }

        private ManagementEventWatcher deviceWatcher;
        private CancellationTokenSource _cts;

        public SpeedometerForm()
        {
            InitializeComponent();
        }

        private void StartDeviceWatcher()
        {
            deviceWatcher = new ManagementEventWatcher(
                new WqlEventQuery(
                    "SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2 OR EventType = 3"));

            deviceWatcher.EventArrived += (s, e) =>
            {
                // UI-Thread!
                BeginInvoke(new Action(LoadSigmaDevice));
            };

            deviceWatcher.Start();
        }


        private void SpeedometerForm_Load(object sender, EventArgs e)
        {
            errorToolStripStatusLabel.Text = "";
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Speedometers". Sie können sie bei Bedarf verschieben oder entfernen.
            this.speedometersTableAdapter.Fill(this.dataSet.Speedometers);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Vehicles". Sie können sie bei Bedarf verschieben oder entfernen.
            this.vehiclesTableAdapter.Fill(this.dataSet.Vehicles);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Companies". Sie können sie bei Bedarf verschieben oder entfernen.
            this.companiesTableAdapter.Fill(this.dataSet.Companies);

            companyComboBox.SelectedValue = 3; // Sigma Sport (currently only supported)
            StartDeviceWatcher();
            LoadSigmaDevice();

            if (SpeedometerID >= 0)
            {
                // Es gibt keine FillBySpeedometerID-Methode, daher filtern wir manuell:
                var rows = this.dataSet.Speedometers.Select($"Id = {SpeedometerID}");
                if (rows.Length > 0)
                {
                    // Optional: Sie können hier die Datenbindung aktualisieren, falls benötigt
                    this.speedometersBindingSource.DataSource = rows.CopyToDataTable();

                }
                else
                {
                    this.speedometersBindingSource.DataSource = null;
                }
            }
        }

        private void LoadSpeedometer()
        {
            SqlConnection con1;

            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
            {
                con1.Open();
                using (SqlCommand com1 = new())
                {
                    com1.CommandText = @"SELECT * FROM Speedometers WHERE Id = " + SpeedometerID.ToString() + " AND User = " + 
                        Properties.Settings.Default.CurrentUserID.ToString();
                    com1.CommandType = CommandType.Text;
                    com1.Connection = con1;
                    using (SqlDataReader reader1 = com1.ExecuteReader())
                    {
                        while (reader1.Read())
                        {
                            vehicleComboBox.SelectedValue = Convert.ToInt32(reader1[3]);
                            nameTextBox.Text = reader1[1].ToString();
                            companyComboBox.SelectedValue = Convert.ToInt32(reader1[4]);
                            dsCheckBox.Checked = Convert.ToBoolean(reader1[5]);
                            idTextBox.Text = reader1[6].ToString();  //TODO: Hex?
                            serialTextBox.Text = reader1[2].ToString();
                            
                            errorToolStripStatusLabel.Text = "Geladen: Speedometers - Datensatz " + SpeedometerID.ToString();
                        }
                        reader1.Close();
                    }
                }
                con1.Close();
            }
        }

        string FindSigmaComPort()
        {
            var searcher = new ManagementObjectSearcher(
                "SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'");

            foreach (ManagementObject device in searcher.Get())
            {
                string name = device["Name"]?.ToString() ?? "";

                // 🔎 Nur dein Gerät
                if (name.IndexOf("SIGMA USB", StringComparison.OrdinalIgnoreCase) < 0)
                    continue;

                // COM-Port aus "(COM7)" extrahieren
                var match = Regex.Match(name, @"\(COM\d+\)");
                if (match.Success)
                {
                    return match.Value.Trim('(', ')'); // COM7
                }
            }

            return null;
        }


        private void LoadSigmaDevice()
        {
            portsComboBox.Items.Clear();

            string port = FindSigmaComPort();

            if (port != null)
            {
                portsComboBox.Items.Add($"SIGMA USB ({port})");
                portsComboBox.SelectedIndex = 0;
            }
            else
            {
                portsComboBox.Items.Add("SIGMA USB nicht angeschlossen");
                portsComboBox.SelectedIndex = 0;
            }
        }

        private void companyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (dsCheckBox.Checked)
            {
                portsComboBox.Enabled = true;
                idTextBox.Enabled = true;
                tachoSearchButton.Enabled = true;
            }
            else
            {
                portsComboBox.Enabled = false;
                idTextBox.Enabled = false;
                tachoSearchButton.Enabled = false;
            }
        }

        private void SpeedometerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            deviceWatcher?.Stop();
            deviceWatcher?.Dispose();
            base.OnFormClosed(e);
        }

        private async void tachoSearchButton_Click(object sender, EventArgs e)
        {
            tachoSearchButton.Enabled = false;
            errorToolStripStatusLabel.Text = "Suche Sigma-Tacho...";

            _cts = new CancellationTokenSource();

            var detector = new SigmaDetector();

            var info = await detector.TryDetectWithReconnectAsync(
                retries: 5,
                delay: TimeSpan.FromSeconds(1),
                token: _cts.Token);

            if (info == null)
            {
                errorToolStripStatusLabel.Text = "Kein Gerät gefunden.";
            }
            else
            {
                errorToolStripStatusLabel.Text = "Gerät erkannt";
                nameTextBox.Text = info.ModelName;
                serialTextBox.Text = info.SerialNumber;
                //txtVersion.Text = info.Version.ToString();
            }

            tachoSearchButton.Enabled = true;
        }
    }

    public class BikeComputerInfo
    {
        public BikeComputerModel Model { get; set; }
        public string ModelName { get; set; } = "";
        public byte Version { get; set; }
        public string SerialNumber { get; set; } = "";
        public byte Type { get; set; }
    }

}
