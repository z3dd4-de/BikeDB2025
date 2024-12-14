using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace BikeDB2024
{
    public partial class CountryForm : Form
    {
        public bool Edit { get; set; }
        public int EditId { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public CountryForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Edit existing country.
        /// </summary>
        private void load()
        {
            SqlConnection con1;

            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
            {
                con1.Open();
                using (SqlCommand com1 = new SqlCommand())
                {
                    com1.CommandText = @"SELECT * FROM Countries WHERE Id = " + EditId.ToString();
                    com1.CommandType = CommandType.Text;
                    com1.Connection = con1;
                    using (SqlDataReader reader1 = com1.ExecuteReader())
                    {
                        while (reader1.Read())
                        {
                            countriesComboBox.SelectedValue = Convert.ToInt32(reader1[0]);
                            countriesComboBox.Enabled = false;
                            nameTextBox.Text = reader1[1].ToString();
                            iso3166TextBox.Text = reader1[2].ToString();
                            prefixTextBox.Text = reader1[3].ToString();
                            continentComboBox.SelectedIndex = Convert.ToInt32(reader1[4]);
                            addButton.Enabled = true;
                            addButton.Text = "Bearbeiten";
                            errorToolStripStatusLabel.Text = "Bearbeiten: Countries - Datensatz " + EditId.ToString();
                        }
                        reader1.Close();
                    }
                }
                con1.Close();
            }
        }

        /// <summary>
        /// Prepares the form and fills comboboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountryForm_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Countries". Sie können sie bei Bedarf verschieben oder entfernen.
            this.countriesTableAdapter.Fill(this.dataSet.Countries);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Continents". Sie können sie bei Bedarf verschieben oder entfernen.
            this.continentsTableAdapter.Fill(this.dataSet.Continents);

            continentComboBox.SelectedIndex = continentComboBox.FindStringExact(Properties.Settings.Default.StdContinent);
            errorToolStripStatusLabel.Text = "";
            nameEmpty();

            if (Edit)
            {
                this.Text = "Land bearbeiten";
                load();
            }
        }

        /// <summary>
        /// Checks for empty nameTextBox.
        /// </summary>
        private void nameEmpty()
        {
            if (nameTextBox.Text == "") addButton.Enabled = false;
        }

        /// <summary>
        /// Validate nameTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (countriesComboBox.FindStringExact(nameTextBox.Text) >= 0)
            {
                errorToolStripStatusLabel.Text = "Fehler: Land ist schon vorhanden!";
                addButton.Enabled = false;
            }
            else 
            { 
                addButton.Enabled = true;
                errorToolStripStatusLabel.Text = "";
            }
        }

        /// <summary>
        /// Inserts new country into database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            if (!Edit)
            {
                int length = countriesComboBox.Items.Count;
                DataSetTableAdapters.CountriesTableAdapter adapter = new DataSetTableAdapters.CountriesTableAdapter();
                adapter.Insert(length, nameTextBox.Text, iso3166TextBox.Text, prefixTextBox.Text, Convert.ToInt32(continentComboBox.SelectedValue),
                    DateTime.Now, DateTime.Now, Properties.Settings.Default.CurrentUserID);
                Close();
            }
            else
            {
                var sql = @"UPDATE Countries SET Country = @country, Iso3166 = @iso, Phone = @phone, Continent = @continent, " +
                        "LastChanged = @last " +
                        "WHERE Id = " + EditId.ToString();
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        using (var command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.Add("@country", SqlDbType.NVarChar).Value = nameTextBox.Text;
                            command.Parameters.Add("@continent", SqlDbType.Int).Value = continentComboBox.SelectedValue;
                            command.Parameters.Add("@phone", SqlDbType.NVarChar).Value = prefixTextBox.Text;
                            command.Parameters.Add("@iso", SqlDbType.NVarChar).Value = iso3166TextBox.Text;
                            command.Parameters.Add("last", SqlDbType.DateTime).Value = DateTime.Now;

                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Helpers.ShowErrorMessage(ex.Message, "Fehler beim Update");
                    this.DialogResult = DialogResult.Abort;
                }
            }
        }
    }
}
