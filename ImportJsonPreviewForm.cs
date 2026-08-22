using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;

namespace BikeDB2024
{
    public partial class ImportJsonPreviewForm : Form
    {
        public string SelectedFilePath;
        public RideData RideData;

        public ImportJsonPreviewForm(string path)
        {
            InitializeComponent();
            SelectedFilePath = path;
            fileLabel.Text = SelectedFilePath;
            RideData = GetRideData();
            PreviewRideData();
        }

        public RideData GetRideData()
        {
            string jsonString = File.ReadAllText(SelectedFilePath);
            RideData? rideData = JsonSerializer.Deserialize<RideData>(jsonString);
            return rideData;
        }

        private void PreviewRideData()
        {
            if (RideData != null)
            {
                contentRichTextBox.Text = $"Datum: \t{RideData.Timestamp.ToString("dd.MM.yyyy HH:mm")}\r\n" +
                                      $"Entfernung: \t{RideData.DistanceKm} km\r\n" +
                                      $"Fahrzeit: \t{RideData.Duration} min\r\n" +
                                      $"Durchschnitt: \t{RideData.MeanSpeedKmh} km/h\r\n" +
                                      $"Vmax: \t{RideData.MaxSpeedKmh} km/h\r\n" +
                                      $"Fahrrad: \t{RideData.BikeName}";
            }
            else
            {
                contentRichTextBox.Text = "Tachodaten konnten nicht geladen werden.";
            }
        }

        private void changeFileButton_Click(object sender, EventArgs e)
        {
            ImportJsonForm json = new();
            json.ShowDialog();
            Close();
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            //TODO: Implement import logic here
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
