using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024.FlightDB
{
    public partial class PlaneManufacturersForm : Form
    {
        public bool Edit { get; set; }
        public int EditId { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlaneManufacturersForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor for editing existing object.
        /// </summary>
        /// <param name="id"></param>
        public PlaneManufacturersForm(int id)
        {
            EditId = id;
            Edit = true;
            InitializeComponent();
        }

        /// <summary>
        /// Assign image path to text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageButton_Click(object sender, EventArgs e)
        {
            if (imageFileDialog.ShowDialog() == DialogResult.OK)
            {
                logoTextBox.Text = imageFileDialog.FileName;
            }
            else
            {
                logoTextBox.Text = String.Empty;
            }
        }

        /// <summary>
        /// Insert or update a manufacturer.
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
                            int id = NextId("PlaneManufacturers");
                            string sqlquery = "INSERT INTO PlaneManufacturers " +
                            "(Id, Name, Nationality, Logo, Link, Types, Created, LastChanged, [User]) " +
                            "VALUES (@id, @name, @nation, @logo, @link, @types, @created, @lastchanged, @user)";
                            myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            myCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = nameTextBox.Text;
                            myCommand.Parameters.Add("@nation", SqlDbType.Int).Value = countryComboBox.SelectedValue;
                            myCommand.Parameters.Add("@logo", SqlDbType.NVarChar).Value = logoTextBox.Text;
                            myCommand.Parameters.Add("@link", SqlDbType.NVarChar).Value = linkTextBox.Text;
                            myCommand.Parameters.Add("@types", SqlDbType.NVarChar).Value = getTypes();
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
                    ShowErrorMessage(ex.Message, "Fehler beim Speichern der Flugzeughersteller");
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                var sql = @"UPDATE PlaneManufacturers SET Name = @name, Nationality = @nation, Logo = @logo, Link = @link, " +
                        "Types = @types, [User] = @user, LastChanged = @lastchanged " +
                        "WHERE Id = " + EditId.ToString();
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        using (var myCommand = new SqlCommand(sql, connection))
                        {
                            myCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = nameTextBox.Text;
                            myCommand.Parameters.Add("@nation", SqlDbType.Int).Value = countryComboBox.SelectedValue;
                            myCommand.Parameters.Add("@logo", SqlDbType.NVarChar).Value = logoTextBox.Text;
                            myCommand.Parameters.Add("@link", SqlDbType.NVarChar).Value = linkTextBox.Text;
                            myCommand.Parameters.Add("@types", SqlDbType.NVarChar).Value = getTypes();
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
                    ShowErrorMessage(ex.Message, "Fehler beim Update des Flugzeugherstellers");
                    this.DialogResult = DialogResult.Abort;
                }
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Initialize the form during loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaneManufacturersForm_Load(object sender, EventArgs e)
        {
            if (Edit)
            {
                addButton.Text = "Bearbeiten";
                this.Text = "Flugzeughersteller bearbeiten";
                load();
            }
            else
            {
                addButton.Enabled = false;
                addButton.Text = "Hinzufügen";
                this.Text = "Flugzeughersteller hinzufügen";
            }
            InitCountryComboBox(countryComboBox);
        }

        /// <summary>
        /// Load existing manufacturer for editing.
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
                            com1.CommandText = @"SELECT * FROM PlaneManufacturers WHERE Id = " + EditId.ToString();
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;
                            using (SqlDataReader reader1 = com1.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    nameTextBox.Text = reader1.GetString(1);
                                    countryComboBox.SelectedValue = reader1.GetInt32(2);
                                    logoTextBox.Text = reader1.GetString(3);
                                    linkTextBox.Text = reader1.GetString(4);
                                    loadComboBoxTypes(loadTypes(reader1.GetString(5)));

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
                ShowErrorMessage(ex.Message, "Fehler beim Laden des Flugzeugherstellers");
            }
        }

        /// <summary>
        /// Add a new type into the type combobox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addTypeButton_Click(object sender, EventArgs e)
        {
            if (typeTextBox.Text != String.Empty && !typesComboBox.Items.Contains(typeTextBox.Text))
            {
                typesComboBox.Items.Add(typeTextBox.Text);
            }
        }

        private string getTypes()
        {
            string ret = "";
            foreach (string type in typesComboBox.Items)
            {
                if (type.Trim().Length > 0)
                    ret += type + ";";
            }
            return ret;
        }

        private string[] loadTypes(string types)
        {
            string[] tmp = types.Split(';');
            string[] ret = new string[tmp.Length];
            for (int i = 0; i < tmp.Length; i++)
            {
                ret[i] = tmp[i].Trim();
            }
            return ret;
        }

        private void loadComboBoxTypes(string[] types)
        {
            typesComboBox.Items.Clear();
            foreach (string type in types)
            {
                typesComboBox.Items.Add(type);
            }
        }
    }
}
