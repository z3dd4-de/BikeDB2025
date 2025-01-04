using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024.FlightDB
{
    public partial class FlightForm : Form
    {
        public int EditId { get; set; }
        public bool Edit { get; set; }
        DefaultComboBoxes start, end, plane;

        /// <summary>
        /// Constructor.
        /// </summary>
        public FlightForm()
        {
            InitializeComponent();
            errorToolStripStatusLabel.Text = "";
        }

        /// <summary>
        /// Constructor for editing existing object.
        /// </summary>
        /// <param name="id"></param>
        public FlightForm(int id)
        {
            EditId = id;
            Edit = true;
            InitializeComponent();
        }

        /// <summary>
        /// Initialize form during loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlightForm_Load(object sender, EventArgs e)
        {
            //TODO change to airports
            start = new DefaultComboBoxes(startComboBox, DefaultComboBoxes.CB_Types.CITIES, false);
            end = new DefaultComboBoxes(landingComboBox, DefaultComboBoxes.CB_Types.CITIES, false);
            plane = new DefaultComboBoxes(planeComboBox, DefaultComboBoxes.CB_Types.PLANES, false);

            classComboBox.SelectedIndex = 2;
            if (Edit)
            {
                addButton.Text = "Bearbeiten";
                this.Text = "Flug bearbeiten";
                load();
            }
            else
            {
                addButton.Enabled = false;
                addButton.Text = "Hinzufügen";
                this.Text = "Flug hinzufügen";
            }
        }

        /// <summary>
        /// Load existing flight into the form.
        /// </summary>
        private void load()
        {
            try
            {
                if (EditId >= 0)
                {
                    SqlConnection con1;

                    using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        con1.Open();
                        using (SqlCommand com1 = new SqlCommand())
                        {
                            com1.CommandText = @"SELECT * FROM Flights WHERE Id = " + EditId.ToString();
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;
                            using (SqlDataReader reader1 = com1.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    dateTimePicker.Value = reader1.GetDateTime(1);
                                    dateTextBox.Text = reader1.GetString(2);
                                    airlineComboBox.SelectedValue = reader1.GetInt32(3);
                                    planeComboBox.SelectedValue = reader1.GetInt32(4);
                                    flightTextBox.Text = reader1.GetString(5);
                                    startComboBox.SelectedValue = reader1.GetInt32(6);
                                    landingComboBox.SelectedValue = reader1.GetInt32(7);
                                    descRichTextBox.Text = reader1.GetString(8);
                                    seatTextBox.Text = reader1.GetString(9);
                                    classComboBox.SelectedValue = reader1.GetInt32(10);
                                }
                                reader1.Close();
                            }
                        }
                        con1.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Laden des Flugs");
            }
        }

        /// <summary>
        /// Insert or update a flight.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            SqlConnection myConnection;
            if (!Edit)
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
                            "(Id, Date, DateString, Airline, Plane, FlightNumber, Takeoff, Landing, Remark, Seat, " +
                            "Class, Created, LastChanged, [User]) " +
                            "VALUES (@id, @date, @datestr, @airline, @plane, @number, @start, @end, @remark, @seat, @class, @created, @lastchanged, @user)";
                            myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            myCommand.Parameters.Add("@date", SqlDbType.DateTime).Value = dateTimePicker.Value;
                            myCommand.Parameters.Add("@datestr", SqlDbType.NChar).Value = dateTextBox.Text;
                            myCommand.Parameters.Add("@airline", SqlDbType.Int).Value = airlineComboBox.SelectedValue;
                            myCommand.Parameters.Add("@plane", SqlDbType.Int).Value = planeComboBox.SelectedValue;
                            myCommand.Parameters.Add("@number", SqlDbType.NChar).Value = flightTextBox.Text;
                            myCommand.Parameters.Add("@start", SqlDbType.Int).Value = startComboBox.SelectedValue;
                            myCommand.Parameters.Add("@end", SqlDbType.Int).Value = landingComboBox.SelectedValue;
                            myCommand.Parameters.Add("@remark", SqlDbType.NVarChar).Value = descRichTextBox.Text;
                            myCommand.Parameters.Add("@seat", SqlDbType.NChar).Value = seatTextBox.Text;
                            myCommand.Parameters.Add("@class", SqlDbType.Int).Value = classComboBox.SelectedValue;
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
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, "Fehler beim Speichern des Flugs");
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                var sql = @"UPDATE Flights SET Date = @date, DateString = @datestr, Airline = @airline, Plane = @plane, " +
                        "FlightNumber = @number, Takeoff = @start, Landing = @end, Remark = @remark " +
                        "Seat = @seat, Class = @class, LastChanged = @lastchanged, [User] = @user " +
                        "WHERE Id = " + EditId.ToString();
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        using (var myCommand = new SqlCommand(sql, connection))
                        {
                            myCommand.Parameters.Add("@date", SqlDbType.DateTime).Value = dateTimePicker.Value;
                            myCommand.Parameters.Add("@datestr", SqlDbType.NChar).Value = dateTextBox.Text;
                            myCommand.Parameters.Add("@airline", SqlDbType.Int).Value = airlineComboBox.SelectedValue;
                            myCommand.Parameters.Add("@plane", SqlDbType.Int).Value = planeComboBox.SelectedValue;
                            myCommand.Parameters.Add("@number", SqlDbType.NChar).Value = flightTextBox.Text;
                            myCommand.Parameters.Add("@start", SqlDbType.Int).Value = startComboBox.SelectedValue;
                            myCommand.Parameters.Add("@end", SqlDbType.Int).Value = landingComboBox.SelectedValue;
                            myCommand.Parameters.Add("@remark", SqlDbType.NVarChar).Value = descRichTextBox.Text;
                            myCommand.Parameters.Add("@seat", SqlDbType.NChar).Value = seatTextBox.Text;
                            myCommand.Parameters.Add("@class", SqlDbType.Int).Value = classComboBox.SelectedValue;
                            myCommand.Parameters.Add("@lastchanged", SqlDbType.DateTime).Value = DateTime.Now;
                            myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;

                            connection.Open();
                            myCommand.ExecuteNonQuery();
                            connection.Close();
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, "Fehler beim Update des Flugs");
                    this.DialogResult = DialogResult.Abort;
                }
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
