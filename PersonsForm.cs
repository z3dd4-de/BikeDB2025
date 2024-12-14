using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class PersonsForm : Form
    {
        public bool Edit { get; set; }
        public int EditId { get; set; }

        private string password = "";

        /// <summary>
        /// Constructor.
        /// </summary>
        public PersonsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize the form after loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonsForm_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Cities". Sie können sie bei Bedarf verschieben oder entfernen.
            this.citiesTableAdapter.Fill(this.dataSet.Cities);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Countries". Sie können sie bei Bedarf verschieben oder entfernen.
            this.countriesTableAdapter.Fill(this.dataSet.Countries);

            errorToolStripStatusLabel.Text = string.Empty;
            userNameTextBox.Enabled = false;
            pwdButton.Enabled = false;
            if (!Properties.Settings.Default.AdminLoggedIn)
            {
                isAdminCheckBox.Enabled = false;
                isUserCheckBox.Enabled = false;
            }
            if (Edit)
            {
                addButton.Text = "Bearbeiten";
                this.Text = "Person bearbeiten";
                load();
            }
            else
            {
                addButton.Text = "Hinzufügen";
                this.Text = "Neue Person hinzufügen";
            }
        }

        /// <summary>
        /// Edit existing person.
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
                            com1.CommandText = @"SELECT * FROM Persons WHERE Id = " + EditId.ToString();
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;
                            using (SqlDataReader reader1 = com1.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    userNameTextBox.Text = reader1[1].ToString();
                                    if (userNameTextBox.Text.Length > 0)
                                    {
                                        pwdButton.Enabled = true;
                                        pwdButton.Text = "Ändern";
                                    }
                                    lastNameTextBox.Text = reader1[2].ToString();
                                    nameTextBox.Text = reader1[3].ToString();
                                    cityComboBox.SelectedValue = Convert.ToInt32(reader1[4]);
                                    birthdateTimePicker.Value = Convert.ToDateTime(reader1[5]);
                                    if (!reader1.IsDBNull(6))
                                    {
                                        deathDateTimePicker.Value = Convert.ToDateTime(reader1[6]);
                                        deathDateTimePicker.Enabled = true;
                                        deadCheckBox.Checked = true;
                                        deathDateTimePicker.Checked = true;
                                    }
                                    else
                                    {
                                        deathDateTimePicker.Enabled = false;
                                        deadCheckBox.Checked = false;
                                        deathDateTimePicker.Checked = false;
                                    }
                                    phoneTextBox.Text = reader1[7].ToString(); 
                                    emailTextBox.Text = reader1[8].ToString();
                                    plzTextBox.Text = reader1[9].ToString();
                                    street1TextBox.Text = reader1[10].ToString();
                                    street2TextBox.Text = reader1[11].ToString();
                                    countryComboBox.SelectedValue= Convert.ToInt32(reader1[12]);
                                    imageTextBox.Text = reader1[13].ToString();
                                    remarkRichTextBox.Text = reader1[14].ToString();
                                    if (reader1.GetSqlByte(15) == 1) notShowCheckBox.Checked = true;
                                    else notShowCheckBox.Checked = false;
                                    if (reader1.GetSqlByte(18) == 1) isUserCheckBox.Checked = true;
                                    else isUserCheckBox.Checked = false;
                                    if (reader1.GetSqlByte(19) == 1) isAdminCheckBox.Checked = true;
                                    else isAdminCheckBox.Checked = false;

                                    addButton.Enabled = true;
                                    addButton.Text = "Bearbeiten";
                                    errorToolStripStatusLabel.Text = "Bearbeiten: Persons - Datensatz " + EditId.ToString();
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
                ShowErrorMessage(ex.Message, "Fehler beim Laden der Person");
            }
        }

        /// <summary>
        /// Handle User checkbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void isUserCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (isUserCheckBox.Checked)
            {
                userNameTextBox.Enabled = true;
                pwdButton.Enabled = true;
            }
            else
            {
                userNameTextBox.Enabled = false;
                pwdButton.Enabled = false;
                isAdminCheckBox.Checked = false;
            }
        }

        /// <summary>
        /// Handle Admin checkbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void isAdminCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isUserCheckBox.Checked && isAdminCheckBox.Checked)
            {
                isUserCheckBox.Checked = true;
            }
        }

        /// <summary>
        /// Filename for person's image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileOpenButton_Click(object sender, EventArgs e)
        {
            if (imageOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageTextBox.Text = imageOpenFileDialog.FileName;
            }
        }

        /// <summary>
        /// Opens a new form to change the password.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pwdButton_Click(object sender, EventArgs e)
        {
            PasswordForm passwordForm = new PasswordForm();
            if (passwordForm.ShowDialog() == DialogResult.OK)
            {
                password = passwordForm.Password;
            }
        }

        /// <summary>
        /// Save or edit the current person.
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
                            int id = NextId("Persons");
                            byte user = 0;
                            if (isUserCheckBox.Checked) user = 1;
                            byte admin = 0;
                            if (isAdminCheckBox.Checked) admin = 1;
                            byte notshown = 0;
                            if (notShowCheckBox.Checked) notshown = 1;
                            string sqlquery = "INSERT INTO Persons " +
                            "(Id, Username, Lastname, Name, City, Birthdate, Deathdate, Phone, Email, PLZ, Street1, Street2, Country, Image, " +
                            "Remark, NotShown, Created, LastChanged, [User], IsUser, IsAdmin, Password) " +
                            "VALUES (@id, @username, @lastname, @name, @city, @birthdate, @deathdate, @phone, @email, @plz, @str1, @str2, @country, @image, " +
                            "@remark, @notshown, @created, @lastchanged, @user, @isuser, @isadmin, @pwd)";
                            myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            myCommand.Parameters.Add("@username", SqlDbType.NVarChar).Value = userNameTextBox.Text;
                            myCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = nameTextBox.Text;
                            myCommand.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = lastNameTextBox.Text;
                            myCommand.Parameters.Add("@city", SqlDbType.Int).Value = cityComboBox.SelectedValue;
                            myCommand.Parameters.Add("@birthdate", SqlDbType.Date).Value = birthdateTimePicker.Value;
                            myCommand.Parameters.Add("@deathdate", SqlDbType.Date).Value = deathDateTimePicker.Value;
                            myCommand.Parameters.Add("@phone", SqlDbType.NVarChar).Value = phoneTextBox.Text;
                            myCommand.Parameters.Add("@email", SqlDbType.NVarChar).Value = emailTextBox.Text;
                            myCommand.Parameters.Add("@plz", SqlDbType.NVarChar).Value = plzTextBox.Text;
                            myCommand.Parameters.Add("@str1", SqlDbType.NVarChar).Value = street1TextBox.Text;
                            myCommand.Parameters.Add("@str2", SqlDbType.NVarChar).Value = street2TextBox.Text;
                            myCommand.Parameters.Add("@country", SqlDbType.Int).Value = countryComboBox.SelectedValue;
                            myCommand.Parameters.Add("@image", SqlDbType.NVarChar).Value = imageTextBox.Text;
                            myCommand.Parameters.Add("@remark", SqlDbType.NVarChar).Value = remarkRichTextBox.Text;
                            myCommand.Parameters.Add("@notshown", SqlDbType.TinyInt).Value = notshown;
                            myCommand.Parameters.Add("@created", SqlDbType.DateTime).Value = DateTime.Now;
                            myCommand.Parameters.Add("@lastchanged", SqlDbType.DateTime).Value = DateTime.Now;
                            myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                            myCommand.Parameters.Add("@isuser", SqlDbType.TinyInt).Value = user;
                            myCommand.Parameters.Add("@isadmin", SqlDbType.TinyInt).Value = admin;
                            myCommand.Parameters.Add("@pwd", SqlDbType.NVarChar).Value = password;
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
                    ShowErrorMessage(ex.Message, "Fehler beim Speichern der Person");
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                var sql = @"UPDATE Persons SET Username = @username, Lastname = @last, Name = @name, City = @city, Birthdate = @birthdate, " +
                        "Deathdate = @deathdate, Phone = @phone, Email = @email, PLZ = @plz, Street1 = @str1, Street2 = @str2, Country = @country, Image = @Image, " +
                        "Remark = @remark, NotShown = @notshown, LastChanged = @lastchanged, [User] = @user, IsUser = @isuser, IsAdmin = @isadmin, Password = @pwd " +
                        "WHERE Id = " + EditId.ToString();
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        using (var myCommand = new SqlCommand(sql, connection))
                        {
                            byte user = 0;
                            if (isUserCheckBox.Checked) user = 1;
                            byte admin = 0;
                            if (isAdminCheckBox.Checked) admin = 1;
                            byte notshown = 0;
                            if (notShowCheckBox.Checked) notshown = 1;
                            myCommand.Parameters.Add("@username", SqlDbType.NVarChar).Value = userNameTextBox.Text;
                            myCommand.Parameters.Add("@last", SqlDbType.NVarChar).Value = lastNameTextBox.Text;
                            myCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = nameTextBox.Text;
                            myCommand.Parameters.Add("@city", SqlDbType.Int).Value = cityComboBox.SelectedValue;
                            myCommand.Parameters.Add("@birthdate", SqlDbType.Date).Value = birthdateTimePicker.Value;
                            myCommand.Parameters.Add("@deathdate", SqlDbType.Date).Value = deathDateTimePicker.Value;
                            myCommand.Parameters.Add("@phone", SqlDbType.NVarChar).Value = phoneTextBox.Text;
                            myCommand.Parameters.Add("@email", SqlDbType.NVarChar).Value = emailTextBox.Text;
                            myCommand.Parameters.Add("@plz", SqlDbType.NVarChar).Value = plzTextBox.Text;
                            myCommand.Parameters.Add("@str1", SqlDbType.NVarChar).Value = street1TextBox.Text;
                            myCommand.Parameters.Add("@str2", SqlDbType.NVarChar).Value = street2TextBox.Text;
                            myCommand.Parameters.Add("@country", SqlDbType.Int).Value = countryComboBox.SelectedValue;
                            myCommand.Parameters.Add("@image", SqlDbType.NVarChar).Value = imageTextBox.Text;
                            myCommand.Parameters.Add("@remark", SqlDbType.NVarChar).Value = remarkRichTextBox.Text;
                            myCommand.Parameters.Add("@notshown", SqlDbType.TinyInt).Value = notshown;
                            myCommand.Parameters.Add("@lastchanged", SqlDbType.DateTime).Value = DateTime.Now;
                            myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                            myCommand.Parameters.Add("@isuser", SqlDbType.TinyInt).Value = user;
                            myCommand.Parameters.Add("@isadmin", SqlDbType.TinyInt).Value = admin;
                            myCommand.Parameters.Add("@pwd", SqlDbType.NVarChar).Value = password;

                            connection.Open();
                            myCommand.ExecuteNonQuery();
                            connection.Close();
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, "Fehler beim Update der Person");
                    this.DialogResult = DialogResult.Abort;
                }
            }
            Close();
        }

        /// <summary>
        /// When the city is selected, the country can be set automatically.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(GetDatabaseEntry("Cities", "Country"));
            string cntry = GetDatabaseEntry("Countries", "Country", id);
            countryComboBox.Text = cntry;
        }

        /// <summary>
        /// If person was dead, enable death date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (deadCheckBox.Checked)
            {
                deathDateTimePicker.Enabled = true;
                deathDateTimePicker.Checked = true;
            }
            else
            {
                deathDateTimePicker.Enabled = false;
                deathDateTimePicker.Checked = false;
            }
        }
    }
}
