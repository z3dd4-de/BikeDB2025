using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class RouteForm : Form
    {
        #region Public properties
        public bool Edit { get; set; }
        public int RouteId { get; set; }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public RouteForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load routes for the current user into combobox. 
        /// </summary>
        private void initRoutes()
        {
            routesComboBox.DataSource = null;
            routesComboBox.Items.Clear();

            SqlConnection con1;
            List<Route> data = new List<Route>();
            routesComboBox.DisplayMember = "Text";
            routesComboBox.ValueMember = "Value";

            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        com1.CommandText = @"SELECT * FROM Routes WHERE [User] = " + Properties.Settings.Default.CurrentUserID.ToString();
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                data.Add(new Route(reader1.GetInt32(0)));
                            }
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
                routesComboBox.DataSource = data;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Laden der Strecke");
            }
        }

        /// <summary>
        /// In edit mode load an existing route into the form.
        /// </summary>
        private void loadRoute()
        {
            SqlConnection con1;

            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
            {
                con1.Open();
                using (SqlCommand com1 = new SqlCommand())
                {
                    com1.CommandText = @"SELECT * FROM Routes WHERE Id = " + RouteId.ToString();
                    com1.CommandType = CommandType.Text;
                    com1.Connection = con1;
                    using (SqlDataReader reader1 = com1.ExecuteReader())
                    {
                        while (reader1.Read())
                        {
                            routesComboBox.SelectedValue = Convert.ToInt32(reader1[0]);
                            routesComboBox.Enabled = false;
                            nameTextBox.Text = reader1[1].ToString();
                            cityComboBox.SelectedIndex = Convert.ToInt32(reader1[2]);
                            cityStartComboBox.SelectedValue = Convert.ToInt32(reader1[3]);
                            cityEndComboBox.SelectedValue = Convert.ToInt32(reader1[4]);
                            citiesTextBox.Text = reader1[5].ToString();
                            typeComboBox.SelectedValue = Convert.ToInt32(reader1[6]);
                            maxAltTextBox.Text = reader1[7].ToString();
                            altTextBox.Text = reader1[8].ToString();
                            remarkRichTextBox.Text = reader1[9].ToString();
                            altProfileTextBox.Text = reader1[10].ToString();
                            imageTextBox.Text = reader1[11].ToString();
                            if (reader1.GetSqlInt16(12) == 1) notShownCheckBox.Checked = true;
                            else notShownCheckBox.Checked = false;
                            addButton.Enabled = true;
                            addButton.Text = "Bearbeiten";
                            errorToolStripStatusLabel.Text = "Bearbeiten: Routes - Datensatz " + RouteId.ToString();
                        }
                        reader1.Close();
                    }
                }
                con1.Close();
            }
        }

        /// <summary>
        /// Prepare the form after loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RouteForm_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Routes". Sie können sie bei Bedarf verschieben oder entfernen.
            this.routesTableAdapter.Fill(this.dataSet.Routes);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.RouteTypes". Sie können sie bei Bedarf verschieben oder entfernen.
            this.routeTypesTableAdapter.Fill(this.dataSet.RouteTypes);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Cities". Sie können sie bei Bedarf verschieben oder entfernen.
            this.citiesTableAdapter.Fill(this.dataSet.Cities);

            errorToolStripStatusLabel.Text = "";
            nameEmpty();
            initRoutes();

            if (Edit)
            {
                this.Text = "Strecke bearbeiten";
                loadRoute();
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
        /// Validate nameTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (routesComboBox.FindStringExact(nameTextBox.Text) >= 0)
            {
                errorToolStripStatusLabel.Text = "Fehler: Strecke ist schon vorhanden!";
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
        /// Insert the route into the database or edit an existing route.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Edit)
                {
                    int maxAlt = 0;
                    int alt = 0;
                    if (maxAltTextBox.Text != String.Empty)
                    {
                        maxAlt = Convert.ToInt32(maxAltTextBox.Text);
                    }
                    if (altTextBox.Text != String.Empty)
                    {
                        alt = Convert.ToInt32(altTextBox.Text);
                    }
                    int length = routesComboBox.Items.Count;
                    byte notshown = 0;
                    if (notShownCheckBox.Checked)
                    {
                        notshown = 1;
                    }
                    DataSetTableAdapters.RoutesTableAdapter adapter = new DataSetTableAdapters.RoutesTableAdapter();
                    adapter.Insert(length, nameTextBox.Text, cityComboBox.SelectedIndex,
                        cityStartComboBox.SelectedIndex,
                        cityEndComboBox.SelectedIndex,
                        citiesTextBox.Text,
                        typeComboBox.SelectedIndex,
                        maxAlt,
                        alt,
                        remarkRichTextBox.Text,
                        altProfileTextBox.Text,
                        imageTextBox.Text,
                        notshown,
                        DateTime.Now,
                        DateTime.Now,
                        Properties.Settings.Default.CurrentUserID);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    byte notshown = 0;
                    if (notShownCheckBox.Checked)
                    {
                        notshown = 1;
                    }
                    var sql = @"UPDATE Routes SET RouteName = @RouteName, City = @City, CityStart = @CityStart, CityEnd = @CityEnd, Cities = @Cities, " +
                        "RouteType = @RouteType, MaxAlt = @MaxAlt, Altitude = @Altitude, Remarks = @Remarks, AltProfile=@AltProfile, Image = @Image, " +
                        "NotShown = @shown, LastChanged = @last " +
                        "WHERE Id = " + RouteId.ToString();
                    try
                    {
                        using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                        {
                            using (var command = new SqlCommand(sql, connection))
                            {
                                command.Parameters.Add("@RouteName", SqlDbType.NVarChar).Value = nameTextBox.Text;
                                command.Parameters.Add("@City", SqlDbType.Int).Value = cityComboBox.SelectedIndex;
                                command.Parameters.Add("@CityStart", SqlDbType.Int).Value = cityStartComboBox.SelectedIndex;
                                command.Parameters.Add("@CityEnd", SqlDbType.Int).Value = cityEndComboBox.SelectedIndex;
                                command.Parameters.Add("@Cities", SqlDbType.NVarChar).Value = nameTextBox.Text;
                                command.Parameters.Add("@RouteType", SqlDbType.Int).Value = typeComboBox.SelectedIndex;
                                command.Parameters.Add("@MaxAlt", SqlDbType.Int).Value = Convert.ToInt32(maxAltTextBox.Text);
                                command.Parameters.Add("@Altitude", SqlDbType.Int).Value = Convert.ToInt32(altTextBox.Text);
                                command.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = remarkRichTextBox.Text;
                                command.Parameters.Add("@AltProfile", SqlDbType.NVarChar).Value = altProfileTextBox.Text;
                                command.Parameters.Add("@Image", SqlDbType.NVarChar).Value = imageTextBox.Text;
                                command.Parameters.Add("@shown", SqlDbType.TinyInt).Value = notshown;
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
                        ShowErrorMessage(ex.Message, "Fehler beim Update der Strecke");
                        this.DialogResult = DialogResult.Abort;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Anlegen einer neuen Strecke");
                this.DialogResult = DialogResult.Abort;
            }           
            Close();
        }

        /// <summary>
        /// Load an altitude profile image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void altitudeButton_Click(object sender, EventArgs e)
        {
            altitudeOpenFileDialog.InitialDirectory = Properties.Settings.Default.ImageFolder;
            if (altitudeOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                altProfileTextBox.Text = altitudeOpenFileDialog.FileName;
            }
        }

        /// <summary>
        /// Load an image for the route.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageButton_Click(object sender, EventArgs e)
        {
            imageOpenFileDialog.InitialDirectory = Properties.Settings.Default.ImageFolder;
            if (imageOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageTextBox.Text = imageOpenFileDialog.FileName;
            }
        }
    }
}
