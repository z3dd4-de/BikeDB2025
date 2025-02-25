using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Documents;
using System.Windows.Forms;
using static BikeDB2024.Helpers;
//using System.Runtime.InteropServices;

namespace BikeDB2024
{
    internal class Helpers
    {
        #region Enumerations
        public enum JobType { EXPORT, IMPORT };
        public enum JobStatus { NONE, DROP_ALL, APPEND_ALL, DROP_TOUR, APPEND_TOUR };
        public enum LogType { ERROR, INFO, WARNING, LOGIN, LOGOUT, EXPORT, IMPORT, NEW_ENTRY };
        public enum Installation { SINGLE_USER, MULTI_USER, QUICK_LOGIN, STRICT, SINGLE_ADMIN };
        public enum VehicleType { BIKE, FOSSIL, ELECTRIC, FLIGHTS }
        #endregion

        #region Database Helper Functions
        /// <summary>
        /// Simple function to return number of entries in database table "tablename".
        /// </summary>
        /// <param name="tablename">Name of table to be queried.</param>
        /// <returns>Number of entries (int) in table, "-1" if empty.</returns>
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

        public static int CountIds(string tablename, string where, bool user = true)
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
                            " WHERE [User] = " + Properties.Settings.Default.CurrentUserID.ToString() +
                            " AND " + where;
                        }
                        else
                        {
                            sqlquery = "SELECT COUNT(Id) FROM " + tablename + " WHERE " + where;
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
        /// A given ComboBox is filled with Countries.
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="sorted"></param>
        public static void InitCountryComboBox(ComboBox comboBox, bool sorted = true)
        {
            comboBox.Sorted = sorted;
            comboBox.Items.Clear();
            SqlConnection con1;
            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        com1.CommandText = $"SELECT Id FROM Countries";
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;

                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                Country c = new Country(reader1.GetInt32(0));
                                comboBox.Items.Add(c);
                            }
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Datenbankzugriff (Countries)");
            }
        }

        /// <summary>
        /// A given ComboBox is filled with Cities.
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="sorted"></param>
        public static void InitCityComboBox(ComboBox comboBox, bool sorted = true)
        {
            comboBox.Sorted = sorted;
            comboBox.Items.Clear();
            SqlConnection con1;

            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        com1.CommandText = $"SELECT Id FROM Cities";
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;

                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                City c = new City(reader1.GetInt32(0));
                                comboBox.Items.Add(c);
                            }
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Datenbankzugriff (Cities)");
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

        #region Save/Load Settings: every user can have own settings.
        /// <summary>
        /// Return location from a string (database table "Settings", location fields).
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static Point GetPointFromString(string setting)
        {
            Point point = new Point(50, 50);    // default
            string temp = GetDatabaseEntry("Settings", setting, true);
            string[] t = temp.Split(';');
            try
            {
                if (t.Length == 2)
                {
                    point.X = Convert.ToInt32(t[0].Trim());
                    point.Y = Convert.ToInt32(t[1].Trim());
                }
            }
            catch (Exception)
            {
            }
            return point;
        }

        /// <summary>
        /// Return size from a string (database table "Settings", size fields).
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static Size GetSizeFromString(string setting)
        {
            Size size = new Size(800, 600);     // default
            string temp = GetDatabaseEntry("Settings", setting, true);
            string[] t = temp.Split(';');
            try
            {
                if (t.Length == 2)
                {
                    size.Width = Convert.ToInt32(t[0].Trim());
                    size.Height = Convert.ToInt32(t[1].Trim());
                }
            }
            catch (Exception)
            {
            }
            return size;
        }

        /// <summary>
        /// Return bool from a tinyint value (database table "Settings", tinyint fields).
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static bool GetBoolFromTinyInt(string setting)
        {
            bool ret = false;     // default
            string temp = GetDatabaseEntry("Settings", setting, true);
            try
            {
                if (Convert.ToByte(temp) == 1) { ret = true; }
            }
            catch (Exception)
            {
            }
            return ret;
        }

        public static bool GetBoolFromTinyInt(byte value)
        {
            bool ret = value == 1 ? true : false;

            return ret;
        }

        /// <summary>
        /// Get tinyint from a bool to store value into the database.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Byte GetTinyIntFromBool(bool value)
        {
            if (value) return (Byte)1;
            else return (Byte)0;
        }

        /// <summary>
        /// Some settings should not be null for new users, so they are initialized here.
        /// </summary>
        public static void InitSettings()
        {
            int user_id = Properties.Settings.Default.CurrentUserID;
            if (GetDatabaseEntry("Users", "Id", "Id = " + user_id.ToString()) == "-1")
            {
                // User's first login
                Properties.Settings.Default.UseAltimeter = false;
                Properties.Settings.Default.StdVehicle = "";
                Properties.Settings.Default.StdContinent = "Europa";
                Properties.Settings.Default.StdCountry = "Deutschland";
                Properties.Settings.Default.StdBundesland = "";
                Properties.Settings.Default.StdCity = "";
                Properties.Settings.Default.StdRoute = "";
                Properties.Settings.Default.WindowLocation = new Point(0,0);
                Properties.Settings.Default.WindowSize = new Size(800, 500);
                Properties.Settings.Default.ShowBirthdays = true;
                Properties.Settings.Default.ShowFlightDB = false;
                Properties.Settings.Default.ShowWelcomeForm = true;
                Properties.Settings.Default.ShowHelp = true;
                Properties.Settings.Default.ShowNotifyIcon = true;
                Properties.Settings.Default.ShowToolbar = true;
                Properties.Settings.Default.ImageFolder = "";
                Properties.Settings.Default.HelpLocation = new Point(0, 0);
                Properties.Settings.Default.HelpSize = new Size(500, 400);
                Properties.Settings.Default.StatLocation = new Point(50, 50);
                Properties.Settings.Default.StatSize = new Size(500, 400);
                Properties.Settings.Default.ToolbarLocation = new Point(4, 0);
                Properties.Settings.Default.ToolbarSize = new Size(423, 25);
                Properties.Settings.Default.ImageViewerLocation = new Point(50, 50);
                Properties.Settings.Default.ImageViewerSize = new Size(750, 600);
                Properties.Settings.Default.EntfaltungLocation = new Point(0, 0);
                Properties.Settings.Default.EntfaltungSize = new Size(500, 400);
                Properties.Settings.Default.ImageEditorPath = "";
                Properties.Settings.Default.ImageEditorName = "";
                Properties.Settings.Default.AdminLocation = new Point(100, 100);
                Properties.Settings.Default.AdminSize = new Size(800, 500);
                Properties.Settings.Default.NotifyTime = 5000;
                Properties.Settings.Default.FlightDBLocation = new Point(50, 50);
                Properties.Settings.Default.FlightDBSize = new Size(800, 500);
                Properties.Settings.Default.UseSigmaDockingStation = false;
                if (Properties.Settings.Default.UseAdminSettings)
                {
                    LoadAdminSettings();
                }
                else
                {
                    Properties.Settings.Default.GoogleEarth = "";
                }
                Properties.Settings.Default.Save();
            }
            else
            {
                LoadUserSettings();
            }
        }

        /// <summary>
        /// Save all runtime Properties.Settings to database when the application closes or the user loggs off.
        /// </summary>
        public static void SaveUserSettings()
        {
            int user_id = Properties.Settings.Default.CurrentUserID;
            SqlConnection myConnection;
            try
            {
                using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand())
                    {
                        string sqlquery = "INSERT INTO Settings" +
                        " ([User], UseAltimeter, DefaultContinent, DefaultCountry, DefaultCity, DefaultRoute, DefaultVehicle, DefaultBundesland, " +
                        "WindowLocation, WindowSize, GoogleEarthPath, ImageEditorPath, ImageEditorName, ImageFolderPath, GpxFolderPath, " +
                        "ShowHelp, ShowToolbar, ShowWelcome, ShowNotification, ShowLastUser, ShowBirthdays, ShowNotifyIcon, ShowFlightDB, " +
                        "KeepLoggedIn, UseDockingStation, AdminChanged, IsMultiUser, UserLoggedIn, AdminLoggedIn, AdminLocation, AdminSize, " +
                        "FlightDBLocation, FlightDBSize, EntfaltungLocation, EntfaltungSize, ImageViewerLocation, ImageViewerSize, HelpLocation, " +
                        "HelpSize, StatLocation, StatSize, ToolbarLocation, ToolbarSize, MinPasswordLength, NotifyTime, LastUser, CurrentUserId, " +
                        "CurrentUserName, InstallationType) " +
                        "VALUES (@user, @alti, @cont, @count, @city, @route, @veh, @bl, @winloc, @winsize, @gpath, @imgedpath, @imgedname, " +
                        "@imgfolder, @gpxfolder, @shelp, @stoolbar, @swelcome, @snoti, @slast, @sbd, @snotif, @sflight, @keepli, @useds, @adchg, @ismulti, " +
                        "@uli, @ali, @adloc, @adsize, @floc, @fsize, @entloc, @entsize, @imgvloc, @imgvsize, @helploc, @helpsize, @statloc, " +
                        "@statsize, @toolloc, @toolsize, @minpwd, @notitime, @lastu, @curui, @curun, @inst)";
                        myCommand.Parameters.Add("@user", SqlDbType.Int).Value = user_id;
                        myCommand.Parameters.Add("@alti", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Properties.Settings.Default.UseAltimeter);
                        myCommand.Parameters.Add("@cont", SqlDbType.NVarChar).Value = Properties.Settings.Default.StdContinent;
                        myCommand.Parameters.Add("@count", SqlDbType.NVarChar).Value = Properties.Settings.Default.StdCountry;
                        myCommand.Parameters.Add("@city", SqlDbType.NVarChar).Value = Properties.Settings.Default.StdCity;
                        myCommand.Parameters.Add("@route", SqlDbType.NVarChar).Value = Properties.Settings.Default.StdRoute;
                        myCommand.Parameters.Add("@veh", SqlDbType.NVarChar).Value = Properties.Settings.Default.StdVehicle;
                        myCommand.Parameters.Add("@bl", SqlDbType.NVarChar).Value = Properties.Settings.Default.StdBundesland;
                        myCommand.Parameters.Add("@winloc", SqlDbType.NVarChar).Value = Properties.Settings.Default.WindowLocation.ToString();
                        myCommand.Parameters.Add("@winsize", SqlDbType.NVarChar).Value = Properties.Settings.Default.WindowSize.ToString();
                        myCommand.Parameters.Add("@gpath", SqlDbType.NVarChar).Value = Properties.Settings.Default.GoogleEarth;
                        myCommand.Parameters.Add("@imgedpath", SqlDbType.NVarChar).Value = Properties.Settings.Default.ImageEditorPath;
                        myCommand.Parameters.Add("@imgedname", SqlDbType.NVarChar).Value = Properties.Settings.Default.ImageEditorName;
                        myCommand.Parameters.Add("@imgfolder", SqlDbType.NVarChar).Value = Properties.Settings.Default.ImageFolder;
                        myCommand.Parameters.Add("@gpxfolder", SqlDbType.NVarChar).Value = Properties.Settings.Default.GpxFolder;
                        myCommand.Parameters.Add("@shelp", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Properties.Settings.Default.ShowHelp);
                        myCommand.Parameters.Add("@stoolbar", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Properties.Settings.Default.ShowToolbar);
                        myCommand.Parameters.Add("@swelcome", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Properties.Settings.Default.ShowWelcomeForm);
                        myCommand.Parameters.Add("@snoti", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Properties.Settings.Default.ShowNotifyIcon);
                        myCommand.Parameters.Add("@slast", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Properties.Settings.Default.ShowLastUser);
                        myCommand.Parameters.Add("@sbd", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Properties.Settings.Default.ShowBirthdays);
                        myCommand.Parameters.Add("@snotif", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Properties.Settings.Default.ShowNotifyIcon);
                        myCommand.Parameters.Add("@sflight", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Properties.Settings.Default.ShowFlightDB);
                        myCommand.Parameters.Add("@keepli", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Properties.Settings.Default.KeepLoggedIn);
                        myCommand.Parameters.Add("@useds", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Properties.Settings.Default.UseSigmaDockingStation);
                        myCommand.Parameters.Add("@adchg", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Properties.Settings.Default.AdminChanged);
                        myCommand.Parameters.Add("@ismulti", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Properties.Settings.Default.IsMultiUser);
                        myCommand.Parameters.Add("@uli", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Properties.Settings.Default.UserLoggedIn);
                        myCommand.Parameters.Add("@ali", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Properties.Settings.Default.AdminLoggedIn);
                        myCommand.Parameters.Add("@adloc", SqlDbType.NVarChar).Value = Properties.Settings.Default.AdminLocation.ToString();
                        myCommand.Parameters.Add("@adsize", SqlDbType.NVarChar).Value = Properties.Settings.Default.AdminSize.ToString();
                        myCommand.Parameters.Add("@floc", SqlDbType.NVarChar).Value = Properties.Settings.Default.FlightDBLocation.ToString();
                        myCommand.Parameters.Add("@fsize", SqlDbType.NVarChar).Value = Properties.Settings.Default.FlightDBSize.ToString();
                        myCommand.Parameters.Add("@entloc", SqlDbType.NVarChar).Value = Properties.Settings.Default.EntfaltungLocation.ToString();
                        myCommand.Parameters.Add("@entsize", SqlDbType.NVarChar).Value = Properties.Settings.Default.EntfaltungSize.ToString();
                        myCommand.Parameters.Add("@imgvloc", SqlDbType.NVarChar).Value = Properties.Settings.Default.ImageViewerLocation.ToString();
                        myCommand.Parameters.Add("@imgvsize", SqlDbType.NVarChar).Value = Properties.Settings.Default.ImageViewerSize.ToString();
                        myCommand.Parameters.Add("@helploc", SqlDbType.NVarChar).Value = Properties.Settings.Default.HelpLocation.ToString();
                        myCommand.Parameters.Add("@helpsize", SqlDbType.NVarChar).Value = Properties.Settings.Default.HelpSize.ToString();
                        myCommand.Parameters.Add("@statloc", SqlDbType.NVarChar).Value = Properties.Settings.Default.StatLocation.ToString();
                        myCommand.Parameters.Add("@statsize", SqlDbType.NVarChar).Value = Properties.Settings.Default.StatSize.ToString();
                        myCommand.Parameters.Add("@toolloc", SqlDbType.NVarChar).Value = Properties.Settings.Default.ToolbarLocation.ToString();
                        myCommand.Parameters.Add("@toolsize", SqlDbType.NVarChar).Value = Properties.Settings.Default.ToolbarSize.ToString();
                        myCommand.Parameters.Add("@minpwd", SqlDbType.Int).Value = Properties.Settings.Default.MinPasswordLength;
                        myCommand.Parameters.Add("@notitime", SqlDbType.Int).Value = Properties.Settings.Default.NotifyTime;
                        myCommand.Parameters.Add("@lastu", SqlDbType.NVarChar).Value = Properties.Settings.Default.LastUser;
                        myCommand.Parameters.Add("@curui", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                        myCommand.Parameters.Add("@curun", SqlDbType.NVarChar).Value = Properties.Settings.Default.CurrentUserName;
                        myCommand.Parameters.Add("@inst", SqlDbType.NVarChar).Value = Properties.Settings.Default.InstallationType;

                        myCommand.CommandText = sqlquery;
                        myCommand.CommandType = CommandType.Text;
                        myCommand.Connection = myConnection;
                        myCommand.ExecuteNonQuery();
                    }
                    using (SqlCommand myCommand = new SqlCommand())
                    {
                        /*string sqlquery = "UPDATE Vehicles SET Entfaltung = @entf WHERE Id = " + vec_id;
                        myCommand.Parameters.Add("@entf", SqlDbType.Int).Value = id;
                        myCommand.CommandText = sqlquery;
                        myCommand.CommandType = CommandType.Text;
                        myCommand.Connection = myConnection;
                        myCommand.ExecuteNonQuery();*/
                    }
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Einfügen von Daten in Settings");
            }
        }

        /// <summary>
        /// Load all runtime Properties.Settings from database when the application starts.
        /// </summary>
        public static void LoadUserSettings()   //TODO
        {
            int user_id = Properties.Settings.Default.CurrentUserID;
            Properties.Settings.Default.UseAltimeter = false;
            Properties.Settings.Default.StdVehicle = "";
            Properties.Settings.Default.StdContinent = "Europa";
            Properties.Settings.Default.StdCountry = "Deutschland";
            Properties.Settings.Default.StdBundesland = "";
            Properties.Settings.Default.StdCity = "";
            Properties.Settings.Default.StdRoute = "";
            Properties.Settings.Default.WindowLocation = new Point(0, 0);
            Properties.Settings.Default.WindowSize = new Size(800, 500);
            Properties.Settings.Default.ShowBirthdays = true;
            Properties.Settings.Default.ShowFlightDB = false;
            Properties.Settings.Default.ShowWelcomeForm = true;
            Properties.Settings.Default.ShowHelp = true;
            Properties.Settings.Default.ShowNotifyIcon = true;
            Properties.Settings.Default.ShowToolbar = true;
            Properties.Settings.Default.ImageFolder = "";
            Properties.Settings.Default.HelpLocation = new Point(0, 0);
            Properties.Settings.Default.HelpSize = new Size(500, 400);
            Properties.Settings.Default.StatLocation = new Point(50, 50);
            Properties.Settings.Default.StatSize = new Size(500, 400);
            Properties.Settings.Default.ToolbarLocation = new Point(4, 0);
            Properties.Settings.Default.ToolbarSize = new Size(423, 25);
            Properties.Settings.Default.ImageViewerLocation = new Point(50, 50);
            Properties.Settings.Default.ImageViewerSize = new Size(750, 600);
            Properties.Settings.Default.EntfaltungLocation = new Point(0, 0);
            Properties.Settings.Default.EntfaltungSize = new Size(500, 400);
            Properties.Settings.Default.ImageEditorPath = "";
            Properties.Settings.Default.ImageEditorName = "";
            Properties.Settings.Default.AdminLocation = new Point(100, 100);
            Properties.Settings.Default.AdminSize = new Size(800, 500);
            Properties.Settings.Default.NotifyTime = 5000;
            Properties.Settings.Default.FlightDBLocation = new Point(50, 50);
            Properties.Settings.Default.FlightDBSize = new Size(800, 500);
            Properties.Settings.Default.UseSigmaDockingStation = false;
            if (Properties.Settings.Default.UseAdminSettings)
            {
                LoadAdminSettings();
            }
            else
            {
                Properties.Settings.Default.GoogleEarth = "";
            }
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Admin settings are generally the ones stored for user = 0. All admins share these settings. Users cannot change them.
        /// </summary>
        public static void LoadAdminSettings()   //TODO
        {
            /*Properties.Settings.Default.GoogleEarth = "";
            Properties.Settings.Default.ImageEditorPath = "";
            Properties.Settings.Default.ImageEditorName = "";
            Properties.Settings.Default.ShowFlightDB = true;
            Properties.Settings.Default.ShowLastUser = "";
            Properties.Settings.Default.AdminChanged = "";
            Properties.Settings.Default.KeepLoggedIn = "";
            Properties.Settings.Default.IsMultiUser = "";*/
        }

        public static void SaveAdminSettings()   //TODO
        {
            /*Properties.Settings.Default.GoogleEarth = "";
            Properties.Settings.Default.ImageEditorPath = "";
            Properties.Settings.Default.ImageEditorName = "";
            Properties.Settings.Default.ShowFlightDB = true;
            Properties.Settings.Default.ShowLastUser = "";
            Properties.Settings.Default.AdminChanged = "";
            Properties.Settings.Default.KeepLoggedIn = "";
            Properties.Settings.Default.IsMultiUser = "";*/
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

        #region Methods to change the table entry "NotShown" of available objects
        public enum VisibilityObject { AIRLINE, AIRPORT, CITY, COMPANY, COST, COUNTRY, FLIGHT, PERSON, PLANEMANUFACTURER,
            PLANE, ROUTE, VEHICLE } 

        /// <summary>
        /// Return a List of ints with all objects that are not shown for a user.
        /// </summary>
        /// <param name="vobject"></param>
        /// <returns></returns>
        public static List<int> GetNotShownObjects(VisibilityObject vobject)
        {
            int user_id = Properties.Settings.Default.CurrentUserID;
            string column = "";
            List<int> result_ints = new List<int>();
            switch (vobject)
            {
                case VisibilityObject.AIRLINE:
                    column = "Airlines";
                    break;
                case VisibilityObject.AIRPORT:
                    column = "Airports";
                    break;
                case VisibilityObject.CITY:
                    column = "Cities";
                    break;
                case VisibilityObject.COMPANY:
                    column = "Companies";
                    break;
                case VisibilityObject.COST:
                    column = "Costs";
                    break;
                case VisibilityObject.COUNTRY:
                    column = "Countries";
                    break;
                case VisibilityObject.FLIGHT:
                    column = "Flights";
                    break;
                case VisibilityObject.PERSON:
                    column = "Persons";
                    break;
                case VisibilityObject.PLANEMANUFACTURER:
                    column = "PlaneManufacturers";
                    break;
                case VisibilityObject.PLANE:
                    column = "Planes";
                    break;
                case VisibilityObject.ROUTE:
                    column = "Routes";
                    break;
                case VisibilityObject.VEHICLE:
                    column = "Vehicles";
                    break;
                default:
                    break;
            }
            SqlConnection con1;
            string result = "";
            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        com1.CommandText = $"SELECT {column} FROM NotShownObjects WHERE User = {user_id}";
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;

                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                if (reader1[0].ToString() != "")
                                    result = reader1.GetString(0);
                            }
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
                if (result.Length >= 1)
                {
                    try
                    {
                        if (result.Contains(";"))
                        {
                            string[] tmp = result.Split(';');
                            foreach (string s in tmp)
                            {
                                string s_new = s.Trim();
                                if (s_new.Length > 0 && Convert.ToInt32(s_new) >= 0)
                                    result_ints.Add(Convert.ToInt32(s_new));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage(ex.Message, "Fehler bei Konvertierung zu int (NotShownObjects)");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Datenbankzugriff (NotShownObjects)");
            }
            return result_ints;
        }

        public static void ChangeComboBoxVisibility(VisibilityObject vobject, bool shown, int id)
        {
            string table = "";
            byte bshown = GetTinyIntFromBool(shown);
            string sshown = bshown.ToString();
            switch (vobject)
            {
                case VisibilityObject.AIRLINE:
                    table = "Airlines";
                    break;
                case VisibilityObject.AIRPORT:
                    table = "Airport";
                    break;
                case VisibilityObject.CITY:
                    table = "Cities";
                    break;
                case VisibilityObject.COMPANY:
                    table = "Companies";
                    break;
                case VisibilityObject.COST:
                    table = "Costs";
                    break;
                case VisibilityObject.COUNTRY:
                    table = "Countries";
                    break;
                case VisibilityObject.FLIGHT:
                    table = "Flights";
                    break;
                case VisibilityObject.PERSON:
                    table = "Persons";
                    break;
                case VisibilityObject.PLANEMANUFACTURER:
                    table = "PlaneManufacturers";
                    break;
                case VisibilityObject.PLANE:
                    table = "Planes";
                    break;
                case VisibilityObject.ROUTE:
                    table = "Routes";
                    break;
                case VisibilityObject.VEHICLE:
                    table = "Vehicles";
                    break;
                default:
                    break;
            }
            string sql = $"UPDATE {table} SET NotShown = {sshown} WHERE Id = {id}";
            MessageBox.Show(sql);   //TODO
        }

        public static void LoadVisibilty(VisibilityObject vobject, int id)
        {
            string table = "";
            switch (vobject)
            {
                case VisibilityObject.AIRLINE:
                    table = "Airlines";
                    break;
                case VisibilityObject.AIRPORT:
                    table = "Airport";
                    break;
                case VisibilityObject.CITY:
                    table = "Cities";
                    break;
                case VisibilityObject.COMPANY:
                    table = "Companies";
                    break;
                case VisibilityObject.COST:
                    table = "Costs";
                    break;
                case VisibilityObject.COUNTRY:
                    table = "Countries";
                    break;
                case VisibilityObject.FLIGHT:
                    table = "Flights";
                    break;
                case VisibilityObject.PERSON:
                    table = "Persons";
                    break;
                case VisibilityObject.PLANEMANUFACTURER:
                    table = "PlaneManufacturers";
                    break;
                case VisibilityObject.PLANE:
                    table = "Planes";
                    break;
                case VisibilityObject.ROUTE:
                    table = "Routes";
                    break;
                case VisibilityObject.VEHICLE:
                    table = "Vehicles";
                    break;
                default:
                    break;
            }
            string sshown = GetDatabaseEntry(table, "NotShown", id, true);
            if (sshown == "0")  //TODO
            {

            }
            else if (sshown == "1")
            {

            }
        }
        #endregion

        #region Image Galeries
        /// <summary>
        /// Check if a given path is a file or a folder.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static (string, bool) GetFileOrFolder(string path)
        {
            bool is_file = true;
            if (path == null || path == String.Empty)
            {
                return ("", false);
            }
            else 
            {
                if (File.Exists(path))
                {

                }
                else if (Directory.Exists(path))
                {
                    is_file = false;
                }
            }
            return (path, is_file);
        }

        public enum LocalDirectories { AIRPORTS, LOGOS, PLANES, USER, APP }

        /// <summary>
        /// Get the real path of certain built-in directories.
        /// </summary>
        /// <param name="directories"></param>
        /// <returns></returns>
        public static string GetDirectoryName(LocalDirectories directories)
        {
            string dir = "";
            switch (directories)
            {
                case LocalDirectories.AIRPORTS:
                    dir = Application.StartupPath + "/FlightDB/Airports/";
                    break;
                case LocalDirectories.LOGOS:
                    dir = Application.StartupPath + "/FlightDB/Logos/";
                    break;
                case LocalDirectories.PLANES:
                    dir = Application.StartupPath + "/FlightDB/Planes/";
                    break;
                case LocalDirectories.USER:
                    dir = Properties.Settings.Default.ImageFolder;
                    break;
                case LocalDirectories.APP:
                    dir = Application.StartupPath;
                    break;
                default:
                    break;
            }
            return dir;
        }

        /// <summary>
        /// Test if a path contains a reference for a built-in directory.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string TestFileOrFolderPath(string path)
        {
            if (File.Exists(path))
            {
                return path;
            }
            string dir = "";
            if (path.StartsWith("<airports>"))
            {
                dir = GetDirectoryName(LocalDirectories.AIRPORTS);
                path.Replace("<airports>", dir);
            }
            if (path.StartsWith("<logo>"))
            {
                dir = GetDirectoryName(LocalDirectories.LOGOS);
                path.Replace("<logo>", dir);
            }
            else if (path.StartsWith("<plane>"))
            {
                dir = GetDirectoryName(LocalDirectories.PLANES);
                path.Replace("<plane>", dir);
            }
            else if (path.StartsWith("<user>"))
            {
                dir = GetDirectoryName(LocalDirectories.USER);
                path.Replace("<user>", dir);
            }
            else if (path.StartsWith("<app>"))
            {
                dir = GetDirectoryName(LocalDirectories.APP);
                path.Replace("<app>", dir);
            }
            else if (path.StartsWith("<help>"))
            {
                dir = GetDirectoryName(LocalDirectories.APP) + "HelpPages/";
                path.Replace("<help>", dir);
            }
            if (!Directory.Exists(path))
            {
                path = "";
            }
            return path;
        }

        /// <summary>
        /// Open a file in the internal ImageViewer or a folder in explorer.exe.
        /// </summary>
        /// <param name="path"></param>
        public static void OpenFileOrFolder(string path)
        {
            path = TestFileOrFolderPath(path);
            (string s, bool b) = GetFileOrFolder(path);
            if (b)
            {
                ImageViewerForm imageViewerForm = new ImageViewerForm();
                imageViewerForm.Filename = s;
                imageViewerForm.Show();
            }
            else
            {
                if (path != String.Empty)
                    Process.Start(new ProcessStartInfo(path));
            }
        }

        /// <summary>
        /// Create a thumbnail in the same location as the original image (if thumbnail doesn't exist).
        /// </summary>
        /// <param name="path"></param>
        public static void CreateThumbnail(string path)
        {
            int max_size = 350;     // size of thumbnails with setting "large"
            if (!ThumbnailExists(path))
            {
                Bitmap img = new Bitmap(path);
                img = ScaleImage(img, max_size, max_size);
                img.Save(GetThumbnailFilename(path));
                img.Dispose();
            }
        }

        /// <summary>
        /// Scale image correctly.
        /// https://efundies.com/scale-an-image-in-c-sharp-preserving-aspect-ratio/
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        static Bitmap ScaleImage(Bitmap bmp, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / bmp.Width;
            var ratioY = (double)maxHeight / bmp.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(bmp.Width * ratio);
            var newHeight = (int)(bmp.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                graphics.DrawImage(bmp, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        /// <summary>
        /// Check if a thumbnail already exists (contains "_tn").
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool ThumbnailExists(string path)
        {
            bool exists = false;
            string tn_filename = GetThumbnailFilename(path);
            if (tn_filename == "") { exists = true; }
            FileInfo fileInfo = new FileInfo(tn_filename);
            if (fileInfo.Exists)
            {
                exists = true;
            }
            return exists;
        }

        /// <summary>
        /// Get thumbnail filename for a given file: directory\filename + "_tn" + extension
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Empty string for directories or already existing thumbnails, otherwise TN filename.</returns>
        public static string GetThumbnailFilename(string path)
        {
            string filename = "";
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                // If "path" is already a thumbnail, return
                if (fileInfo.Name.Contains("_tn"))
                {
                    return filename;
                }
                string fn = Path.GetFileNameWithoutExtension(fileInfo.Name);
                filename = Path.GetDirectoryName(path) + "\\" + fn + "_tn" + fileInfo.Extension;
            }
            return filename;
        }
        #endregion

        /// <summary>
        /// Use for e.g. to load logos of plane manufacturers in FlightDB.
        /// Can use internal directories, switches visibility of the picture box if no image is given.
        /// </summary>
        /// <param name="pb"></param>
        /// <param name="image"></param>
        public static void ShowImageInPicureBox(PictureBox pb, string image)
        {
            if (image == null || image == String.Empty || !File.Exists(image))
            {
                pb.Visible = false;
            }
            else
            {
                pb.Image = Image.FromFile(TestFileOrFolderPath(image)); 
                pb.Visible = true;
            }
        }

        /// <summary>
        /// http://www.csharphelper.com/howtos/howto_file_size_in_words.html
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToFileSize(/*this*/ double value)
        {
            string[] suffixes = { "bytes", "KB", "MB", "GB",
                "TB", "PB", "EB", "ZB", "YB"};
            for (int i = 0; i < suffixes.Length; i++)
            {
                if (value <= (Math.Pow(1024, i + 1)))
                {
                    return ThreeNonZeroDigits(value /
                        Math.Pow(1024, i)) +
                        " " + suffixes[i];
                }
            }

            return ThreeNonZeroDigits(value /
                Math.Pow(1024, suffixes.Length - 1)) +
                " " + suffixes[suffixes.Length - 1];
        }

        /// <summary>
        /// Return the value formatted to include at most three
        /// non-zero digits and at most two digits after the decimal point. 
        /// Examples:
        ///         1
        ///       123
        ///        12.3 
        ///         1.23 
        ///         0.12 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ThreeNonZeroDigits(double value)
        {
            if (value >= 100)
            {
                // No digits after the decimal.
                return value.ToString("0,0");
            }
            else if (value >= 10)
            {
                // One digit after the decimal.
                return value.ToString("0.0");
            }
            else
            {
                // Two digits after the decimal.
                return value.ToString("0.00");
            }
        }
    }
}
