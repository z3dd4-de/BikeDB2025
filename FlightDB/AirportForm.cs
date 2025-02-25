using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024.FlightDB
{
    public partial class AirportForm : Form
    {
        public int EditId { get; set; }
        public bool Edit { get; set; }
        DefaultComboBoxes country, city;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AirportForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor for editing existing object.
        /// </summary>
        /// <param name="id"></param>
        public AirportForm(int id)
        {
            EditId = id;
            Edit = true;
            InitializeComponent();
        }

        /// <summary>
        /// Initialize the form during loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AirportForm_Load(object sender, EventArgs e)
        {
            country = new DefaultComboBoxes(countryComboBox, DefaultComboBoxes.CB_Types.COUNTRIES, false);
            city = new DefaultComboBoxes(cityComboBox, DefaultComboBoxes.CB_Types.CITIES, false);

            errorToolStripStatusLabel.Text = "";
            if (Edit)
            {
                addButton.Text = "Bearbeiten";
                this.Text = "Flughafen bearbeiten";
                load();
            }
            else
            {
                addButton.Enabled = false;
                addButton.Text = "Hinzufügen";
                this.Text = "Flughafen hinzufügen";
            }
        }

        /// <summary>
        /// Load existing airport for editing.
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
                            com1.CommandText = @"SELECT * FROM Airports WHERE Id = " + EditId.ToString();
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;
                            using (SqlDataReader reader1 = com1.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    nameTextBox.Text = reader1[1].ToString();
                                    cityComboBox.SelectedValue = reader1.GetInt32(2);
                                    countryComboBox.SelectedValue = reader1.GetInt32(3);
                                    icaoTextBox.Text = reader1.GetString(4);
                                    iataTextBox.Text = reader1.GetString(5);
                                    gpsTextBox.Text = reader1.GetString(6);
                                    heightTextBox.Text = reader1.GetString(7);
                                    linkTextBox.Text = reader1.GetString(8);
                                    imageTextBox.Text = reader1.GetString(9);
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
                ShowErrorMessage(ex.Message, "Fehler beim Laden des Flughafens");
            }
        }

        /// <summary>
        /// Assign image path to text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageButton_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() == DialogResult.OK)
            {
                imageTextBox.Text = openImageDialog.FileName;
            }
            else imageTextBox.Text = "";
        }

        /// <summary>
        /// Insert or update an airport.
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
                            int id = NextId("Airports");
                            string sqlquery = "INSERT INTO Airports " +
                            "(Id, Name, City, Country, ICAO, IATA, Gps, Height, Link, Image, Created, LastChanged, [User]) " +
                            "VALUES (@id, @name, @city, @country, @icao, @iata, @gps, height, link, image, @created, @lastchanged, @user)";
                            myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            myCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = nameTextBox.Text;
                            myCommand.Parameters.Add("@city", SqlDbType.Int).Value = cityComboBox.SelectedValue;
                            myCommand.Parameters.Add("@country", SqlDbType.Int).Value = countryComboBox.SelectedValue;
                            myCommand.Parameters.Add("@icao", SqlDbType.NChar).Value = icaoTextBox.Text;
                            myCommand.Parameters.Add("@iata", SqlDbType.NChar).Value = iataTextBox.Text;
                            myCommand.Parameters.Add("@gps", SqlDbType.NVarChar).Value = gpsTextBox.Text;
                            myCommand.Parameters.Add("@height", SqlDbType.Int).Value = Convert.ToInt32(heightTextBox.Text);
                            myCommand.Parameters.Add("@link", SqlDbType.NVarChar).Value = linkTextBox.Text;
                            myCommand.Parameters.Add("@image", SqlDbType.NVarChar).Value = imageTextBox.Text;
                            myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                            myCommand.Parameters.Add("@created", SqlDbType.DateTime).Value = DateTime.Now;
                            myCommand.Parameters.Add("@lastchanged", SqlDbType.DateTime).Value = DateTime.Now;

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
                    ShowErrorMessage(ex.Message, "Fehler beim Speichern des Flughafens");
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                var sql = @"UPDATE Airports SET Name = @name, City = @city, Country = @country, ICAO = @icao, " +
                        "IATA = @iata, Gps = @gps, Link = @link, Image = @image, [User] = @user, LastChanged = @lastchanged " +
                        "WHERE Id = " + EditId.ToString();
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        using (var myCommand = new SqlCommand(sql, connection))
                        {
                            myCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = nameTextBox.Text;
                            myCommand.Parameters.Add("@city", SqlDbType.Int).Value = cityComboBox.SelectedValue;
                            myCommand.Parameters.Add("@country", SqlDbType.Int).Value = countryComboBox.SelectedValue;
                            myCommand.Parameters.Add("@icao", SqlDbType.NChar).Value = icaoTextBox.Text;
                            myCommand.Parameters.Add("@iata", SqlDbType.NChar).Value = iataTextBox.Text;
                            myCommand.Parameters.Add("@gps", SqlDbType.NVarChar).Value = gpsTextBox.Text;
                            myCommand.Parameters.Add("@height", SqlDbType.Int).Value = Convert.ToInt32(heightTextBox.Text);
                            myCommand.Parameters.Add("@link", SqlDbType.NVarChar).Value = linkTextBox.Text;
                            myCommand.Parameters.Add("@image", SqlDbType.NVarChar).Value = imageTextBox.Text;
                            myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                            myCommand.Parameters.Add("@lastchanged", SqlDbType.DateTime).Value = DateTime.Now;

                            connection.Open();
                            myCommand.ExecuteNonQuery();
                            connection.Close();
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, "Fehler beim Update des Flughafens");
                    this.DialogResult = DialogResult.Abort;
                }
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
