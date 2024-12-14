using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class CostsForm : Form
    {
        public bool Edit { get; set; }
        public int EditId { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public CostsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize form when loaded. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CostsForm_Load(object sender, EventArgs e)
        {
            if (Edit)
            {
                addButton.Text = "Bearbeiten";
                this.Text = "Kosten und Ausgaben bearbeiten";
                load();
            }
            else
            {
                addButton.Enabled = false;
                addButton.Text = "Hinzufügen";
                this.Text = "Kosten und Ausgaben hinzufügen";
            }
            loadVehicles();
            loadCategories();
        }

        /// <summary>
        /// Enable or disable Button.
        /// </summary>
        private void testButton()
        {
            if (titleTextBox.Text != "" && priceTextBox.Text != "")
            {
                addButton.Enabled = true;
            }
            else { addButton.Enabled = false; }
        }

        /// <summary>
        /// Categories depend on the vehicle type.
        /// </summary>
        private void loadCategories(VehicleType mode = VehicleType.BIKE)
        {
            categoryComboBox.DataSource = null;
            categoryComboBox.Items.Clear();

            SqlConnection con1;
            string sqlquery = "";
            switch (mode)
            {
                default:
                case VehicleType.BIKE:
                    sqlquery = @"SELECT * FROM CostCategories WHERE " +
                            "ElectricVehicles IS NULL AND Engines IS NULL ORDER BY CategoryName";
                    break;
                case VehicleType.FOSSIL:
                    sqlquery = @"SELECT * FROM CostCategories WHERE " +
                            " ElectricVehicles IS NULL AND (Engines IS NULL OR Engines = 1) ORDER BY CategoryName";
                    break;
                case VehicleType.ELECTRIC:
                    sqlquery = @"SELECT * FROM CostCategories WHERE [User] = " + Properties.Settings.Default.CurrentUserID.ToString() +
                            " AND (ElectricVehicles IS NULL OR ElectricVehicles = 1) AND Engines IS NULL ORDER BY CategoryName";
                    break;
            }

            List<CostCategory> data = new List<CostCategory>();
            categoryComboBox.DisplayMember = "Text";
            categoryComboBox.ValueMember = "Value";

            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        com1.CommandText = sqlquery; 
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                data.Add(new CostCategory(reader1.GetInt32(0)));
                            }
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
                categoryComboBox.DataSource = data;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Laden der Kostenkategorie");
            }
        }

        /// <summary>
        /// When the vehicle type is changed, the categories are reloaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vehicleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (vehicleComboBox.SelectedIndex != -1)
            {
                var veh = vehicleComboBox.SelectedItem as Vehicle;
                loadCategories(veh.Vehiclemode);
            }
        }

        /// <summary>
        /// Load vehicles Combobox.
        /// </summary>
        private void loadVehicles()
        {
            vehicleComboBox.DataSource = null;
            vehicleComboBox.Items.Clear();

            SqlConnection con1;
            List<Vehicle> data = new List<Vehicle>();
            vehicleComboBox.DisplayMember = "Text";
            vehicleComboBox.ValueMember = "Value";
            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        com1.CommandText = @"SELECT * FROM Vehicles WHERE [User] = " + Properties.Settings.Default.CurrentUserID.ToString();
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                data.Add(new Vehicle(reader1.GetInt32(0)));
                            }
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
                vehicleComboBox.DataSource = data;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Laden des Fahrzeugs");
            }
        }

        /// <summary>
        /// Load existing costs for editing.
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
                            com1.CommandText = @"SELECT * FROM Costs WHERE Id = " + EditId.ToString();
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;
                            using (SqlDataReader reader1 = com1.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    titleTextBox.Text = reader1.GetString(1);
                                    dateTimePicker.Value = reader1.GetDateTime(2);
                                    categoryComboBox.SelectedValue = reader1.GetInt32(3);
                                    descriptionRichTextBox.Text = reader1.GetString(4);
                                    priceTextBox.Text = FillDecimalForTextbox(reader1.GetDecimal(5), 3).ToString(); 
                                    vehicleComboBox.SelectedValue= reader1.GetInt32(6);
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
                ShowErrorMessage(ex.Message, "Fehler beim Laden der Kosten");
            }
        }

        /// <summary>
        /// Add new costs or edit existing id.
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
                            int id = NextId("Costs");
                            string sqlquery = "INSERT INTO Costs " +
                            "(Id, CostTitle, Date, CostCategory, Description, Price, Vehicle, [User], Created, LastChanged) " +
                            "VALUES (@id, @title, @date, @category, @descr, @price, @vehicle, @user, @created, @lastchanged)";
                            myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            myCommand.Parameters.Add("@title", SqlDbType.NVarChar).Value = titleTextBox.Text;
                            myCommand.Parameters.Add("@date", SqlDbType.Date).Value = dateTimePicker.Value;
                            myCommand.Parameters.Add("@category", SqlDbType.Int).Value = categoryComboBox.SelectedValue;
                            myCommand.Parameters.Add("@descr", SqlDbType.NVarChar).Value = descriptionRichTextBox.Text;
                            myCommand.Parameters.Add("@price", SqlDbType.Money).Value = priceTextBox.Text;
                            myCommand.Parameters.Add("@vehicle", SqlDbType.Int).Value = vehicleComboBox.SelectedValue;
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
                    ShowErrorMessage(ex.Message, "Fehler beim Speichern der Kosten");
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                var sql = @"UPDATE Costs SET CostTitle = @title, Date = @date, CostCategory = @category, Description = @descr, " +
                        "Price = @price, Vehicle = @vehicle, [User] = @user, LastChanged = @lastchanged " +
                        "WHERE Id = " + EditId.ToString();
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        using (var myCommand = new SqlCommand(sql, connection))
                        {
                            myCommand.Parameters.Add("@title", SqlDbType.NVarChar).Value = titleTextBox.Text;
                            myCommand.Parameters.Add("@date", SqlDbType.Date).Value = dateTimePicker.Value;
                            myCommand.Parameters.Add("@category", SqlDbType.Int).Value = categoryComboBox.SelectedValue;
                            myCommand.Parameters.Add("@descr", SqlDbType.NVarChar).Value = descriptionRichTextBox.Text;
                            myCommand.Parameters.Add("@price", SqlDbType.Money).Value = priceTextBox.Text;
                            myCommand.Parameters.Add("@vehicle", SqlDbType.Int).Value = vehicleComboBox.SelectedValue;
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
                    ShowErrorMessage(ex.Message, "Fehler beim Update der Kosten");
                    this.DialogResult = DialogResult.Abort;
                }
            }
            Close();
        }

        #region Check button enabled or disabled.
        private void titleTextBox_TextChanged(object sender, EventArgs e)
        {
            testButton();
        }

        private void priceTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            addButton.Enabled = false;
        }

        /// <summary>
        /// Check if the price could be converted to a decimal.
        /// </summary>
        /// <returns></returns>
        private bool testPrice()
        {
            bool test = false;
            decimal tmp = 0M;
            try
            {
                tmp = Convert.ToDecimal(priceTextBox.Text);
                test = true;
            }
            catch (Exception)
            {
            }
            return test;
        }

        /// <summary>
        /// Simple validation of the price textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void priceTextBox_Leave(object sender, EventArgs e)
        {
            if (testPrice())
                testButton();
            else
                addButton.Enabled = false;
        }
        #endregion
    }
}
