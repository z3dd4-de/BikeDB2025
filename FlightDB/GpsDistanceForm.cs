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
        /// Enumeration for Location objects.
        /// </summary>
        enum GpsType { AIRPORT, CITY };

        /// <summary>
        /// Constructor.
        /// </summary>
        public GpsDistanceForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load a Location object (airport or city) into a ComboBox. 
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="type"></param>
        private void fillComboBox(ComboBox cb, GpsType type)
        {
            SqlConnection con1;
            List<Location> data = new List<Location>();
            cb.DataSource = null;
            cb.Sorted = true;
            cb.Items.Clear();
            cb.DisplayMember = "Text";
            cb.ValueMember = "Value";

            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        switch (type)
                        {
                            case GpsType.AIRPORT:
                                com1.CommandText = @"SELECT * FROM Airports WHERE GPS IS NOT NULL";    //[User] = " + Properties.Settings.Default.CurrentUserID.ToString();
                                com1.CommandType = CommandType.Text;
                                com1.Connection = con1;
                                using (SqlDataReader reader1 = com1.ExecuteReader())
                                {
                                    while (reader1.Read())
                                    {
                                        data.Add(new Airport(reader1.GetInt32(0)));
                                    }
                                    reader1.Close();
                                }
                                break;
                            case GpsType.CITY:
                                com1.CommandText = @"SELECT * FROM Cities WHERE Gps IS NOT NULL";    //[User] = " + Properties.Settings.Default.CurrentUserID.ToString();
                                com1.CommandType = CommandType.Text;
                                com1.Connection = con1;
                                using (SqlDataReader reader1 = com1.ExecuteReader())
                                {
                                    while (reader1.Read())
                                    {
                                        data.Add(new City(reader1.GetInt32(0)));
                                    }
                                    reader1.Close();
                                }
                                break;
                        }
                    }
                    con1.Close();
                }
                cb.DataSource = data;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Laden des Ortes");
            }
        }

        /// <summary>
        /// Preload ComboBoxes and initialize the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GpsDistanceForm_Load(object sender, EventArgs e)
        {
            distanceLabel.Text = "";
            fillComboBox(startComboBox, GpsType.CITY);
            fillComboBox(endComboBox, GpsType.CITY);
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
                fillComboBox(startComboBox, GpsType.CITY);
            }
        }

        private void airport1RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            fillComboBox(startComboBox, GpsType.AIRPORT);
        }

        private void city2RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            fillComboBox(endComboBox, GpsType.CITY);
        }

        private void airport2RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            fillComboBox(endComboBox, GpsType.AIRPORT);
        }
        #endregion
    }
}
