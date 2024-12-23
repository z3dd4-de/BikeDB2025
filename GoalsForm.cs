using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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

        #region Predefined goals
        private double daysTillEndOfWeek(DateTime date)
        {
            double add_days = 0.0f;
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday:      // Sunday is End of Week in Germany
                    break;
                case DayOfWeek.Monday:
                    add_days = 6.0f;
                    break;
                case DayOfWeek.Tuesday:
                    add_days = 5.0f;
                    break;
                case DayOfWeek.Wednesday:
                    add_days = 4.0f;
                    break;
                case DayOfWeek.Thursday:
                    add_days = 3.0f;
                    break;
                case DayOfWeek.Friday:
                    add_days = 2.0f;
                    break;
                case DayOfWeek.Saturday:
                    add_days = 1.0f;
                    break;
                default:
                    break;
            }
            return add_days;
        }

        private double daysTillEndOfMonth(DateTime date)
        {
            double add_days = 0.0f;
            int day = date.Day;
            int days = DateTime.DaysInMonth(date.Year, date.Month);
            add_days = days - day;
            return add_days;
        }

        private DateTime getSilvester(DateTime date)
        {
            return new DateTime(date.Year, 12, 31);
        }

        private void EndeDerWocheToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            goalDateTimePicker.Value = dt.AddDays(daysTillEndOfWeek(DateTime.Now));
            titleTextBox.Text = " erledigen";
        }

        private void EndeDesMonatsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            goalDateTimePicker.Value = dt.AddDays(daysTillEndOfMonth(DateTime.Now));
            titleTextBox.Text = " erledigen";
        }

        private void EndeDesJahresToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            goalDateTimePicker.Value = getSilvester(DateTime.Now);
            titleTextBox.Text = " erledigen";
        }

        private void KmToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "20 km Fahrrad fahren";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void KmToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "30 km Fahrrad fahren";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void KmToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "50 km Fahrrad fahren";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void KmToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "100 km Fahrrad fahren";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void KmToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "200 km Fahrrad fahren";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void EndeDerWocheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            goalDateTimePicker.Value = dt.AddDays(daysTillEndOfWeek(DateTime.Now));
            titleTextBox.Text = " km zurücklegen bis " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }

        private void EndeDesMonatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            goalDateTimePicker.Value = dt.AddDays(daysTillEndOfMonth(dt));
            titleTextBox.Text = " km zurücklegen bis " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }

        private void EndeDesJahresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            goalDateTimePicker.Value = getSilvester(DateTime.Now);
            titleTextBox.Text = " km zurücklegen bis " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }

        private void KmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "Tour mit 20 km machen";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void KmToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "Tour mit 30 km machen";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void KmToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "Tour mit 50 km machen";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void KmToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "Tour mit 100 km machen";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void KmToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "Tour mit 200 km machen";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void WandernGehenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "Wandern gehen";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void KonzertBesuchenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "Konzert besuchen";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void SchwimmenGehenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "Schwimmen gehen";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void JoggenGehenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "Joggen gehen";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void JemandenTreffenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "Person treffen";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void JemandenAnrufenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "Person anrufen";
            remarkRichTextBox.Text = titleTextBox.Text;
        }

        private void NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            goalDateTimePicker.Value = dt.AddDays(daysTillEndOfMonth(dt));
            titleTextBox.Text = " bis " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }

        private void MontagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            double add_days = 0.0f;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    add_days = 1.0f;
                    break;
                case DayOfWeek.Monday:
                    add_days = 7.0f;
                    break;
                case DayOfWeek.Tuesday:
                    add_days = 6.0f;
                    break;
                case DayOfWeek.Wednesday:
                    add_days = 5.0f;
                    break;
                case DayOfWeek.Thursday:
                    add_days = 4.0f;
                    break;
                case DayOfWeek.Friday:
                    add_days = 3.0f;
                    break;
                case DayOfWeek.Saturday:
                    add_days = 2.0f;
                    break;
                default:
                    break;
            }
            goalDateTimePicker.Value = dt.AddDays(add_days);
            titleTextBox.Text = "Erledigen bis " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }

        private void DienstagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            double add_days = 0.0f;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    add_days = 2.0f;
                    break;
                case DayOfWeek.Monday:
                    add_days = 1.0f;
                    break;
                case DayOfWeek.Tuesday:
                    add_days = 7.0f;
                    break;
                case DayOfWeek.Wednesday:
                    add_days = 6.0f;
                    break;
                case DayOfWeek.Thursday:
                    add_days = 5.0f;
                    break;
                case DayOfWeek.Friday:
                    add_days = 4.0f;
                    break;
                case DayOfWeek.Saturday:
                    add_days = 3.0f;
                    break;
                default:
                    break;
            }
            goalDateTimePicker.Value = dt.AddDays(add_days);
            titleTextBox.Text = "Erledigen bis " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }

        private void MittwochToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            double add_days = 0.0f;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    add_days = 3.0f;
                    break;
                case DayOfWeek.Monday:
                    add_days = 2.0f;
                    break;
                case DayOfWeek.Tuesday:
                    add_days = 1.0f;
                    break;
                case DayOfWeek.Wednesday:
                    add_days = 7.0f;
                    break;
                case DayOfWeek.Thursday:
                    add_days = 6.0f;
                    break;
                case DayOfWeek.Friday:
                    add_days = 5.0f;
                    break;
                case DayOfWeek.Saturday:
                    add_days = 4.0f;
                    break;
                default:
                    break;
            }
            goalDateTimePicker.Value = dt.AddDays(add_days);
            titleTextBox.Text = "Erledigen bis " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }

        private void DonnerstagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            double add_days = 0.0f;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    add_days = 4.0f;
                    break;
                case DayOfWeek.Monday:
                    add_days = 3.0f;
                    break;
                case DayOfWeek.Tuesday:
                    add_days = 2.0f;
                    break;
                case DayOfWeek.Wednesday:
                    add_days = 1.0f;
                    break;
                case DayOfWeek.Thursday:
                    add_days = 7.0f;
                    break;
                case DayOfWeek.Friday:
                    add_days = 6.0f;
                    break;
                case DayOfWeek.Saturday:
                    add_days = 5.0f;
                    break;
                default:
                    break;
            }
            goalDateTimePicker.Value = dt.AddDays(add_days);
            titleTextBox.Text = "Erledigen bis " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }

        private void FreitagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            double add_days = 0.0f;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    add_days = 5.0f;
                    break;
                case DayOfWeek.Monday:
                    add_days = 4.0f;
                    break;
                case DayOfWeek.Tuesday:
                    add_days = 3.0f;
                    break;
                case DayOfWeek.Wednesday:
                    add_days = 2.0f;
                    break;
                case DayOfWeek.Thursday:
                    add_days = 1.0f;
                    break;
                case DayOfWeek.Friday:
                    add_days = 7.0f;
                    break;
                case DayOfWeek.Saturday:
                    add_days = 6.0f;
                    break;
                default:
                    break;
            }
            goalDateTimePicker.Value = dt.AddDays(add_days);
            titleTextBox.Text = "Erledigen bis " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }

        private void SamstagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            double add_days = 0.0f;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    add_days = 6.0f;
                    break;
                case DayOfWeek.Monday:
                    add_days = 5.0f;
                    break;
                case DayOfWeek.Tuesday:
                    add_days = 4.0f;
                    break;
                case DayOfWeek.Wednesday:
                    add_days = 3.0f;
                    break;
                case DayOfWeek.Thursday:
                    add_days = 2.0f;
                    break;
                case DayOfWeek.Friday:
                    add_days = 1.0f;
                    break;
                case DayOfWeek.Saturday:
                    add_days = 7.0f;
                    break;
                default:
                    break;
            }
            goalDateTimePicker.Value = dt.AddDays(add_days);
            titleTextBox.Text = "Erledigen bis " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }

        private void SonntagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            double add_days = 0.0f;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    add_days = 7.0f;
                    break;
                case DayOfWeek.Monday:
                    add_days = 6.0f;
                    break;
                case DayOfWeek.Tuesday:
                    add_days = 5.0f;
                    break;
                case DayOfWeek.Wednesday:
                    add_days = 4.0f;
                    break;
                case DayOfWeek.Thursday:
                    add_days = 3.0f;
                    break;
                case DayOfWeek.Friday:
                    add_days = 2.0f;
                    break;
                case DayOfWeek.Saturday:
                    add_days = 1.0f;
                    break;
                default:
                    break;
            }
            goalDateTimePicker.Value = dt.AddDays(add_days);
            titleTextBox.Text = "Erledigen bis " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }

        private void WeihnachtenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            DateTime val = new DateTime(dt.Year, 12, 24);
            goalDateTimePicker.Value = val;
            if (val < dt) goalDateTimePicker.Value = new DateTime(dt.Year + 1, 12, 24);
            titleTextBox.Text = "Erledigen bis Weihnachten: " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }

        private void SilvesterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            goalDateTimePicker.Value = getSilvester(DateTime.Now);
            titleTextBox.Text = "Erledigen bis Silvester: " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }

        private void NeujahrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            goalDateTimePicker.Value = new DateTime(dt.Year + 1, 1, 1);
            titleTextBox.Text = "Erledigen bis Neujahr: " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }

        private void ValentinstagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            DateTime val = new DateTime(dt.Year, 2, 14);
            goalDateTimePicker.Value = val;
            if (val < dt) goalDateTimePicker.Value = new DateTime(dt.Year + 1, 2, 14);
            titleTextBox.Text = "Erledigen bis zum Valentinstag: " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }

        private void TagDerDtEinheitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            DateTime val = new DateTime(dt.Year, 10, 3);
            goalDateTimePicker.Value = val;
            if (val < dt) goalDateTimePicker.Value = new DateTime(dt.Year + 1, 10, 3);
            titleTextBox.Text = "Erledigen bis zum Tag der deutschen Einheit: " + goalDateTimePicker.Value.ToString("dd.MM.YYYY");
        }
        #endregion
    }
}
