using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class EntfaltungForm : Form
    {
        #region Private variables
        private string _mask = string.Empty;
        private int[] _kb;
        private int[] _rz;
        #endregion

        #region Properties
        public string Vehicle { get; set; }
        public int VehicleId { get; set; }
        public int VehicleEntfaltung { get; set; }
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EntfaltungForm()
        {
            InitializeComponent();
            Vehicle = "";
            VehicleEntfaltung = -1;
        }

        /// <summary>
        /// Constructor to preload a setting.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="entf_id"></param>
        public EntfaltungForm(string name, int vehicle_id, int entf_id)
        {
            InitializeComponent();
            Vehicle = name;
            VehicleId = vehicle_id;
            VehicleEntfaltung = entf_id;
        }

        /// <summary>
        /// Close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EntfaltungForm_Load(object sender, EventArgs e)
        {
            if (Vehicle == "")
            {
                kb1MaskedTextBox.Visible = false;
                kb2MaskedTextBox.Visible = false;
                kb3MaskedTextBox.Visible = false;
                ritzelMaskedTextBox.Visible = false;
                
                SqlConnection con1;

                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        com1.CommandText = @"SELECT VehicleName FROM Vehicles WHERE VehicleType IN (1,3,4) AND [User] = " + Properties.Settings.Default.CurrentUserID.ToString();
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                vehicleToolStripComboBox.Items.Add(reader1[0].ToString());
                            }
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
            }
            else            // Load existing "entfaltung"
            {
                loadEntfaltung();
            }

            if (Properties.Settings.Default.EntfaltungLocation != new Point(0, 0))
            {
                this.Location = Properties.Settings.Default.EntfaltungLocation;
            }
            if (Properties.Settings.Default.EntfaltungSize != new Size(500, 400))
            {
                this.Size = Properties.Settings.Default.EntfaltungSize;
            }

            string curDir = Directory.GetCurrentDirectory();
            this.entfaltungWebBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Entfaltung_wp.html", curDir));
        }

        /// <summary>
        /// When the form closes, location is saved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntfaltungForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.EntfaltungLocation = this.Location;
            Properties.Settings.Default.EntfaltungSize = this.Size;
            this.DialogResult = DialogResult.OK;
        }

        #region checkButtonEnabled()
        /// <summary>
        /// Change visiblity of textboxes depending on how many front gears are available.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blattToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Convert.ToInt16(blattToolStripComboBox.Text))
            {
                case 0:
                default:
                    kb1MaskedTextBox.Visible = false;
                    kb2MaskedTextBox.Visible = false;
                    kb3MaskedTextBox.Visible = false;
                    ritzelMaskedTextBox.Visible = false;
                    break;
                case 1:
                    kb1MaskedTextBox.Visible = true;
                    kb2MaskedTextBox.Visible = false;
                    kb3MaskedTextBox.Visible = false;
                    ritzelMaskedTextBox.Visible = true;
                    break;
                case 2:
                    kb1MaskedTextBox.Visible = true;
                    kb2MaskedTextBox.Visible = true;
                    kb3MaskedTextBox.Visible = false;
                    ritzelMaskedTextBox.Visible = true;
                    break;
                case 3:
                    kb1MaskedTextBox.Visible = true;
                    kb2MaskedTextBox.Visible = true;
                    kb3MaskedTextBox.Visible = true;
                    ritzelMaskedTextBox.Visible = true;
                    break;
            }
            checkButtonEnabled();
        }

        /// <summary>
        /// Rear gears are changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ritzelToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tmp = "99";
            ritzelMaskedTextBox.Mask = "";
            if (ritzelToolStripComboBox.Text == "1")
            {
                ritzelMaskedTextBox.Mask = tmp;
            }
            else
            {
                for (int i = 0; i < Convert.ToInt16(ritzelToolStripComboBox.Text); i++)
                {
                    ritzelMaskedTextBox.Mask += tmp + ";";
                }
            }
            _mask = ritzelMaskedTextBox.Text;

            checkButtonEnabled();
        }

        /// <summary>
        /// Vehicle changed in ComboBox. If entfaltung is available, it is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vehicleToolStripComboBox_TextChanged(object sender, EventArgs e)
        {
            string test = GetDatabaseEntry("Vehicles", "Entfaltung", "VehicleName = '" + vehicleToolStripComboBox.Text + "'");
            if (test != "-1" && test != "") 
            {
                VehicleEntfaltung = Convert.ToInt32(test);
                loadEntfaltung(); 
            }
            else checkButtonEnabled();
        }

        private void umfangToolStripComboBox_TextChanged(object sender, EventArgs e)
        {
            checkButtonEnabled();
        }

        private void kb1MaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            checkButtonEnabled();
        }

        private void kb2MaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            checkButtonEnabled();
        }

        private void kb3MaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            checkButtonEnabled();
        }

        private void ritzelMaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            checkButtonEnabled();
        }

        /// <summary>
        /// Check if the checkButton is enabled or not.
        /// </summary>
        private void checkButtonEnabled()
        {
            saveButton.Enabled = false;
            if (ritzelMaskedTextBox.Text != "" && ritzelMaskedTextBox.Text != _mask && umfangToolStripComboBox.Text != "" && vehicleToolStripComboBox.Text != ""
                && kb1MaskedTextBox.Text != "" && ritzelToolStripComboBox.Text != "" && blattToolStripComboBox.Text != "")
            {
                if (blattToolStripComboBox.Text == "1")
                {
                    calcButton.Enabled = true;
                }
                else if (blattToolStripComboBox.Text == "2")
                {
                    if (kb2MaskedTextBox.Text != "")
                    {
                        calcButton.Enabled = true;
                    }
                }
                else if (blattToolStripComboBox.Text == "3")
                {
                    if (kb2MaskedTextBox.Text != "" && kb3MaskedTextBox.Text != "")
                    {
                        calcButton.Enabled = true;
                    }
                    else calcButton.Enabled = false;
                }
            }
            else calcButton.Enabled = false;
        }
        #endregion

        /// <summary>
        /// When all data is collected, the "entfaltung" can be calculated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calcButton_Click(object sender, EventArgs e)
        {
            calc();
        }

        /// <summary>
        /// Calculate "entfaltung".
        /// </summary>
        private void calc()
        {
            entfaltungsToolStripStatusLabel.Text = "";
            sortKettenblaetter();
            sortRitzel();
            createTableLayout();
            entfaltungsTabControl.SelectedIndex = 1;
        }

        /// <summary>
        /// Sort front gears.
        /// </summary>
        private void sortKettenblaetter()
        {
            _kb = Array.Empty<int>();
            if (blattToolStripComboBox.Text == "1")
            {
                _kb = new int[1];
                _kb.SetValue(Convert.ToInt16(kb1MaskedTextBox.Text), 0);
            }
            else if (blattToolStripComboBox.Text == "2")
            {
                _kb = new int[2];
                _kb.SetValue(Convert.ToInt16(kb1MaskedTextBox.Text), 0);
                _kb.SetValue(Convert.ToInt16(kb2MaskedTextBox.Text), 1);
                Array.Sort(_kb);
                kb1MaskedTextBox.Text = _kb[0].ToString();
                kb2MaskedTextBox.Text = _kb[1].ToString();
            }
            else if (blattToolStripComboBox.Text == "3")
            {
                _kb = new int[3];
                _kb.SetValue(Convert.ToInt16(kb1MaskedTextBox.Text), 0);
                _kb.SetValue(Convert.ToInt16(kb2MaskedTextBox.Text), 1);
                _kb.SetValue(Convert.ToInt16(kb3MaskedTextBox.Text), 2);
                Array.Sort(_kb);
                kb1MaskedTextBox.Text = _kb[0].ToString();
                kb2MaskedTextBox.Text = _kb[1].ToString();
                kb3MaskedTextBox.Text = _kb[2].ToString();
            }
        }

        /// <summary>
        /// Sort rear gears.
        /// </summary>
        private void sortRitzel()
        {
            try
            {
                string tmp = "";
                _rz = new int[Convert.ToInt16(ritzelToolStripComboBox.Text)];
                string[] ritzel = ritzelMaskedTextBox.Text.Split(';');
                for (int i = 0; i < ritzel.Length; i++)
                {
                    if (ritzel[i] != "") _rz[i] = Convert.ToInt16(ritzel[i]);
                }
                Array.Sort(_rz);
                Array.Reverse(_rz);
                ritzelMaskedTextBox.Text = "";
                for (int i = 0; i < _rz.Length; i++)
                {
                    tmp += _rz[i].ToString() + ";";
                }
                ritzelMaskedTextBox.Text = tmp;
            }
            catch (Exception)
            {
                ShowErrorMessage("Fehler: Ritzel müssen als Ganzzahlen eingegeben werden!", "Fehler bei Ritzeleingabe");
            }
        }

        /// <summary>
        /// Create the "entfaltung" table dynamically.
        /// </summary>
        private void createTableLayout()
        {
            try
            {
                Label[,] cells = new Label[_kb.Length + 1, _rz.Length + 1];

                entfaltungsTableLayoutPanel.SuspendLayout();
                entfaltungsTableLayoutPanel.Controls.Clear();
                entfaltungsTableLayoutPanel.ColumnStyles.Clear();
                entfaltungsTableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
                entfaltungsTableLayoutPanel.ColumnCount = _rz.Length + 2;
                entfaltungsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
                entfaltungsTableLayoutPanel.RowCount = _kb.Length + 2;


                for (int i = 0; i <= _kb.Length + 1; i++)        // i = row
                {
                    for (int j = 0; j <= _rz.Length + 1; j++)    // j = column 
                    {
                        if (i == 0 && j == 0)
                        {
                            entfaltungsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50f));
                            cells[i, j] = new Label();
                            cells[i, j].Text = "v '\' h";
                            cells[i, j].TextAlign = ContentAlignment.MiddleCenter;
                            entfaltungsTableLayoutPanel.Controls.Add(cells[i, j], j, i);
                        }
                        else if (i == 0 && j > 0 && j <= _rz.Length)
                        {
                            cells[i, j] = new Label();
                            cells[i, j].Text = _rz[j - 1].ToString();
                            cells[i, j].TextAlign = ContentAlignment.MiddleCenter;
                            entfaltungsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50f));
                            entfaltungsTableLayoutPanel.Controls.Add(cells[i, j], j, i);
                        }
                        else if (i > 0 && j == 0 && i <= _kb.Length)
                        {
                            cells[i, j] = new Label();
                            cells[i, j].Text = _kb[i - 1].ToString();
                            cells[i, j].TextAlign = ContentAlignment.TopCenter;
                            entfaltungsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
                            entfaltungsTableLayoutPanel.Controls.Add(cells[i, j], j, i);
                        }
                        else if (i > 0 && i <= _kb.Length && j > 0 && j <= _rz.Length)
                        {
                            cells[i, j] = new Label();
                            cells[i, j].Text = (Convert.ToInt32(umfangToolStripComboBox.Text) / (_rz[j - 1] / Convert.ToDouble(_kb[i - 1])) / 1000).ToString();
                            cells[i, j].TextAlign = ContentAlignment.TopCenter;
                            entfaltungsTableLayoutPanel.Controls.Add(cells[i, j], j, i);
                        }
                        else if (i == 0 && j == _rz.Length + 1)
                        {
                            entfaltungsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
                        }
                        else if (j == 0 && i == _kb.Length + 1)
                        {
                            entfaltungsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
                        }
                    }
                }
                entfaltungsTableLayoutPanel.ResumeLayout();
                saveButton.Enabled = true;
                entfaltungsToolStripStatusLabel.Text = "Entfaltung wurde berechnet!";
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Berechnen der Entfaltung");
            }
        }

        #region Umfang - Context menu
        private void x190ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "1272";
        }

        private void x175ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "1580";
        }

        private void x138ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "1948";
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "1900";
        }

        private void x138ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2086";
        }

        private void x13837590ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2100";
        }

        private void x150ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2030";
        }

        private void x160ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2025";
        }

        private void x175ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2050";
        }

        private void x200ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2075";
        }

        private void x210ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2100";
        }

        private void x225ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2120";
        }

        private void x225ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2128";
        }

        private void x11428630ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2174";
        }

        private void x11432630ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2220";
        }

        private void x11240635ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2265";
        }

        private void x12532622ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2170";
        }

        private void x13535622ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2185";
        }

        private void x14037622ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2200";
        }

        private void x15040622ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2220";
        }

        private void x16042622ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2230";
        }

        private void x17547622ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2250";
        }

        private void x20050622ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2280";
        }

        private void x21054622ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2295";
        }

        private void x22557622ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2288";
        }

        private void x23560622ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2330";
        }

        private void x23571ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "1973";
        }

        private void x18C18622ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2102";
        }

        private void x20C20622ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2100";
        }

        private void x23C23622ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2125";
        }

        private void x25C25622ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2135";
        }

        private void x28C28622ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            umfangToolStripComboBox.Text = "2150";
        }
        #endregion

        /// <summary>
        /// Save data into the database, update existing data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            SqlConnection myConnection;
            int vec_id = Convert.ToInt32(GetDatabaseEntry("Vehicles", "Id", "VehicleName = '" + vehicleToolStripComboBox.Text + "'"));
            string test = GetDatabaseEntry("Vehicles", "Entfaltung", "VehicleName = '" + vehicleToolStripComboBox.Text + "'");
            if (test == "-1")
            {
                int id = 0;
                try
                {
                    id = NextId("Entfaltung");
                }
                catch (Exception)
                {
                }
                string kettenbl = kb1MaskedTextBox.Text;
                if (kb2MaskedTextBox.Text != "") kettenbl += ";" + kb2MaskedTextBox.Text;
                if (kb3MaskedTextBox.Text != "") kettenbl += ";" + kb3MaskedTextBox.Text;
                try
                {
                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        myConnection.Open();
                        using (SqlCommand myCommand = new SqlCommand())
                        {
                            string sqlquery = "INSERT INTO Entfaltung" +
                            " (Id, BikeId, Front, Back, Wheel, Unit) " +
                            "VALUES (@id, @bike, @front, @back, @wheel, @unit)";
                            myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            myCommand.Parameters.Add("@bike", SqlDbType.Int).Value = vec_id;
                            myCommand.Parameters.Add("@front", SqlDbType.NVarChar).Value = kettenbl;
                            myCommand.Parameters.Add("@back", SqlDbType.NVarChar).Value = ritzelMaskedTextBox.Text;
                            myCommand.Parameters.Add("@wheel", SqlDbType.Float).Value = Convert.ToDouble(umfangToolStripComboBox.Text);
                            myCommand.Parameters.Add("@unit", SqlDbType.NVarChar).Value = "mm";
                            myCommand.CommandText = sqlquery;
                            myCommand.CommandType = CommandType.Text;
                            myCommand.Connection = myConnection;
                            myCommand.ExecuteNonQuery();
                        }
                        using (SqlCommand myCommand = new SqlCommand())
                        {
                            string sqlquery = "UPDATE Vehicles SET Entfaltung = @entf WHERE Id = " + vec_id;
                            myCommand.Parameters.Add("@entf", SqlDbType.Int).Value = id;
                            myCommand.CommandText = sqlquery;
                            myCommand.CommandType = CommandType.Text;
                            myCommand.Connection = myConnection;
                            myCommand.ExecuteNonQuery();
                        }
                        myConnection.Close();
                        saveButton.Text = "Bearbeiten";
                        saveButton.Enabled = false;
                        entfaltungsToolStripStatusLabel.Text = $"Entfaltung wurde für {vehicleToolStripComboBox.Text} gespeichert!";
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, "Fehler beim Einfügen von Daten in Entfaltung"); 
                    entfaltungsToolStripStatusLabel.Text = "Fehler in den eingegebenen Daten!";
                }
            }
            else
            {
                int id = Convert.ToInt32(test);
                string kettenbl = kb1MaskedTextBox.Text;
                if (kb2MaskedTextBox.Text != "") kettenbl += ";" + kb2MaskedTextBox.Text;
                if (kb3MaskedTextBox.Text != "") kettenbl += ";" + kb3MaskedTextBox.Text;
                try
                {
                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        myConnection.Open();
                        using (SqlCommand myCommand = new SqlCommand())
                        {
                            string sqlquery = "UPDATE Entfaltung " +
                            "SET BikeId = @bike, Front = @front, Back = @back, Wheel = @wheel, Unit = @unit " +
                            "WHERE Id = " + id.ToString();
                            myCommand.Parameters.Add("@bike", SqlDbType.Int).Value = vec_id;
                            myCommand.Parameters.Add("@front", SqlDbType.NVarChar).Value = kettenbl;
                            myCommand.Parameters.Add("@back", SqlDbType.NVarChar).Value = ritzelMaskedTextBox.Text;
                            myCommand.Parameters.Add("@wheel", SqlDbType.Float).Value = Convert.ToDouble(umfangToolStripComboBox.Text);
                            myCommand.Parameters.Add("@unit", SqlDbType.NVarChar).Value = "mm";
                            myCommand.CommandText = sqlquery;
                            myCommand.CommandType = CommandType.Text;
                            myCommand.Connection = myConnection;
                            myCommand.ExecuteNonQuery();
                        }
                        using (SqlCommand myCommand = new SqlCommand())
                        {
                            string sqlquery = "UPDATE Vehicles SET Entfaltung = @entf WHERE Id = " + vec_id;
                            myCommand.Parameters.Add("@entf", SqlDbType.Int).Value = id;
                            myCommand.CommandText = sqlquery;
                            myCommand.CommandType = CommandType.Text;
                            myCommand.Connection = myConnection;
                            myCommand.ExecuteNonQuery();
                        }
                        myConnection.Close();
                        saveButton.Text = "Bearbeiten";
                        saveButton.Enabled = false;
                        entfaltungsToolStripStatusLabel.Text = $"Entfaltung wurde für {vehicleToolStripComboBox.Text} gespeichert!";
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, "Fehler beim Bearbeiten von Daten in Entfaltung");
                    entfaltungsToolStripStatusLabel.Text = "Fehler in den eingegebenen Daten!";
                }
            }
        }

        /// <summary>
        /// Load existing setup.
        /// </summary>
        private void loadEntfaltung()
        {
            if (VehicleEntfaltung != -1)
            {
                SqlConnection con1;

                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        com1.CommandText = @"SELECT * FROM Entfaltung WHERE Id = " + VehicleEntfaltung.ToString();
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                string[] strings1 = reader1[2].ToString().Split(';');
                                int[] blaetter = new int[strings1.Length];
                                for (int i = 0; i < strings1.Length; i++)
                                {
                                    blaetter[i] = int.Parse(strings1[i]);
                                    if (i == 0) kb1MaskedTextBox.Text = strings1[i];
                                    else if (i == 1) kb2MaskedTextBox.Text += strings1[i];
                                    else if (i == 2) kb3MaskedTextBox.Text += strings1[i];
                                }
                                string[] strings2 = reader1[3].ToString().Split(';');
                                vehicleToolStripComboBox.Text = Vehicle;
                                blattToolStripComboBox.Text = strings1.Length.ToString();
                                ritzelToolStripComboBox.Text = (strings2.Length - 1).ToString();
                                ritzelMaskedTextBox.Text = reader1[3].ToString();
                                umfangToolStripComboBox.Text = reader1[4].ToString();
                            }
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
                calc();
                entfaltungsToolStripStatusLabel.Text = "Entfaltung geladen...";
                saveButton.Enabled = false;
                saveButton.Text = "Bearbeiten";
            }
        }
    }
}
