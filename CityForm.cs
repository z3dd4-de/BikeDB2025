using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class CityForm : Form
    {
        #region Public properties
        public bool Edit { get; set; }
        public int CityId { get; set; }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public CityForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Prepare the form after loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CityForm_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Bundeslaender". Sie können sie bei Bedarf verschieben oder entfernen.
            this.bundeslaenderTableAdapter.Fill(this.dataSet.Bundeslaender);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Countries". Sie können sie bei Bedarf verschieben oder entfernen.
            this.countriesTableAdapter.Fill(this.dataSet.Countries);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Cities". Sie können sie bei Bedarf verschieben oder entfernen.
            this.citiesTableAdapter.Fill(this.dataSet.Cities);
            
            if (Properties.Settings.Default.StdBundesland != "")
            {
                bundeslandComboBox.SelectedIndex = bundeslandComboBox.FindStringExact(Properties.Settings.Default.StdBundesland);
            }

            errorToolStripStatusLabel.Text = "";
            nameEmpty();
            enableBundeslaender();

            if (Edit)
            {
                this.Text = "Stadt bearbeiten";
                loadCity();
            }
        }

        /// <summary>
        /// In edit mode, load an existing city into the form.
        /// </summary>
        private void loadCity()
        {
            SqlConnection con1;

            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
            {
                con1.Open();
                using (SqlCommand com1 = new SqlCommand())
                {
                    com1.CommandText = @"SELECT * FROM Cities WHERE Id = " + CityId.ToString();
                    com1.CommandType = CommandType.Text;
                    com1.Connection = con1;
                    using (SqlDataReader reader1 = com1.ExecuteReader())
                    {
                        while (reader1.Read())
                        {
                            citiesComboBox.SelectedValue = Convert.ToInt32(reader1[0]);
                            citiesComboBox.Enabled = false;
                            nameTextBox.Text = reader1[1].ToString();
                            countryComboBox.SelectedIndex = Convert.ToInt32(reader1[2]);
                            bundeslandComboBox.SelectedIndex = Convert.ToInt32(reader1[3]);
                            prefixTextBox.Text = reader1[4].ToString();
                            linkTextBox.Text = reader1[5].ToString();
                            kfzTextBox.Text = reader1[6].ToString();
                            heightTextBox.Text = reader1[7].ToString();
                            remarkRichTextBox.Text = reader1[8].ToString();
                            imageTextBox.Text = reader1[9].ToString();
                            gpsTextBox.Text = reader1[10].ToString();
                            addButton.Enabled = true;
                            addButton.Text = "Bearbeiten";
                            errorToolStripStatusLabel.Text = "Bearbeiten: Cities - Datensatz " + CityId.ToString();
                        }
                        reader1.Close();
                    }
                }
                con1.Close();
            }
        }

        /// <summary>
        /// Name cannot be empty.
        /// </summary>
        private void nameEmpty()
        {
            if (nameTextBox.Text == "") addButton.Enabled = false;
        }

        /// <summary>
        /// Insert city into the database or edit existing city.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Edit)
                {
                    int bl, con;
                    if (Convert.ToInt32(countryComboBox.SelectedValue) >= 0)
                    {
                        con = (int)countryComboBox.SelectedValue;
                    }
                    else con = -1;

                    if (bundeslandComboBox.Enabled && bundeslandComboBox.Text != "")
                    {
                        bl = (int)bundeslandComboBox.SelectedValue;
                    }
                    else bl = -1;
                    int height = 0;
                    if (heightTextBox.Text != "")
                    {
                        height = Convert.ToInt32(heightTextBox.Text);
                    }
                    int id = NextId("Cities");
                    DataSetTableAdapters.CitiesTableAdapter adapter = new DataSetTableAdapters.CitiesTableAdapter();
                    adapter.Insert(id, nameTextBox.Text,
                        con,
                        bl,
                        prefixTextBox.Text, linkTextBox.Text,
                        kfzTextBox.Text, height, remarkRichTextBox.Text, imageTextBox.Text, gpsTextBox.Text,
                        DateTime.Now, DateTime.Now, Properties.Settings.Default.CurrentUserID);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    var sql = @"UPDATE Cities SET CityName = @CityName, Country = @Country, Bundesland = @Bundesland, CityPrefix = @CityPrefix, Link = @Link, " +
                        "Kfz = @Kfz, Height = @Height, Remark = @Remark, Image = @Image, Gps = @Gps, LastChanged = @last " +
                        "WHERE Id = " + CityId.ToString();
                    try
                    {
                        using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                        {
                            using (var command = new SqlCommand(sql, connection))
                            {
                                command.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = nameTextBox.Text;
                                command.Parameters.Add("@Country", SqlDbType.Int).Value = (int)countryComboBox.SelectedValue;
                                command.Parameters.Add("@Bundesland", SqlDbType.Int).Value = (int)bundeslandComboBox.SelectedValue;
                                command.Parameters.Add("@CityPrefix", SqlDbType.NVarChar).Value = prefixTextBox.Text;
                                command.Parameters.Add("@Link", SqlDbType.NVarChar).Value = linkTextBox.Text;
                                command.Parameters.Add("@Kfz", SqlDbType.NVarChar).Value = kfzTextBox.Text;
                                command.Parameters.Add("@Height", SqlDbType.Int).Value = Convert.ToInt32(heightTextBox.Text);
                                command.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = remarkRichTextBox.Text;
                                command.Parameters.Add("@Image", SqlDbType.NVarChar).Value = imageTextBox.Text;
                                command.Parameters.Add("@Gps", SqlDbType.NVarChar).Value = gpsTextBox.Text;
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
                        ShowErrorMessage(ex.Message, "Fehler beim Update der Stadt");
                        this.DialogResult = DialogResult.Abort;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Anlegen der neuen Stadt");
                this.DialogResult = DialogResult.Abort;
            }            
            Close();
        }

        /// <summary>
        /// Enables or disables addButton.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (citiesComboBox.FindStringExact(nameTextBox.Text) >= 0)
            {
                errorToolStripStatusLabel.Text = "Fehler: Stadt ist schon vorhanden!";
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
        /// Calls enableBundeslaender().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void countryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            enableBundeslaender();
        }

        /// <summary>
        /// Bundesländer are only enabled for German cities.
        /// </summary>
        private void enableBundeslaender()
        {
            if (countryComboBox.Text == "Deutschland")
            {
                bundeslandComboBox.Enabled = true;
            }
            else bundeslandComboBox.Enabled = false;
        }

        /// <summary>
        /// Add an image to the city.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openImageButton_Click(object sender, EventArgs e)
        {
            openImageDialog.InitialDirectory = Properties.Settings.Default.ImageFolder;
            if (openImageDialog.ShowDialog() == DialogResult.OK)
            {
                imageTextBox.Text = openImageDialog.FileName;
            }
        }
    }
}
