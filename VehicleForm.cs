using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class VehicleForm : Form
    {
        #region Public Properties
        public bool Edit { get; set; }
        public int VehicleId { get; set; }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public VehicleForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Prepare the form after loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VehicleForm_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.VehicleTypes". Sie können sie bei Bedarf verschieben oder entfernen.
            this.vehicleTypesTableAdapter.Fill(this.dataSet.VehicleTypes);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Companies". Sie können sie bei Bedarf verschieben oder entfernen.
            this.companiesTableAdapter.Fill(this.dataSet.Companies);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Vehicles". Sie können sie bei Bedarf verschieben oder entfernen.
            this.vehiclesTableAdapter.Fill(this.dataSet.Vehicles);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Vehicles". Sie können sie bei Bedarf verschieben oder entfernen.
            this.vehiclesTableAdapter.Fill(this.dataSet.Vehicles);
            errorToolStripStatusLabel.Text = "";
            vehiclesComboBox.Text = "";
            nameEmpty();

            if (Edit)
            {
                this.Text = "Fahrzeug bearbeiten";
                loadVehicle();
            }
        }

        /// <summary>
        /// When in Edit mode, the selected vehicle is loaded.
        /// </summary>
        private void loadVehicle()
        {
            SqlConnection con1;

            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
            {
                con1.Open();
                using (SqlCommand com1 = new SqlCommand())
                {
                    com1.CommandText = @"SELECT * FROM Vehicles WHERE Id = " + VehicleId.ToString();
                    com1.CommandType = CommandType.Text;
                    com1.Connection = con1;
                    using (SqlDataReader reader1 = com1.ExecuteReader())
                    {
                        while (reader1.Read())
                        {
                            vehiclesComboBox.SelectedIndex = Convert.ToInt32(reader1[0]);
                            vehiclesComboBox.Enabled = false;
                            nameTextBox.Text = reader1[1].ToString();
                            constructorComboBox.SelectedIndex = Convert.ToInt32(reader1[2]);
                            typeComboBox.SelectedIndex = Convert.ToInt32(reader1[3]);
                            boughtDateTimePicker.Value = Convert.ToDateTime(reader1[4].ToString());
                            buildYearTextBox.Text = reader1[5].ToString();
                            priceTextBox.Text = reader1[6].ToString();
                            inventoryRichTextBox.Text = reader1[7].ToString();
                            fileTextBox.Text = reader1[8].ToString();
                            licenseTextBox.Text = reader1[13].ToString();
                            addButton.Enabled = true;
                            addButton.Text = "Bearbeiten";
                            errorToolStripStatusLabel.Text = "Bearbeiten: Vehicles - Datensatz " + VehicleId.ToString();
                        }
                        reader1.Close();
                    }
                }
                con1.Close();
            }
        }

        /// <summary>
        /// nameTextBox cannot be empty.
        /// </summary>
        private void nameEmpty()
        {
            if (nameTextBox.Text == "") addButton.Enabled = false;
        }

        /// <summary>
        /// Validate nameTextBox when its content changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (vehiclesComboBox.FindStringExact(nameTextBox.Text) >= 0)
            {
                errorToolStripStatusLabel.Text = "Fehler: Fahrzeug ist schon vorhanden!";
                addButton.Enabled = false;
            }
            else 
            { 
                addButton.Enabled = true;
                errorToolStripStatusLabel.Text = "";
            }
            nameEmpty();
        }

        /// <summary>
        /// Insert the vehicle into the database or update it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Edit)
                {
                    int length = vehiclesComboBox.Items.Count;
                    int year;
                    decimal price;

                    if (buildYearTextBox.Text == "") year = -1;
                    else year = Convert.ToInt32(buildYearTextBox.Text);
                    if (priceTextBox.Text == "") price = 0.0m;
                    else price = Convert.ToDecimal(priceTextBox.Text);

                    DataSetTableAdapters.VehiclesTableAdapter adapter = new DataSetTableAdapters.VehiclesTableAdapter();
                    adapter.Insert(length,
                        nameTextBox.Text,
                        constructorComboBox.SelectedIndex,
                        typeComboBox.SelectedIndex,
                        Convert.ToDateTime(boughtDateTimePicker.Text),
                        year,
                        price,
                        inventoryRichTextBox.Text,
                        fileTextBox.Text,
                        null,           // Entfaltung wird im EntfaltungsForm bestimmt
                        DateTime.Now,
                        DateTime.Now,
                        Properties.Settings.Default.CurrentUserID,
                        licenseTextBox.Text);          
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    var sql = @"UPDATE Vehicles SET VehicleName = @VehicleName, Manufacturer = @Manufacturer, VehicleType = @VehicleType, " +
                        "BoughtOn = @BoughtOn, BuildYear = @BuildYear, Price = @Price, Equipment = @Equipment, Image = @Image, " +
                        "LastChanged = @last, LicensePlate = @license " +
                        "WHERE Id = " + VehicleId.ToString();
                    try
                    {
                        using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                        {
                            using (var command = new SqlCommand(sql, connection))
                            {
                                command.Parameters.Add("@VehicleName", SqlDbType.NVarChar).Value = nameTextBox.Text;
                                command.Parameters.Add("@Manufacturer", SqlDbType.Int).Value = constructorComboBox.SelectedIndex;
                                command.Parameters.Add("@VehicleType", SqlDbType.Int).Value = typeComboBox.SelectedIndex;
                                command.Parameters.Add("@BoughtOn", SqlDbType.Date).Value = Convert.ToDateTime(boughtDateTimePicker.Text);
                                command.Parameters.Add("@BuildYear", SqlDbType.Int).Value = Convert.ToInt32(buildYearTextBox.Text);
                                command.Parameters.Add("@Price", SqlDbType.Money).Value = Convert.ToDecimal(priceTextBox.Text);
                                command.Parameters.Add("@Equipment", SqlDbType.NVarChar).Value = inventoryRichTextBox.Text;
                                command.Parameters.Add("@Image", SqlDbType.NVarChar).Value = fileTextBox.Text;
                                command.Parameters.Add("@last", SqlDbType.DateTime).Value = DateTime.Now;
                                command.Parameters.Add("@license", SqlDbType.NChar).Value = licenseTextBox.Text;
                                
                                connection.Open();
                                command.ExecuteNonQuery();
                                connection.Close();
                                this.DialogResult = DialogResult.OK;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage(ex.Message, "Fehler beim Update");
                        this.DialogResult = DialogResult.Abort;
                    }
                }
            }
            catch (Exception)
            {
                this.DialogResult = DialogResult.Abort;
            }            
            Close();
        }

        /// <summary>
        /// Load an image for the vehicle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Bilddatei für Fahrzeug auswählen";
            openFileDialog.FileName = "";
            openFileDialog.Filter = "PNG-Bilddatei|*.png|JPG-Bilddatei|*.jpg|GIF-Bilddatei|*.gif|Alle Dateien (*.*)|*.*";
            if (Properties.Settings.Default.ImageFolder != "")
            {
                openFileDialog.InitialDirectory = Properties.Settings.Default.ImageFolder;
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileTextBox.Text = openFileDialog.FileName;
            }
        }
    }
}
