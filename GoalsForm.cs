using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class GoalsForm : Form
    {
        public bool Edit { get; set; }
        public int EditId { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public GoalsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize the form after loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoalsForm_Load(object sender, EventArgs e)
        {
            errorToolStripStatusLabel.Text = string.Empty;
            saveButton.Enabled = false;
            if (Edit)
            {
                saveButton.Text = "Bearbeiten";
                this.Text = "Ziel bearbeiten";
                load();
            }
            else
            {
                saveButton.Text = "Hinzufügen";
                this.Text = "Neues Ziel hinzufügen";
            }
        }

        /// <summary>
        /// Edit existing goal.
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
                            com1.CommandText = @"SELECT * FROM Goals WHERE Id = " + EditId.ToString();
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;
                            using (SqlDataReader reader1 = com1.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    titleTextBox.Text = reader1[1].ToString();
                                    remarkRichTextBox.Text = reader1[2].ToString();
                                    goalDateTimePicker.Value = Convert.ToDateTime(reader1[3]);
                                    if (Convert.ToInt32(reader1[4]) == 1) doneCheckBox.Checked = true;
                                    else doneCheckBox.Checked = false;
                                    saveButton.Enabled = true;
                                    saveButton.Text = "Bearbeiten";
                                    errorToolStripStatusLabel.Text = "Bearbeiten: Goals - Datensatz " + EditId.ToString();
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
                ShowErrorMessage(ex.Message, "Fehler beim Laden des Ziels");
            }
        }

        #region Check button
        private void titleTextBox_TextChanged(object sender, EventArgs e)
        {
            checkButton();
        }

        private void remarkRichTextBox_TextChanged(object sender, EventArgs e)
        {
            checkButton();
        }

        /// <summary>
        /// Check if save button is enables or disabled.
        /// </summary>
        private void checkButton()
        {
            if (titleTextBox.Text.Length > 0 && remarkRichTextBox.Text.Length > 0)
            {
                saveButton.Enabled = true;
            }
            else
            {
                saveButton.Enabled = false;
            }
        }

        /// <summary>
        /// Enable the button, even if text hasn't changed, but when the goal has been achieved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            saveButton.Enabled = true;
        }
        #endregion

        /// <summary>
        /// Save or edit the current goal.
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
                            int id = NextId("Goals");
                            int achieved = 0;
                            if (doneCheckBox.Checked)
                            {
                                achieved = 1;
                            }
                            string sqlquery = "INSERT INTO Goals " +
                            "(Id, Title, Remark, Date, Achieved, Created, LastChanged, [User]) " +
                            "VALUES (@id, @title, @remark, @date, @achieved, @created, @lastchanged, @user)";
                            myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            myCommand.Parameters.Add("@title", SqlDbType.NVarChar).Value = titleTextBox.Text;
                            myCommand.Parameters.Add("@remark", SqlDbType.NVarChar).Value = remarkRichTextBox.Text;
                            myCommand.Parameters.Add("@date", SqlDbType.Date).Value = goalDateTimePicker.Value;
                            myCommand.Parameters.Add("@achieved", SqlDbType.TinyInt).Value = achieved;
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
                    ShowErrorMessage(ex.Message, "Fehler beim Speichern des Ziels");
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                var sql = @"UPDATE Goals SET Title = @title, Remark = @remark, Date = @date, Achieved = @achieved, LastChanged = @lastchanged " +
                        "WHERE Id = " + EditId.ToString();
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        using (var myCommand = new SqlCommand(sql, connection))
                        {
                            int achieved = 0;
                            if (doneCheckBox.Checked)
                            {
                                achieved = 1;
                            }
                            myCommand.Parameters.Add("@title", SqlDbType.NVarChar).Value = titleTextBox.Text;
                            myCommand.Parameters.Add("@remark", SqlDbType.NVarChar).Value = remarkRichTextBox.Text;
                            myCommand.Parameters.Add("@date", SqlDbType.Date).Value = goalDateTimePicker.Value;
                            myCommand.Parameters.Add("@achieved", SqlDbType.TinyInt).Value = achieved;
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
                    ShowErrorMessage(ex.Message, "Fehler beim Update des Ziels");
                    this.DialogResult = DialogResult.Abort;
                }
            }
            Close();
        }
    }
}
