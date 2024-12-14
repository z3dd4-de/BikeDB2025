using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    internal class Helpers
    {
        #region Enumerations
        public enum JobType { EXPORT, IMPORT };
        public enum JobStatus { NONE, DROP_ALL, APPEND_ALL, DROP_TOUR, APPEND_TOUR };
        public enum LogType { ERROR, INFO, WARNING, LOGIN, LOGOUT, EXPORT, IMPORT, NEW_ENTRY };
        public enum Installation { SINGLE_USER, MULTI_USER, QUICK_LOGIN, STRICT, SINGLE_ADMIN };
        public enum VehicleType { BIKE, FOSSIL, ELECTRIC }
        #endregion

        #region Database Helper Functions
        /// <summary>
        /// Simple function to return number of entries in database table "tablename".
        /// </summary>
        /// <param name="tablename">Name of table to be queried.</param>
        /// <returns>Number of entries (int) in table, "0" if empty.</returns>
        public static int CountIds(string tablename, bool user = true)
        {
            SqlConnection myConnection;
            int result = -1;
            try
            {
                using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand())
                    {

                        string sqlquery = "";
                        if (user)
                        {
                            sqlquery = "SELECT COUNT(Id) FROM " + tablename +
                            " WHERE [User] = " + Properties.Settings.Default.CurrentUserID.ToString();
                        }
                        else
                        {
                            sqlquery = "SELECT COUNT(Id) FROM " + tablename;
                        }
                        myCommand.CommandText = sqlquery;
                        myCommand.CommandType = CommandType.Text;
                        myCommand.Connection = myConnection;
                        result = Convert.ToInt32(myCommand.ExecuteScalar());
                    }
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Zählen von Einträgen");
            }
            return result;
        }

        /// <summary>
        /// Returns the maximum id of a table, e.g. to calculate the next id.
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static int MaxId(string tablename)
        {
            SqlConnection myConnection;
            int result = -1;
            try
            {
                using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand())
                    {
                        string sqlquery = "SELECT MAX(Id) FROM " + tablename;
                        myCommand.CommandText = sqlquery;
                        myCommand.CommandType = CommandType.Text;
                        myCommand.Connection = myConnection;
                        result = Convert.ToInt32(myCommand.ExecuteScalar());
                    }
                    myConnection.Close();
                }
            }
            catch (Exception)
            {
                result = -1;     // empty table
            }
            return result;
        }

        /// <summary>
        /// Get the next (empty) Id of a table.
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public static int NextId(string tablename)
        {
            int result = MaxId(tablename) + 1;
            return result;
        }

        /// <summary>
        /// Deletes the content of a table without dropping the table.
        /// </summary>
        /// <param name="tablename">Table to be cleared.</param>
        public static void ClearTable(string tablename)
        {
            SqlConnection myConnection;
            try
            {
                using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand())
                    {
                        string sqlquery = "TRUNCATE TABLE " + tablename;
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
                ShowErrorMessage(ex.Message, "Fehler beim Leeren der Tabelle " + tablename);
            }
        }

        /// <summary>
        /// Select column "value" from "tablename" where id = "id".
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="value"></param>
        /// <param name="id"></param>
        /// <returns>Only one value from column "value".</returns>
        public static string GetDatabaseEntry(string tablename, string value, int id, bool user = false)
        {
            SqlConnection con1;
            string result = "";
            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        if (user)
                        {
                            com1.CommandText = $"SELECT {value} FROM {tablename} WHERE Id = " + id.ToString() + " AND [User] = " + Properties.Settings.Default.CurrentUserID;
                        }
                        else com1.CommandText = $"SELECT {value} FROM {tablename} WHERE Id = " + id.ToString();
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                result = reader1[0].ToString();
                            }
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Datenbankzugriff");
            }
            return result;
        }

        /// <summary>
        /// This method can be used for aggregation functions like MAX, SUM, AVG... in SQL.
        /// E.g. "Table", "SUM(val)" gives "SELECT SUM(val) FROM Table".
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="value"></param>
        /// <returns>Only one value.</returns>
        public static string GetDatabaseEntry(string tablename, string value, bool user = false)
        {
            SqlConnection con1;
            string result = "";
            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        if (user)
                        {
                            com1.CommandText = $"SELECT {value} FROM {tablename} WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                        }
                        else com1.CommandText = $"SELECT {value} FROM {tablename}";// WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                result = reader1[0].ToString();
                            }
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Datenbankzugriff");
            }
            return result;
        }

        /// <summary>
        /// This method can be used for aggregation functions like MAX, SUM, AVG... in SQL.
        /// E.g. "Table", "SUM(val)" gives "SELECT SUM(val) FROM Table WHERE Vehicle = ...".
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="value"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static string GetDatabaseEntry(string tablename, string value, string where, bool user = false)
        {
            SqlConnection con1;
            string result = "";
            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        if (user)
                        {
                            com1.CommandText = $"SELECT {value} FROM {tablename} WHERE " + where + " AND [User] = " + Properties.Settings.Default.CurrentUserID;
                        }
                        else com1.CommandText = $"SELECT {value} FROM {tablename} WHERE " + where; //+ " AND [User] = " + Properties.Settings.Default.CurrentUserID;
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                result = reader1[0].ToString();
                            }
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Datenbankzugriff");
            }
            return result;
        }

        /// <summary>
        /// Used for statistics. Returns min, average and max of a value.
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="value"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static string MinAvgMax(string tablename, string value, string where)
        {
            SqlConnection con1;
            string result = "";
            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        com1.CommandText = $"SELECT CAST(MIN({value}) as decimal(6, 2)), CAST(AVG({value}) as decimal(6, 2)), CAST(MAX({value}) as decimal(6, 2)) FROM {tablename} WHERE " + where
                             + " AND [User] = " + Properties.Settings.Default.CurrentUserID;
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                result = reader1[0].ToString() + " - " + reader1[1].ToString() + " - " + reader1[2].ToString() + " [min, avg, max]";
                            }
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Datenbankzugriff");
            }
            return result;
        }

        /// <summary>
        /// Write event into the database.
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        public static void WriteLogEntry(LogType logType, string message)
        {
            string type = "";
            switch (logType)
            {
                case LogType.ERROR:
                    type = "ERROR";
                    break;
                case LogType.INFO:
                    type = "INFO";
                    break;
                case LogType.WARNING:
                    type = "WARNING";
                    break;
                case LogType.LOGIN:
                    type = "LOGIN";
                    break;
                case LogType.LOGOUT:
                    type = "LOGOUT";
                    break;
                case LogType.EXPORT:
                    type = "EXPORT";
                    break;
                case LogType.IMPORT:
                    type = "IMPORT";
                    break;
                case LogType.NEW_ENTRY:
                    type = "NEW_ENTRY";
                    break;
            }

            SqlConnection myConnection = null;
            try
            {
                using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand())
                    {
                        int id = NextId("Log");
                        string sqlquery = "INSERT INTO Log " +
                        "(Id, Type, Remark, [User], Created) " +
                        "VALUES (@id, @type, @remark, @user, @created)";
                        myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        myCommand.Parameters.Add("@type", SqlDbType.NVarChar).Value = type;
                        myCommand.Parameters.Add("@remark", SqlDbType.NVarChar).Value = message;
                        myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                        myCommand.Parameters.Add("@created", SqlDbType.DateTime).Value = DateTime.Now;
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
                ShowErrorMessage(ex.Message, "Fehler beim Speichern des Log-Eintrags");
            }
        }

        /// <summary>
        /// For navigation purposes the ids of objects (vehicles, routes...) need to be known in MainForm.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="user"></param>
        /// <returns>Array of ids.</returns>
        public static int[] GetObjectIds(string table, bool user = true)
        {
            int cnt = CountIds(table, user);
            int[] ids = new int[cnt];
            if (cnt > 0)
            {
                int i = 0;
                SqlConnection con1;
                try
                {
                    using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        con1.Open();
                        using (SqlCommand com1 = new SqlCommand())
                        {
                            if (user)
                            {
                                com1.CommandText = $"SELECT Id FROM {table} WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                            }
                            else com1.CommandText = $"SELECT Id FROM {table}";
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;

                            using (SqlDataReader reader1 = com1.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    ids[i] = reader1.GetInt32(0);
                                    i++;
                                }
                                reader1.Close();
                            }
                        }
                        con1.Close();
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, "Fehler beim Datenbankzugriff");
                }
            }
            return ids;
        }

        /// <summary>
        /// For navigation purposes the ids of objects (vehicles, routes...) need to be known in MainForm.
        /// </summary>
        /// <param name="table">Table name.</param>
        /// <param name="orderby">Order by field name(s).</param>
        /// <param name="user">User: bool if current user is used in WHERE clause.</param>
        /// <returns>Array of ids.</returns>
        public static int[] GetObjectIds(string table, string orderby, bool user = true)
        {
            int cnt = CountIds(table, user);
            int[] ids = new int[cnt];
            if (cnt > 0)
            {
                int i = 0;
                SqlConnection con1;
                try
                {
                    using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        con1.Open();
                        using (SqlCommand com1 = new SqlCommand())
                        {
                            if (user)
                            {
                                com1.CommandText = $"SELECT Id FROM {table} WHERE [User] = " + Properties.Settings.Default.CurrentUserID +
                                    " ORDER BY " + orderby;
                            }
                            else if (user && table == "Vehicles")   // Hiking (Wandern) and Jogging is for all users available
                            {
                                com1.CommandText = $"SELECT Id FROM {table} WHERE Id IN (0, 1) AND [User] = " + Properties.Settings.Default.CurrentUserID +
                                    " ORDER BY " + orderby;
                            }
                            else com1.CommandText = $"SELECT Id FROM {table} ORDER BY {orderby}";
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;

                            using (SqlDataReader reader1 = com1.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    ids[i] = reader1.GetInt32(0);
                                    i++;
                                }
                                reader1.Close();
                            }
                        }
                        con1.Close();
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, "Fehler beim Datenbankzugriff");
                }
            }
            return ids;
        }
        #endregion

        #region Save/Load Settings
        public static Point GetPointFromString(string setting)
        {
            Point point = new Point();
            string temp = GetDatabaseEntry("Settings", setting, true);
            //TODO fertig machen
            return point;
        }

        public static Size GetSizeFromString(string setting)
        {
            Size size = new Size();
            string temp = GetDatabaseEntry("Settings", setting, true);
            //TODO fertig machen
            return size;
        }

        public static bool GetBoolFromTinyInt(string setting)
        {
            //TODO fertig machen
            return true;
        }

        public static void SaveUserSettings()
        {
            int user_id = Properties.Settings.Default.CurrentUserID;

        }

        public static void LoadUserSettings()
        {
            int user_id = Properties.Settings.Default.CurrentUserID;

        }
        #endregion

        #region Password functions
        /// <summary>
        /// Helper function to create a password from a given character set.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="characterSet"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetRandomString(int length, IEnumerable<char> characterSet)
        {
            if (length < 0)
                throw new ArgumentException("length must not be negative", "length");
            if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
                throw new ArgumentException("length is too big", "length");
            if (characterSet == null)
                throw new ArgumentNullException("characterSet");
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
                throw new ArgumentException("characterSet must not be empty", "characterSet");

            var bytes = new byte[length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }

        /// <summary>
        /// Create a random password with a given length.
        /// https://stackoverflow.com/questions/54991/generating-random-passwords
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CreateRandomPassword(int length)
        {
            const string alphanumericCharacters =
                "ABCDEFGHJKLMNPQRSTUVWXYZ" +
                "abcdefghjkmnpqrstuvwxyz" +
                "123456789";
            return GetRandomString(length, alphanumericCharacters);
        }

        /// <summary>
        /// Get the SHA384 hash for a given password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Password hash</returns>
        public static string GetPasswordHash(string password)
        {
            byte[] data = new byte[password.Length];
            byte[] result;
            for (int i = 0; i < password.Length; i++)
            {
                data.SetValue(Convert.ToByte(password[i]), i);
            }
            using (SHA384 sha = SHA384.Create())
            {
                result = sha.ComputeHash(data);
            }
            string hash = "";
            for (int i = 0; i < result.Length; i++)
            {
                hash += result[i].ToString();
            }
            return hash;
        }
        #endregion

        #region Error Messages
        /// <summary>
        /// Shows an error message.
        /// </summary>
        /// <param name="message"></param>
        public static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Fehler!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            WriteLogEntry(LogType.ERROR, message);
        }

        /// <summary>
        /// Shows an error message with an additional caption.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption"></param>
        public static void ShowErrorMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            WriteLogEntry(LogType.ERROR, message);
        }
        #endregion

        #region General Helper functions
        /// <summary>
        /// Used for masked TextBoxes. Fills blanks in front of decimal values.
        /// </summary>
        /// <param name="value">Decimal value.</param>
        /// <param name="length">Count chars before "." or ",".</param>
        /// <returns></returns>
        public static string FillDecimalForTextbox(decimal value, int length)
        {
            string result = "";
            int pos = -1;
            if (value.ToString().Contains("."))
            {
                pos = value.ToString().IndexOf(".");
            }
            if (value.ToString().Contains(",")) pos = value.ToString().IndexOf(",");
            int del = length - pos;
            if (del > 0)
            {
                for (int i = 0; i < del; i++)
                {
                    result += " ";
                }
            }
            return result += value.ToString();
        }

        /// <summary>
        /// Used for login and logout.
        /// </summary>
        /// <param name="logged_in"></param>
        /// <param name="admin"></param>
        public static void SetLoginStatus(bool logged_in, bool admin = false)
        {
            Properties.Settings.Default.UserLoggedIn = logged_in;
            Properties.Settings.Default.AdminLoggedIn = admin;
            /*if (logged_in)
            {
                WriteLogEntry(LogType.LOGIN, "");
            }*/
        }
        #endregion
    }
}
