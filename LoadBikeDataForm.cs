using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BikeDB2024
{
    public partial class LoadBikeDataForm : Form
    {
        public RideData data;
        public int VehicleID => (int)vehicleComboBox.SelectedValue;
        public int SelectedVehicleID { get; private set; } = -1;


        public LoadBikeDataForm(RideData data)
        {
            InitializeComponent();
            this.data = data;
        }

        private void LoadBikeDataForm_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Vehicles". Sie können sie bei Bedarf verschieben oder entfernen.
            this.vehiclesTableAdapter.Fill(this.dataSet.Vehicles);
            if (data != null)
            {
                //bikeLabel.Text = data.BikeName;
                bool found = false;
                foreach (DataRowView row in vehicleComboBox.Items)
                {
                    if ((string)row["BikeName"] == data.BikeName)
                    {
                        vehicleComboBox.SelectedValue = row["VehicleID"];
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    errorToolTip.SetToolTip(vehicleComboBox, $"Fahrrad nicht in der Datenbank gefunden: {data.BikeName}");
                }
                dateTimePicker.Value = data.Timestamp;
                kmLabel.Text = $"{data.DistanceKm:F2} km";
                timeLabel.Text = data.Duration.ToString(@"hh\:mm\:ss");
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (data != null)
            {
                this.data.Timestamp = dateTimePicker.Value;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
            if (vehicleComboBox.SelectedValue != null)
                SelectedVehicleID = Convert.ToInt32(vehicleComboBox.SelectedValue);
            else
                SelectedVehicleID = -1;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
