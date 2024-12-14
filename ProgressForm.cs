using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using ExcelLibrary.SpreadSheet;     //https://code.google.com/archive/p/excellibrary/
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class ProgressForm : Form
    {
        #region Private variables
        private string fileName;
        private string saveFolder;
        private Workbook workbook;
        private JobType jobType;
        private string[] tables;
        private Array array = new int[12];
        private JobStatus status;

        int cnt_tables = 0;
        int cnt_rows = 0;
        int tmp = 0;
        int percent = 0;
        int current_table = 0;

        string version = "";
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProgressForm()
        {
            InitializeComponent();
            okButton.Enabled = false;
            Version = "1.0.0.0";
        }

        #region Properties / Public variables
        public Array Checkboxes = new bool[12];
        public string FileName { get => fileName; set => fileName = value; }
        public string SaveFolder { get => saveFolder; set => saveFolder = value; }
        internal JobType JobType { get => jobType; set => jobType = value; }
        internal JobStatus Status { get => status; set => status = value; }
        public string Version { get => version; set => version = value; }
        #endregion

        private void exImportWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            
            if (JobType == JobType.EXPORT)
            {
                workbook = new Workbook();

                Worksheet ws_ver = new Worksheet("Version");
                ws_ver.Cells[0, 0] = new Cell("Don't change this worksheet or the import might fail.");
                ws_ver.Cells[1, 0] = new Cell("Version");
                ws_ver.Cells[2, 0] = new Cell(version);
                ws_ver.Cells[3, 0] = new Cell("Tables changed:");
                ws_ver.Cells[4, 0] = new Cell("None - original version");
                ws_ver.Cells[6, 0] = new Cell("Created:");
                ws_ver.Cells[7, 0] = new Cell(DateTime.Now, @"hh:mm:ss DD-MMM-YYYY");
                workbook.Worksheets.Add(ws_ver);

                for (int i = 0; i < cnt_tables; i++)
                {
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        switch (i)
                        {
                            case 0:
                                if ((bool)Checkboxes.GetValue(i))
                                {
                                    Worksheet ws_tour = new Worksheet("Tour");

                                    // Table name
                                    ws_tour.Cells[0, 0] = new Cell("Tabelle: Tour");
                                    // Column names
                                    ws_tour.Cells[1, 0] = new Cell("Id");
                                    ws_tour.Cells[1, 1] = new Cell("Date");
                                    ws_tour.Cells[1, 2] = new Cell("Route");
                                    ws_tour.Cells[1, 3] = new Cell("Vehicle");
                                    ws_tour.Cells[1, 4] = new Cell("Km");
                                    ws_tour.Cells[1, 5] = new Cell("Time");
                                    ws_tour.Cells[1, 6] = new Cell("AverageSpeed");
                                    ws_tour.Cells[1, 7] = new Cell("MaxSpeed");
                                    ws_tour.Cells[1, 8] = new Cell("AccumulatedHeight");
                                    ws_tour.Cells[1, 9] = new Cell("MaxAltitude");
                                    ws_tour.Cells[1, 10] = new Cell("Remark");
                                    ws_tour.Cells[1, 11] = new Cell("Persons");
                                    ws_tour.Cells[1, 12] = new Cell("Created");
                                    ws_tour.Cells[1, 13] = new Cell("LastChanged");
                                    ws_tour.Cells[1, 14] = new Cell("User");

                                    if ((int)array.GetValue(i) != 0)    // Table is not empty
                                    {
                                        SqlConnection con1;
                                        int j = 2;
                                        try
                                        {
                                            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                            {
                                                con1.Open();
                                                using (SqlCommand com1 = new SqlCommand())
                                                {
                                                    com1.CommandText = $"SELECT * FROM Tour WHERE [User] = " + Properties.Settings.Default.CurrentUserID; 
                                                    com1.CommandType = CommandType.Text;
                                                    com1.Connection = con1;
                                                    using (SqlDataReader reader1 = com1.ExecuteReader())
                                                    {
                                                        while (reader1.Read())
                                                        {
                                                            ws_tour.Cells[j, 0] = new Cell(reader1[0].ToString());
                                                            ws_tour.Cells[j, 1] = new Cell(reader1[1].ToString());
                                                            ws_tour.Cells[j, 2] = new Cell(reader1[2].ToString());
                                                            ws_tour.Cells[j, 3] = new Cell(reader1[3].ToString());
                                                            ws_tour.Cells[j, 4] = new Cell(reader1[4].ToString());
                                                            ws_tour.Cells[j, 5] = new Cell(reader1[5].ToString());
                                                            ws_tour.Cells[j, 6] = new Cell(reader1[6].ToString());
                                                            ws_tour.Cells[j, 7] = new Cell(reader1[7].ToString());
                                                            ws_tour.Cells[j, 8] = new Cell(reader1[8].ToString());
                                                            ws_tour.Cells[j, 9] = new Cell(reader1[9].ToString());
                                                            ws_tour.Cells[j, 10] = new Cell(reader1[10].ToString());
                                                            ws_tour.Cells[j, 11] = new Cell(reader1[11].ToString());
                                                            ws_tour.Cells[j, 12] = new Cell(ReturnNowIfNull(reader1[12].ToString()));
                                                            ws_tour.Cells[j, 13] = new Cell(ReturnNowIfNull(reader1[12].ToString()));
                                                            ws_tour.Cells[j, 14] = new Cell(AdminIfNull(reader1[14].ToString()));
                                                            j++;
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
                                    else ws_tour.Cells[2, 0] = new Cell("Tabelle enthält keine Daten! Diese Zelle kann gelöscht werden, um manuell Daten einzugeben.");
                                    workbook.Worksheets.Add(ws_tour);
                                    percent += (int)array.GetValue(i) / cnt_rows;
                                    worker.ReportProgress(percent);
                                }
                                break;
                            case 1:
                                if ((bool)Checkboxes.GetValue(i))
                                {
                                    Worksheet ws_countries = new Worksheet("Countries");
                                    // Table name
                                    ws_countries.Cells[0, 0] = new Cell("Tabelle: Countries");
                                    // Column names
                                    ws_countries.Cells[1, 0] = new Cell("Id");
                                    ws_countries.Cells[1, 1] = new Cell("Country");
                                    ws_countries.Cells[1, 2] = new Cell("Iso3166");
                                    ws_countries.Cells[1, 3] = new Cell("Phone");
                                    ws_countries.Cells[1, 4] = new Cell("Continent");
                                    ws_countries.Cells[1, 5] = new Cell("Created");
                                    ws_countries.Cells[1, 6] = new Cell("LastChanged");
                                    ws_countries.Cells[1, 7] = new Cell("User");

                                    if ((int)array.GetValue(i) != 0)    // Table is not empty
                                    {
                                        SqlConnection con1;
                                        int j = 2;
                                        try
                                        {
                                            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                            {
                                                con1.Open();
                                                using (SqlCommand com1 = new SqlCommand())
                                                {
                                                    com1.CommandText = $"SELECT * FROM Countries WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                                                    com1.CommandType = CommandType.Text;
                                                    com1.Connection = con1;
                                                    using (SqlDataReader reader1 = com1.ExecuteReader())
                                                    {
                                                        while (reader1.Read())
                                                        {
                                                            ws_countries.Cells[j, 0] = new Cell(reader1[0].ToString());
                                                            ws_countries.Cells[j, 1] = new Cell(reader1[1].ToString());
                                                            ws_countries.Cells[j, 2] = new Cell(reader1[2].ToString());
                                                            ws_countries.Cells[j, 3] = new Cell(reader1[3].ToString());
                                                            ws_countries.Cells[j, 4] = new Cell(reader1[4].ToString());
                                                            ws_countries.Cells[j, 5] = new Cell(ReturnNowIfNull(reader1[5].ToString()));
                                                            ws_countries.Cells[j, 6] = new Cell(ReturnNowIfNull(reader1[6].ToString()));
                                                            ws_countries.Cells[j, 7] = new Cell(AdminIfNull(reader1[7].ToString()));
                                                            j++;
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
                                    else ws_countries.Cells[2, 0] = new Cell("Tabelle enthält keine Daten! Diese Zelle kann gelöscht werden, um manuell Daten einzugeben.");

                                    workbook.Worksheets.Add(ws_countries);
                                    percent += (int)array.GetValue(i) / cnt_rows;
                                    worker.ReportProgress(percent);
                                }
                                break;
                            case 2:
                                if ((bool)Checkboxes.GetValue(i))
                                {
                                    Worksheet ws_cities = new Worksheet("Cities");
                                    // Table name
                                    ws_cities.Cells[0, 0] = new Cell("Tabelle: Cities");
                                    // Column names
                                    ws_cities.Cells[1, 0] = new Cell("Id");
                                    ws_cities.Cells[1, 1] = new Cell("CityName");
                                    ws_cities.Cells[1, 2] = new Cell("Country");
                                    ws_cities.Cells[1, 3] = new Cell("Bundesland");
                                    ws_cities.Cells[1, 4] = new Cell("CityPrefix");
                                    ws_cities.Cells[1, 5] = new Cell("Link");
                                    ws_cities.Cells[1, 6] = new Cell("Kfz");
                                    ws_cities.Cells[1, 7] = new Cell("Height");
                                    ws_cities.Cells[1, 8] = new Cell("Remark");
                                    ws_cities.Cells[1, 9] = new Cell("Image");
                                    ws_cities.Cells[1, 10] = new Cell("Gps");
                                    ws_cities.Cells[1, 11] = new Cell("Created");
                                    ws_cities.Cells[1, 12] = new Cell("LastChanged");
                                    ws_cities.Cells[1, 13] = new Cell("User");

                                    if ((int)array.GetValue(i) != 0)    // Table is not empty
                                    {
                                        SqlConnection con1;
                                        int j = 2;
                                        try
                                        {
                                            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                            {
                                                con1.Open();
                                                using (SqlCommand com1 = new SqlCommand())
                                                {
                                                    com1.CommandText = $"SELECT * FROM Cities WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                                                    com1.CommandType = CommandType.Text;
                                                    com1.Connection = con1;
                                                    using (SqlDataReader reader1 = com1.ExecuteReader())
                                                    {
                                                        while (reader1.Read())
                                                        {
                                                            ws_cities.Cells[j, 0] = new Cell(reader1[0].ToString());
                                                            ws_cities.Cells[j, 1] = new Cell(reader1[1].ToString());
                                                            ws_cities.Cells[j, 2] = new Cell(reader1[2].ToString());
                                                            ws_cities.Cells[j, 3] = new Cell(reader1[3].ToString());
                                                            ws_cities.Cells[j, 4] = new Cell(reader1[4].ToString());
                                                            ws_cities.Cells[j, 5] = new Cell(reader1[5].ToString());
                                                            ws_cities.Cells[j, 6] = new Cell(reader1[6].ToString());
                                                            ws_cities.Cells[j, 7] = new Cell(reader1[7].ToString());
                                                            ws_cities.Cells[j, 8] = new Cell(reader1[8].ToString());
                                                            ws_cities.Cells[j, 9] = new Cell(reader1[9].ToString());
                                                            ws_cities.Cells[j, 10] = new Cell(reader1[10].ToString());
                                                            ws_cities.Cells[j, 11] = new Cell(ReturnNowIfNull(reader1[11].ToString()));
                                                            ws_cities.Cells[j, 12] = new Cell(ReturnNowIfNull(reader1[12].ToString()));
                                                            ws_cities.Cells[j, 13] = new Cell(AdminIfNull(reader1[13].ToString()));
                                                            j++;
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
                                    else ws_cities.Cells[2, 0] = new Cell("Tabelle enthält keine Daten! Diese Zelle kann gelöscht werden, um manuell Daten einzugeben.");

                                    workbook.Worksheets.Add(ws_cities);
                                    percent += (int)array.GetValue(i) / cnt_rows;
                                    worker.ReportProgress(percent);
                                }
                                break;
                            case 3:
                                if ((bool)Checkboxes.GetValue(i))
                                {
                                    Worksheet ws_routes = new Worksheet("Routes");
                                    // Table name
                                    ws_routes.Cells[0, 0] = new Cell("Tabelle: Routes");
                                    // Column names
                                    ws_routes.Cells[1, 0] = new Cell("Id");
                                    ws_routes.Cells[1, 1] = new Cell("RouteName");
                                    ws_routes.Cells[1, 2] = new Cell("City");
                                    ws_routes.Cells[1, 3] = new Cell("CityStart");
                                    ws_routes.Cells[1, 4] = new Cell("CityEnd");
                                    ws_routes.Cells[1, 5] = new Cell("Cities");
                                    ws_routes.Cells[1, 6] = new Cell("RouteType");
                                    ws_routes.Cells[1, 7] = new Cell("MaxAlt");
                                    ws_routes.Cells[1, 8] = new Cell("Altitude");
                                    ws_routes.Cells[1, 9] = new Cell("Remarks");
                                    ws_routes.Cells[1, 10] = new Cell("AltProfile");
                                    ws_routes.Cells[1, 11] = new Cell("Image");
                                    ws_routes.Cells[1, 12] = new Cell("Created");
                                    ws_routes.Cells[1, 13] = new Cell("LastChanged");
                                    ws_routes.Cells[1, 14] = new Cell("User");

                                    if ((int)array.GetValue(i) != 0)    // Table is not empty
                                    {
                                        SqlConnection con1;
                                        int j = 2;
                                        try
                                        {
                                            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                            {
                                                con1.Open();
                                                using (SqlCommand com1 = new SqlCommand())
                                                {
                                                    com1.CommandText = $"SELECT * FROM Routes WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                                                    com1.CommandType = CommandType.Text;
                                                    com1.Connection = con1;
                                                    using (SqlDataReader reader1 = com1.ExecuteReader())
                                                    {
                                                        while (reader1.Read())
                                                        {
                                                            ws_routes.Cells[j, 0] = new Cell(reader1[0].ToString());
                                                            ws_routes.Cells[j, 1] = new Cell(reader1[1].ToString());
                                                            ws_routes.Cells[j, 2] = new Cell(reader1[2].ToString());
                                                            ws_routes.Cells[j, 3] = new Cell(reader1[3].ToString());
                                                            ws_routes.Cells[j, 4] = new Cell(reader1[4].ToString());
                                                            ws_routes.Cells[j, 5] = new Cell(reader1[5].ToString());
                                                            ws_routes.Cells[j, 6] = new Cell(reader1[6].ToString());
                                                            ws_routes.Cells[j, 7] = new Cell(reader1[7].ToString());
                                                            ws_routes.Cells[j, 8] = new Cell(reader1[8].ToString());
                                                            ws_routes.Cells[j, 9] = new Cell(reader1[9].ToString());
                                                            ws_routes.Cells[j, 10] = new Cell(reader1[10].ToString());
                                                            ws_routes.Cells[j, 11] = new Cell(reader1[11].ToString());
                                                            ws_routes.Cells[j, 12] = new Cell(ReturnNowIfNull(reader1[12].ToString()));
                                                            ws_routes.Cells[j, 13] = new Cell(ReturnNowIfNull(reader1[13].ToString()));
                                                            ws_routes.Cells[j, 14] = new Cell(AdminIfNull(reader1[14].ToString()));
                                                            j++;
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
                                    else ws_routes.Cells[2, 0] = new Cell("Tabelle enthält keine Daten! Diese Zelle kann gelöscht werden, um manuell Daten einzugeben.");

                                    workbook.Worksheets.Add(ws_routes);
                                    percent += (int)array.GetValue(i) / cnt_rows;
                                    worker.ReportProgress(percent);
                                }
                                break;
                            case 4:
                                if ((bool)Checkboxes.GetValue(i))
                                {
                                    Worksheet ws_vehicles = new Worksheet("Vehicles");
                                    // Table name
                                    ws_vehicles.Cells[0, 0] = new Cell("Tabelle: Vehicles");
                                    // Column names
                                    ws_vehicles.Cells[1, 0] = new Cell("Id");
                                    ws_vehicles.Cells[1, 1] = new Cell("VehicleName");
                                    ws_vehicles.Cells[1, 2] = new Cell("Manufacturer");
                                    ws_vehicles.Cells[1, 3] = new Cell("VehicleType");
                                    ws_vehicles.Cells[1, 4] = new Cell("BoughtOn");
                                    ws_vehicles.Cells[1, 5] = new Cell("BuildYear");
                                    ws_vehicles.Cells[1, 6] = new Cell("Price");
                                    ws_vehicles.Cells[1, 7] = new Cell("Equipment");
                                    ws_vehicles.Cells[1, 8] = new Cell("Image");
                                    ws_vehicles.Cells[1, 9] = new Cell("Entfaltung");
                                    ws_vehicles.Cells[1, 10] = new Cell("Created");
                                    ws_vehicles.Cells[1, 11] = new Cell("LastChanged");
                                    ws_vehicles.Cells[1, 12] = new Cell("User");
                                    ws_vehicles.Cells[1, 13] = new Cell("LicensePlate");

                                    if ((int)array.GetValue(i) != 0)    // Table is not empty
                                    {
                                        SqlConnection con1;
                                        int j = 2;
                                        try
                                        {
                                            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                            {
                                                con1.Open();
                                                using (SqlCommand com1 = new SqlCommand())
                                                {
                                                    com1.CommandText = $"SELECT * FROM Vehicles WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                                                    com1.CommandType = CommandType.Text;
                                                    com1.Connection = con1;
                                                    using (SqlDataReader reader1 = com1.ExecuteReader())
                                                    {
                                                        while (reader1.Read())
                                                        {
                                                            ws_vehicles.Cells[j, 0] = new Cell(reader1[0].ToString());
                                                            ws_vehicles.Cells[j, 1] = new Cell(reader1[1].ToString());
                                                            ws_vehicles.Cells[j, 2] = new Cell(reader1[2].ToString());
                                                            ws_vehicles.Cells[j, 3] = new Cell(reader1[3].ToString());
                                                            ws_vehicles.Cells[j, 4] = new Cell(reader1[4].ToString());
                                                            ws_vehicles.Cells[j, 5] = new Cell(reader1[5].ToString());
                                                            ws_vehicles.Cells[j, 6] = new Cell(reader1[6].ToString());
                                                            ws_vehicles.Cells[j, 7] = new Cell(reader1[7].ToString());
                                                            ws_vehicles.Cells[j, 8] = new Cell(reader1[8].ToString());
                                                            ws_vehicles.Cells[j, 9] = new Cell(reader1[9].ToString());
                                                            ws_vehicles.Cells[j, 10] = new Cell(ReturnNowIfNull(reader1[10].ToString()));
                                                            ws_vehicles.Cells[j, 11] = new Cell(ReturnNowIfNull(reader1[11].ToString()));
                                                            ws_vehicles.Cells[j, 12] = new Cell(AdminIfNull(reader1[12].ToString()));
                                                            ws_vehicles.Cells[j, 13] = new Cell(reader1[13].ToString());
                                                            j++;
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
                                    else ws_vehicles.Cells[2, 0] = new Cell("Tabelle enthält keine Daten! Diese Zelle kann gelöscht werden, um manuell Daten einzugeben.");

                                    workbook.Worksheets.Add(ws_vehicles);
                                    percent += (int)array.GetValue(i) / cnt_rows;
                                    worker.ReportProgress(percent);
                                }
                                break;
                            case 5:
                                if ((bool)Checkboxes.GetValue(i))
                                {
                                    Worksheet ws_companies = new Worksheet("Companies");
                                    // Table name
                                    ws_companies.Cells[0, 0] = new Cell("Tabelle: Companies");
                                    // Column names
                                    ws_companies.Cells[1, 0] = new Cell("Id");
                                    ws_companies.Cells[1, 1] = new Cell("CompanyName");
                                    ws_companies.Cells[1, 2] = new Cell("Link");
                                    ws_companies.Cells[1, 3] = new Cell("Created");
                                    ws_companies.Cells[1, 4] = new Cell("LastChanged");
                                    ws_companies.Cells[1, 5] = new Cell("User");

                                    if ((int)array.GetValue(i) != 0)    // Table is not empty
                                    {
                                        SqlConnection con1;
                                        int j = 2;
                                        try
                                        {
                                            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                            {
                                                con1.Open();
                                                using (SqlCommand com1 = new SqlCommand())
                                                {
                                                    com1.CommandText = $"SELECT * FROM Companies WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                                                    com1.CommandType = CommandType.Text;
                                                    com1.Connection = con1;
                                                    using (SqlDataReader reader1 = com1.ExecuteReader())
                                                    {
                                                        while (reader1.Read())
                                                        {
                                                            ws_companies.Cells[j, 0] = new Cell(reader1[0].ToString());
                                                            ws_companies.Cells[j, 1] = new Cell(reader1[1].ToString());
                                                            ws_companies.Cells[j, 2] = new Cell(reader1[2].ToString());
                                                            ws_companies.Cells[j, 3] = new Cell(ReturnNowIfNull(reader1[3].ToString()));
                                                            ws_companies.Cells[j, 4] = new Cell(ReturnNowIfNull(reader1[4].ToString()));
                                                            ws_companies.Cells[j, 5] = new Cell(AdminIfNull(reader1[5].ToString()));
                                                            j++;
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
                                    else ws_companies.Cells[2, 0] = new Cell("Tabelle enthält keine Daten! Diese Zelle kann gelöscht werden, um manuell Daten einzugeben.");

                                    workbook.Worksheets.Add(ws_companies);
                                    percent += (int)array.GetValue(i) / cnt_rows;
                                    worker.ReportProgress(percent);
                                }
                                break;
                            case 6:
                                if ((bool)Checkboxes.GetValue(i))
                                {
                                    Worksheet ws_entfaltung = new Worksheet("Entfaltung");
                                    // Table name
                                    ws_entfaltung.Cells[0, 0] = new Cell("Tabelle: Entfaltung");
                                    // Column names
                                    ws_entfaltung.Cells[1, 0] = new Cell("Id");
                                    ws_entfaltung.Cells[1, 1] = new Cell("BikeId");
                                    ws_entfaltung.Cells[1, 2] = new Cell("Front");
                                    ws_entfaltung.Cells[1, 3] = new Cell("Back");
                                    ws_entfaltung.Cells[1, 4] = new Cell("Wheel");
                                    ws_entfaltung.Cells[1, 5] = new Cell("Unit");
                                    ws_entfaltung.Cells[1, 6] = new Cell("Created");
                                    ws_entfaltung.Cells[1, 7] = new Cell("LastChanged");
                                    ws_entfaltung.Cells[1, 8] = new Cell("User");

                                    if ((int)array.GetValue(i) != 0)    // Table is not empty
                                    {
                                        SqlConnection con1;
                                        int j = 2;
                                        try
                                        {
                                            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                            {
                                                con1.Open();
                                                using (SqlCommand com1 = new SqlCommand())
                                                {
                                                    com1.CommandText = $"SELECT * FROM Entfaltung WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                                                    com1.CommandType = CommandType.Text;
                                                    com1.Connection = con1;
                                                    using (SqlDataReader reader1 = com1.ExecuteReader())
                                                    {
                                                        while (reader1.Read())
                                                        {
                                                            ws_entfaltung.Cells[j, 0] = new Cell(reader1[0].ToString());
                                                            ws_entfaltung.Cells[j, 1] = new Cell(reader1[1].ToString());
                                                            ws_entfaltung.Cells[j, 2] = new Cell(reader1[2].ToString());
                                                            ws_entfaltung.Cells[j, 3] = new Cell(reader1[3].ToString());
                                                            ws_entfaltung.Cells[j, 4] = new Cell(reader1[4].ToString());
                                                            ws_entfaltung.Cells[j, 5] = new Cell(reader1[5].ToString());
                                                            ws_entfaltung.Cells[j, 6] = new Cell(ReturnNowIfNull(reader1[6].ToString()));
                                                            ws_entfaltung.Cells[j, 7] = new Cell(ReturnNowIfNull(reader1[7].ToString()));
                                                            ws_entfaltung.Cells[j, 8] = new Cell(AdminIfNull(reader1[8].ToString()));
                                                            j++;
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
                                    else ws_entfaltung.Cells[2, 0] = new Cell("Tabelle enthält keine Daten! Diese Zelle kann gelöscht werden, um manuell Daten einzugeben.");

                                    workbook.Worksheets.Add(ws_entfaltung);
                                    percent += (int)array.GetValue(i) / cnt_rows;
                                    worker.ReportProgress(percent);
                                }
                                break;
                            case 7:     
                                if ((bool)Checkboxes.GetValue(i))
                                {
                                    Worksheet ws_vectype = new Worksheet("VehicleTypes");
                                    // Table name
                                    ws_vectype.Cells[0, 0] = new Cell("Tabelle: VehicleTypes");
                                    // Column names
                                    ws_vectype.Cells[1, 0] = new Cell("Id");
                                    ws_vectype.Cells[1, 1] = new Cell("VehicleType");
                                    ws_vectype.Cells[1, 2] = new Cell("Created");
                                    ws_vectype.Cells[1, 3] = new Cell("LastChanged");
                                    ws_vectype.Cells[1, 4] = new Cell("User");

                                    if ((int)array.GetValue(i) != 0)    // Table is not empty
                                    {
                                        SqlConnection con1;
                                        int j = 2;
                                        try
                                        {
                                            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                            {
                                                con1.Open();
                                                using (SqlCommand com1 = new SqlCommand())
                                                {
                                                    com1.CommandText = $"SELECT * FROM VehicleTypes WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                                                    com1.CommandType = CommandType.Text;
                                                    com1.Connection = con1;
                                                    using (SqlDataReader reader1 = com1.ExecuteReader())
                                                    {
                                                        while (reader1.Read())
                                                        {
                                                            ws_vectype.Cells[j, 0] = new Cell(reader1[0].ToString());
                                                            ws_vectype.Cells[j, 1] = new Cell(reader1[1].ToString());
                                                            ws_vectype.Cells[j, 2] = new Cell(ReturnNowIfNull(reader1[2].ToString()));
                                                            ws_vectype.Cells[j, 3] = new Cell(ReturnNowIfNull(reader1[3].ToString()));
                                                            ws_vectype.Cells[j, 4] = new Cell(AdminIfNull(reader1[4].ToString()));
                                                            j++;
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
                                    else ws_vectype.Cells[2, 0] = new Cell("Tabelle enthält keine Daten! Diese Zelle kann gelöscht werden, um manuell Daten einzugeben.");

                                    workbook.Worksheets.Add(ws_vectype);
                                    percent += (int)array.GetValue(i) / cnt_rows;
                                    worker.ReportProgress(percent);
                                }
                                break;
                            case 8:     
                                if ((bool)Checkboxes.GetValue(i))
                                {
                                    Worksheet ws_routetype = new Worksheet("RouteTypes");
                                    // Table name
                                    ws_routetype.Cells[0, 0] = new Cell("Tabelle: RouteTypes");
                                    // Column names
                                    ws_routetype.Cells[1, 0] = new Cell("Id");
                                    ws_routetype.Cells[1, 1] = new Cell("RouteType");
                                    ws_routetype.Cells[1, 2] = new Cell("Created");
                                    ws_routetype.Cells[1, 3] = new Cell("LastChanged");
                                    ws_routetype.Cells[1, 4] = new Cell("User");

                                    if ((int)array.GetValue(i) != 0)    // Table is not empty
                                    {
                                        SqlConnection con1;
                                        int j = 2;
                                        try
                                        {
                                            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                            {
                                                con1.Open();
                                                using (SqlCommand com1 = new SqlCommand())
                                                {
                                                    com1.CommandText = $"SELECT * FROM RouteTypes WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                                                    com1.CommandType = CommandType.Text;
                                                    com1.Connection = con1;
                                                    using (SqlDataReader reader1 = com1.ExecuteReader())
                                                    {
                                                        while (reader1.Read())
                                                        {
                                                            ws_routetype.Cells[j, 0] = new Cell(reader1[0].ToString());
                                                            ws_routetype.Cells[j, 1] = new Cell(reader1[1].ToString());
                                                            ws_routetype.Cells[j, 2] = new Cell(ReturnNowIfNull(reader1[2].ToString()));
                                                            ws_routetype.Cells[j, 3] = new Cell(ReturnNowIfNull(reader1[3].ToString()));
                                                            ws_routetype.Cells[j, 4] = new Cell(AdminIfNull(reader1[4].ToString()));
                                                            j++;
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
                                    else ws_routetype.Cells[2, 0] = new Cell("Tabelle enthält keine Daten! Diese Zelle kann gelöscht werden, um manuell Daten einzugeben.");

                                    workbook.Worksheets.Add(ws_routetype);
                                    percent += (int)array.GetValue(i) / cnt_rows;
                                    worker.ReportProgress(percent);
                                }
                                break;
                            case 9:
                                if ((bool)Checkboxes.GetValue(i))
                                {
                                    Worksheet ws_persons = new Worksheet("Persons");
                                    // Table name
                                    ws_persons.Cells[0, 0] = new Cell("Tabelle: Persons");
                                    // Column names
                                    ws_persons.Cells[1, 0] = new Cell("Id");
                                    ws_persons.Cells[1, 1] = new Cell("Username");
                                    ws_persons.Cells[1, 2] = new Cell("Lastname");
                                    ws_persons.Cells[1, 3] = new Cell("Name");
                                    ws_persons.Cells[1, 4] = new Cell("City");
                                    ws_persons.Cells[1, 5] = new Cell("Birthdate");
                                    ws_persons.Cells[1, 6] = new Cell("Phone");
                                    ws_persons.Cells[1, 7] = new Cell("Email");
                                    ws_persons.Cells[1, 8] = new Cell("PLZ");
                                    ws_persons.Cells[1, 9] = new Cell("Street1");
                                    ws_persons.Cells[1, 10] = new Cell("Street2");
                                    ws_persons.Cells[1, 11] = new Cell("Country");
                                    ws_persons.Cells[1, 12] = new Cell("Image");
                                    ws_persons.Cells[1, 13] = new Cell("Remark");
                                    ws_persons.Cells[1, 14] = new Cell("Created");
                                    ws_persons.Cells[1, 15] = new Cell("LastChanged");
                                    ws_persons.Cells[1, 16] = new Cell("User");
                                    ws_persons.Cells[1, 17] = new Cell("IsUser");
                                    ws_persons.Cells[1, 18] = new Cell("IsAdmin");
                                    ws_persons.Cells[1, 19] = new Cell("Password");                                   

                                    if ((int)array.GetValue(i) != 0)    // Table is not empty
                                    {
                                        SqlConnection con1;
                                        int j = 2;
                                        try
                                        {
                                            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                            {
                                                con1.Open();
                                                using (SqlCommand com1 = new SqlCommand())
                                                {
                                                    com1.CommandText = $"SELECT * FROM Persons WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                                                    com1.CommandType = CommandType.Text;
                                                    com1.Connection = con1;
                                                    using (SqlDataReader reader1 = com1.ExecuteReader())
                                                    {
                                                        while (reader1.Read())
                                                        {
                                                            ws_persons.Cells[j, 0] = new Cell(reader1[0].ToString());
                                                            ws_persons.Cells[j, 1] = new Cell(reader1[1].ToString());
                                                            ws_persons.Cells[j, 2] = new Cell(reader1[2].ToString());
                                                            ws_persons.Cells[j, 3] = new Cell(reader1[3].ToString());
                                                            ws_persons.Cells[j, 4] = new Cell(reader1[4].ToString());
                                                            ws_persons.Cells[j, 5] = new Cell(reader1[5].ToString());
                                                            ws_persons.Cells[j, 6] = new Cell(reader1[6].ToString());
                                                            ws_persons.Cells[j, 7] = new Cell(reader1[7].ToString());
                                                            ws_persons.Cells[j, 8] = new Cell(reader1[8].ToString());
                                                            ws_persons.Cells[j, 9] = new Cell(reader1[9].ToString());
                                                            ws_persons.Cells[j, 10] = new Cell(reader1[10].ToString());
                                                            ws_persons.Cells[j, 11] = new Cell(reader1[11].ToString());
                                                            ws_persons.Cells[j, 12] = new Cell(reader1[12].ToString());
                                                            ws_persons.Cells[j, 13] = new Cell(reader1[13].ToString());
                                                            ws_persons.Cells[j, 14] = new Cell(ReturnNowIfNull(reader1[14].ToString()));
                                                            ws_persons.Cells[j, 15] = new Cell(ReturnNowIfNull(reader1[15].ToString()));
                                                            ws_persons.Cells[j, 16] = new Cell(AdminIfNull(reader1[16].ToString()));
                                                            ws_persons.Cells[j, 17] = new Cell(reader1[17].ToString());
                                                            ws_persons.Cells[j, 18] = new Cell(reader1[18].ToString());
                                                            ws_persons.Cells[j, 19] = new Cell(reader1[19].ToString());
                                                            j++;
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
                                    else ws_persons.Cells[2, 0] = new Cell("Tabelle enthält keine Daten! Diese Zelle kann gelöscht werden, um manuell Daten einzugeben.");

                                    workbook.Worksheets.Add(ws_persons);
                                    percent += (int)array.GetValue(i) / cnt_rows;
                                    worker.ReportProgress(percent);
                                }
                                break;
                            case 10:
                                if ((bool)Checkboxes.GetValue(i))
                                {
                                    Worksheet ws_notes = new Worksheet("Notes");
                                    // Table name
                                    ws_notes.Cells[0, 0] = new Cell("Tabelle: Notes");
                                    // Column names
                                    ws_notes.Cells[1, 0] = new Cell("Id");
                                    ws_notes.Cells[1, 1] = new Cell("Title");
                                    ws_notes.Cells[1, 2] = new Cell("Remark");
                                    ws_notes.Cells[1, 3] = new Cell("Created");
                                    ws_notes.Cells[1, 4] = new Cell("LastChanged");
                                    ws_notes.Cells[1, 5] = new Cell("User");

                                    if ((int)array.GetValue(i) != 0)    // Table is not empty
                                    {
                                        SqlConnection con1;
                                        int j = 2;
                                        try
                                        {
                                            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                            {
                                                con1.Open();
                                                using (SqlCommand com1 = new SqlCommand())
                                                {
                                                    com1.CommandText = $"SELECT * FROM Notes WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                                                    com1.CommandType = CommandType.Text;
                                                    com1.Connection = con1;
                                                    using (SqlDataReader reader1 = com1.ExecuteReader())
                                                    {
                                                        while (reader1.Read())
                                                        {
                                                            ws_notes.Cells[j, 0] = new Cell(reader1[0].ToString());
                                                            ws_notes.Cells[j, 1] = new Cell(reader1[1].ToString());
                                                            ws_notes.Cells[j, 2] = new Cell(reader1[2].ToString());
                                                            ws_notes.Cells[j, 3] = new Cell(ReturnNowIfNull(reader1[3].ToString()));
                                                            ws_notes.Cells[j, 4] = new Cell(ReturnNowIfNull(reader1[4].ToString()));
                                                            ws_notes.Cells[j, 5] = new Cell(AdminIfNull(reader1[5].ToString()));
                                                            j++;
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
                                    else ws_notes.Cells[2, 0] = new Cell("Tabelle enthält keine Daten! Diese Zelle kann gelöscht werden, um manuell Daten einzugeben.");

                                    workbook.Worksheets.Add(ws_notes);
                                    percent += (int)array.GetValue(i) / cnt_rows;
                                    worker.ReportProgress(percent);
                                }
                                break;
                            case 11:
                                if ((bool)Checkboxes.GetValue(i))
                                {
                                    Worksheet ws_goals = new Worksheet("Goals");
                                    // Table name
                                    ws_goals.Cells[0, 0] = new Cell("Tabelle: Goals");
                                    // Column names
                                    ws_goals.Cells[1, 0] = new Cell("Id");
                                    ws_goals.Cells[1, 1] = new Cell("Title");
                                    ws_goals.Cells[1, 2] = new Cell("Remark");
                                    ws_goals.Cells[1, 3] = new Cell("Date");
                                    ws_goals.Cells[1, 4] = new Cell("Achieved");
                                    ws_goals.Cells[1, 5] = new Cell("Created");
                                    ws_goals.Cells[1, 6] = new Cell("LastChanged");
                                    ws_goals.Cells[1, 7] = new Cell("User");

                                    if ((int)array.GetValue(i) != 0)    // Table is not empty
                                    {
                                        SqlConnection con1;
                                        int j = 2;
                                        try
                                        {
                                            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                            {
                                                con1.Open();
                                                using (SqlCommand com1 = new SqlCommand())
                                                {
                                                    com1.CommandText = $"SELECT * FROM Goals WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                                                    com1.CommandType = CommandType.Text;
                                                    com1.Connection = con1;
                                                    using (SqlDataReader reader1 = com1.ExecuteReader())
                                                    {
                                                        while (reader1.Read())
                                                        {
                                                            ws_goals.Cells[j, 0] = new Cell(reader1[0].ToString());
                                                            ws_goals.Cells[j, 1] = new Cell(reader1[1].ToString());
                                                            ws_goals.Cells[j, 2] = new Cell(reader1[2].ToString());
                                                            ws_goals.Cells[j, 3] = new Cell(reader1[3].ToString());
                                                            ws_goals.Cells[j, 4] = new Cell(reader1[4].ToString());
                                                            ws_goals.Cells[j, 5] = new Cell(ReturnNowIfNull(reader1[5].ToString()));
                                                            ws_goals.Cells[j, 6] = new Cell(ReturnNowIfNull(reader1[6].ToString()));
                                                            ws_goals.Cells[j, 7] = new Cell(AdminIfNull(reader1[7].ToString()));
                                                            j++;
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
                                    else ws_goals.Cells[2, 0] = new Cell("Tabelle enthält keine Daten! Diese Zelle kann gelöscht werden, um manuell Daten einzugeben.");

                                    workbook.Worksheets.Add(ws_goals);
                                    percent += (int)array.GetValue(i) / cnt_rows;
                                    worker.ReportProgress(percent);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                /*worksheet.Cells[0, 1] = new Cell((short)1);
                worksheet.Cells[3, 3] = new Cell((decimal)3.45);
                worksheet.Cells[4, 0] = new Cell(32764.5, "#,##0.00");
                worksheet.Cells[5, 1] = new Cell(DateTime.Now, @"YYYY-MM-DD");
                worksheet.Cells.ColumnWidth[0, 1] = 3000;
                workbook.Worksheets.Add(worksheet);*/
            }
            else        //JobType.IMPORT, file is already validated
            {
                current_table = 0;
                percent = 0;
                Dictionary<int, string[]> result = new Dictionary<int, string[]>();
                SqlConnection myConnection;

                for (int i = 0; i < tables.Length; i++)
                {
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        switch (tables[current_table])
                        {
                            case "Tour":
                                if (status == JobStatus.APPEND_TOUR || status == JobStatus.APPEND_ALL)
                                {
                                    result = ReadSpreadsheet(tables[current_table], 11, CountIds(tables[current_table]));
                                }
                                else if (status == JobStatus.DROP_TOUR || status == JobStatus.DROP_ALL)
                                {
                                    ClearTable(tables[current_table]);
                                    result = ReadSpreadsheet(tables[current_table], 11);
                                }

                                try
                                {
                                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                    {
                                        myConnection.Open();
                                        for (int x = 0; x < result.Count; x++)
                                        {
                                            using (SqlCommand myCommand = new SqlCommand())
                                            {
                                                string sqlquery = "INSERT INTO " + tables[current_table] +
                                                " (Id, Date, Route, Vehicle, Km, Time, AverageSpeed, MaxSpeed, AccumulatedHeight, MaxAltitude, Remark, Persons, Created, LastChanged, User) " +
                                                "VALUES (@id, @Date, @Route, @Vehicle, @Km, @Time, @AvgSpeed, @MaxSpeed, @Height, @MaxAlt, @Remark @persons, @created, @last, @user)";
                                                myCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(result[x][0]);
                                                int maxAlt = 0;
                                                int alt = 0;
                                                if (result[x][8] != String.Empty)
                                                {
                                                    alt = Convert.ToInt32(result[x][8]);
                                                }
                                                if (result[x][9] != String.Empty)
                                                {
                                                    maxAlt = Convert.ToInt32(result[x][9]);
                                                }
                                                myCommand.Parameters.Add("@Date", SqlDbType.Date).Value = Convert.ToDateTime(result[x][1]);
                                                myCommand.Parameters.Add("@Route", SqlDbType.Int).Value = Convert.ToInt32(result[x][2]);
                                                myCommand.Parameters.Add("@Vehicle", SqlDbType.Int).Value = Convert.ToInt32(result[x][3]);
                                                myCommand.Parameters.Add("@Km", SqlDbType.Decimal).Value = Convert.ToDecimal(result[x][4]);
                                                myCommand.Parameters.Add("@Time", SqlDbType.Time).Value = DateTime.Parse(result[x][5], CultureInfo.InvariantCulture).TimeOfDay;
                                                myCommand.Parameters.Add("@AvgSpeed", SqlDbType.Decimal).Value = Convert.ToDecimal(result[x][6]);
                                                myCommand.Parameters.Add("@MaxSpeed", SqlDbType.Decimal).Value = Convert.ToDecimal(result[x][7]);
                                                myCommand.Parameters.Add("@Height", SqlDbType.Int).Value = alt;
                                                myCommand.Parameters.Add("@MaxAlt", SqlDbType.Int).Value = maxAlt;
                                                myCommand.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = result[x][10];
                                                myCommand.Parameters.Add("@persons", SqlDbType.NVarChar).Value = result[x][11];
                                                myCommand.Parameters.Add("@created", SqlDbType.Date).Value = Convert.ToDateTime(result[x][12]);
                                                myCommand.Parameters.Add("@last", SqlDbType.Date).Value = DateTime.Now;
                                                myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Convert.ToInt32(result[x][14]);
                                                myCommand.CommandText = sqlquery;
                                                myCommand.CommandType = CommandType.Text;
                                                myCommand.Connection = myConnection;
                                                myCommand.ExecuteNonQuery();
                                            }
                                        }
                                        myConnection.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ShowErrorMessage(ex.Message, "Fehler beim Einfügen von Daten in " + tables[current_table]);
                                }
                                break;
                            case "Countries":
                                if (status == JobStatus.APPEND_TOUR || status == JobStatus.DROP_TOUR)
                                {
                                    break;
                                }
                                else if (status == JobStatus.APPEND_ALL)
                                {
                                    result = ReadSpreadsheet(tables[current_table], 5, CountIds(tables[current_table]));
                                }
                                else if (status == JobStatus.DROP_ALL)
                                {
                                    ClearTable(tables[current_table]);
                                    result = ReadSpreadsheet(tables[current_table], 5);
                                }

                                try
                                {
                                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                    {
                                        myConnection.Open();
                                        for (int x = 0; x < result.Count; x++)
                                        {
                                            using (SqlCommand myCommand = new SqlCommand())
                                            {
                                                string sqlquery = "INSERT INTO " + tables[current_table] +
                                                " (Id, Country, Iso3166, Phone, Continent, Created, LastChanged, User) " +
                                                "VALUES (@id, @country, @iso3166, @phone, @continent, @created, @last, @user)";
                                                myCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(result[x][0]);
                                                myCommand.Parameters.Add("@country", SqlDbType.NVarChar).Value = result[x][1];
                                                myCommand.Parameters.Add("@iso3166", SqlDbType.NVarChar).Value = (result[x][2]);
                                                myCommand.Parameters.Add("@phone", SqlDbType.NVarChar).Value = result[x][3];
                                                myCommand.Parameters.Add("@continent", SqlDbType.Int).Value = Convert.ToInt32(result[x][4]);
                                                myCommand.Parameters.Add("@created", SqlDbType.Date).Value = Convert.ToDateTime(result[x][5]);
                                                myCommand.Parameters.Add("@last", SqlDbType.Date).Value = DateTime.Now;
                                                myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Convert.ToInt32(result[x][7]);
                                                myCommand.CommandText = sqlquery;
                                                myCommand.CommandType = CommandType.Text;
                                                myCommand.Connection = myConnection;
                                                myCommand.ExecuteNonQuery();
                                            }
                                        }
                                        myConnection.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ShowErrorMessage(ex.Message, "Fehler beim Einfügen von Daten in " + tables[current_table]);
                                }
                                break;
                            case "Cities":
                                if (status == JobStatus.APPEND_TOUR || status == JobStatus.DROP_TOUR)
                                {
                                    break;
                                }
                                else if (status == JobStatus.APPEND_ALL)
                                {
                                    result = ReadSpreadsheet(tables[current_table], 11, CountIds(tables[current_table]));
                                }
                                else if (status == JobStatus.DROP_ALL)
                                {
                                    ClearTable(tables[current_table]);
                                    result = ReadSpreadsheet(tables[current_table], 11);
                                }

                                try
                                {
                                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                    {
                                        myConnection.Open();
                                        for (int x = 0; x < result.Count; x++)
                                        {
                                            using (SqlCommand myCommand = new SqlCommand())
                                            {
                                                string sqlquery = "INSERT INTO " + tables[current_table] +
                                                " (Id, CityName, Country, Bundesland, CityPrefix, Link, Kfz, Height, Remark, Image, Gps, Created, LastChanged, User) " +
                                                "VALUES (@id, @CityName, @Country, @Bundesland, @CityPrefix, @Link, @Kfz, @Height, @Remark, @Image, @Gps, @created, @last, @user)";
                                                myCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(result[x][0]);
                                                myCommand.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = result[x][1];
                                                myCommand.Parameters.Add("@Country", SqlDbType.Int).Value = Convert.ToInt32(result[x][2]);
                                                myCommand.Parameters.Add("@Bundesland", SqlDbType.Int).Value = Convert.ToInt32(result[x][3]);
                                                myCommand.Parameters.Add("@CityPrefix", SqlDbType.NVarChar).Value = result[x][4];
                                                myCommand.Parameters.Add("@Link", SqlDbType.NVarChar).Value = result[x][5];
                                                myCommand.Parameters.Add("@Kfz", SqlDbType.NVarChar).Value = result[x][6];
                                                myCommand.Parameters.Add("@Height", SqlDbType.Int).Value = Convert.ToInt32(result[x][7]);
                                                myCommand.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = result[x][8];
                                                myCommand.Parameters.Add("@Image", SqlDbType.NVarChar).Value = result[x][9];
                                                myCommand.Parameters.Add("@Gps", SqlDbType.NVarChar).Value = result[x][10];
                                                myCommand.Parameters.Add("@created", SqlDbType.Date).Value = Convert.ToDateTime(result[x][11]);
                                                myCommand.Parameters.Add("@last", SqlDbType.Date).Value = DateTime.Now;
                                                myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Convert.ToInt32(result[x][13]);
                                                myCommand.CommandText = sqlquery;
                                                myCommand.CommandType = CommandType.Text;
                                                myCommand.Connection = myConnection;
                                                myCommand.ExecuteNonQuery();
                                            }
                                        }
                                        myConnection.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ShowErrorMessage(ex.Message, "Fehler beim Einfügen von Daten in " + tables[current_table]);
                                }
                                break;
                            case "Routes":
                                if (status == JobStatus.APPEND_TOUR || status == JobStatus.DROP_TOUR)
                                {
                                    break;
                                }
                                else if (status == JobStatus.APPEND_ALL)
                                {
                                    result = ReadSpreadsheet(tables[current_table], 12, CountIds(tables[current_table]));
                                }
                                else if (status == JobStatus.DROP_ALL)
                                {
                                    ClearTable(tables[current_table]);
                                    result = ReadSpreadsheet(tables[current_table], 12);
                                }

                                try
                                {
                                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                    {
                                        myConnection.Open();
                                        for (int x = 0; x < result.Count; x++)
                                        {
                                            using (SqlCommand myCommand = new SqlCommand())
                                            {
                                                string sqlquery = "INSERT INTO " + tables[current_table] +
                                                " (Id, RouteName, City, CityStart, CityEnd, Cities, RouteType, MaxAlt, Altitude, Remarks, AltProfile, Image, Created, LastChanged, User) " +
                                                "VALUES (@id, @RouteName, @City, @CityStart, @CityEnd, @Cities, @RouteType, @MaxAlt, @Altitude, @Remarks, @AltProfile, @Image, @created, @last, @user)";
                                                myCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(result[x][0]);
                                                myCommand.Parameters.Add("@RouteName", SqlDbType.NVarChar).Value = result[x][1];
                                                myCommand.Parameters.Add("@City", SqlDbType.Int).Value = Convert.ToInt32(result[x][2]);
                                                myCommand.Parameters.Add("@CityStart", SqlDbType.Int).Value = Convert.ToInt32(result[x][3]);
                                                myCommand.Parameters.Add("@CityEnd", SqlDbType.Int).Value = Convert.ToInt32(result[x][4]);
                                                myCommand.Parameters.Add("@Cities", SqlDbType.NVarChar).Value = result[x][5];
                                                myCommand.Parameters.Add("@RouteType", SqlDbType.Int).Value = Convert.ToInt32(result[x][6]);
                                                myCommand.Parameters.Add("@MaxAlt", SqlDbType.Int).Value = Convert.ToInt32(result[x][7]);
                                                myCommand.Parameters.Add("@Altitude", SqlDbType.Int).Value = Convert.ToInt32(result[x][8]);
                                                myCommand.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = result[x][9];
                                                myCommand.Parameters.Add("@AltProfile", SqlDbType.NVarChar).Value = result[x][10];
                                                myCommand.Parameters.Add("@Image", SqlDbType.NVarChar).Value = result[x][11];
                                                myCommand.Parameters.Add("@created", SqlDbType.Date).Value = Convert.ToDateTime(result[x][12]);
                                                myCommand.Parameters.Add("@last", SqlDbType.Date).Value = DateTime.Now;
                                                myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Convert.ToInt32(result[x][14]);
                                                myCommand.CommandText = sqlquery;
                                                myCommand.CommandType = CommandType.Text;
                                                myCommand.Connection = myConnection;
                                                myCommand.ExecuteNonQuery();
                                            }
                                        }
                                        myConnection.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ShowErrorMessage(ex.Message, "Fehler beim Einfügen von Daten in " + tables[current_table]);
                                }
                                break;
                            case "Vehicles":
                                if (status == JobStatus.APPEND_TOUR || status == JobStatus.DROP_TOUR)
                                {
                                    break;
                                }
                                else if (status == JobStatus.APPEND_ALL)
                                {
                                    result = ReadSpreadsheet(tables[current_table], 10, CountIds(tables[current_table]));
                                }
                                else if (status == JobStatus.DROP_ALL)
                                {
                                    ClearTable(tables[current_table]);
                                    result = ReadSpreadsheet(tables[current_table], 10);
                                }

                                try
                                {
                                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                    {
                                        myConnection.Open();
                                        for (int x = 0; x < result.Count; x++)
                                        {
                                            using (SqlCommand myCommand = new SqlCommand())
                                            {
                                                string sqlquery = "INSERT INTO " + tables[current_table] +
                                                " (Id, VehicleName, Manufacturer, VehicleType, BoughtOn, BuildYear, Price, Equipment, Image, Entfaltung, Created, LastChanged, User, LicensePlate) " +
                                                "VALUES (@id, @VehicleName, @Manufacturer, @VehicleType, @BoughtOn, @BuildYear, @Price, @Equipment, @Image, @Entfaltung, @created, @last, @user, @license)";
                                                myCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(result[x][0]);
                                                myCommand.Parameters.Add("@VehicleName", SqlDbType.NVarChar).Value = result[x][1];
                                                myCommand.Parameters.Add("@Manufacturer", SqlDbType.Int).Value = result[x][2];
                                                myCommand.Parameters.Add("@VehicleType", SqlDbType.Int).Value = result[x][3];
                                                myCommand.Parameters.Add("@BoughtOn", SqlDbType.Date).Value = Convert.ToDateTime(result[x][4]);
                                                myCommand.Parameters.Add("@BuildYear", SqlDbType.Int).Value = Convert.ToInt32(result[x][5]);
                                                myCommand.Parameters.Add("@Price", SqlDbType.Money).Value = Convert.ToDecimal(result[x][6]);
                                                myCommand.Parameters.Add("@Equipment", SqlDbType.NVarChar).Value = result[x][7];
                                                myCommand.Parameters.Add("@Image", SqlDbType.NVarChar).Value = result[x][8];
                                                myCommand.Parameters.Add("@created", SqlDbType.Date).Value = Convert.ToDateTime(result[x][9]);
                                                myCommand.Parameters.Add("@last", SqlDbType.Date).Value = DateTime.Now;
                                                myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Convert.ToInt32(result[x][11]);
                                                myCommand.Parameters.Add("@license", SqlDbType.NVarChar).Value = result[x][12];
                                                int entf = -1;
                                                if (result[x][9] != String.Empty) entf = Convert.ToInt32(result[x][9]);
                                                myCommand.Parameters.Add("@Entfaltung", SqlDbType.Int).Value = entf;
                                                myCommand.CommandText = sqlquery;
                                                myCommand.CommandType = CommandType.Text;
                                                myCommand.Connection = myConnection;
                                                myCommand.ExecuteNonQuery();
                                            }
                                        }
                                        myConnection.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ShowErrorMessage(ex.Message, "Fehler beim Einfügen von Daten in " + tables[current_table]);
                                }
                                break;
                            case "Companies":
                                if (status == JobStatus.APPEND_TOUR || status == JobStatus.DROP_TOUR)
                                {
                                    break;
                                }
                                else if (status == JobStatus.APPEND_ALL)
                                {
                                    result = ReadSpreadsheet(tables[current_table], 3, CountIds(tables[current_table]));
                                }
                                else if (status == JobStatus.DROP_ALL)
                                {
                                    ClearTable(tables[current_table]);
                                    result = ReadSpreadsheet(tables[current_table], 3);
                                }

                                try
                                {
                                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                    {
                                        myConnection.Open();
                                        for (int x = 0; x < result.Count; x++)
                                        {
                                            using (SqlCommand myCommand = new SqlCommand())
                                            {
                                                string sqlquery = "INSERT INTO " + tables[current_table] +
                                                " (Id, CompanyName, Link, Created, LastChanged, User) " +
                                                "VALUES (@id, @company, @link, @created, @last, @user)";
                                                myCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(result[x][0]);
                                                myCommand.Parameters.Add("@company", SqlDbType.NVarChar).Value = (result[x][1]);
                                                myCommand.Parameters.Add("@link", SqlDbType.NVarChar).Value = (result[x][2]);
                                                myCommand.Parameters.Add("@created", SqlDbType.Date).Value = Convert.ToDateTime(result[x][3]);
                                                myCommand.Parameters.Add("@last", SqlDbType.Date).Value = DateTime.Now;
                                                myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Convert.ToInt32(result[x][5]);
                                                myCommand.CommandText = sqlquery;
                                                myCommand.CommandType = CommandType.Text;
                                                myCommand.Connection = myConnection;
                                                myCommand.ExecuteNonQuery();
                                            }
                                        }
                                        myConnection.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ShowErrorMessage(ex.Message, "Fehler beim Einfügen von Daten in " + tables[current_table]);
                                }
                                break;
                            case "Entfaltung":
                                if (status == JobStatus.APPEND_TOUR || status == JobStatus.DROP_TOUR)
                                {
                                    break;
                                }
                                else if (status == JobStatus.APPEND_ALL)
                                {
                                    result = ReadSpreadsheet(tables[current_table], 6, CountIds(tables[current_table]));
                                }
                                else if (status == JobStatus.DROP_ALL)
                                {
                                    ClearTable(tables[current_table]);
                                    result = ReadSpreadsheet(tables[current_table], 6);
                                }

                                try
                                {
                                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                    {
                                        myConnection.Open();
                                        for (int x = 0; x < result.Count; x++)
                                        {
                                            using (SqlCommand myCommand = new SqlCommand())
                                            {
                                                string sqlquery = "INSERT INTO " + tables[current_table] +
                                                " (Id, BikeId, Front, Back, Wheel, Unit, Created, LastChanged, User) " +
                                                "VALUES (@id, @bike, @front, @back, @wheel, @unit, @created, @last, @user)";
                                                myCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(result[x][0]);
                                                myCommand.Parameters.Add("@bike", SqlDbType.Int).Value = Convert.ToInt32(result[x][1]);
                                                myCommand.Parameters.Add("@front", SqlDbType.NVarChar).Value = result[x][2];
                                                myCommand.Parameters.Add("@back", SqlDbType.NVarChar).Value = result[x][3];
                                                myCommand.Parameters.Add("@wheel", SqlDbType.Float).Value = Convert.ToDouble(result[x][4]);
                                                myCommand.Parameters.Add("@unit", SqlDbType.NVarChar).Value = (result[x][5]);
                                                myCommand.Parameters.Add("@created", SqlDbType.Date).Value = Convert.ToDateTime(result[x][6]);
                                                myCommand.Parameters.Add("@last", SqlDbType.Date).Value = DateTime.Now;
                                                myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Convert.ToInt32(result[x][8]);
                                                myCommand.CommandText = sqlquery;
                                                myCommand.CommandType = CommandType.Text;
                                                myCommand.Connection = myConnection;
                                                myCommand.ExecuteNonQuery();
                                            }
                                        }
                                        myConnection.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ShowErrorMessage(ex.Message, "Fehler beim Einfügen von Daten in " + tables[current_table]);
                                }
                                break;
                            case "VehicleTypes":
                                if (status == JobStatus.APPEND_TOUR || status == JobStatus.DROP_TOUR)
                                {
                                    break;
                                }
                                else if (status == JobStatus.APPEND_ALL)
                                {
                                    result = ReadSpreadsheet(tables[current_table], 2, CountIds(tables[current_table]));
                                }
                                else if (status == JobStatus.DROP_ALL)
                                {
                                    ClearTable(tables[current_table]);
                                    result = ReadSpreadsheet(tables[current_table], 2);
                                }

                                try
                                {
                                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                    {
                                        myConnection.Open();
                                        for (int x = 0; x < result.Count; x++)
                                        {
                                            using (SqlCommand myCommand = new SqlCommand())
                                            {
                                                string sqlquery = "INSERT INTO " + tables[current_table] +
                                                " (Id, VehicleType, Created, LastChanged, User) " +
                                                "VALUES (@id, @vehicletype, @created, @last, @user)";
                                                myCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(result[x][0]);
                                                myCommand.Parameters.Add("@vehicletype", SqlDbType.NVarChar).Value = (result[x][1]);
                                                myCommand.Parameters.Add("@created", SqlDbType.Date).Value = Convert.ToDateTime(result[x][2]);
                                                myCommand.Parameters.Add("@last", SqlDbType.Date).Value = DateTime.Now;
                                                myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Convert.ToInt32(result[x][4]);
                                                myCommand.CommandText = sqlquery;
                                                myCommand.CommandType = CommandType.Text;
                                                myCommand.Connection = myConnection;
                                                myCommand.ExecuteNonQuery();
                                            }
                                        }
                                        myConnection.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ShowErrorMessage(ex.Message, "Fehler beim Einfügen von Daten in " + tables[current_table]);
                                }
                                break;
                            case "RouteTypes":
                                if (status == JobStatus.APPEND_TOUR || status == JobStatus.DROP_TOUR)
                                {
                                    break;
                                }
                                else if (status == JobStatus.APPEND_ALL)
                                {
                                    result = ReadSpreadsheet(tables[current_table], 2, CountIds(tables[current_table]));
                                }
                                else if (status == JobStatus.DROP_ALL)
                                {
                                    ClearTable(tables[current_table]);
                                    result = ReadSpreadsheet(tables[current_table], 2);
                                }

                                try
                                {
                                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                    {
                                        myConnection.Open();
                                        for (int x = 0; x < result.Count; x++)
                                        {
                                            using (SqlCommand myCommand = new SqlCommand())
                                            {
                                                string sqlquery = "INSERT INTO " + tables[current_table] +
                                                " (Id, RouteType, Created, LastChanged, User) " +
                                                "VALUES (@id, @routetype, @created, @last, @user)";
                                                myCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(result[x][0]);
                                                myCommand.Parameters.Add("@routetype", SqlDbType.NVarChar).Value = (result[x][1]);
                                                myCommand.Parameters.Add("@created", SqlDbType.Date).Value = Convert.ToDateTime(result[x][2]);
                                                myCommand.Parameters.Add("@last", SqlDbType.Date).Value = DateTime.Now;
                                                myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Convert.ToInt32(result[x][4]);
                                                myCommand.CommandText = sqlquery;
                                                myCommand.CommandType = CommandType.Text;
                                                myCommand.Connection = myConnection;
                                                myCommand.ExecuteNonQuery();
                                            }
                                        }
                                        myConnection.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ShowErrorMessage(ex.Message, "Fehler beim Einfügen von Daten in " + tables[current_table]);
                                }
                                break;
                            case "Persons":
                                if (status == JobStatus.APPEND_TOUR || status == JobStatus.DROP_TOUR)
                                {
                                    break;
                                }
                                else if (status == JobStatus.APPEND_ALL)
                                {
                                    result = ReadSpreadsheet(tables[current_table], 2, CountIds(tables[current_table]));
                                }
                                else if (status == JobStatus.DROP_ALL)
                                {
                                    ClearTable(tables[current_table]);
                                    result = ReadSpreadsheet(tables[current_table], 2);
                                }

                                try
                                {
                                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                    {
                                        myConnection.Open();
                                        for (int x = 0; x < result.Count; x++)
                                        {
                                            using (SqlCommand myCommand = new SqlCommand())
                                            {
                                                string sqlquery = "INSERT INTO " + tables[current_table] +
                                                " (Id, Username, Lastname, Name, City, Birthdate, Phone, Email, PLZ, Street1, Street2, Country, Image, " +
                                                    "Remark, Created, LastChanged, User, IsUser, IsAdmin, Password) " +
                                                    "VALUES (@id, @username, @lastname, @name, @city, @birthdate, @phone, @email, @plz, @str1, @str2, @country, @image, " +
                                                    "@remark, @created, @lastchanged, @user, @isuser, @isadmin, @pwd)";
                                                myCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(result[x][0]);
                                                myCommand.Parameters.Add("@username", SqlDbType.NVarChar).Value = result[x][1];
                                                myCommand.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = result[x][2];
                                                myCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = result[x][3];
                                                myCommand.Parameters.Add("@city", SqlDbType.Int).Value = Convert.ToInt32(result[x][4]);
                                                myCommand.Parameters.Add("@birthdate", SqlDbType.Date).Value = Convert.ToDateTime(result[x][5]);
                                                myCommand.Parameters.Add("@phone", SqlDbType.NVarChar).Value = result[x][6];
                                                myCommand.Parameters.Add("@email", SqlDbType.NVarChar).Value = result[x][7];
                                                myCommand.Parameters.Add("@plz", SqlDbType.NVarChar).Value = result[x][8];
                                                myCommand.Parameters.Add("@str1", SqlDbType.NVarChar).Value = result[x][9];
                                                myCommand.Parameters.Add("@str2", SqlDbType.NVarChar).Value = result[x][10];
                                                myCommand.Parameters.Add("@country", SqlDbType.Int).Value = Convert.ToInt32(result[x][11]);
                                                myCommand.Parameters.Add("@image", SqlDbType.NVarChar).Value = result[x][12];
                                                myCommand.Parameters.Add("@remark", SqlDbType.NVarChar).Value = result[x][13];
                                                myCommand.Parameters.Add("@created", SqlDbType.DateTime).Value = Convert.ToDateTime(result[x][14]);
                                                myCommand.Parameters.Add("@lastchanged", SqlDbType.DateTime).Value = DateTime.Now;
                                                myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Convert.ToInt32(result[x][16]);
                                                myCommand.Parameters.Add("@isuser", SqlDbType.TinyInt).Value = result[x][17];
                                                myCommand.Parameters.Add("@isadmin", SqlDbType.TinyInt).Value = result[x][18];
                                                myCommand.Parameters.Add("@pwd", SqlDbType.NVarChar).Value = result[x][19];
                                                myCommand.CommandText = sqlquery;
                                                myCommand.CommandType = CommandType.Text;
                                                myCommand.Connection = myConnection;
                                                myCommand.ExecuteNonQuery();
                                            }
                                        }
                                        myConnection.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ShowErrorMessage(ex.Message, "Fehler beim Einfügen von Daten in " + tables[current_table]);
                                }
                                break;
                            case "Notes":
                                if (status == JobStatus.APPEND_TOUR || status == JobStatus.DROP_TOUR)
                                {
                                    break;
                                }
                                else if (status == JobStatus.APPEND_ALL)
                                {
                                    result = ReadSpreadsheet(tables[current_table], 2, CountIds(tables[current_table]));
                                }
                                else if (status == JobStatus.DROP_ALL)
                                {
                                    ClearTable(tables[current_table]);
                                    result = ReadSpreadsheet(tables[current_table], 2);
                                }

                                try
                                {
                                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                    {
                                        myConnection.Open();
                                        for (int x = 0; x < result.Count; x++)
                                        {
                                            using (SqlCommand myCommand = new SqlCommand())
                                            {
                                                string sqlquery = "INSERT INTO " + tables[current_table] +
                                                " (Id, Title, Remark, Created, LastChanged, User) " +
                                                "VALUES (@id, @title, @remark, @created, @last, @user)";
                                                myCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(result[x][0]);
                                                myCommand.Parameters.Add("@title", SqlDbType.NVarChar).Value = (result[x][1]);
                                                myCommand.Parameters.Add("@remark", SqlDbType.NVarChar).Value = (result[x][2]);
                                                myCommand.Parameters.Add("@created", SqlDbType.Date).Value = Convert.ToDateTime(result[x][3]);
                                                myCommand.Parameters.Add("@last", SqlDbType.Date).Value = DateTime.Now;
                                                myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Convert.ToInt32(result[x][5]);
                                                myCommand.CommandText = sqlquery;
                                                myCommand.CommandType = CommandType.Text;
                                                myCommand.Connection = myConnection;
                                                myCommand.ExecuteNonQuery();
                                            }
                                        }
                                        myConnection.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ShowErrorMessage(ex.Message, "Fehler beim Einfügen von Daten in " + tables[current_table]);
                                }
                                break;
                            case "Goals":
                                if (status == JobStatus.APPEND_TOUR || status == JobStatus.DROP_TOUR)
                                {
                                    break;
                                }
                                else if (status == JobStatus.APPEND_ALL)
                                {
                                    result = ReadSpreadsheet(tables[current_table], 2, CountIds(tables[current_table]));
                                }
                                else if (status == JobStatus.DROP_ALL)
                                {
                                    ClearTable(tables[current_table]);
                                    result = ReadSpreadsheet(tables[current_table], 2);
                                }

                                try
                                {
                                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                    {
                                        myConnection.Open();
                                        for (int x = 0; x < result.Count; x++)
                                        {
                                            using (SqlCommand myCommand = new SqlCommand())
                                            {
                                                string sqlquery = "INSERT INTO " + tables[current_table] +
                                                " (Id, Title, Remark, Date, Achieved, Created, LastChanged, User) " +
                                                "VALUES (@id, @title, @remark, @date, @achieved, @created, @last, @user)";
                                                myCommand.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(result[x][0]);
                                                myCommand.Parameters.Add("@title", SqlDbType.NVarChar).Value = (result[x][1]);
                                                myCommand.Parameters.Add("@remark", SqlDbType.NVarChar).Value = (result[x][2]);
                                                myCommand.Parameters.Add("@date", SqlDbType.Date).Value = Convert.ToDateTime(result[x][3]);
                                                myCommand.Parameters.Add("@achieved", SqlDbType.Int).Value = Convert.ToInt32(result[x][4]);
                                                myCommand.Parameters.Add("@created", SqlDbType.Date).Value = Convert.ToDateTime(result[x][5]);
                                                myCommand.Parameters.Add("@last", SqlDbType.Date).Value = DateTime.Now;
                                                myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Convert.ToInt32(result[x][7]);
                                                myCommand.CommandText = sqlquery;
                                                myCommand.CommandType = CommandType.Text;
                                                myCommand.Connection = myConnection;
                                                myCommand.ExecuteNonQuery();
                                            }
                                        }
                                        myConnection.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ShowErrorMessage(ex.Message, "Fehler beim Einfügen von Daten in " + tables[current_table]);
                                }
                                break;
                        }
                        // Report progress
                        current_table = i;
                        percent = (int)((i + 1) * 100 / (tables.Length));
                        worker.ReportProgress(percent);
                    }
                }
            }
        }

        /// <summary>
        /// Reads a table from an Excel file and returns a dictionary with key = id, values = columns.
        /// </summary>
        /// <param name="name">Name of table inside Excel file.</param>
        /// <param name="column_count">Numbers of columns in table</param>
        /// <param name="start_id">0 if table is cleared, start_id != 0 if values are added to table.</param>
        /// <returns>Dictionary<int, string[]></returns>
        private Dictionary<int, string[]> ReadSpreadsheet(string name, int column_count, int start_id = 0)
        {
            Dictionary<int, string[]> result = new Dictionary<int, string[]>();
            Workbook book = Workbook.Load(FileName);
            foreach (Worksheet sheet in book.Worksheets)
            {
                if (sheet.Name == name)
                {
                    int row = 2;
                    while (true)
                    {
                        if (!sheet.Cells[row,0].ToString().Contains("Tabelle enthält keine Daten")
                            && sheet.Cells[row, 0].ToString() != "")
                        {
                            string[] line = new string[column_count];
                            for (int i = 0; i < column_count; i++)
                            {
                                line[i] = sheet.Cells[row, i].ToString();
                                
                            }
                            result[start_id] = line;
                        }
                        else
                        {
                            break;
                        }
                        row++;
                        start_id++;
                    }
                }
                else continue;
            }
            return result;
        }

        /// <summary>
        /// Both jobs (export or import) can change the progressbar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exImportWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (JobType)
            {
                case JobType.EXPORT:
                    jobRichTextBox.Text += "..";
                    break;
                case JobType.IMPORT:
                    jobRichTextBox.Text += $"Importiere {e.ProgressPercentage}%...\n";      //{tables[current_table]} - 
                    break;
                default:
                    break;
            }
            
            progressBar.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Background worker has completed its job or has been interupted (error or canceled).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exImportWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string job = "";
            switch (JobType)
            {
                case JobType.EXPORT:
                    job = "Export";
                    break;
                case JobType.IMPORT:
                    job = "Import";
                    break;
                default:
                    break;
            }
            if (e.Cancelled == true)
            {
                jobRichTextBox.Text += $"\n{job} wurde abgebrochen!";
            }
            else if (e.Error != null)
            {
                jobRichTextBox.Text += $"\nFehler: {e.Error.Message}";
            }
            else
            {
                progressBar.Value = 100;
                jobRichTextBox.Text += $"\n{job} erfolgreich abgeschlossen.\n\n";
                if (JobType == JobType.EXPORT)
                {
                    workbook.Save(SaveFolder + @"\" + fileName);
                }
            }
            okButton.Enabled = true;
            cancelButton.Enabled = false;
        }

        /// <summary>
        /// Close the form when the background worker has completed its job.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Cancel button stops running background worker or closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (exImportWorker.WorkerSupportsCancellation && exImportWorker.IsBusy)
            {
                exImportWorker.CancelAsync();
            }
            else if (!exImportWorker.IsBusy)
            {
                this.DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        /// <summary>
        /// ProgressForm has loaded and the textbox is prepared with job details and more information.
        /// When everything is prepared, the background worker starts its job.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressForm_Shown(object sender, EventArgs e)
        {
            if (JobType == JobType.EXPORT)
            {
                if ((bool)Checkboxes.GetValue(0))
                {
                    cnt_tables++;
                    tmp = CountIds("Tour");
                    array.SetValue(tmp, 0);
                    cnt_rows += tmp;
                }
                else
                {
                    array.SetValue(0, 0);
                }
                if ((bool)Checkboxes.GetValue(1))
                {
                    cnt_tables++;
                    tmp = CountIds("Countries");
                    array.SetValue(tmp, 1);
                    cnt_rows += tmp;
                }
                else
                {
                    array.SetValue(0, 1);
                }
                if ((bool)Checkboxes.GetValue(2))
                {
                    cnt_tables++;
                    tmp = CountIds("Cities");
                    array.SetValue(tmp, 2);
                    cnt_rows += tmp;
                }
                else
                {
                    array.SetValue(0, 2);
                }
                if ((bool)Checkboxes.GetValue(3))
                {
                    cnt_tables++;
                    tmp = CountIds("Routes");
                    array.SetValue(tmp, 3);
                    cnt_rows += tmp;
                }
                else
                {
                    array.SetValue(0, 3);
                }
                if ((bool)Checkboxes.GetValue(4))
                {
                    cnt_tables++;
                    tmp = CountIds("Vehicles");
                    array.SetValue(tmp, 4);
                    cnt_rows += tmp;
                }
                else
                {
                    array.SetValue(0, 4);
                }
                if ((bool)Checkboxes.GetValue(5))
                {
                    cnt_tables++;
                    tmp = CountIds("Companies");
                    array.SetValue(tmp, 5);
                    cnt_rows += tmp;
                }
                else
                {
                    array.SetValue(0, 5);
                }
                if ((bool)Checkboxes.GetValue(6))
                {
                    cnt_tables++;
                    tmp = CountIds("Entfaltung");
                    array.SetValue(tmp, 6);
                    cnt_rows += tmp;
                }
                else
                {
                    array.SetValue(0, 6);
                }
                if ((bool)Checkboxes.GetValue(7))
                {
                    cnt_tables++;
                    tmp = CountIds("VehicleTypes");
                    array.SetValue(tmp, 7);
                    cnt_rows += tmp;
                }
                else
                {
                    array.SetValue(0, 7);
                }
                if ((bool)Checkboxes.GetValue(8))
                {
                    cnt_tables++;
                    tmp = CountIds("RouteTypes");
                    array.SetValue(tmp, 8);
                    cnt_rows += tmp;
                }
                else
                {
                    array.SetValue(0, 8);
                }
                if ((bool)Checkboxes.GetValue(9))
                {
                    cnt_tables++;
                    tmp = CountIds("Persons");
                    array.SetValue(tmp, 9);
                    cnt_rows += tmp;
                }
                else
                {
                    array.SetValue(0, 9);
                }
                if ((bool)Checkboxes.GetValue(10))
                {
                    cnt_tables++;
                    tmp = CountIds("Notes");
                    array.SetValue(tmp, 10);
                    cnt_rows += tmp;
                }
                else
                {
                    array.SetValue(0, 10);
                }
                if ((bool)Checkboxes.GetValue(11))
                {
                    cnt_tables++;
                    tmp = CountIds("Goals");
                    array.SetValue(tmp, 11);
                    cnt_rows += tmp;
                }
                else
                {
                    array.SetValue(0, 11);
                }

                jobRichTextBox.Text = "Starte Export\n";
                jobRichTextBox.Text += "Datei: " + FileName;
                jobRichTextBox.Text += $"\n{cnt_rows} Zeilen in {cnt_tables} Tabellen.\n";
                exImportWorker.RunWorkerAsync();
            }
            else        // JobType.IMPORT
            {
                jobRichTextBox.Text = "Starte Import\n";
                jobRichTextBox.Text += "Datei: " + FileName;
                string job = "";
                switch (status)
                {
                    case JobStatus.NONE:
                        break;
                    case JobStatus.DROP_ALL:
                        job = "Alle Tabellen löschen und neu befüllen.";
                        break;
                    case JobStatus.APPEND_ALL:
                        job = "Bestehende Daten behalten und neue Werte anfügen.";
                        break;
                    case JobStatus.DROP_TOUR:
                        job = "Tabelle Tagestour löschen und neu befüllen.";
                        break;
                    case JobStatus.APPEND_TOUR:
                        job = "Tabelle Tagestour behalten und neue Werte anfügen.";
                        break;
                    default:
                        break;
                }
                jobRichTextBox.Text += $"\nAufgabe: {job}";

                int cnt_sheets = 0;
                bool cont = false;
                bool tour_found = false;
                try
                {
                    Workbook book = Workbook.Load(FileName);
                    tables = new string[book.Worksheets.Count - 1];     // Sheet "Version" is not counted
                    foreach (Worksheet sheet in book.Worksheets)
                    {
                        // Check version of Excel file
                        if (sheet.Name == "Version")
                        {
                            if (sheet.Cells[2, 0].StringValue == version)
                            {
                                jobRichTextBox.Text += $"\nVersion {version} kann importiert werden!\n\nTabellen {tables.Length}:\n=======";
                                cont = true;
                            }
                            else
                            {
                                jobRichTextBox.Text += $"\nFalsche Version: {sheet.Cells[2, 0]} \nImport abgebrochen!\n\n";
                                break;
                            }
                        }
                        else
                        {
                            if (status == JobStatus.DROP_TOUR || status == JobStatus.APPEND_TOUR)
                            {
                                if (sheet.Name == "Tour")
                                {
                                    jobRichTextBox.Text += $"\n{sheet.Name} ";
                                    tables = new string[1];
                                    tables[0] = "Tour";
                                    tour_found = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else 
                            {
                                jobRichTextBox.Text += $"\n{sheet.Name} ";
                                tables[cnt_sheets] = sheet.Name;
                                cnt_sheets++;
                            }
                        }
                    }
                    if (cont)
                    {
                        if ((status == JobStatus.DROP_TOUR || status == JobStatus.APPEND_TOUR) && tour_found)
                        {
                            jobRichTextBox.Text += $"\nTour gefunden...\n\n";
                            exImportWorker.RunWorkerAsync();
                        }
                        else if (status == JobStatus.NONE)      // should not happen, but just in case...
                        {
                            Close();
                        }
                        else
                        {
                            jobRichTextBox.Text += $"\n{cnt_sheets} Tabellen gefunden...\n";
                            exImportWorker.RunWorkerAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, "Fehler beim Import von Datei");
                }
            }
        }

        /// <summary>
        /// When the content of the textbox changes, scroll to the end.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jobRichTextBox_TextChanged(object sender, EventArgs e)
        {
            jobRichTextBox.SelectionStart = jobRichTextBox.Text.Length;
            jobRichTextBox.ScrollToCaret();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DateTime ReturnNowIfNull(string dt)
        {
            if (dt == String.Empty)
            {
                return DateTime.Now;
            }
            else
            {
                return Convert.ToDateTime(dt);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private int AdminIfNull(string user)
        {
            if (user == String.Empty)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(user);
            }
        }
    }
}
