using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using BikeDB2024.FlightDB;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class FlightDBForm : Form
    {
        #region Private Variables
        private int tabPage = 0;
        private int cnt_flights = 0;
        private int cnt_airports = 0;
        private int cnt_planes = 0;
        private int cnt_cities = 0;
        private int cnt_airlines = 0;
        private int cnt_costs = 0;
        private int cnt_countries = 0;
        private int current_flight = -1;
        private int current_airport = -1;
        private int current_plane = -1;
        private int current_city = -1;
        private int current_airline = -1;
        private int current_cost = -1;
        private int current_country = -1;
        private bool edit_data = false;
        private bool logged_in = false;
        // Ids as Arrays for navigation
        private int[] flight_ids = null;
        private int flight_id = 0;
        private int[] airport_ids = null;
        private int airport_id = 0;
        private int[] city_ids = null;
        private int city_id = 0;
        private int[] plane_ids = null;
        private int plane_id = 0;
        private int[] airline_ids = null;
        private int airline_id = 0;
        private int[] cost_ids = null;
        private int cost_id = 0;
        private int[] country_ids = null;
        private int country_id = 0;

        public int CurrentUserId { get; set; }
        public string CurrentUserName { get; set; }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public FlightDBForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Executed when the FlightDBForm is loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlightDBForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.FlightDBLocation != new Point(100, 100))
                this.Location = Properties.Settings.Default.FlightDBLocation;
            else this.Location = new Point(100, 100);
            if (Properties.Settings.Default.FlightDBSize != new Size(800, 500))
                this.Size = Properties.Settings.Default.FlightDBSize;
            else this.Size = new Size(800, 500);
            if (CurrentUserId >= 0 && CurrentUserName != String.Empty)
            {
                logged_in = true;
                flightDbToolStripStatusLabel.Text = 
                    Properties.Settings.Default.CurrentUserName + " (" + CurrentUserId.ToString() + ")";
            }
            else
                logged_in = false;
            checkLogin();
        }

        /// <summary>
        /// Check local login status.
        /// </summary>
        private void checkLogin()
        {
            if (logged_in)
            {
                neuToolStripMenuItem.Visible = true;
                statistikToolStripMenuItem.Visible = true;
                importierenToolStripMenuItem.Visible = true;
                exportierenToolStripMenuItem.Visible = true;
                toolStripMenuItem1.Visible = true;
                toolStripMenuItem2.Visible = true;
                flightToolStripButton.Visible = true;
                airportToolStripButton.Visible = true;
                airlinetoolStripButton.Visible = true;
                planeToolStripButton.Visible = true;
                manufacturerToolStripButton.Visible = true;
                cost0ToolStripButton.Visible = true;
                countryToolStripButton.Visible = true;
                cityToolStripButton.Visible = true;
                toolStripSeparator2.Visible = true;
                flightDbTabControl.Visible = true;
            }
            else
            {
                neuToolStripMenuItem.Visible = false;
                statistikToolStripMenuItem.Visible = false;
                importierenToolStripMenuItem.Visible = false;
                exportierenToolStripMenuItem.Visible = false;
                toolStripMenuItem1.Visible = false;
                toolStripMenuItem2.Visible = false;
                flightToolStripButton.Visible = false;
                airportToolStripButton.Visible = false;
                airlinetoolStripButton.Visible = false;
                planeToolStripButton.Visible = false;
                manufacturerToolStripButton.Visible = false;
                cost0ToolStripButton.Visible = false;
                countryToolStripButton.Visible = false;
                cityToolStripButton.Visible = false;
                toolStripSeparator2.Visible = false;
                flightDbTabControl.Visible = false;
            }
        }

        /// <summary>
        /// Save settings when the form closes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlightDBForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.FlightDBLocation = this.Location;
            Properties.Settings.Default.FlightDBSize = this.Size;
        }

        /// <summary>
        /// Change tabpage on flightDbTabControl.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flightDbTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabPage = flightDbTabControl.SelectedIndex;
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        #region Close form
        private void schließenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void closeToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Show Help
        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            showHelp();
        }

        private void hilfeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showHelp();
        }

        private void showHelp()
        {

        }
        #endregion

        #region Show Info
        private void infoToolStripButton_Click(object sender, EventArgs e)
        {
            showInfo();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showInfo();
        }

        private void showInfo()
        {
            AboutFlightDBForm aboutFlightDBForm = new AboutFlightDBForm();
            aboutFlightDBForm.ShowDialog();
        }
        #endregion

        #region New Flight
        private void flugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showFlightForm();
        }

        private void flightToolStripButton_Click(object sender, EventArgs e)
        {
            showFlightForm();
        }

        //TODO: Remove FlightForm as it is integrated in tabpage = 0
        private void showFlightForm()
        {
            /*FlightForm flightForm = new FlightForm();
            if (flightForm.ShowDialog() == DialogResult.OK)
            {

            }*/
            flightDbTabControl.SelectedIndex = 0;
        }
        #endregion

        #region New Airport
        private void flughafenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showAirportForm();
        }

        private void airportToolStripButton_Click(object sender, EventArgs e)
        {
            showAirportForm();
        }
        
        private void showAirportForm()
        {
            AirportForm airportForm = new AirportForm();
            if (airportForm.ShowDialog() == DialogResult.OK)
            {

            }
        }
        #endregion

        #region New Plane
        private void flugzeugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPlaneForm();
        }

        private void planeToolStripButton_Click(object sender, EventArgs e)
        {
            showPlaneForm();
        }

        private void showPlaneForm()
        {
            PlaneForm planeForm = new PlaneForm();
            if (planeForm.ShowDialog() == DialogResult.OK)
            {

            }
        }
        #endregion

        #region New Costs
        private void kostenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showCosts();
        }

        private void costToolStripButton_Click(object sender, EventArgs e)
        {
            showCosts();
        }

        private void showCosts()
        {
            CostsForm costForm = new CostsForm();
            costForm.IsFlight = true;
            if (costForm.ShowDialog() == DialogResult.OK)
            {

            }
        }
        #endregion

        #region New Country
        private void landToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showCountryForm();
        }

        private void countryToolStripButton_Click(object sender, EventArgs e)
        {
            showCountryForm();
        }

        private void showCountryForm()
        {
            CountryForm countryForm = new CountryForm();
            if (countryForm.ShowDialog() == DialogResult.OK)
            {

            }
        }
        #endregion

        #region New City
        private void stadtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showCityForm();
        }

        private void cityToolStripButton_Click(object sender, EventArgs e)
        {
            showCityForm();
        }

        private void showCityForm()
        {
            CityForm cityForm = new CityForm();
            if (cityForm.ShowDialog() == DialogResult.OK)
            {

            }
        }
        #endregion

        #region New Airline
        private void fluggesellschaftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showAirlineForm();
        }

        private void airlinetoolStripButton_Click(object sender, EventArgs e)
        {
            showAirlineForm();
        }

        private void showAirlineForm()
        {
            AirlineForm airlineForm = new AirlineForm();
            if (airlineForm.ShowDialog() == DialogResult.OK)
            {

            }
        }
        #endregion

        #region New Plane Manufacturer
        private void flugzeugherstellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showConstructorForm();
        }

        private void manufacturerToolStripButton_Click(object sender, EventArgs e)
        {
            showConstructorForm();
        }

        private void showConstructorForm()
        {
            PlaneManufacturersForm planeManufacturersForm = new PlaneManufacturersForm();
            if (planeManufacturersForm.ShowDialog() == DialogResult.OK)
            {

            }
        }
        #endregion

        #region AddButton: new/edit Flight
        /// <summary>
        /// Add or edit a flight.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            SqlConnection myConnection;
            if (!edit_data)
            {
                try
                {
                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        myConnection.Open();
                        using (SqlCommand myCommand = new SqlCommand())
                        {
                            int id = NextId("Flights");
                            string sqlquery = "INSERT INTO Flights " +
                            "(Id, Date, DateString, Airline, Plane, FlightNumber, Takeoff, Landing, Remark, Seat, Class, " +
                            "Image, FileyType, NotShown, Created, LastChanged, [User]) " +
                            "VALUES (@id, @date, @datestr, @airline, @plane, @flightnr, @start, @end, @remark, @seat, @class, " +
                            "@image, @type, @notshown, @created, @lastchanged, @user)";
                            myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            if (unknownCheckBox.Checked)
                            {
                                myCommand.Parameters.Add("@date", SqlDbType.Date).Value = null;
                                myCommand.Parameters.Add("@datestr", SqlDbType.NChar).Value = datestrTextBox.Text;
                            }
                            else
                            {
                                myCommand.Parameters.Add("@date", SqlDbType.Date).Value = dateTimePicker.Value;
                                myCommand.Parameters.Add("@datestr", SqlDbType.NChar).Value = "";
                            }
                            myCommand.Parameters.Add("@airline", SqlDbType.Int).Value = airlineComboBox.SelectedValue;
                            myCommand.Parameters.Add("@plane", SqlDbType.Int).Value = planeComboBox.SelectedValue;
                            myCommand.Parameters.Add("@flightnr", SqlDbType.NVarChar).Value = flightTextBox.Text;
                            myCommand.Parameters.Add("@start", SqlDbType.Int).Value = startComboBox.SelectedValue;
                            myCommand.Parameters.Add("@end", SqlDbType.Int).Value = landingComboBox.SelectedValue;
                            myCommand.Parameters.Add("@remark", SqlDbType.NVarChar).Value = descRichTextBox.Text;
                            myCommand.Parameters.Add("@seat", SqlDbType.NChar).Value = seatTextBox.Text;
                            myCommand.Parameters.Add("@class", SqlDbType.Int).Value = classComboBox.SelectedValue;
                            myCommand.Parameters.Add("@image", SqlDbType.NVarChar).Value = fileTextBox.Text;
                            myCommand.Parameters.Add("@type", SqlDbType.Int).Value = fileTypeComboBox.SelectedIndex;
                            myCommand.Parameters.Add("@notshown", SqlDbType.TinyInt).Value = GetTinyIntFromBool(false);
                            myCommand.Parameters.Add("@created", SqlDbType.DateTime).Value = DateTime.Now;
                            myCommand.Parameters.Add("@lastchanged", SqlDbType.DateTime).Value = DateTime.Now;
                            myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;

                            myCommand.CommandText = sqlquery;
                            myCommand.CommandType = CommandType.Text;
                            myCommand.Connection = myConnection;
                            myCommand.ExecuteNonQuery();
                        }
                        myConnection.Close();
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, "Fehler beim Speichern des Fluges");
                }
            }
            else
            {
                var sql = @"UPDATE Flights SET Date = @date, DateString = @datestr, Airline = @airline, Plane = @plane, " +
                        "FlightNumber = @flightnr, Takeoff = @start, Landing = @end, Remark = @remark, Seat = @seat, " +
                        "Class = @class, Image = @image, FileType = @type " +
                        "LastChanged = @lastchanged, [User] = @user " +
                        "WHERE Id = " + current_flight.ToString();
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        using (var myCommand = new SqlCommand(sql, connection))
                        {
                            if (unknownCheckBox.Checked)
                            {
                                myCommand.Parameters.Add("@date", SqlDbType.Date).Value = null;
                                myCommand.Parameters.Add("@datestr", SqlDbType.NChar).Value = datestrTextBox.Text;
                            }
                            else
                            {
                                myCommand.Parameters.Add("@date", SqlDbType.Date).Value = dateTimePicker.Value;
                                myCommand.Parameters.Add("@datestr", SqlDbType.NChar).Value = "";
                            }
                            myCommand.Parameters.Add("@airline", SqlDbType.Int).Value = airlineComboBox.SelectedValue;
                            myCommand.Parameters.Add("@plane", SqlDbType.Int).Value = planeComboBox.SelectedValue;
                            myCommand.Parameters.Add("@flightnr", SqlDbType.NVarChar).Value = flightTextBox.Text;
                            myCommand.Parameters.Add("@start", SqlDbType.Int).Value = startComboBox.SelectedValue;
                            myCommand.Parameters.Add("@end", SqlDbType.Int).Value = landingComboBox.SelectedValue;
                            myCommand.Parameters.Add("@remark", SqlDbType.NVarChar).Value = descRichTextBox.Text;
                            myCommand.Parameters.Add("@seat", SqlDbType.NChar).Value = seatTextBox.Text;
                            myCommand.Parameters.Add("@class", SqlDbType.Int).Value = classComboBox.SelectedValue;
                            myCommand.Parameters.Add("@image", SqlDbType.NVarChar).Value = fileTextBox.Text;
                            myCommand.Parameters.Add("@type", SqlDbType.Int).Value = fileTypeComboBox.SelectedIndex;
                            //myCommand.Parameters.Add("@notshown", SqlDbType.TinyInt).Value = GetTinyIntFromBool(false);
                            myCommand.Parameters.Add("@lastchanged", SqlDbType.DateTime).Value = DateTime.Now;
                            myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                            
                            connection.Open();
                            myCommand.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, "Fehler beim Update des Fluges");
                }
            }
        }

        /// <summary>
        /// If date is unknown enable date string textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void unknownCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (unknownCheckBox.Checked)
            {
                datestrTextBox.Enabled = true;
                dateTimePicker.Enabled = false;
            }
            else
            {
                datestrTextBox.Enabled = false;
                dateTimePicker.Enabled = true;
            }
        }

        /// <summary>
        /// Enable or disable fileButton.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileTypeComboBox.SelectedIndex == 2)    // Link
            {
                fileButton.Enabled = false;
            }
            else
            {
                fileButton.Enabled = true;
            }
        }

        /// <summary>
        /// Open file or folder depending on selected file type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileButton_Click(object sender, EventArgs e)
        {
            if (fileTypeComboBox.SelectedIndex == 0)
            {
                openFlightFileDialog.Filter = "PNG-Bilddatei|*.png|JPG-Bilddatei|*.jpg|GIF-Bilddatei|*.gif|Alle Dateien (*.*)|*.*";
                openFlightFileDialog.Title = "Bilddatei auswählen";
                if (openFlightFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileTextBox.Text = openFlightFileDialog.FileName;
                }
            }
            else if (fileTypeComboBox.SelectedIndex == 1)
            {
                flightFolderBrowserDialog.Description = "Test";
                flightFolderBrowserDialog.SelectedPath = Properties.Settings.Default.ImageFolder;
                if (flightFolderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    fileTextBox.Text = flightFolderBrowserDialog.SelectedPath;
                }
            }
            else if (fileTypeComboBox.SelectedIndex == 3)
            {
                openFlightFileDialog.Filter = "AVI-Datei|*.avi|MPG-Datei|*.mpg|MP4-Datei|*.mp4|Alle Dateien (*.*)|*.*";
                openFlightFileDialog.Title = "Videodatei auswählen";
                if (openFlightFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileTextBox.Text = openFlightFileDialog.FileName;
                }
            }
        }
        #endregion

        #region Flights
        private void flightToolStripButton0_Click(object sender, EventArgs e)
        {
            flight_id = 0;
            current_flight = flight_ids[flight_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void flightToolStripButton1_Click(object sender, EventArgs e)
        {
            flight_id--;
            current_flight = flight_ids[flight_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void flightToolStripButton2_Click(object sender, EventArgs e)
        {
            flight_id++;
            current_flight = flight_ids[flight_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void flightToolStripButton3_Click(object sender, EventArgs e)
        {
            flight_id = cnt_flights - 1;
            current_flight = flight_ids[flight_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Edit current flight.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flightToolStripButton4_Click(object sender, EventArgs e)
        {
            SqlConnection con1;
            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        com1.CommandText = $"SELECT * FROM Flights WHERE Id = " + current_flight.ToString();
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                if (reader1[1].ToString() != "")
                                {
                                    dateTimePicker.Value = Convert.ToDateTime(reader1[1]);
                                    dateTimePicker.Enabled = true;
                                    unknownCheckBox.Checked = false;
                                    datestrTextBox.Enabled = false;
                                }
                                if (reader1[2].ToString() != "")
                                {
                                    datestrTextBox.Text = reader1[2].ToString();
                                    datestrTextBox.Enabled = true;
                                    unknownCheckBox.Checked = true;
                                    dateTimePicker.Enabled = false;
                                }
                                airlineComboBox.SelectedValue = (int)reader1[3];
                                planeComboBox.SelectedValue = (int)reader1[4];
                                flightTextBox.Text = reader1[5].ToString();
                                startComboBox.SelectedValue = (int)reader1[6];
                                landingComboBox.SelectedValue = (int)reader1[7];
                                seatTextBox.Text = reader1[9].ToString();
                                classComboBox.SelectedValue = (int)reader1[10];
                                fileTextBox.Text = reader1[11].ToString();
                                fileTypeComboBox.SelectedIndex = (int)reader1[12];
                                descRichTextBox.Text = reader1[11].ToString();
                            }
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
                edit_data = true;
                addButton.Text = "Bearbeiten";
                flightDbTabControl.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Datenbankzugriff");
            }
        }

        /// <summary>
        /// Delete button might be removed...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flightToolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void showFlightToolStripSplitButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "showFlightCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.FLIGHT, false, current_flight);
            }
            else if (e.ClickedItem.Name == "showFlightNotCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.FLIGHT, true, current_flight);
            }
            showFlightToolStripSplitButton.Image = e.ClickedItem.Image;
        }
        #endregion

        #region Airports
        private void airport0ToolStripButton_Click(object sender, EventArgs e)
        {
            airport_id = 0;
            current_airport = airport_ids[airport_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void airport1ToolStripButton_Click(object sender, EventArgs e)
        {
            airport_id--;
            current_airport = airport_ids[airport_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void airport2ToolStripButton_Click(object sender, EventArgs e)
        {
            airport_id++;
            current_airport = airport_ids[airport_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void airport3ToolStripButton_Click(object sender, EventArgs e)
        {
            airport_id = cnt_airports - 1;
            current_airport = airport_ids[airport_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Edit current airport.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void airport4ToolStripButton_Click(object sender, EventArgs e)
        {
            AirportForm airportForm = new AirportForm();
            airportForm.Edit = true;
            airportForm.EditId = current_airline;
            if (airportForm.ShowDialog() == DialogResult.OK)
            {
                flightDbBackgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Delete button might be removed...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void airport5ToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void showAirportToolStripSplitButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "showAirportCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.AIRPORT, false, current_airport);
            }
            else if (e.ClickedItem.Name == "showAirportNotCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.AIRPORT, true, current_airport);
            }
            showAirportToolStripSplitButton.Image = e.ClickedItem.Image;
        }
        #endregion

        #region Planes
        private void plane0ToolStripButton_Click(object sender, EventArgs e)
        {
            plane_id = 0;
            current_plane = plane_ids[plane_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void plane1ToolStripButton_Click(object sender, EventArgs e)
        {
            plane_id--;
            current_plane = plane_ids[plane_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void plane2ToolStripButton_Click(object sender, EventArgs e)
        {
            plane_id++;
            current_plane = plane_ids[plane_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void plane3ToolStripButton_Click(object sender, EventArgs e)
        {
            plane_id = cnt_planes - 1;
            current_plane = plane_ids[plane_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Edit current plane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plane4ToolStripButton_Click(object sender, EventArgs e)
        {
            PlaneForm planeForm = new PlaneForm();
            planeForm.Edit = true;
            planeForm.EditId = current_airline;
            if (planeForm.ShowDialog() == DialogResult.OK)
            {
                flightDbBackgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Delete button might be removed...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plane5ToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void showPlaneToolStripSplitButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "showPlanesCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.PLANE, false, current_plane);
            }
            else if (e.ClickedItem.Name == "showPlanesNotCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.PLANE, true, current_plane);
            }
            showPlaneToolStripSplitButton.Image = e.ClickedItem.Image;
        }
        #endregion

        #region Airlines
        private void airline0ToolStripButton_Click(object sender, EventArgs e)
        {
            airline_id = 0;
            current_airline = airline_ids[airline_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void airline1ToolStripButton_Click(object sender, EventArgs e)
        {
            airline_id--;
            current_airline = airline_ids[airline_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void airline2ToolStripButton_Click(object sender, EventArgs e)
        {
            airline_id++;
            current_airline = airline_ids[airline_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void airline3ToolStripButton_Click(object sender, EventArgs e)
        {
            airline_id = cnt_airlines - 1;
            current_airline = airline_ids[airline_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Edit current airline.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void airline4ToolStripButton_Click(object sender, EventArgs e)
        {
            AirlineForm airlineForm = new AirlineForm();
            airlineForm.Edit = true;
            airlineForm.EditId = current_airline;
            if (airlineForm.ShowDialog() == DialogResult.OK)
            {
                flightDbBackgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Delete button might be removed...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void airline5ToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void showAirlineToolStripSplitButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "showAirlineCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.AIRLINE, false, current_airline);
            }
            else if (e.ClickedItem.Name == "showAirlineNotCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.AIRLINE, true, current_airline);
            }
            showAirlineToolStripSplitButton.Image = e.ClickedItem.Image;
        }
        #endregion

        #region Costs
        private void cost0ToolStripButton_Click(object sender, EventArgs e)
        {
            cost_id = 0;
            current_cost = cost_ids[cost_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void cost1ToolStripButton_Click(object sender, EventArgs e)
        {
            cost_id--;
            current_cost = cost_ids[cost_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void cost2ToolStripButton_Click(object sender, EventArgs e)
        {
            cost_id++;
            current_cost = cost_ids[cost_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void cost3ToolStripButton_Click(object sender, EventArgs e)
        {
            cost_id = cnt_costs - 1;
            current_cost = cost_ids[cost_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Edit current cost.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cost4ToolStripButton_Click(object sender, EventArgs e)
        {
            CostsForm costsForm = new CostsForm();
            costsForm.Edit = true;
            costsForm.EditId = current_country;
            if (costsForm.ShowDialog() == DialogResult.OK)
            {
                flightDbBackgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Delete button might be removed...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cost5ToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void showCostToolStripSplitButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "showCostsCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.COST, false, current_cost);
            }
            else if (e.ClickedItem.Name == "showCostsNotCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.COST, true, current_cost);
            }
            showCostToolStripSplitButton.Image = e.ClickedItem.Image;
        }
        #endregion

        #region Countries
        private void country0ToolStripButton_Click(object sender, EventArgs e)
        {
            country_id = 0;
            current_country = country_ids[country_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void country1ToolStripButton_Click(object sender, EventArgs e)
        {
            country_id--;
            current_country = country_ids[country_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void country2ToolStripButton_Click(object sender, EventArgs e)
        {
            country_id++;
            current_country = country_ids[country_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void country3ToolStripButton_Click(object sender, EventArgs e)
        {
            country_id = cnt_countries - 1;
            current_country = country_ids[country_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Edit current country.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void country4ToolStripButton_Click(object sender, EventArgs e)
        {
            CountryForm countryForm = new CountryForm();
            countryForm.Edit = true;
            countryForm.EditId = current_country;
            if (countryForm.ShowDialog() == DialogResult.OK)
            {
                flightDbBackgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Delete button might be removed...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void country5ToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void showCountryToolStripSplitButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "showCountriesCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.COUNTRY, false, current_country);
            }
            else if (e.ClickedItem.Name == "showCountriesNotCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.COUNTRY, true, current_country);
            }
            showCountryToolStripSplitButton.Image = e.ClickedItem.Image;
        }
        #endregion

        #region Cities
        private void city0ToolStripButton_Click(object sender, EventArgs e)
        {
            city_id = 0;
            current_city = city_ids[city_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void city1ToolStripButton_Click(object sender, EventArgs e)
        {
            city_id--;
            current_city = city_ids[city_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void city2ToolStripButton_Click(object sender, EventArgs e)
        {
            city_id++;
            current_city = city_ids[city_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        private void city3ToolStripButton_Click(object sender, EventArgs e)
        {
            city_id = cnt_cities - 1;
            current_city = city_ids[city_id];
            flightDbBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Edit current city.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void city4ToolStripButton_Click(object sender, EventArgs e)
        {
            CityForm cityForm = new CityForm();
            cityForm.Edit = true;
            cityForm.CityId = current_city;
            if (cityForm.ShowDialog() == DialogResult.OK)
            {
                flightDbBackgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Delete button might be removed...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void city5ToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void showCityToolStripSplitButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "showCitiesCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.CITY, false, current_city);
            }
            else if (e.ClickedItem.Name == "showCitiesNotCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.CITY, true, current_city);
            }
            showCityToolStripSplitButton.Image = e.ClickedItem.Image;
        }
        #endregion

        #region BackgroundWorker
        /// <summary>
        /// Background Worker to count database entries depending on selected tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flightDbBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (tabPage != 0)
                {
                    int result = -1;
                    switch (tabPage)
                    {
                        case 1:
                            result = CountIds("Flights");
                            break;
                        case 2:
                            result = CountIds("Airports");
                            break;
                        case 3:
                            result = CountIds("Planes");
                            break;
                        case 4:
                            result = CountIds("Airlines");
                            break;
                        case 5:
                            result = CountIds("Costs");
                            break;
                        case 6:
                            result = CountIds("Countries", false);
                            break;
                        case 7:
                            result = CountIds("Cities", false);
                            break;
                    }
                    e.Result = result;

                    if (result != 0)
                    {
                        switch (tabPage)
                        {
                            case 1:
                                if (current_flight == -1)
                                {
                                    flight_ids = GetObjectIds("Flights", "Date");
                                    if (flight_ids.Length > 0)
                                        flight_id = 0;
                                    else
                                        flight_id = -1;
                                }
                                break;
                            case 2:
                                if (current_airport == -1)
                                {
                                    airport_ids = GetObjectIds("Airports", "Name");
                                    if (airport_ids.Length > 0)
                                        airport_id = 0;
                                    else
                                        airport_id = -1;
                                }
                                break;
                            case 3:
                                if (current_plane == -1)
                                {
                                    plane_ids = GetObjectIds("Planes", "Name");
                                    if (plane_ids.Length > 0)
                                        plane_id = 0;
                                    else
                                        plane_id = -1;
                                }
                                break;
                            case 4:
                                if (current_airline == -1)
                                {
                                    airline_ids = GetObjectIds("Airlines", "Name");
                                    if (airline_ids.Length > 0)
                                        airline_id = 0;
                                    else
                                        airline_id = -1;
                                }
                                break;
                            case 5:
                                if (current_cost == -1)
                                {
                                    cost_ids = GetObjectIds("Costs", "Date");
                                    if (cost_ids.Length > 0)
                                        cost_id = 0;
                                    else
                                        cost_id = -1;
                                }
                                break;
                            case 6:
                                if (current_country == -1)
                                {
                                    country_ids = GetObjectIds("Countries", "Country");
                                    if (country_ids.Length > 0)
                                        country_id = 0;
                                    else
                                        country_id = -1;
                                }
                                break;
                            case 7:
                                if (current_city == -1)
                                {
                                    city_ids = GetObjectIds("Cities", "CityName");
                                    if (city_ids.Length > 0)
                                        city_id = 0;
                                    else
                                        city_id = -1;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    e.Result = -1;
                }
            }
            catch (System.Exception ex)
            {
                ShowErrorMessage(ex.Message, "BG-Worker DoWork...");
            }
        }

        /// <summary>
        /// After the background worker did its job, the GUI is refreshed according to the associated tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void flightDbBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int result = (int)e.Result;
            if (tabPage != 0)
            {
                SqlConnection myConnection, con1;
                SqlParameter idParam;

                //Add id portion
                idParam = new SqlParameter();
                idParam.ParameterName = "@id";
                
                string whereclause = " WHERE ";
                string sqlquery = "SELECT * FROM ";
                string tablename = "";

                try
                {
                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        myConnection.Open();
                        using (SqlCommand myCommand = new SqlCommand())
                        {
                            switch (tabPage)
                            {
                                case 1:
                                    tablename = "Flights";
                                    if (flight_id >= 0 && flight_ids != null)
                                        idParam.Value = flight_ids[flight_id];
                                    else
                                        idParam.Value = -1;
                                    cnt_flights = result;
                                    flightAirlineLabel.Text = "";
                                    flightPlaneLabel.Text = "";
                                    flightLabel.Text = "";
                                    flightStartLabel.Text = "";
                                    flightEndLabel.Text = "";
                                    flightSeatLabel.Text = "";
                                    flightClassLabel.Text = "";
                                    flightRemarkRichTextBox.Text = "";
                                    flightToolStripButton0.Enabled = false;
                                    flightToolStripButton1.Enabled = false;
                                    flightToolStripButton2.Enabled = false;
                                    flightToolStripButton3.Enabled = false;
                                    flightToolStripButton4.Enabled = false;
                                    flightToolStripButton5.Enabled = false;
                                    if (result == 0)
                                    {
                                        flightTitleLabel.Text = "Keine Flüge vorhanden!";
                                        flightAirlineLabel.Text = "Bitte erst mindestens einen Flug erstellen.";
                                    }
                                    else if (result == 1)
                                    {
                                        flightToolStripButton4.Enabled = true;
                                        flightToolStripButton5.Enabled = true;
                                    }
                                    else if (result >= 2)
                                    {
                                        flightToolStripButton4.Enabled = true;
                                        flightToolStripButton5.Enabled = true;
                                        // First element
                                        if (flight_ids[flight_id] == flight_ids.First())
                                        {
                                            flightToolStripButton2.Enabled = true;
                                            flightToolStripButton3.Enabled = true;
                                        }
                                        // Last element
                                        else if (flight_ids[flight_id] == flight_ids.Last())
                                        {
                                            flightToolStripButton0.Enabled = true;
                                            flightToolStripButton1.Enabled = true;
                                        }
                                        else
                                        {
                                            flightToolStripButton0.Enabled = true;
                                            flightToolStripButton1.Enabled = true;
                                            flightToolStripButton2.Enabled = true;
                                            flightToolStripButton3.Enabled = true;
                                        }
                                    }
                                    break;
                                case 2:
                                    tablename = "Airports";
                                    if (airport_id >= 0 && airport_ids != null)
                                        idParam.Value = airport_ids[airport_id];
                                    else
                                        idParam.Value = -1;
                                    cnt_airports = result;
                                    airportCountryLabel.Text = "";
                                    airportCityLabel.Text = "";
                                    airportCodesLabel.Text = "";
                                    airportGpsLabel.Text = "";
                                    airportHeightLabel.Text = "";
                                    airportLinkLabel.Text = "";
                                    airportRemarkRichTextBox.Text = "";
                                    airport0ToolStripButton.Enabled = false;
                                    airport1ToolStripButton.Enabled = false;
                                    airport2ToolStripButton.Enabled = false;
                                    airport3ToolStripButton.Enabled = false;
                                    airport4ToolStripButton.Enabled = false;
                                    airport5ToolStripButton.Enabled = false;
                                    if (result == 0)
                                    {
                                        airportTitleLabel.Text = "Keine Flughäfen vorhanden!";
                                        airportCountryLabel.Text = "Bitte erst mindestens einen Flughafen erstellen.";
                                    }
                                    else if (result == 1)
                                    {
                                        airport5ToolStripButton.Enabled = true;
                                        airport5ToolStripButton.Enabled = true;
                                    }
                                    else if (result >= 2)
                                    {
                                        airport4ToolStripButton.Enabled = true;
                                        airport5ToolStripButton.Enabled = true;
                                        // First element
                                        if (airport_ids[flight_id] == airport_ids.First())
                                        {
                                            airport2ToolStripButton.Enabled = true;
                                            airport3ToolStripButton.Enabled = true;
                                        }
                                        // Last element
                                        else if (airport_ids[flight_id] == airport_ids.Last())
                                        {
                                            airport0ToolStripButton.Enabled = true;
                                            airport1ToolStripButton.Enabled = true;
                                        }
                                        else
                                        {
                                            airport0ToolStripButton.Enabled = true;
                                            airport1ToolStripButton.Enabled = true;
                                            airport2ToolStripButton.Enabled = true;
                                            airport3ToolStripButton.Enabled = true;
                                        }
                                    }
                                    break;
                                case 3:
                                    tablename = "Planes";
                                    if (plane_id >= 0 && plane_ids != null)
                                        idParam.Value = plane_ids[plane_id];
                                    else
                                        idParam.Value = -1;
                                    cnt_planes = result;
                                    planeManufacturerLabel.Text = "";
                                    planeNationalityLabel.Text = "";
                                    planeTypeLabel.Text = "";
                                    planeVariantLabel.Text = "";
                                    planeCategoryLabel.Text = "";
                                    planeRegistrationLabel.Text = "";
                                    planeSeatsLabel.Text = "";
                                    planeRemarkRichTextBox.Text = "";
                                    plane0ToolStripButton.Enabled = false;
                                    plane1ToolStripButton.Enabled = false;
                                    plane2ToolStripButton.Enabled = false;
                                    plane3ToolStripButton.Enabled = false;
                                    plane4ToolStripButton.Enabled = false;
                                    plane5ToolStripButton.Enabled = false;
                                    if (result == 0)
                                    {
                                        planeTitleLabel.Text = "Keine Flugzeuge vorhanden!";
                                        planeManufacturerLabel.Text = "Bitte erst mindestens ein Flugzeug erstellen.";
                                    }
                                    else if (result == 1)
                                    {
                                        plane5ToolStripButton.Enabled = true;
                                        plane5ToolStripButton.Enabled = true;
                                    }
                                    else if (result >= 2)
                                    {
                                        plane4ToolStripButton.Enabled = true;
                                        plane5ToolStripButton.Enabled = true;
                                        // First element
                                        if (plane_ids[plane_id] == plane_ids.First())
                                        {
                                            plane2ToolStripButton.Enabled = true;
                                            plane3ToolStripButton.Enabled = true;
                                        }
                                        // Last element
                                        else if (plane_ids[plane_id] == plane_ids.Last())
                                        {
                                            plane0ToolStripButton.Enabled = true;
                                            plane1ToolStripButton.Enabled = true;
                                        }
                                        else
                                        {
                                            plane0ToolStripButton.Enabled = true;
                                            plane1ToolStripButton.Enabled = true;
                                            plane2ToolStripButton.Enabled = true;
                                            plane3ToolStripButton.Enabled = true;
                                        }
                                    }
                                    break;
                                case 4:
                                    tablename = "Airlines";
                                    if (airline_id >= 0 && airline_ids != null)
                                        idParam.Value = airline_ids[airline_id];
                                    else
                                        idParam.Value = -1;
                                    cnt_planes = result;
                                    airlineCountryLabel.Text = "";
                                    airlineLinkLabel.Text = "";
                                    airlineTypeLabel.Text = "";
                                    airlineFleetLabel.Text = "";
                                    airlineEmployeesLabel.Text = "";
                                    airlinePaxLabel.Text = "";
                                    airlineCodesLabel.Text = "";
                                    airlineRemarkRichTextBox.Text = "";
                                    airline0ToolStripButton.Enabled = false;
                                    airline1ToolStripButton.Enabled = false;
                                    airline2ToolStripButton.Enabled = false;
                                    airline3ToolStripButton.Enabled = false;
                                    airline4ToolStripButton.Enabled = false;
                                    airline5ToolStripButton.Enabled = false;
                                    if (result == 0)
                                    {
                                        airlineTitleLabel.Text = "Keine Flugzeuge vorhanden!";
                                        airlineCountryLabel.Text = "Bitte erst mindestens ein Flugzeug erstellen.";
                                    }
                                    else if (result == 1)
                                    {
                                        airline5ToolStripButton.Enabled = true;
                                        airline5ToolStripButton.Enabled = true;
                                    }
                                    else if (result >= 2)
                                    {
                                        airline4ToolStripButton.Enabled = true;
                                        airline5ToolStripButton.Enabled = true;
                                        // First element
                                        if (airline_ids[airline_id] == airline_ids.First())
                                        {
                                            airline2ToolStripButton.Enabled = true;
                                            airline3ToolStripButton.Enabled = true;
                                        }
                                        // Last element
                                        else if (airline_ids[airline_id] == airline_ids.Last())
                                        {
                                            airline0ToolStripButton.Enabled = true;
                                            airline1ToolStripButton.Enabled = true;
                                        }
                                        else
                                        {
                                            airline0ToolStripButton.Enabled = true;
                                            airline1ToolStripButton.Enabled = true;
                                            airline2ToolStripButton.Enabled = true;
                                            airline3ToolStripButton.Enabled = true;
                                        }
                                    }
                                    break;
                                case 5:
                                    tablename = "Costs";
                                    if (cost_id >= 0 && cost_ids != null)
                                        idParam.Value = cost_ids[cost_id];
                                    else
                                        idParam.Value = -1;
                                    cnt_costs = result;
                                    costTitleLabel.Text = "";
                                    costCategoryLabel.Text = "";
                                    costVehicleLabel.Text = "";
                                    costPriceLabel.Text = "";
                                    costRemarkRichTextBox.Text = "";
                                    cost0ToolStripButton.Enabled = false;
                                    cost1ToolStripButton.Enabled = false;
                                    cost2ToolStripButton.Enabled = false;
                                    cost3ToolStripButton.Enabled = false;
                                    cost4ToolStripButton.Enabled = false;
                                    cost5ToolStripButton.Enabled = false;
                                    if (result == 0)
                                    {
                                        costTitleLabel.Text = "Keine Kosten vorhanden!";
                                        costCategoryLabel.Text = "Bitte erst mindestens eine Ausgabe erstellen.";
                                    }
                                    else if (result == 1)
                                    {
                                        cost5ToolStripButton.Enabled = true;
                                        cost5ToolStripButton.Enabled = true;
                                    }
                                    else if (result >= 2)
                                    {
                                        cost4ToolStripButton.Enabled = true;
                                        cost5ToolStripButton.Enabled = true;
                                        // First element
                                        if (cost_ids[cost_id] == cost_ids.First())
                                        {
                                            cost2ToolStripButton.Enabled = true;
                                            cost3ToolStripButton.Enabled = true;
                                        }
                                        // Last element
                                        else if (cost_ids[cost_id] == cost_ids.Last())
                                        {
                                            cost0ToolStripButton.Enabled = true;
                                            cost1ToolStripButton.Enabled = true;
                                        }
                                        else
                                        {
                                            cost0ToolStripButton.Enabled = true;
                                            cost1ToolStripButton.Enabled = true;
                                            cost2ToolStripButton.Enabled = true;
                                            cost3ToolStripButton.Enabled = true;
                                        }
                                    }
                                    break;
                                case 6:
                                    tablename = "Countries";
                                    if (country_id >= 0 && country_ids != null)
                                        idParam.Value = country_ids[country_id];
                                    else
                                        idParam.Value = -1;
                                    cnt_countries = result;
                                    countryTitleLabel.Text = "";
                                    countryContinentLabel.Text = "";
                                    countryIsoLabel.Text = "";
                                    countryPhoneLabel.Text = "";
                                    countryRemarkRichTextBox.Text = "";
                                    country0ToolStripButton.Enabled = false;
                                    country1ToolStripButton.Enabled = false;
                                    country2ToolStripButton.Enabled = false;
                                    country3ToolStripButton.Enabled = false;
                                    country4ToolStripButton.Enabled = false;
                                    country5ToolStripButton.Enabled = false;
                                    if (result == 0)
                                    {
                                        countryTitleLabel.Text = "Keine Länder vorhanden!";
                                        countryContinentLabel.Text = "Bitte erst mindestens ein Land erstellen.";
                                    }
                                    else if (result == 1)
                                    {
                                        country5ToolStripButton.Enabled = true;
                                        country5ToolStripButton.Enabled = true;
                                    }
                                    else if (result >= 2)
                                    {
                                        country4ToolStripButton.Enabled = true;
                                        country5ToolStripButton.Enabled = true;
                                        // First element
                                        if (country_ids[country_id] == country_ids.First())
                                        {
                                            country2ToolStripButton.Enabled = true;
                                            country3ToolStripButton.Enabled = true;
                                        }
                                        // Last element
                                        else if (country_ids[country_id] == country_ids.Last())
                                        {
                                            country0ToolStripButton.Enabled = true;
                                            country1ToolStripButton.Enabled = true;
                                        }
                                        else
                                        {
                                            country0ToolStripButton.Enabled = true;
                                            country1ToolStripButton.Enabled = true;
                                            country2ToolStripButton.Enabled = true;
                                            country3ToolStripButton.Enabled = true;
                                        }
                                    }
                                    break;
                                case 7:
                                    tablename = "Cities";
                                    if (city_id >= 0 && city_ids != null)
                                        idParam.Value = city_ids[city_id];
                                    else
                                        idParam.Value = -1;
                                    cnt_cities = result;
                                    countryLabel.Text = "";
                                    bundeslandLabel.Text = "";
                                    prefixLabel.Text = "";
                                    kfzLabel.Text = "";
                                    cityLinkLabel.Text = "";
                                    heightLabel.Text = "";
                                    cityRemarkRichTextBox.Text = "";
                                    gpsLabel.Text = "";
                                    airportLabel.Text = "";
                                    city0ToolStripButton.Enabled = false;
                                    city1ToolStripButton.Enabled = false;
                                    city2ToolStripButton.Enabled = false;
                                    city3ToolStripButton.Enabled = false;
                                    city4ToolStripButton.Enabled = false;
                                    city5ToolStripButton.Enabled = false;
                                    if (result == 0)
                                    {
                                        cityTitleLabel.Text = "Keine Städte vorhanden!";
                                        countryLabel.Text = "Bitte erst mindestens eine Stadt erstellen.";
                                    }
                                    else if (result == 1)
                                    {
                                        city4ToolStripButton.Enabled = true;
                                        city5ToolStripButton.Enabled = true;
                                    }
                                    else if (result >= 2)
                                    {
                                        city4ToolStripButton.Enabled = true;
                                        city5ToolStripButton.Enabled = true;
                                        // First element
                                        if (city_ids[city_id] == city_ids.First())
                                        {
                                            city2ToolStripButton.Enabled = true;
                                            city3ToolStripButton.Enabled = true;
                                        }
                                        // Last element
                                        else if (city_ids[city_id] == city_ids.Last())
                                        {
                                            city0ToolStripButton.Enabled = true;
                                            city1ToolStripButton.Enabled = true;
                                        }
                                        else
                                        {
                                            city0ToolStripButton.Enabled = true;
                                            city1ToolStripButton.Enabled = true;
                                            city2ToolStripButton.Enabled = true;
                                            city3ToolStripButton.Enabled = true;
                                        }
                                    }
                                    break;
                            }
                            sqlquery += tablename;
                            if (tabPage > 0 && tabPage < 8)
                            {
                                if (tabPage == 6 || tabPage == 7)
                                {
                                    whereclause += "Id = @id";
                                }
                                else
                                {
                                    whereclause += "Id = @id AND [User] = @user";
                                }
                                myCommand.Parameters.Add("@id", SqlDbType.Int).Value = idParam.Value;
                            }
                            if (tabPage == 1 || tabPage == 5)
                            {
                                whereclause += " ORDER BY Date";
                            }
                            myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                            sqlquery += whereclause;
                            myCommand.CommandText = sqlquery;
                            myCommand.CommandType = CommandType.Text;
                            myCommand.Connection = myConnection;
                            DateTime dt = DateTime.Now;

                            using (SqlDataReader reader = myCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader[0].ToString() == idParam.Value.ToString() && tabPage > 0 && tabPage < 8)
                                    {
                                        switch (tabPage)
                                        {
                                            case 1:
                                                string dateStr = "";
                                                string plane = "";
                                                string planeImage = "";
                                                string takeoff = "";
                                                string landing = "";
                                                string iata1 = "";
                                                string iata2 = "";

                                                // Plane
                                                if (reader[4].ToString() != String.Empty)
                                                {
                                                    using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                                    {
                                                        con1.Open();
                                                        using (SqlCommand com1 = new SqlCommand())
                                                        {
                                                            com1.CommandText = @"SELECT Name, Image FROM Planes WHERE Id = " + reader[4].ToString();
                                                            com1.CommandType = CommandType.Text;
                                                            com1.Connection = con1;
                                                            using (SqlDataReader reader1 = com1.ExecuteReader())
                                                            {
                                                                while (reader1.Read())
                                                                {
                                                                    plane = reader1[2].ToString();
                                                                    planeImage = reader1[7].ToString();
                                                                }
                                                                reader1.Close();
                                                            }
                                                        }
                                                        con1.Close();
                                                    }
                                                }
                                                // Date
                                                if (reader[2].ToString() != String.Empty)
                                                {
                                                    dateStr = "ca. " + reader[2].ToString();
                                                }
                                                else
                                                {
                                                    dt = Convert.ToDateTime(reader[1]);
                                                    dateStr = dt.ToString("dd. MMMM yyyy");
                                                }
                                                // Airports
                                                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                                {
                                                    con1.Open();
                                                    using (SqlCommand com1 = new SqlCommand())
                                                    {
                                                        com1.CommandText = @"SELECT Id, Name, IATA FROM Cities";
                                                        com1.CommandType = CommandType.Text;
                                                        com1.Connection = con1;
                                                        using (SqlDataReader reader1 = com1.ExecuteReader())
                                                        {
                                                            while (reader1.Read())
                                                            {
                                                                if (reader1[0].ToString() == reader[6].ToString())
                                                                {
                                                                    takeoff = reader1[1].ToString() + " (" + reader1[5].ToString() + ")";
                                                                    iata1 = reader1[5].ToString();
                                                                }
                                                                if (reader1[0].ToString() == reader[7].ToString())
                                                                {
                                                                    landing = reader1[1].ToString() + " (" + reader1[5].ToString() + ")";
                                                                    iata2 = reader1[5].ToString();
                                                                }    
                                                            }
                                                            reader1.Close();
                                                        }
                                                    }
                                                    con1.Close();
                                                }
                                                if (iata1 != String.Empty && iata2 != String.Empty)
                                                {
                                                    flightTitleLabel.Text = "Flug vom " + dateStr + " (" + iata1 + " - " + iata2 + ")";
                                                }
                                                else
                                                {
                                                    flightTitleLabel.Text = "Flug vom " + dateStr;
                                                }
                                                flightAirlineLabel.Text = GetDatabaseEntry("Airlines", "Name", "Id = " + reader[3].ToString());
                                                flightPlaneLabel.Text = plane;
                                                flightLabel.Text = reader[5].ToString();
                                                flightStartLabel.Text = takeoff + ((iata1 != String.Empty) ? " (" + iata1 + ")" : "");
                                                flightEndLabel.Text = landing + ((iata2 != String.Empty) ? " (" + iata2 + ")" : ""); ;
                                                flightSeatLabel.Text = reader[9].ToString();
                                                flightClassLabel.Text = reader[10].ToString();
                                                flightRemarkRichTextBox.Text = reader[8].ToString();
                                                // Images
                                                if (reader[11].ToString() != String.Empty)
                                                    routePictureBox.Image = System.Drawing.Image.FromFile(reader[11].ToString());
                                                else
                                                    routePictureBox.Image = routePictureBox.ErrorImage;
                                                if (planeImage != String.Empty)
                                                    planePictureBox.Image = System.Drawing.Image.FromFile(planeImage);
                                                else
                                                    planePictureBox.Image = planePictureBox.ErrorImage;
                                                break;
                                            case 2:
                                                airportTitleLabel.Text = reader[1].ToString();
                                                airportCountryLabel.Text = GetDatabaseEntry("Countries", "Country", "Id = " + reader[3].ToString());
                                                airportCityLabel.Text = GetDatabaseEntry("Cities", "CityName", "Id = " + reader[2].ToString());
                                                airportCodesLabel.Text = reader[4].ToString() + " / " + reader[5].ToString();
                                                airportGpsLabel.Text = reader[6].ToString();
                                                airportHeightLabel.Text = reader[7].ToString() + " m ü. NHN";
                                                airportLinkLabel.Text = reader[8].ToString();
                                                airportRemarkRichTextBox.Text = reader[9].ToString();
                                                if (reader[10].ToString() != String.Empty)
                                                    airportPictureBox.Image = System.Drawing.Image.FromFile(reader[10].ToString());
                                                else
                                                    airportPictureBox.Image = airportPictureBox.ErrorImage;
                                                break;
                                            case 3:
                                                string manufacturer = "";
                                                string logoImg = "";
                                                int countryId = -1;
                                                // PlaneManufacturers
                                                if (reader[4].ToString() != String.Empty)
                                                {
                                                    using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                                    {
                                                        con1.Open();
                                                        using (SqlCommand com1 = new SqlCommand())
                                                        {
                                                            com1.CommandText = @"SELECT Name, Nationality, Logo FROM PlaneManufacturers WHERE Id = " + reader[1].ToString();
                                                            com1.CommandType = CommandType.Text;
                                                            com1.Connection = con1;
                                                            using (SqlDataReader reader1 = com1.ExecuteReader())
                                                            {
                                                                while (reader1.Read())
                                                                {
                                                                    manufacturer = reader1[0].ToString();
                                                                    countryId = reader1.GetInt32(1);
                                                                    logoImg = reader1[2].ToString();
                                                                }
                                                                reader1.Close();
                                                            }
                                                        }
                                                        con1.Close();
                                                    }
                                                }
                                                planeTitleLabel.Text = manufacturer;
                                                planeManufacturerLabel.Text = manufacturer;
                                                planeNationalityLabel.Text = GetDatabaseEntry("Countries", "Country", "Id = " + countryId.ToString());
                                                planeTypeLabel.Text = reader[3].ToString();
                                                planeRegistrationLabel.Text = reader[4].ToString();
                                                planeCategoryLabel.Text = GetDatabaseEntry("PlaneCategories", "Category", "Id = " + reader[5].ToString());
                                                planeVariantLabel.Text = "";    //TODO
                                                planeSeatsLabel.Text = reader[6].ToString();
                                                planeRemarkRichTextBox.Text = reader[8].ToString();
                                                // Images
                                                if (reader[7].ToString() != String.Empty)
                                                    planesPictureBox.Image = System.Drawing.Image.FromFile(reader[7].ToString());
                                                else
                                                    planesPictureBox.Image = planesPictureBox.ErrorImage;
                                                ShowImageInPicureBox(planeLogoPictureBox, logoImg);
                                                break;
                                            case 4:
                                                airportTitleLabel.Text = reader[1].ToString();
                                                airlineCountryLabel.Text = GetDatabaseEntry("Countries", "Country", "Id = " + reader[2].ToString());
                                                airlineTypeLabel.Text = reader[3].ToString();
                                                airlineFleetLabel.Text = reader[4].ToString();
                                                airlineEmployeesLabel.Text = reader[5].ToString();
                                                airlinePaxLabel.Text = reader[6].ToString();
                                                airlineCodesLabel.Text = reader[7].ToString() + " / " + reader[8].ToString();
                                                airlineLinkLabel.Text = reader[9].ToString();
                                                if (reader[10].ToString() != String.Empty)
                                                    logoPictureBox.Image = System.Drawing.Image.FromFile(reader[10].ToString());
                                                else
                                                    logoPictureBox.Image = logoPictureBox.ErrorImage;
                                                airlineRemarkRichTextBox.Text = reader[11].ToString();
                                                break;
                                            case 5:
                                                dt = Convert.ToDateTime(reader[2]);
                                                costTitleLabel.Text = dt.ToString("dd. MMMM yyyy") + ": " + reader[1].ToString();
                                                costCategoryLabel.Text = GetDatabaseEntry("CostCategories", "CategoryName", "Id = " + reader[3].ToString());
                                                costVehicleLabel.Text = GetDatabaseEntry("Vehicles", "VehicleName", "Id = " + reader[6].ToString());
                                                costPriceLabel.Text = Convert.ToDecimal(reader[5]).ToString("C2");
                                                costRemarkRichTextBox.Text = reader[4].ToString();
                                                break;
                                            case 6:
                                                countryTitleLabel.Text = reader[1].ToString();
                                                countryContinentLabel.Text = GetDatabaseEntry("Continents", "Continent", "Id = " + reader[4].ToString());
                                                countryIsoLabel.Text = reader[2].ToString();
                                                countryPhoneLabel.Text = reader[3].ToString();
                                                countryRemarkRichTextBox.Text = reader[5].ToString();
                                                if (reader[6].ToString() != String.Empty)
                                                    countryPictureBox.Image = System.Drawing.Image.FromFile(reader[6].ToString());
                                                else
                                                    countryPictureBox.Image = countryPictureBox.ErrorImage;
                                                break;
                                            case 7:
                                                string bland = "";
                                                string land = "";
                                                cityTitleLabel.Text = reader[1].ToString();
                                                if (reader[3].ToString() != String.Empty)
                                                {
                                                    bland = GetDatabaseEntry("Bundeslaender", "Bundesland", (int)reader[3]);
                                                }
                                                if (reader[2].ToString() != String.Empty)
                                                {
                                                    land = GetDatabaseEntry("Countries", "Country", (int)reader[2]);
                                                }
                                                countryLabel.Text = land;
                                                bundeslandLabel.Text = bland;
                                                codeLabel.Text = reader[4].ToString();
                                                prefixLabel.Text = reader[5].ToString();
                                                cityLinkLabel.Text = reader[6].ToString();
                                                kfzLabel.Text = reader[7].ToString();
                                                if (reader[8].ToString() != String.Empty)
                                                {
                                                    heightLabel.Text = reader[8].ToString() + " m ü. NHN";
                                                }
                                                else { heightLabel.Text = ""; }
                                                cityRemarkRichTextBox.Text = reader[9].ToString();
                                                if (reader[10].ToString() != String.Empty)
                                                {
                                                    cityPictureBox.Image = System.Drawing.Image.FromFile(reader[10].ToString());
                                                }
                                                else cityPictureBox.Image = cityPictureBox.ErrorImage;
                                                gpsLabel.Text = reader[11].ToString();
                                                airportLabel.Text = "";
                                                break;
                                        }
                                    }                                    
                                }
                                reader.Close();
                            }
                        }
                        myConnection.Close();
                    }
                }
                catch (System.Exception ex)
                {
                    ShowErrorMessage(ex.Message, "BG-Worker Completed...");
                }
            }
        }
        #endregion

        private void distanzberechnungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GpsDistanceForm gpsForm = new GpsDistanceForm();
            gpsForm.ShowDialog();
        }
    }
}
