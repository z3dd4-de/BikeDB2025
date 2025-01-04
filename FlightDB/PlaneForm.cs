using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024.FlightDB
{
    public partial class PlaneForm : Form
    {
        public int EditId { get; set; }
        public bool Edit { get; set; }
        private DefaultComboBoxes manu, cats;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlaneForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor for editing existing object.
        /// </summary>
        /// <param name="id"></param>
        public PlaneForm(int id)
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
        private void PlaneForm_Load(object sender, EventArgs e)
        {
            manu = new DefaultComboBoxes(constructorComboBox, DefaultComboBoxes.CB_Types.PLANE_TYPES, false);
            cats = new DefaultComboBoxes(planeTypeComboBox, DefaultComboBoxes.CB_Types.PLANE_TYPES, false);

            modeComboBox.SelectedIndex = 2;
            errorToolStripStatusLabel.Text = "";
            if (Edit)
            {
                addButton.Text = "Bearbeiten";
                this.Text = "Flugzeug bearbeiten";
                load();
            }
            else
            {
                addButton.Enabled = false;
                addButton.Text = "Hinzufügen";
                this.Text = "Flugzeug hinzufügen";
            }
        }

        /// <summary>
        /// Load existing plane for editing.
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
                            com1.CommandText = @"SELECT * FROM Planes WHERE Id = " + EditId.ToString();
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;
                            using (SqlDataReader reader1 = com1.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    constructorComboBox.SelectedValue = reader1.GetInt32(1);
                                    subTypeTextBox.Text = reader1.GetString(2); 
                                    planeTypeComboBox.SelectedText = reader1.GetString(3);
                                    regTextBox.Text = reader1.GetString(4);
                                    modeComboBox.SelectedValue = reader1.GetInt32(5);
                                    seatsTextBox.Text = reader1.GetString(6);
                                    imageTextBox.Text = reader1.GetString(7);
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
                ShowErrorMessage(ex.Message, "Fehler beim Laden des Flugzeugs");
            }
        }

        /// <summary>
        /// Assign image path to text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageButton_Click(object sender, EventArgs e)
        {
            if (openImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageTextBox.Text = openImageFileDialog.FileName;
            }
        }

        /// <summary>
        /// Insert or update a plane.
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
                            int id = NextId("Planes");
                            string sqlquery = "INSERT INTO Planes " +
                            "(Id, Manufacturer, Name, Type, Registration, Category, Seats, Image, " +
                            "NotShown, Created, LastChanged, [User]) " +
                            "VALUES (@id, @manu, @name, @type, @reg, @cat, @seat, @image, @notshown, @created, @lastchanged, @user)";
                            myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            myCommand.Parameters.Add("@manu", SqlDbType.Int).Value = constructorComboBox.SelectedValue;
                            myCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = subTypeTextBox.Text;
                            myCommand.Parameters.Add("@type", SqlDbType.NVarChar).Value = planeTypeComboBox.SelectedText;
                            myCommand.Parameters.Add("@reg", SqlDbType.NChar).Value = regTextBox.Text;
                            myCommand.Parameters.Add("@cat", SqlDbType.Int).Value = modeComboBox.SelectedValue;
                            myCommand.Parameters.Add("@seat", SqlDbType.NChar).Value = seatsTextBox.Text;
                            myCommand.Parameters.Add("@image", SqlDbType.NVarChar).Value = imageTextBox.Text;
                            myCommand.Parameters.Add("@notshown", SqlDbType.TinyInt).Value = GetTinyIntFromBool(false);
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
                    ShowErrorMessage(ex.Message, "Fehler beim Speichern des Flugzeugs");
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                var sql = @"UPDATE Planes SET Manufacturer = @manu, Name = @name, Type = @type, Registration = @reg, Category = @cat, " +
                        "Seats = @seat, Image = @image, NotShown = @notshown, LastChanged = @lastchanged, [User] = @user " +
                        "WHERE Id = " + EditId.ToString();
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        using (var myCommand = new SqlCommand(sql, connection))
                        {
                            myCommand.Parameters.Add("@manu", SqlDbType.Int).Value = constructorComboBox.SelectedValue;
                            myCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = subTypeTextBox.Text;
                            myCommand.Parameters.Add("@type", SqlDbType.NVarChar).Value = planeTypeComboBox.SelectedText;
                            myCommand.Parameters.Add("@reg", SqlDbType.NChar).Value = regTextBox.Text;
                            myCommand.Parameters.Add("@cat", SqlDbType.Int).Value = modeComboBox.SelectedValue;
                            myCommand.Parameters.Add("@seat", SqlDbType.NChar).Value = seatsTextBox.Text;
                            myCommand.Parameters.Add("@image", SqlDbType.NVarChar).Value = imageTextBox.Text;
                            myCommand.Parameters.Add("@notshown", SqlDbType.TinyInt).Value = GetTinyIntFromBool(false);
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
                    ShowErrorMessage(ex.Message, "Fehler beim Update des Flugzeugs");
                    this.DialogResult = DialogResult.Abort;
                }
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
