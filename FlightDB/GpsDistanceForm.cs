using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024.FlightDB
{
    public partial class GpsDistanceForm : Form
    {
        double lat1;
        double lat2;
        double lon1;
        double lon2;
        GpsCoordinate coord1;
        GpsCoordinate coord2;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GpsDistanceForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Preload ComboBoxes and initialize the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GpsDistanceForm_Load(object sender, EventArgs e)
        {
            distanceLabel.Text = "";
            FillLocationComboBox(startComboBox, GpsType.CITY);
            FillLocationComboBox(endComboBox, GpsType.CITY);
        }

        /// <summary>
        /// When the ComboBox entry changes, new Coordinates have to be created.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (startComboBox.Text != String.Empty)
            {
                Location loc = (Location)startComboBox.SelectedItem;
                loadCoordinate(lat1TextBox, lon1TextBox, loc.GpsString);
            }   
        }

        /// <summary>
        /// When the ComboBox entry changes, new Coordinates have to be created.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (endComboBox.Text != String.Empty)
            {
                Location loc = (Location)endComboBox.SelectedItem;
                loadCoordinate(lat2TextBox, lon2TextBox, loc.GpsString);
            }
        }

        /// <summary>
        /// Load the GPS coordinates.
        /// </summary>
        /// <param name="tb1"></param>
        /// <param name="tb2"></param>
        /// <param name="coord"></param>
        private void loadCoordinate(TextBox tb1, TextBox tb2, string coord)
        {
            if (tb1 == lat1TextBox)
            {
                coord1 = new GpsCoordinate(coord);
                tb1.Text = coord1.Latitude;
                tb2.Text = coord1.Longitude;
                lat1 = coord1.LatitudeValue;
                lon1 = coord1.LongitudeValue;
            }
            else if (tb1 ==  lat2TextBox)
            {
                coord2 = new GpsCoordinate(coord);
                tb1.Text = coord2.Latitude;
                tb2.Text = coord2.Longitude;
                lat2 = coord2.LatitudeValue;
                lon2 = coord2.LongitudeValue;
            }
            checkButtonState();
        }

        #region Check Button State
        private void lat1TextBox_TextChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        private void lat2TextBox_TextChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        private void lon1TextBox_TextChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        private void lon2TextBox_TextChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        private void checkButtonState()
        {
            if (lat1TextBox.Text != String.Empty && lat2TextBox.Text != String.Empty
                && lon1TextBox.Text != String.Empty && lon2TextBox.Text != String.Empty)
            {
                calcButton.Enabled = true;
            }
            else { calcButton.Enabled = false; }
        }
        #endregion

        /// <summary>
        /// Calculate Great Distance between two GPS coordinates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calcButton_Click(object sender, EventArgs e)
        {
            if (lat1 > 0 && lat2 > 0 && lon1 > 0 && lon2 > 0)
            {
                distanceLabel.Text = coord1.GreatCircleDistance(lat1, lon1, lat2, lon2).ToString("F2", CultureInfo.CreateSpecificCulture("de-DE")) + " km";
            }
        }

        #region Change Location object and refill ComboBoxes.
        private void city1RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (city1RadioButton.Checked)
            {
                FillLocationComboBox(startComboBox, GpsType.CITY);
            }
        }

        private void airport1RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            FillLocationComboBox(startComboBox, GpsType.AIRPORT);
        }

        private void city2RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            FillLocationComboBox(endComboBox, GpsType.CITY);
        }

        private void airport2RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            FillLocationComboBox(endComboBox, GpsType.AIRPORT);
        }
        #endregion
    }
}
