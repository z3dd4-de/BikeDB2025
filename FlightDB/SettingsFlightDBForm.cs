using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024.FlightDB
{
    public partial class SettingsFlightDBForm : Form
    {
        public SettingsFlightDBForm()
        {
            InitializeComponent();
        }

        private void SettingsFlightDBForm_Load(object sender, EventArgs e)
        {
            FillGoogleMarkerTypeComboBox(takeoffComboBox);
            FillGoogleMarkerTypeComboBox(landingComboBox);
            FillLocationComboBox(airportComboBox, GpsType.AIRPORT);
            // Settings
            if (Properties.Settings.Default.GPSCoordAngle == true)
            {
                
            }
            if (Properties.Settings.Default.FDBAirportSetting != -1)
            {
                airportComboBox.SelectedValue = Properties.Settings.Default.FDBAirportSetting;
            }
            if (Properties.Settings.Default.FDBAirlineSetting != -1)
            {

            }
            if (Properties.Settings.Default.FDBPlaneSetting != -1)
            {

            }
            classComboBox.SelectedText = Properties.Settings.Default.FDBClassSetting;
            // Layout Takeoff
            if (Properties.Settings.Default.FDBTakeOffSetting >= 0)
            {
                Marker test = new Marker(Properties.Settings.Default.FDBTakeOffSetting);
                takeoffComboBox.SelectedValue = test.Value;
            }
            // Layout Landing
            if (Properties.Settings.Default.FDBLandingSetting >= 0)
            {
                Marker test = new Marker(Properties.Settings.Default.FDBLandingSetting);
                landingComboBox.SelectedValue = test.Value;
            }
            if (Properties.Settings.Default.FDBStartTypeSetting == 0)
            {
                startAirportRadioButton.Checked = true;
                FillLocationComboBox(startComboBox, GpsType.AIRPORT);
            }
            else
            {
                startCityRadioButton.Checked = true;
                FillLocationComboBox(startComboBox, GpsType.CITY);
            }
            // Start Position
            zoomTrackBar.Minimum = 1;
            zoomTrackBar.Maximum = 20;
            zoomTrackBar.Value = Properties.Settings.Default.FDBZoomSetting;
            zoomTextBox.Text = Properties.Settings.Default.FDBZoomSetting.ToString();
        }

        private void airportComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void airlineComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void planeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void classComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FDBClassSetting = classComboBox.SelectedText;
        }

        private void takeoffComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Marker test = (Marker)takeoffComboBox.SelectedItem;
            if (test != null)
            {
                MessageBox.Show(test.Value.ToString());
                Properties.Settings.Default.FDBTakeOffSetting = test.Value;
            }
        }

        private void landingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Marker test = (Marker)landingComboBox.SelectedItem;
            if (test != null)
            {
                MessageBox.Show(test.Value.ToString());
                Properties.Settings.Default.FDBLandingSetting = test.Value;
            }
        }

        private void startAirportRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            changeRadioButtons();
        }

        private void startCityRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            changeRadioButtons();
        }

        private void changeRadioButtons()
        {
            //startAirportRadioButton.Checked = !startAirportRadioButton.Checked;
            //startCityRadioButton.Checked = !startCityRadioButton.Checked;
            if (startAirportRadioButton.Checked)
            {
                Properties.Settings.Default.FDBStartTypeSetting = 0;
                FillLocationComboBox(startComboBox, GpsType.AIRPORT);
            }
            if (startCityRadioButton.Checked)
            {
                Properties.Settings.Default.FDBStartTypeSetting = 1;
                FillLocationComboBox(startComboBox, GpsType.CITY);
            }
        }

        private void startComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FDBStartSetting = startComboBox.SelectedText;
        }

        private void zoomTrackBar_Scroll(object sender, EventArgs e)
        {
            Properties.Settings.Default.FDBZoomSetting = zoomTrackBar.Value;
            zoomTextBox.Text = zoomTrackBar.Value.ToString();
        }

        private void SettingsFlightDBForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
