using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024.FlightDB
{
    public partial class AirlineForm : Form
    {
        public int EditId { get; set; }
        public bool Edit { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public AirlineForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor for editing existing object.
        /// </summary>
        /// <param name="id"></param>
        public AirlineForm(int id)
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
        private void AirlineForm_Load(object sender, EventArgs e)
        {
            if (Edit)
            {
                addButton.Text = "Bearbeiten";
                this.Text = "Fluggesellschaft bearbeiten";
                load();
            }
            else
            {
                addButton.Enabled = false;
                addButton.Text = "Hinzufügen";
                this.Text = "Fluggesellschaft hinzufügen";
            }
        }

        /// <summary>
        /// Load existing airline for editing.
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
                            com1.CommandText = @"SELECT * FROM Airlines WHERE Id = " + EditId.ToString();
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;
                            using (SqlDataReader reader1 = com1.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    nameTextBox.Text = reader1.GetString(1);
                                    countryComboBox.SelectedValue = reader1.GetInt32(2);
                                    linkTextBox.Text = reader1.GetString(3);
                                    logoTextBox.Text = reader1.GetString(4);
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
                ShowErrorMessage(ex.Message, "Fehler beim Laden der Fluggesellschaft");
            }
        }

        /// <summary>
        /// Insert or update an airline.
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
                            int id = NextId("Airlines");
                            string sqlquery = "INSERT INTO Airlines " +
                            "(Id, Name, Country, Link, Logo, Created, LastChanged, [User]) " +
                            "VALUES (@id, @name, @country, @link, @logo, @created, @lastchanged, @user)";
                            myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            myCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = nameTextBox.Text;
                            myCommand.Parameters.Add("@country", SqlDbType.Int).Value = countryComboBox.SelectedValue;
                            myCommand.Parameters.Add("@link", SqlDbType.NVarChar).Value = linkTextBox.Text;
                            myCommand.Parameters.Add("@logo", SqlDbType.NVarChar).Value = logoTextBox.Text;
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
                    ShowErrorMessage(ex.Message, "Fehler beim Speichern der Fluggesellschaft");
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                var sql = @"UPDATE Airlines SET Name = @name, Country = @country, Link = @link, Logo = @logo, " +
                        "LastChanged = @lastchanged, [User] = @user " +
                        "WHERE Id = " + EditId.ToString();
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        using (var myCommand = new SqlCommand(sql, connection))
                        {
                            myCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = nameTextBox.Text;
                            myCommand.Parameters.Add("@country", SqlDbType.Int).Value = countryComboBox.SelectedValue;
                            myCommand.Parameters.Add("@link", SqlDbType.NVarChar).Value = linkTextBox.Text;
                            myCommand.Parameters.Add("@logo", SqlDbType.NVarChar).Value = logoTextBox.Text;
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
                    ShowErrorMessage(ex.Message, "Fehler beim Update der Fluggesellschaft");
                    this.DialogResult = DialogResult.Abort;
                }
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Assign image path to text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openLogoButton_Click(object sender, EventArgs e)
        {
            if (openLogoDialog.ShowDialog() == DialogResult.OK)
            {
                logoTextBox.Text = openLogoDialog.FileName;
            }
            else logoTextBox.Text = "";
        }
    }
}
