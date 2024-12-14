using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class NotesForm : Form
    {
        public bool Edit { get; set; }
        public int EditId { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotesForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize the form after loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotesForm_Load(object sender, EventArgs e)
        {
            errorToolStripStatusLabel.Text = "";
            saveButton.Enabled = false;
            if (Edit)
            {
                saveButton.Text = "Bearbeiten";
                this.Text = "Notiz bearbeiten";
                load();
            }
            else
            {
                saveButton.Text = "Hinzufügen";
                this.Text = "Neue Notiz hinzufügen";
            }
        }

        /// <summary>
        /// Edit existing note.
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
                            com1.CommandText = @"SELECT * FROM Notes WHERE Id = " + EditId.ToString();
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;
                            using (SqlDataReader reader1 = com1.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    titleTextBox.Text = reader1[1].ToString();
                                    remarkRichTextBox.Text = reader1[2].ToString();
                                    saveButton.Enabled = true;
                                    saveButton.Text = "Bearbeiten";
                                    errorToolStripStatusLabel.Text = "Bearbeiten: Notes - Datensatz " + EditId.ToString();
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
                ShowErrorMessage(ex.Message, "Fehler beim Laden der Notiz");
            }
        }

        /// <summary>
        /// Save or edit the current note.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
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
                            int id = NextId("Notes");
                            string sqlquery = "INSERT INTO Notes " +
                            "(Id, Title, Remark, Created, LastChanged, [User]) " +
                            "VALUES (@id, @title, @remark, @created, @lastchanged, @user)";
                            myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            myCommand.Parameters.Add("@title", SqlDbType.NVarChar).Value = titleTextBox.Text;
                            myCommand.Parameters.Add("@remark", SqlDbType.NVarChar).Value = remarkRichTextBox.Text;
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
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, "Fehler beim Speichern der Notiz");
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                var sql = @"UPDATE Notes SET Title = @title, Remark = @remark, LastChanged = @lastchanged " +
                        "WHERE Id = " + EditId.ToString();
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        using (var myCommand = new SqlCommand(sql, connection))
                        {
                            myCommand.Parameters.Add("@title", SqlDbType.NVarChar).Value = titleTextBox.Text;
                            myCommand.Parameters.Add("@remark", SqlDbType.NVarChar).Value = remarkRichTextBox.Text;
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
                    ShowErrorMessage(ex.Message, "Fehler beim Update der Notiz");
                    this.DialogResult = DialogResult.Abort;
                }
            }
            Close();
        }

        #region Check Save Button
        private void titleTextBox_TextChanged(object sender, EventArgs e)
        {
            checkNote();
        }

        private void remarkRichTextBox_TextChanged(object sender, EventArgs e)
        {
            checkNote();
        }

        /// <summary>
        /// Check if button is enabled or not.
        /// </summary>
        private void checkNote()
        {
            if (titleTextBox.Text.Length > 0 && remarkRichTextBox.Text.Length > 0) 
            {
                saveButton.Enabled = true;
            }
            else { saveButton.Enabled = false; }
        }
        #endregion
    }
}
