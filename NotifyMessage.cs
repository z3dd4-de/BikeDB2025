using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    internal class NotifyMessage
    {
        #region Properties
        public string Text { get; set; }
        public string Title { get; set; }
        public ToolTipIcon Icon { get; set; }
        public NotifyIcon MyNotifyIcon { get; set; }
        public int Time { get; set; }
        public bool Enabled { get; set; }
        public bool HasGoal { get; set; }
        public bool HasBirthday { get; set; }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="time"></param>
        public NotifyMessage(int time = 5000)
        {
            MyNotifyIcon = null;
            Icon = ToolTipIcon.Info;
            if (time < 100) time *= 1000;
            Time = time;
        }

        /// <summary>
        /// Check if a goal is due today.
        /// </summary>
        private void getGoals()
        {
            try
            {
                SqlConnection con1;
                string result = "";
                int cnt = 0;
                DateTime today = DateTime.Today;

                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        com1.CommandText = @"SELECT * FROM Goals WHERE [User] = " + Properties.Settings.Default.CurrentUserID.ToString()
                            + " AND Achieved = 0";
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            if (reader1.HasRows)
                            {
                                while (reader1.Read())
                                {
                                    if (!reader1.IsDBNull(1))
                                    {
                                        if (reader1.GetDateTime(3) == today)
                                        {
                                            result += "Folgendes Ziel sollte heute erledigt werden: \n";
                                            result += reader1.GetString(1) + "\n";
                                            cnt++;
                                        }
                                    }
                                }
                                if (cnt > 0)
                                {
                                    HasGoal = true;
                                    Text = result;
                                }
                            }
                            
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Laden von Zielen");
            }
        }

        /// <summary>
        /// Check who has got birthday today. TODO: Birthday of current user.
        /// </summary>
        private void getBirthdays()
        {
            try
            {
                SqlConnection con1;
                string result = "";
                int cnt = 0;
                DateTime today = DateTime.Today;
                
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        com1.CommandText = @"SELECT * FROM BirthdateView WHERE [User] = " + Properties.Settings.Default.CurrentUserID.ToString();
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            if (reader1.HasRows)
                            {
                                while (reader1.Read())
                                {
                                    if (!reader1.IsDBNull(2))
                                    {
                                        if (reader1.GetDateTime(2).Month == today.Month
                                            && reader1.GetDateTime(2).Day == today.Day)
                                        {
                                            result += "Heute hat Geburtstag: \n";
                                            result += reader1.GetString(0) + " " + reader1.GetString(1) + " (" + reader1.GetString(3) + ")\n";
                                            cnt++;
                                        }
                                    }
                                }
                                if (cnt > 0)
                                {
                                    HasBirthday = true;
                                    if (HasGoal)
                                    {
                                        Text += "\n" + result;
                                    }
                                    else
                                        Text = result;
                                }
                            }
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Laden von Geburtstagen");
            }
        }

        /// <summary>
        /// Show goals and birthdays of today. If there are none, Text will remain empty.
        /// </summary>
        private void setText()
        {
            Text = "";
            Title = DateTime.Today.ToString("dd. MMM.yyyy") + ":\n";
            getGoals();
            getBirthdays();
        }

        /// <summary>
        /// Alias for ShowMessage(). Called by the timer in MainForm at midnight.
        /// </summary>
        public void NewDay()
        {
            ShowMessage();
        } 

        /// <summary>
        /// Show the message with internal variables.
        /// </summary>
        public void ShowMessage()
        {
            setText();
            ShowMessage(Title, Text, Icon);
        }

        /// <summary>
        /// Can be used by any Form to directly show a message.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="icon"></param>
        public void ShowMessage(string title, string message, ToolTipIcon icon = ToolTipIcon.Info)
        {
            if (Text != "")
            {
                MyNotifyIcon.BalloonTipText = message;
                MyNotifyIcon.BalloonTipTitle = title;
                MyNotifyIcon.BalloonTipIcon = icon;
                MyNotifyIcon.ShowBalloonTip(Time);
            }
        }
    }
}
