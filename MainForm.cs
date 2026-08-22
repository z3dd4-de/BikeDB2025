using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using BikeDB2024.FlightDB;
using ExcelLibrary.BinaryFileFormat;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using LibUsbDotNet;
using LibUsbDotNet.DeviceNotify;
using LibUsbDotNet.Main;
using QiHe.CodeLib;
using static BikeDB2024.Helpers;
using System.Text.Json;
using System.IO;

namespace BikeDB2024
{
    public partial class MainForm : Form
    {
        #region Private Variables
        private int tabPage = 0;
        private int cnt_vehicles = 0;
        private int cnt_routes = 0;
        private int cnt_tours = 0;
        private int cnt_cities = 0;
        private int cnt_persons = 0;
        private int cnt_goals = 0;
        private int cnt_notes = 0;
        private int current_vehicle = -1;
        private int current_entfaltung = -1;
        private int current_route = -1;
        private int current_tour = -1;
        private int current_city = -1;
        private int current_person = -1;
        private int current_goal = -1;
        private int current_note = -1;
        private int current_gallery = -1;
        private bool edit_data = false;
        private bool logged_in = false;
        // Ids as Arrays for navigation
        private int[] vehicle_ids = null;
        private int vehicle_id = 0;
        private int[] route_ids = null;
        private int route_id = 0;
        private int[] city_ids = null;
        private int city_id = 0;
        private int[] person_ids = null;
        private int person_id = 0;
        private int[] goal_ids = null;
        private int goal_id = 0;
        private int[] tour_ids = null;
        private int tour_id = 0;
        private int[] note_ids = null;
        private int note_id = 0;
        //GMap
        private GMapControl cityMapControl;
        // Sigma Docking Station
        #region SET YOUR USB Vendor and Product ID!
        /*private const int vid = 7581;
        private const int pid = 4113;
        private UsbDeviceFinder MyUsbFinder = new(vid, pid);
        private IDeviceNotifier UsbDeviceNotifier = DeviceNotifier.OpenDeviceNotifier();
        private UsbDevice MyUsbDevice;*/
        private DirectoryWatcher directoryWatcher;
        #endregion
        #endregion

        #region MainForm: Constructor, Settings...
        /// <summary>
        /// Constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitMapControl();
            this.Text = Properties.Settings.Default.BikeDBVersion;
        }

        /// <summary>
        /// Initialize the map for cities on the mapTabPage.
        /// </summary>
        private void InitMapControl()
        {
            cityMapControl = new GMapControl
            {
                MinZoom = 0,
                MaxZoom = 18,
                Zoom = 10,
                AutoScroll = true,
                DragButton = MouseButtons.Left,
                CanDragMap = true
            };
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            SwitchMapProvider(cityMapControl);

            cityMapControl.Position = new PointLatLng(50.938, 6.9569);
            cityMapControl.ShowCenter = false;
            cityMapControl.Dock = DockStyle.Fill;

            mapTabPage.Controls.Add(cityMapControl);
        }

        /// <summary>
        /// Executed when the MainForm is loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.RouteTypes". Sie können sie bei Bedarf verschieben oder entfernen.
            this.routeTypesTableAdapter.Fill(this.dataSet.RouteTypes);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.VehicleTypes". Sie können sie bei Bedarf verschieben oder entfernen.
            this.vehicleTypesTableAdapter.Fill(this.dataSet.VehicleTypes);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Bundeslaender". Sie können sie bei Bedarf verschieben oder entfernen.
            this.bundeslaenderTableAdapter.Fill(this.dataSet.Bundeslaender);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Countries". Sie können sie bei Bedarf verschieben oder entfernen.
            this.countriesTableAdapter.Fill(this.dataSet.Countries);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Continents". Sie können sie bei Bedarf verschieben oder entfernen.
            this.continentsTableAdapter.Fill(this.dataSet.Continents);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Tour". Sie können sie bei Bedarf verschieben oder entfernen.
            this.tourTableAdapter.Fill(this.dataSet.Tour);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Routes". Sie können sie bei Bedarf verschieben oder entfernen.
            this.routesTableAdapter.Fill(this.dataSet.Routes);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Vehicles". Sie können sie bei Bedarf verschieben oder entfernen.
            this.vehiclesTableAdapter.Fill(this.dataSet.Vehicles);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Cities". Sie können sie bei Bedarf verschieben oder entfernen.
            this.citiesTableAdapter.Fill(this.dataSet.Cities);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Countries". Sie können sie bei Bedarf verschieben oder entfernen.
            //this.countriesTableAdapter.Fill(this.dataSet.Countries);
            SetLoginStatus(false);
            CheckLogin();  
            CheckDockingStation();
        }

        /// <summary>
        /// Load vehicles for the current user. 
        /// </summary>
        private void LoadVehicles()
        {
            setting_vehicleComboBox.DataSource = null;
            vehicleComboBox.DataSource = null;
            setting_vehicleComboBox.Items.Clear();
            vehicleComboBox.Items.Clear();

            SqlConnection con1;
            List<Vehicle> data = new List<Vehicle>();
            setting_vehicleComboBox.DisplayMember = "Text";
            setting_vehicleComboBox.ValueMember = "Value";
            vehicleComboBox.DisplayMember = "Text";
            vehicleComboBox.ValueMember = "Value";

            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new())
                    {
                        com1.CommandText = @"SELECT * FROM Vehicles WHERE [User] = " + Properties.Settings.Default.CurrentUserID.ToString()
                            + " ORDER BY VehicleName";
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using SqlDataReader reader1 = com1.ExecuteReader();
                        while (reader1.Read())
                        {
                            data.Add(new Vehicle(reader1.GetInt32(0)));
                        }
                        reader1.Close();
                    }
                    con1.Close();
                }
                setting_vehicleComboBox.DataSource = data;
                vehicleComboBox.DataSource = data;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Laden des Fahrzeugs");
            }
        }

        /// <summary>
        /// Load routes for the current user. 
        /// </summary>
        private void LoadRoutes()
        {
            setting_routeComboBox.DataSource = null;
            routeComboBox.DataSource = null;
            setting_routeComboBox.Items.Clear();
            routeComboBox.Items.Clear();

            SqlConnection con1;
            List<Route> data = [];
            setting_routeComboBox.DisplayMember = "Text";
            setting_routeComboBox.ValueMember = "Value";
            routeComboBox.DisplayMember = "Text";
            routeComboBox.ValueMember = "Value";

            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new())
                    {
                        com1.CommandText = @"SELECT * FROM Routes WHERE [User] = " + Properties.Settings.Default.CurrentUserID.ToString()
                            + " ORDER BY RouteName";
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using SqlDataReader reader1 = com1.ExecuteReader();
                        while (reader1.Read())
                        {
                            data.Add(new Route(reader1.GetInt32(0)));
                        }
                        reader1.Close();
                    }
                    con1.Close();
                }
                setting_routeComboBox.DataSource = data;
                routeComboBox.DataSource = data;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Laden der Strecke");
            }
        }

        /// <summary>
        /// Check local login status.
        /// </summary>
        private void CheckLogin()
        {
            if (Properties.Settings.Default.UserLoggedIn)
            {
                logged_in = true;
                loginToolStripMenuItem.Visible = false;
                logoutToolStripMenuItem.Visible = true;
                toolStripMenuItem5.Visible = false;
                administrationToolStripMenuItem.Visible = false;
                adminToolStripButton.Visible = false;
            }
            else if (!Properties.Settings.Default.UserLoggedIn)
            {
                logged_in = false;
                loginToolStripMenuItem.Visible = true;
                logoutToolStripMenuItem.Visible = false;
                toolStripMenuItem5.Visible = false;
                administrationToolStripMenuItem.Visible = false;
                adminToolStripButton.Visible = false;
            }
            if (Properties.Settings.Default.AdminLoggedIn)
            {
                toolStripMenuItem5.Visible = true;
                administrationToolStripMenuItem.Visible = true;
                adminToolStripButton.Visible = true;
            }
            SetMenuItemsAvailability();
            CheckDockingStation();
        }

        /// <summary>
        /// Visibility of ToolStripMenu (buttons and labels).
        /// </summary>
        private void SetMenuItemsAvailability()
        {
            if (logged_in)
            {
                ShowStartPage();
                LoadSettings();
                LoadVehicles();
                LoadRoutes();

                mainStatusLabel.Text = "";
                loginToolStripMenuItem.Visible = false;
                logoutToolStripMenuItem.Visible = true;
                passwortÄndernToolStripMenuItem.Visible = true;
                toolStripMenuItem7.Visible = true;
                ansichtToolStripMenuItem.Visible = true;
                statistikToolStripMenuItem.Visible = true;
                toolsToolStripMenuItem.Visible = true;
                importierenToolStripMenuItem.Visible = true;
                exportierenToolStripMenuItem.Visible = true;
                toolStripMenuItem1.Visible = true;
                neuToolStripMenuItem.Visible = true;

                toolStripLabel1.Visible = true;
                toolStripLabel2.Visible = true;
                vehicleToolStripButton.Visible = true;
                companyToolStripButton.Visible = true;
                countryToolStripButton.Visible = true;
                personsToolStripButton.Visible = true;
                goalsToolStripButton.Visible = true;
                notesToolStripButton.Visible = true;
                routeToolStripButton.Visible = true;
                earthToolStripButton.Visible = true;
                imageViewerToolStripButton.Visible = true;
                cityToolStripButton.Visible = true;
                homeToolStripButton.Visible = true;
                statisticsToolStripButton.Visible = true;
                entfaltungToolStripButton.Visible = true;
                settingsToolStripButton.Visible = true;
                calendarToolStripButton.Visible = true;
                costsToolStripButton.Visible = true;
                galleryToolStripButton.Visible = true;

                toolStripSeparator1.Visible = true;
                toolStripSeparator2.Visible = true;

                if (Properties.Settings.Default.ShowFlightDB)
                {
                    flightDBToolStripMenuItem.Visible = true;
                    flightDbToolStripButton.Visible = true;
                }
                else
                {
                    flightDBToolStripMenuItem.Visible = false;
                    flightDbToolStripButton.Visible = false;
                }

                if (Properties.Settings.Default.UseSigmaDockingStation)
                {

                }
                else
                {

                }

                if (Properties.Settings.Default.GPSCoordAngle == true)
                {
                    gpsFormatComboBox.SelectedText = "Grad";
                }
                else
                {
                    gpsFormatComboBox.SelectedText = "Dezimal";
                }

                if (Properties.Settings.Default.UseCadence)
                {

                }
                else
                {

                }

                if (Properties.Settings.Default.UseAltimeter)
                {

                }
                else
                {

                }

                if (Properties.Settings.Default.ImportJson)
                {
                    importJsonButton.Visible = true;
                    jSONImportierenToolStripMenuItem.Visible = true;
                }
                else
                {
                    importJsonButton.Visible = false;
                    jSONImportierenToolStripMenuItem.Visible = false;
                }

                if (Properties.Settings.Default.UseGPSBabel)
                {
                    gPXDateiumwandelnToolStripMenuItem.Visible = true;
                }
                else
                {
                    gPXDateiumwandelnToolStripMenuItem.Visible = false;
                }

                if (Properties.Settings.Default.UseTrax)
                {

                }
                else
                {

                }

                userToolStripStatusLabel.Visible = true;
                userToolStripStatusLabel.Text = Properties.Settings.Default.CurrentUserName +
                    " (" + Properties.Settings.Default.CurrentUserID.ToString() + ")";
            }
            else
            {
                mainStatusLabel.Text = "";
                userToolStripStatusLabel.Visible = false;
                loginToolStripMenuItem.Visible = true;
                logoutToolStripMenuItem.Visible = false;
                passwortÄndernToolStripMenuItem.Visible = false;
                toolStripMenuItem7.Visible = false;
                mainTabControl.Visible = false;
                routeTabControl.Visible = false;
                cityTabControl.Visible = false;
                vehicleTabControl.Visible = false;
                setupTabControl.Visible = false;

                ansichtToolStripMenuItem.Visible = false;
                statistikToolStripMenuItem.Visible = false;
                toolsToolStripMenuItem.Visible = false;
                importierenToolStripMenuItem.Visible = false;
                exportierenToolStripMenuItem.Visible = false;
                toolStripMenuItem1.Visible = false;
                neuToolStripMenuItem.Visible = false;

                toolStripLabel1.Visible = false;
                toolStripLabel2.Visible = false;
                vehicleToolStripButton.Visible = false;
                companyToolStripButton.Visible = false;
                countryToolStripButton.Visible = false;
                personsToolStripButton.Visible = false;
                goalsToolStripButton.Visible = false;
                notesToolStripButton.Visible = false;
                routeToolStripButton.Visible = false;
                earthToolStripButton.Visible = false;
                imageViewerToolStripButton.Visible = false;
                cityToolStripButton.Visible = false;
                homeToolStripButton.Visible = false;
                statisticsToolStripButton.Visible = false;
                entfaltungToolStripButton.Visible = false;
                settingsToolStripButton.Visible = false;
                calendarToolStripButton.Visible = false;
                costsToolStripButton.Visible = false;
                flightDBToolStripMenuItem.Visible = false;
                flightDbToolStripButton.Visible = false;
                galleryToolStripButton.Visible = false;

                toolStripSeparator1.Visible = false;
                toolStripSeparator2.Visible = false;
                adminToolStripButton.Visible = false;
            }
        }

        /// <summary>
        /// If the docking station use is enabled in settings, it will be set up here.
        /// </summary>
        private void CheckDockingStation()
        {
            if (logged_in)
            {
                if (Properties.Settings.Default.UseSigmaDockingStation)
                {
                    /*UsbDeviceNotifier.OnDeviceNotify += OnDeviceNotifyEvent;
                    UsbRegDeviceList regList = UsbDevice.AllDevices.FindAll(MyUsbFinder);
                    if (regList.Count > 0)
                    {
                        testConnection(true);
                    }
                    else if (regList.Count == 0)
                    {
                        testConnection(false);
                    }*/
                    CreateDirectoryWatcher();
                }
            }
            dsConnectedToolStripStatusLabel.Visible = false;
            dsNotConnectedToolStripStatusLabel.Visible = false;
        }

        private void CreateDirectoryWatcher()
        {
            if (Properties.Settings.Default.SigmaDsEnabled && Properties.Settings.Default.SigmaDirectory != String.Empty)
            {
                directoryWatcher = new DirectoryWatcher
                {
                    IsWatcherActive = true
                };
                // Events abonnieren
                directoryWatcher.FileCreated += Watcher_FileCreated;
                directoryWatcher.FileChanged += Watcher_FileChanged;
            }
        }

        private void Watcher_FileCreated(object sender, string filePath)
        {
            LoadJsonAndDisplay(filePath);
        }

        private void Watcher_FileChanged(object sender, string filePath)
        {
            LoadJsonAndDisplay(filePath);
        }

        private void LoadJsonAndDisplay(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            try
            {
                // JSON einlesen
                string json = File.ReadAllText(filePath);

                var data = JsonSerializer.Deserialize<RideData>(json);
                if (data == null) return;

                LoadBikeDataForm rideDataForm = new(data);
                if (rideDataForm.ShowDialog() == DialogResult.OK)
                {
                    // Da es ein UI-Thread ist, Invoke nutzen
                    this.Invoke(new Action(() =>
                    {
                        kmTextBox.Text = data.DistanceKm.ToString("F2", CultureInfo.InvariantCulture);
                        timeTextBox.Text = data.Duration.ToString(@"hh\:mm\:ss");
                        avgTextBox.Text = data.MeanSpeedKmh.ToString("F2", CultureInfo.InvariantCulture);
                        vmaxTextBox.Text = data.MaxSpeedKmh.ToString("F2", CultureInfo.InvariantCulture);
                        cadenceTextBox.Text = data.Cadence.ToString();
                    }));
                }           
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Lesen der JSON: " + ex.Message);
            }
        }

        private void OnDeviceNotifyEvent(object sender, DeviceNotifyEventArgs e)
        {
            // A Device system-level event has occured
            if (e.EventType == EventType.DeviceArrival)
            {
                if (e.Device != null && e.Device.Name.Contains("USB#VID_1D9D&PID_1011"))
                {
                    TestConnection(true);
                    NotifyMessage notifyMessage = new();
                    notifyMessage.ShowMessage("Sigma Docking Station", "wurde angeschlossen");
                }
            }
            else if (e.EventType == EventType.DeviceRemoveComplete)
            {
                if (e.Device != null && e.Device.Name.Contains("USB#VID_1D9D&PID_1011"))
                {
                    TestConnection(false);
                    NotifyMessage notifyMessage = new();
                    notifyMessage.ShowMessage("Sigma Docking Station", "Verbindung wurde getrennt");
                }
            }
        }

        private void TestConnection(bool connect)
        {
            if (connect)
            {
                dsConnectedToolStripStatusLabel.Visible = true;
            }
            else
            {
                dsConnectedToolStripStatusLabel.Visible = false;
            }
        }

        /// <summary>
        /// Basic settings from the user. Saved when application closes, loaded with the application.
        /// </summary>
        private void LoadSettings()
        {
            if (Properties.Settings.Default.UseAltimeter == true)
            {
                useAltimeterCheckBox.Checked = true;
            }
            else { useAltimeterCheckBox.Checked = false; }
            if (Properties.Settings.Default.UseCadence == true)
            {
                useCadenceCheckBox.Checked = true;
            }
            else { useCadenceCheckBox.Checked = false; }
            setting_cityComboBox.SelectedIndex = setting_cityComboBox.FindStringExact(Properties.Settings.Default.StdCity); 
            setting_continentComboBox.SelectedIndex = setting_continentComboBox.FindStringExact(Properties.Settings.Default.StdContinent);
            setting_routeComboBox.SelectedIndex = setting_routeComboBox.FindStringExact(Properties.Settings.Default.StdRoute);
            setting_vehicleComboBox.SelectedIndex = setting_vehicleComboBox.FindStringExact(Properties.Settings.Default.StdVehicle);
            setting_countryComboBox.SelectedIndex = setting_countryComboBox.FindStringExact(Properties.Settings.Default.StdCountry);
            setting_bundeslandComboBox.SelectedIndex = setting_bundeslandComboBox.FindStringExact(Properties.Settings.Default.StdBundesland);
            setting_MapProviderComboBox.SelectedIndex = setting_MapProviderComboBox.FindStringExact(Properties.Settings.Default.MapProvider);
            if (Properties.Settings.Default.WindowLocation != new Point(0, 0))
                this.Location = Properties.Settings.Default.WindowLocation;
            else this.Location = new Point(50, 50);
            if (Properties.Settings.Default.WindowSize != new Size(800,500))
                this.Size = Properties.Settings.Default.WindowSize;
            else this.Size = new Size(1000, 600);
            googleEarthPath.Text = Properties.Settings.Default.GoogleEarth;
            imageFolderPath.Text = Properties.Settings.Default.ImageFolder;
            toolbarToolStripMenuItem.Checked = Properties.Settings.Default.ShowToolbar;
            mainToolStrip.Location = Properties.Settings.Default.ToolbarLocation;
            mainToolStrip.Size = Properties.Settings.Default.ToolbarSize;
            hilfeToolStripMenuItem2.Checked = Properties.Settings.Default.ShowHelp;
            mainToolStrip.Visible = toolbarToolStripMenuItem.Checked;
            willkommenseiteToolStripMenuItem.Checked = Properties.Settings.Default.ShowWelcomeForm;
            imageEditorPath.Text = Properties.Settings.Default.ImageEditorPath;
            imageEditorTextBox.Text = Properties.Settings.Default.ImageEditorName;
            notificationCheckBox.Checked = Properties.Settings.Default.ShowNotifyIcon;
            timerMaskedTextBox.Text = Properties.Settings.Default.NotifyTime.ToString();
            ShowHelp();
        }

        /// <summary>
        /// When the MainForm has loaded, optionally the WelcomeForm is shown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ShowWelcomeForm)
            {
                WelcomeForm welcomeForm = new();
                welcomeForm.ShowDialog();
            }
            springToolStripStatusLabel.Text = String.Empty;
        }

        /// <summary>
        /// Enables or disables visibility of Help items.
        /// </summary>
        private void ShowHelp()
        {
            if (hilfeToolStripMenuItem2.Checked)
            {
                toolStripSeparator2.Visible = true;
                helpToolStripButton.Visible = true;
                infoToolStripButton.Visible = true;
                hilfeToolStripMenuItem.Visible = true;
            }
            else
            {
                toolStripSeparator2.Visible = false;
                helpToolStripButton.Visible = false;
                infoToolStripButton.Visible = false;
                hilfeToolStripMenuItem.Visible = false;
            }
        }

        private void DisableTabPages()
        {
            mainStatusLabel.Text = "";
            mainTabControl.Visible = false;
            vehicleTabControl.Visible = false;
            setupTabControl.Visible = false;
            cityTabControl.Visible = false;
            routeTabControl.Visible = false;

            DisableAnsichtChecked();
        }

        private void DisableAnsichtChecked()
        {
            datenToolStripMenuItem.Checked = false;
            startseiteToolStripMenuItem.Checked = false;
            fahrzeugeToolStripMenuItem.Checked = false;
            fahrzeugeToolStripMenuItem1.Checked = false;
            einstellungenToolStripMenuItem.Checked = false;
            städteToolStripMenuItem.Checked = false;
            streckenToolStripMenuItem.Checked = false;
            streckenToolStripMenuItem1.Checked = false;
            personenToolStripMenuItem.Checked = false;
            zieleToolStripMenuItem.Checked = false;
            notizenToolStripMenuItem.Checked = false;
            kalenderToolStripMenuItem.Checked = false;
        }

        private void ToolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolbarToolStripMenuItem.Checked = !toolbarToolStripMenuItem.Checked;
            mainToolStrip.Visible = toolbarToolStripMenuItem.Checked;
            Properties.Settings.Default.ShowToolbar = toolbarToolStripMenuItem.Checked;
        }

        private void HilfeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            hilfeToolStripMenuItem2.Checked = !hilfeToolStripMenuItem2.Checked;
            Properties.Settings.Default.ShowHelp = hilfeToolStripMenuItem2.Checked;
            ShowHelp();
        }

        private void WillkommenseiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            willkommenseiteToolStripMenuItem.Checked = !willkommenseiteToolStripMenuItem.Checked;
            Properties.Settings.Default.ShowWelcomeForm = willkommenseiteToolStripMenuItem.Checked;
        }
        #endregion

        #region Close application
        private void BeendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ExitToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// During closing of the application all settings are saved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.StdContinent = setting_continentComboBox.Text;
            Properties.Settings.Default.StdCountry = setting_countryComboBox.Text;
            Properties.Settings.Default.StdCity = setting_cityComboBox.Text;
            Properties.Settings.Default.StdBundesland = setting_bundeslandComboBox.Text;
            Properties.Settings.Default.StdRoute = setting_routeComboBox.Text;
            Properties.Settings.Default.StdVehicle = setting_vehicleComboBox.Text;
            Properties.Settings.Default.GoogleEarth = googleEarthPath.Text;
            Properties.Settings.Default.ImageFolder = imageFolderPath.Text;
            Properties.Settings.Default.WindowLocation = this.Location;
            Properties.Settings.Default.WindowSize = this.Size;
            Properties.Settings.Default.ShowHelp = hilfeToolStripMenuItem2.Checked;
            Properties.Settings.Default.ShowToolbar = toolbarToolStripMenuItem.Checked;
            Properties.Settings.Default.ToolbarLocation = mainToolStrip.Location;
            Properties.Settings.Default.ToolbarSize = mainToolStrip.Size;
            Properties.Settings.Default.CurrentUserName = "";
            Properties.Settings.Default.CurrentUserID = -1;
            Properties.Settings.Default.ShowNotifyIcon = notificationCheckBox.Checked;
            Properties.Settings.Default.NotifyTime = Convert.ToInt32(timerMaskedTextBox.Text);
            Properties.Settings.Default.Save();

            if (Properties.Settings.Default.UseSigmaDockingStation)
            {
                /*UsbDeviceNotifier.Enabled = false;  // Disable the device notifier
                // Unhook the device notifier event
                UsbDeviceNotifier.OnDeviceNotify -= OnDeviceNotifyEvent;
                if (MyUsbDevice != null)
                {
                    MyUsbDevice.Close();
                    MyUsbDevice = null;
                }*/
            }
        }
        #endregion

        #region Startpage
        private void StartseiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowStartPage();
        }

        private void HomeToolStripButton_Click(object sender, EventArgs e)
        {
            ShowStartPage();
        }

        private void ShowStartPage()
        {
            ShowMainTab(0);
            startseiteToolStripMenuItem.Checked = true;
            Properties.Settings.Default.UseAltimeter = useAltimeterCheckBox.Checked;
            altitudeTextBox.Enabled = Properties.Settings.Default.UseAltimeter;
            maxAltTextBox.Enabled = Properties.Settings.Default.UseAltimeter;
            cadenceTextBox.Enabled = Properties.Settings.Default.UseCadence;
            GetPersons();
        }

        private void ShowMainTab(int page)
        {
            DisableTabPages();
            mainTabControl.Visible = true;
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.SelectedIndex = page;
        }

        private void GetPersons()
        {
            SqlConnection con1;
            List<Person> data = [];
            personsListBox.DisplayMember = "Text";
            personsListBox.ValueMember = "Value";
            
            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new())
                    {
                        com1.CommandText = @"SELECT * FROM Persons WHERE [User] = " + Properties.Settings.Default.CurrentUserID.ToString();
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using SqlDataReader reader1 = com1.ExecuteReader();
                        while (reader1.Read())
                        {
                            if (reader1.GetInt32(0) != Properties.Settings.Default.CurrentUserID)
                            {
                                //TODO: new Person(id) verwenden
                                data.Add(new Person(reader1.GetInt32(0), reader1.GetString(2), reader1.GetString(3)));
                            }
                        }
                        reader1.Close();
                    }
                    con1.Close();
                }
                personsListBox.DataSource = data;
                if (personsListBox.Items.Count > 0)
                {
                    personsListBox.SetSelected(0, false);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Laden der Person");
            }
        }
        #endregion

        #region Settings
        private void EinstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSetup();
        }

        private void SettingsToolStripButton_Click(object sender, EventArgs e)
        {
            ShowSetup();
        }

        /// <summary>
        /// Load setup into the main window.
        /// </summary>
        private void ShowSetup()
        {
            DisableTabPages();
            setupTabControl.Visible = true;
            setupTabControl.Dock = DockStyle.Fill;
            einstellungenToolStripMenuItem.Checked = true;
        }

        /// <summary>
        /// Switch if altimeter should be used or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UseAltimeterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (useAltimeterCheckBox.Checked)
            {
                Properties.Settings.Default.UseAltimeter = true;
            }
            else
            {
                Properties.Settings.Default.UseAltimeter = false;
            }
        }

        private void Setting_continentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.StdContinent = setting_continentComboBox.Text;
        }

        private void ImageFolderButton_Click(object sender, EventArgs e)
        {
            if (imageFolderPath.Text != "") imageFolderBrowserDialog.SelectedPath = imageFolderPath.Text;

            if (imageFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                imageFolderPath.Text = imageFolderBrowserDialog.SelectedPath;
                Properties.Settings.Default.ImageFolder = imageFolderPath.Text;
            }
        }

        /// <summary>
        /// Switch GPS setting between angle and decimal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GpsFormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gpsFormatComboBox.SelectedText == "Grad")
            {
                Properties.Settings.Default.GPSCoordAngle = true;
            }
            else Properties.Settings.Default.GPSCoordAngle = false;
        }

        /// <summary>
        /// Switch if cadence should be used or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UseCadenceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (useCadenceCheckBox.Checked)
            {
                Properties.Settings.Default.UseCadence = true;
            }
            else
            {
                Properties.Settings.Default.UseCadence = false;
            }
        }
        #endregion

        #region MainTabControl: Pages
        private void StädteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCities();
        }

        /*private void showCities()
        {
            disableTabPages();
            cityTabControl.Visible = true;
            cityTabControl.Dock = DockStyle.Fill;
            städteToolStripMenuItem.Checked = true;
        }*/

        private void ShowCities()
        {
            ShowMainTab(4);
            städteToolStripMenuItem.Checked = true;
        }

        private void StreckenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowRouteTypes();
        }

        private void ShowRouteTypes()
        {
            DisableTabPages();
            routeTabControl.Visible = true;
            routeTabControl.Dock= DockStyle.Fill;
            streckenToolStripMenuItem.Checked = true;
        }

        private void StreckenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowRoutes();
        }

        private void ShowRoutes()
        {
            ShowMainTab(2);
            streckenToolStripMenuItem1.Checked = true;
        }

        private void DatenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowData();
        }

        private void ShowData()
        {
            ShowMainTab(1);
            datenToolStripMenuItem.Checked = true;
        }  

        private void FahrzeugeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowVehicles();
        }

        private void ShowVehicles()
        {
            ShowMainTab(3);
            fahrzeugeToolStripMenuItem1.Checked = true;
        }

        private void FahrzeugeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowVehicleTypes();
        }

        private void ShowVehicleTypes()
        {
            DisableTabPages();
            vehicleTabControl.Visible = true;
            vehicleTabControl.Dock = DockStyle.Fill;
            fahrzeugeToolStripMenuItem.Checked = true;
        }

        private void PersonenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMainTab(5);
            personenToolStripMenuItem.Checked = true;
        }

        private void ZieleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowGoalTab();
        }

        private void ShowGoalTab()
        {
            ShowMainTab(6);
            zieleToolStripMenuItem.Checked = true;
        }

        private void NotizenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMainTab(7);
            notizenToolStripMenuItem.Checked = true;
        }
        #endregion

        #region Calendar
        private void KalenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCalendar();
        }

        private void CalendarToolStripButton_Click(object sender, EventArgs e)
        {
            ShowCalendar();
        }

        private void ShowCalendar()
        {
            ShowMainTab(8);
            kalenderToolStripMenuItem.Checked = true;
        }

        private void MainCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            MainCalendar_ShowDetails();
        }

        private void MainCalendar_ShowDetails()
        {
            SqlConnection con1;
            calendarDetailsRichTextBox.Text = "";
            foreach (DateTime days in mainCalendar.AnnuallyBoldedDates)
            {
                if (days.Month == mainCalendar.SelectionStart.Month
                    && days.Day == mainCalendar.SelectionStart.Day)
                {
                    using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        con1.Open();
                        using (SqlCommand com1 = new())
                        {
                            com1.CommandText = @"SELECT * FROM BirthdateView WHERE [User] = @user";
                            com1.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;
                            using SqlDataReader reader1 = com1.ExecuteReader();
                            while (reader1.Read())
                            {
                                if (reader1[2].ToString() != String.Empty)
                                {
                                    if (Convert.ToDateTime(reader1[2]).Month == mainCalendar.SelectionStart.Month
                                        && Convert.ToDateTime(reader1[2]).Day == mainCalendar.SelectionStart.Day)
                                    {
                                        calendarDetailsRichTextBox.Text += mainCalendar.SelectionStart.ToString("dd.MM.yyyy") + ": ";
                                        calendarDetailsRichTextBox.Text += "Geburtstag von ";
                                        calendarDetailsRichTextBox.Text += reader1[0].ToString() + " ";
                                        calendarDetailsRichTextBox.Text += reader1[1].ToString();
                                    }
                                }
                            }
                            reader1.Close();
                        }
                        // Birthday of current user.
                        using (SqlCommand com1 = new())
                        {
                            com1.CommandText = @"SELECT * FROM Persons WHERE id = @id";
                            com1.Parameters.Add("@id", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;
                            using SqlDataReader reader1 = com1.ExecuteReader();
                            while (reader1.Read())
                            {
                                if (reader1[5].ToString() != String.Empty)
                                {
                                    if (Convert.ToDateTime(reader1[5]).Month == mainCalendar.SelectionStart.Month
                                        && Convert.ToDateTime(reader1[5]).Day == mainCalendar.SelectionStart.Day)
                                    {
                                        calendarDetailsRichTextBox.Text += mainCalendar.SelectionStart.ToString("dd.MM.yyyy") + ": ";
                                        calendarDetailsRichTextBox.Text += "Du hast Geburtstag! Herzlichen Glückwunsch.";
                                    }
                                }
                            }
                            reader1.Close();
                        }
                        con1.Close();
                    }
                }
            }
            foreach (DateTime days in mainCalendar.BoldedDates)
            {
                if (days.Month == mainCalendar.SelectionStart.Month
                    && days.Day == mainCalendar.SelectionStart.Day)
                {
                    using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        con1.Open();
                        using (SqlCommand com1 = new())
                        {
                            com1.CommandText = @"SELECT * FROM DateView WHERE [User] = @user";
                            com1.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;
                            using SqlDataReader reader1 = com1.ExecuteReader();
                            while (reader1.Read())
                            {
                                if (reader1[0].ToString() != String.Empty)
                                {
                                    if (Convert.ToDateTime(reader1[0]).Month == mainCalendar.SelectionStart.Month
                                        && Convert.ToDateTime(reader1[0]).Day == mainCalendar.SelectionStart.Day)
                                    {
                                        string manufacturer = "";
                                        if (reader1[2].ToString() != String.Empty)
                                        {
                                            manufacturer = GetDatabaseEntry("Companies", "CompanyName", Convert.ToInt32(
                                                GetDatabaseEntry("Vehicles", "Manufacturer", Convert.ToInt32(reader1[2]))));
                                            manufacturer += " ";
                                        }
                                        calendarDetailsRichTextBox.Text += mainCalendar.SelectionStart.ToString("dd.MM.yyyy") + ": ";
                                        calendarDetailsRichTextBox.Text += "Tour mit " + manufacturer;
                                        calendarDetailsRichTextBox.Text += GetDatabaseEntry("Vehicles", "VehicleName", "Id = " + reader1[2].ToString());
                                        calendarDetailsRichTextBox.Text += " (" + reader1[3].ToString() + "km";
                                        if (reader1[4].ToString() != String.Empty)
                                        {
                                            calendarDetailsRichTextBox.Text += " - " + reader1[4].ToString() + ")";
                                        }
                                        else
                                        {
                                            calendarDetailsRichTextBox.Text += ")";
                                        }
                                    }
                                }
                            }
                            reader1.Close();
                        }
                        using (SqlCommand com1 = new())
                        {
                            com1.CommandText = @"SELECT * FROM Goals WHERE [User] = @user";
                            com1.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;
                            using SqlDataReader reader1 = com1.ExecuteReader();
                            while (reader1.Read())
                            {
                                if (reader1[3].ToString() != String.Empty)
                                {
                                    if (Convert.ToDateTime(reader1[3]).Month == mainCalendar.SelectionStart.Month
                                        && Convert.ToDateTime(reader1[3]).Day == mainCalendar.SelectionStart.Day)
                                    {
                                        string achieved = "";
                                        if (reader1[4].ToString() != String.Empty)
                                        {
                                            if (reader1[4].ToString() == "0")
                                            {
                                                achieved = "(nicht erreicht)";
                                            }
                                            else if (reader1[4].ToString() == "1")
                                            {
                                                achieved = "(erreicht)";
                                            }
                                        }
                                        calendarDetailsRichTextBox.Text += mainCalendar.SelectionStart.ToString("dd.MM.yyyy") + ": ";
                                        calendarDetailsRichTextBox.Text += "Ziel: \"" + reader1.GetString(1) + "\" ";
                                        calendarDetailsRichTextBox.Text += achieved;
                                    }
                                }
                            }
                            reader1.Close();
                        }
                        con1.Close();
                    }
                }
            }
        }

        private void MainCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            //MessageBox.Show(e.Start.ToString());
        }
        #endregion

        #region Google Earth
        /// <summary>
        /// Opens Google Earth in a browser window to install the latest version.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstallierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.google.com/intl/de/earth/about/versions/"));
        }

        private void StartenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartGoogleEarth();
        }

        private void EarthToolStripButton_Click(object sender, EventArgs e)
        {
            StartGoogleEarth();
        }

        private void StartGoogleEarth()
        {
            try
            {
                if (Properties.Settings.Default.GoogleEarth != "")
                {
                    Process.Start(new ProcessStartInfo(Properties.Settings.Default.GoogleEarth + "\\googleearth.exe"));
                }
                else
                {
                    mainStatusLabel.Text = "Google Earth muss erst installiert oder in den Einstellungen angepasst werden!";
                }
            }
            catch (Exception exception) { ShowErrorMessage(exception.Message, "Fehler beim Starten von Google Earth"); }
        }

        private void FolderSearchButton_Click(object sender, EventArgs e)
        {
            if (googleEarthPath.Text != "") googleEarthFolderBrowserDialog.SelectedPath = googleEarthPath.Text;

            if (googleEarthFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                googleEarthPath.Text = googleEarthFolderBrowserDialog.SelectedPath;
                Properties.Settings.Default.GoogleEarth = googleEarthPath.Text;
            }
        }
        #endregion       

        #region Show InfoForm
        private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowInfoForm();
        }

        private void InfoToolStripButton_Click(object sender, EventArgs e)
        {
            ShowInfoForm();
        }

        private void ShowInfoForm()
        {
            AboutBox aboutBox = new();
            aboutBox.ShowDialog();
        }
        #endregion

        #region VehicleForm
        private void FahrzeugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowVehicleForm();
        }

        private void VehicleToolStripButton_Click(object sender, EventArgs e)
        {
            ShowVehicleForm();
        }

        private void ShowVehicleForm()
        {
            VehicleForm vehicleForm = new();
            if (vehicleForm.ShowDialog() == DialogResult.OK)
            {
                RefreshComboBoxes();
            }
        }
        #endregion

        #region CountryForm
        private void LandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCountryForm();
        }

        private void CountryToolStripButton_Click(object sender, EventArgs e)
        {
            ShowCountryForm();
        }

        private void ShowCountryForm()
        {
            CountryForm countryForm = new();
            if (countryForm.ShowDialog() == DialogResult.OK)
            {
                RefreshComboBoxes();
            }
        }
        #endregion

        #region CityForm
        private void StadtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCityForm();
        }

        private void CityToolStripButton_Click(object sender, EventArgs e)
        {
            ShowCityForm();
        }

        private void ShowCityForm()
        {
            CityForm cityForm = new();
            if (cityForm.ShowDialog() == DialogResult.OK)
            {
                RefreshComboBoxes();
            }
        }
        #endregion

        #region CompanyForm
        private void HerstellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCompanyForm();
        }

        private void CompanyToolStripButton_Click(object sender, EventArgs e)
        {
            ShowCompanyForm();
        }

        private void ShowCompanyForm()
        {
            CompanyForm companyForm = new();
            if (companyForm.ShowDialog() == DialogResult.OK)
            {
                RefreshComboBoxes();
            }
        }
        #endregion

        #region RouteForm
        private void StreckeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowRouteForm();
        }

        private void RouteToolStripButton_Click(object sender, EventArgs e)
        {
            ShowRouteForm();
        }

        private void ShowRouteForm()
        {
            RouteForm routeForm = new();
            if (routeForm.ShowDialog() == DialogResult.OK)
            {
                RefreshComboBoxes();
            }
        }
        #endregion

        #region HelpForm
        private void HelpToolStripButton_Click(object sender, EventArgs e)
        {
            ShowHelpForm();
        }

        private void HilfeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowHelpForm();
        }

        private void ShowHelpForm()
        {
            NewHelpForm helpForm = new();
            helpForm.Show();
        }
        #endregion

        #region ImageViewer
        private void ImageViewerToolStripButton_Click(object sender, EventArgs e)
        {
            ShowImageViewer();
        }

        private void BildbetrachterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowImageViewer();
        }

        private void ShowImageViewer()
        {
            if (openImageDialog.ShowDialog() == DialogResult.OK)
            {
                ImageViewerForm imageViewerForm = new()
                {
                    Filename = openImageDialog.FileName
                };
                imageViewerForm.Show();
            }
        }
        #endregion

        #region Entfaltung
        private void EntfaltungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEntfaltung();
        }

        private void EntfaltungToolStripButton_Click(object sender, EventArgs e)
        {
            ShowEntfaltung();
        }

        private void ShowEntfaltung()
        {
            EntfaltungForm entfaltung = new();
            if (entfaltung.ShowDialog() == DialogResult.OK)
            {
                showDataBackgroundWorker.RunWorkerAsync();
            }
        }
        #endregion

        #region Background Worker
        private void MainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 0 = Daten eingeben
            // 1 = Daten anzeigen
            // 2 = Strecken anzeigen
            // 3 = Fahrzeuge anzeigen
            // 4 = Städte anzeigen
            // 5 = Personen anzeigen
            // 6 = Ziele anzeigen
            // 7 = Notizen anzeigen
            // 8 = Kalender
            tabPage = mainTabControl.SelectedIndex;
            DisableAnsichtChecked();
            switch (tabPage)
            {
                case 0:
                    startseiteToolStripMenuItem.Checked = true;
                    break;
                case 1:
                    datenToolStripMenuItem.Checked = true;
                    break;
                case 2:
                    streckenToolStripMenuItem1.Checked = true;
                    break;
                case 3:
                    fahrzeugeToolStripMenuItem1.Checked = true;
                    break;
                case 4:
                    städteToolStripMenuItem.Checked = true;
                    break;
                case 5:
                    personenToolStripMenuItem.Checked = true;
                    break;
                case 6:
                    zieleToolStripMenuItem.Checked = true;
                    break;
                case 7:
                    notizenToolStripMenuItem.Checked = true;
                    break;
                case 8:
                    kalenderToolStripMenuItem.Checked = true;
                    break;
                default:
                    break;
            }
            if (showDataBackgroundWorker.IsBusy)
            {
                while (showDataBackgroundWorker.IsBusy)
                {
                    System.Threading.Thread.Sleep(500);
                }
                RefreshComboBoxes();
            }
            else RefreshComboBoxes();
        }

        /// <summary>
        /// Insert daily tours into database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (edit_data == false)
                {
                    int newId = NextId("Tour");
                    int maxAlt = 0;
                    int alt = 0;
                    int cad = 0;
                    if (altitudeTextBox.Text != String.Empty)
                    {
                        alt = Convert.ToInt32(altitudeTextBox.Text);
                    }
                    if (maxAltTextBox.Text != String.Empty)
                    {
                        maxAlt = Convert.ToInt32(maxAltTextBox.Text);
                    }
                    if (cadenceTextBox.Text != String.Empty)
                    {
                        cad = Convert.ToInt32(cadenceTextBox.Text);
                    }
                    DateTime dt = DateTime.Parse(timeTextBox.Text, CultureInfo.InvariantCulture);

                    string persons = "";
                    if (personsListBox.SelectedItems.Count > 0)
                    {
                        foreach (Person item in personsListBox.SelectedItems)
                        {
                            persons += item.Value.ToString() + ";";
                        }
                    }
                    decimal avg = 0.0M;
                    if (avgTextBox.Text != avgTextBox.Mask)
                    {
                        avg = Convert.ToDecimal(avgTextBox.Text);
                    }
                    decimal vm = 0.0M;
                    if (vmaxTextBox.Text != vmaxTextBox.Mask)
                    {
                        vm = Convert.ToDecimal(vmaxTextBox.Text);
                    }

                    DataSetTableAdapters.TourTableAdapter adapter = new();
                    int res = adapter.Insert(newId,
                        Convert.ToDateTime(tourDateTimePicker.Text),
                        Convert.ToInt32(routeComboBox.SelectedValue),
                        Convert.ToInt32(vehicleComboBox.SelectedValue),
                        Convert.ToDecimal(kmTextBox.Text),
                        dt.TimeOfDay,
                        avg,
                        vm,
                        alt,
                        maxAlt,
                        cad,
                        remarkRichTextBox.Text,
                        null,
                        persons,
                        DateTime.Now,
                        DateTime.Now,
                        Properties.Settings.Default.CurrentUserID);

                    if (res != 0)
                    {
                        kmTextBox.Text = "";
                        timeTextBox.Text = "";
                        avgTextBox.Text = "";
                        vmaxTextBox.Text = "";
                        altitudeTextBox.Text = "";
                        maxAltTextBox.Text = "";
                        cadenceTextBox.Text = "";
                        remarkRichTextBox.Text = "";
                        personsListBox.SelectedItems.Clear();
                    }
                }
                else        // Edit data
                {
                    var sql = @"UPDATE Tour SET Date = @Date, Route = @Route, Vehicle = @Vehicle, Km = @Km, Time = @Time, AverageSpeed = @AvgSpeed, " +
                        "MaxSpeed = @MaxSpeed, AccumulatedHeight = @Height, MaxAltitude = @MaxAlt, Cadence = @Cadence, Remark = @Remark , Persons = @persons, LastChanged = @last " +
                        "WHERE Id = " + current_tour.ToString();
                    try
                    {
                        using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                        {
                            using var command = new SqlCommand(sql, connection);
                            int maxAlt = 0;
                            int alt = 0;
                            int cad = 0;
                            if (altitudeTextBox.Text != String.Empty)
                            {
                                alt = Convert.ToInt32(altitudeTextBox.Text);
                            }
                            if (maxAltTextBox.Text != String.Empty)
                            {
                                maxAlt = Convert.ToInt32(maxAltTextBox.Text);
                            }
                            if (cadenceTextBox.Text != String.Empty)
                            {
                                cad = Convert.ToInt32(cadenceTextBox.Text);
                            }
                            string persons = "";
                            if (personsListBox.SelectedItems.Count > 0)
                            {
                                foreach (Person item in personsListBox.SelectedItems)
                                {
                                    persons += item.Value.ToString() + ";";
                                }
                            }
                            decimal avg = 0.0M;
                            if (avgTextBox.Text != avgTextBox.Mask)
                            {
                                avg = Convert.ToDecimal(avgTextBox.Text);
                            }
                            decimal vm = 0.0M;
                            if (vmaxTextBox.Text != vmaxTextBox.Mask)
                            {
                                vm = Convert.ToDecimal(vmaxTextBox.Text);
                            }
                            command.Parameters.Add("@Date", SqlDbType.Date).Value = Convert.ToDateTime(tourDateTimePicker.Text);
                            command.Parameters.Add("@Route", SqlDbType.Int).Value = Convert.ToInt32(routeComboBox.SelectedValue);
                            command.Parameters.Add("@Vehicle", SqlDbType.Int).Value = Convert.ToInt32(vehicleComboBox.SelectedValue);
                            command.Parameters.Add("@Km", SqlDbType.Decimal).Value = Convert.ToDecimal(kmTextBox.Text);
                            command.Parameters.Add("@Time", SqlDbType.Time).Value = DateTime.Parse(timeTextBox.Text, CultureInfo.InvariantCulture).TimeOfDay;
                            command.Parameters.Add("@AvgSpeed", SqlDbType.Decimal).Value = avg;
                            command.Parameters.Add("@MaxSpeed", SqlDbType.Decimal).Value = vm;
                            command.Parameters.Add("@Height", SqlDbType.Int).Value = alt;
                            command.Parameters.Add("@MaxAlt", SqlDbType.Int).Value = maxAlt;
                            command.Parameters.Add("@Cadence", SqlDbType.Int).Value = cad;
                            command.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = remarkRichTextBox.Text;
                            command.Parameters.Add("@persons", SqlDbType.NVarChar).Value = persons;
                            command.Parameters.Add("@last", SqlDbType.DateTime).Value = DateTime.Now;

                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                        kmTextBox.Text = "";
                        timeTextBox.Text = "";
                        avgTextBox.Text = "";
                        vmaxTextBox.Text = "";
                        altitudeTextBox.Text = "";
                        maxAltTextBox.Text = "";
                        remarkRichTextBox.Text = "";
                        personsListBox.SelectedItems.Clear();
                        addButton.Text = "Hinzufügen";
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage(ex.Message, "Fehler beim Update (Tägliche Tour)");
                    }
                    edit_data = false;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Eingeben von Daten (Tägliche Tour)");
            }
        }
       
        /// <summary>
        /// Background Worker to count database entries depending on selected tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDataBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (tabPage != 0)
                {
                    int result = -1;
                    switch (tabPage)
                    {
                        case 1:
                            result = CountIds("Tour");
                            break;
                        case 2:
                            result = CountIds("Routes");
                            break;
                        case 3:
                            result = CountIds("Vehicles"); 
                            break;
                        case 4:
                            result = CountIds("Cities", false);
                            break;
                        case 5:
                            result = CountIds("Persons");
                            break;
                        case 6:
                            result = CountIds("Goals");
                            break;
                        case 7:
                            result = CountIds("Notes");
                            break;
                    }
                    e.Result = result;

                    if (result != 0)
                    {
                        switch (tabPage)
                        {
                            case 1:
                                if (current_tour == -1)
                                {
                                    tour_ids = GetObjectIds("Tour", "Date");
                                    if (tour_ids.Length > 0)
                                        tour_id = 0;
                                    else
                                        tour_id = -1;
                                }
                                break;
                            case 2:
                                if (current_route == -1)
                                {
                                    route_ids = GetObjectIds("Routes", "RouteName");
                                    if (route_ids.Length > 0)
                                        route_id = 0;
                                    else
                                        route_id = -1;
                                }
                                break;
                            case 3:
                                if (current_vehicle == -1)
                                {
                                    vehicle_ids = GetObjectIds("Vehicles", "VehicleName");
                                    if (vehicle_ids.Length > 0)
                                        vehicle_id = 0;
                                    else
                                        vehicle_id = -1;
                                }
                                break;
                            case 4:
                                if (current_city == -1)
                                {
                                    city_ids = GetObjectIds("Cities", "CityName", false);
                                    if (city_ids.Length > 0)
                                    {
                                        city_id = 0;
                                    }
                                    else
                                        city_id = -1;
                                }
                                break;
                            case 5:
                                if (current_person == -1)
                                {
                                    person_ids = GetObjectIds("Persons", "Lastname");
                                    if (person_ids.Length > 0)
                                        person_id = 0;
                                    else
                                        person_id = -1;
                                }
                                break;
                            case 6:
                                if (current_goal == -1)
                                {
                                    goal_ids = GetObjectIds("Goals", "Date");
                                    if (goal_ids.Length > 0)
                                        goal_id = 0;
                                    else goal_id = -1;
                                }
                                break;
                            case 7:
                                if (current_note == -1)
                                {
                                    note_ids = GetObjectIds("Notes", "Title");
                                    if (note_ids.Length > 0)
                                        note_id = 0;
                                    else note_id = -1;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    e.Result = -1;
                }
            }
            catch (System.Exception ex)
            {
                ShowErrorMessage(ex.Message, "BG-Worker DoWork...");
            }
        }

        /// <summary>
        /// After the background worker did its job, the GUI is refreshed according to the associated tab page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDataBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int result = (int)e.Result;
            if (tabPage != 0)
            {
                SqlConnection myConnection, con1;
                SqlParameter idParam;

                //Add id portion
                idParam = new SqlParameter
                {
                    ParameterName = "@id"
                };
                /*if (current_tour != 0 && tabPage == 1)
                    idParam.Value = current_tour;
                else if (current_route != 0 && tabPage == 2)
                    idParam.Value = current_route;
                else if (current_vehicle != 0 && tabPage == 3)
                    idParam.Value = current_vehicle;
                else if (current_city != 0 && tabPage == 4)
                    idParam.Value = current_city;
                else if (current_person != 0 && tabPage == 5)
                    idParam.Value = current_person;
                else if (current_goal != 0 && tabPage == 6)
                    idParam.Value = current_goal;
                else if (current_note != 0 && tabPage == 7)
                    idParam.Value = current_note;
                else
                    idParam.Value = 0;*/
                string whereclause = " WHERE ";
                string sqlquery = "SELECT * FROM ";
                string tablename = "";

                try
                {
                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        myConnection.Open();
                        using (SqlCommand myCommand = new())
                        {
                            switch (tabPage)
                            {
                                case 1:
                                    tablename = "Tour";
                                    if (tour_id >= 0 && tour_ids != null)
                                        idParam.Value = tour_ids[tour_id];
                                    else
                                        idParam.Value = -1;
                                    cnt_tours = result;
                                    dataRouteLabel.Text = "";
                                    dataVehicleLabel.Text = "";
                                    dataKmLabel.Text = "";
                                    dataTimeLabel.Text = "";
                                    dataAvgLabel.Text = "";
                                    dataVmaxLabel.Text = "";
                                    dataAltLabel.Text = "";
                                    dataMaxAltLabel.Text = "";
                                    dataRemarkRichTextBox.Text = "";
                                    personsRichTextBox.Text = "";
                                    firstToolStripButton.Enabled = false;
                                    previousToolStripButton.Enabled = false;
                                    nextToolStripButton.Enabled = false;
                                    lastToolStripButton.Enabled = false;
                                    editToolStripButton.Enabled = false;
                                    deleteToolStripButton.Enabled = false;
                                    galleryToolStripSeparator.Visible = false;
                                    showGalleryToolStripButton.Visible = false;
                                    current_gallery = -1;
                                    if (result == 0)
                                    {
                                        dataTitleLabel.Text = "Keine Tagestouren vorhanden!";
                                        dataRouteLabel.Text = "Bitte erst mindestens eine Tagestour erstellen.";
                                    }
                                    else if (result == 1)
                                    {
                                        editToolStripButton.Enabled = true;
                                        deleteToolStripButton.Enabled = true;
                                    }
                                    else if (result >= 2)
                                    {
                                        editToolStripButton.Enabled = true;
                                        deleteToolStripButton.Enabled = true;
                                        // First element
                                        if (tour_ids[tour_id] == tour_ids.First())
                                        {
                                            nextToolStripButton.Enabled = true;
                                            lastToolStripButton.Enabled = true;
                                        }
                                        // Last element
                                        else if (tour_ids[tour_id] == tour_ids.Last())
                                        {
                                            firstToolStripButton.Enabled = true;
                                            previousToolStripButton.Enabled = true;
                                        }
                                        else
                                        {
                                            firstToolStripButton.Enabled = true;
                                            previousToolStripButton.Enabled = true;
                                            nextToolStripButton.Enabled = true;
                                            lastToolStripButton.Enabled = true;
                                        }
                                    }
                                    break;
                                case 2:
                                    tablename = "Routes";
                                    if (route_id >= 0 && route_ids != null)
                                        idParam.Value = route_ids[route_id];
                                    else
                                        idParam.Value = -1;
                                    cnt_routes = result;
                                    cityStartLabel.Text = "";
                                    cityEndLabel.Text = "";
                                    citiesLabel.Text = "";
                                    routeTypeLabel.Text = "";
                                    routeRemarkRichTextBox.Text = "";
                                    maxAltLabel.Text = "";
                                    altLabel.Text = "";
                                    routeToolStripButton0.Enabled = false;
                                    routeToolStripButton1.Enabled = false;
                                    routeToolStripButton2.Enabled = false;
                                    routeToolStripButton3.Enabled = false;
                                    routeToolStripButton4.Enabled = false;
                                    routeToolStripButton5.Enabled = false;
                                    if (result == 0)
                                    {
                                        routeTitleLabel.Text = "Keine Strecken vorhanden!";
                                        cityLabel.Text = "Bitte erst mindestens eine Strecke erstellen.";
                                    }
                                    else if (result == 1)
                                    {
                                        routeToolStripButton4.Enabled = true;
                                        routeToolStripButton5.Enabled = true;
                                    }
                                    else if (result >= 2)
                                    {
                                        routeToolStripButton4.Enabled = true;
                                        routeToolStripButton5.Enabled = true;
                                        // First element
                                        if (route_ids[route_id] == route_ids.First())
                                        {
                                            routeToolStripButton2.Enabled = true;
                                            routeToolStripButton3.Enabled = true;
                                        }
                                        // Last element
                                        else if (route_ids[route_id] == route_ids.Last())
                                        {
                                            routeToolStripButton0.Enabled = true;
                                            routeToolStripButton1.Enabled = true;
                                        }
                                        else
                                        {
                                            routeToolStripButton0.Enabled = true;
                                            routeToolStripButton1.Enabled = true;
                                            routeToolStripButton2.Enabled = true;
                                            routeToolStripButton3.Enabled = true;
                                        }
                                    }
                                    break;
                                case 3:
                                    tablename = "Vehicles";
                                    if (vehicle_id >= 0 && vehicle_ids != null)
                                        idParam.Value = vehicle_ids[vehicle_id];
                                    else
                                        idParam.Value = -1;
                                    cnt_vehicles = result;
                                    typeLabel.Text = "";
                                    equipRichTextBox.Text = "";
                                    vecToolStripButton0.Enabled = false;
                                    vecToolStripButton1.Enabled = false;
                                    vecToolStripButton2.Enabled = false;
                                    vecToolStripButton3.Enabled = false;
                                    vecToolStripButton4.Enabled = false;
                                    vecToolStripButton5.Enabled = false;
                                    entfaltungButton.Enabled = false;
                                    if (result == 0)
                                    {
                                        vehicleTitleLabel.Text = "Keine Fahrzeuge vorhanden!";
                                        boughtLabel.Text = "Bitte erst mindestens ein Fahrzeug erstellen.";
                                    }
                                    else if (result == 1)
                                    {
                                        vecToolStripButton4.Enabled = true;
                                        vecToolStripButton5.Enabled = true;
                                    }
                                    else if (result >= 2)
                                    {
                                        vecToolStripButton4.Enabled = true;
                                        vecToolStripButton5.Enabled = true;
                                        // First element
                                        if (vehicle_ids[vehicle_id] == vehicle_ids.First())
                                        {
                                            vecToolStripButton2.Enabled = true;
                                            vecToolStripButton3.Enabled = true;
                                        }
                                        // Last element
                                        else if (vehicle_ids[vehicle_id] == vehicle_ids.Last())
                                        {
                                            vecToolStripButton0.Enabled = true;
                                            vecToolStripButton1.Enabled = true;
                                        }
                                        else
                                        {
                                            //MessageBox.Show("Else " + vehicle_id.ToString() + " " + vehicle_ids.Last().ToString());
                                            vecToolStripButton0.Enabled = true;
                                            vecToolStripButton1.Enabled = true;
                                            vecToolStripButton2.Enabled = true;
                                            vecToolStripButton3.Enabled = true;
                                        }
                                    }
                                    break;
                                case 4:
                                    tablename = "Cities";
                                    if (city_id >= 0 && city_ids != null)
                                        idParam.Value = city_ids[city_id];
                                    else
                                        idParam.Value = -1;
                                    cnt_cities = result;
                                    countryLabel.Text = "";
                                    bundeslandLabel.Text = "";
                                    prefixLabel.Text = "";
                                    kfzLabel.Text = "";
                                    cityLinkLabel.Text = "";
                                    heightLabel.Text = "";
                                    cityRemarkRichTextBox.Text = "";
                                    gpsLabel.Text = "";
                                    city1ToolStripButton.Enabled = false;
                                    city2ToolStripButton.Enabled = false;
                                    city3ToolStripButton.Enabled = false;
                                    city4ToolStripButton.Enabled = false;
                                    city5ToolStripButton.Enabled = false;
                                    city6ToolStripButton.Enabled = false;
                                    if (result == 0)
                                    {
                                        cityNameLabel.Text = "Keine Städte vorhanden!";
                                        countryLabel.Text = "Bitte erst mindestens eine Stadt erstellen.";
                                    }
                                    else if (result == 1)
                                    {
                                        city5ToolStripButton.Enabled = true;
                                        city6ToolStripButton.Enabled = true;
                                    }
                                    else if (result >= 2)
                                    {
                                        city5ToolStripButton.Enabled = true;
                                        city6ToolStripButton.Enabled = true;
                                        // First element
                                        if (city_ids[city_id] == city_ids.First())
                                        {
                                            city3ToolStripButton.Enabled = true;
                                            city4ToolStripButton.Enabled = true;
                                        }
                                        // Last element
                                        else if (city_ids[city_id] == city_ids.Last())
                                        {
                                            city1ToolStripButton.Enabled = true;
                                            city2ToolStripButton.Enabled = true;
                                        }
                                        else
                                        {
                                            city1ToolStripButton.Enabled = true;
                                            city2ToolStripButton.Enabled = true;
                                            city3ToolStripButton.Enabled = true;
                                            city4ToolStripButton.Enabled = true;
                                        }
                                    }
                                    break;
                                case 5:
                                    tablename = "Persons";
                                    if (person_id >= 0 && person_ids != null)
                                        idParam.Value = person_ids[person_id]; 
                                    else
                                        idParam.Value = -1;
                                    cnt_persons = result;
                                    personLabel.Text = "";
                                    usernameLabel.Text = "";
                                    birthdateLabel.Text = "";
                                    str1Label.Text = "";
                                    str2Label.Text = "";
                                    plzCityLabel.Text = "";
                                    userCountryLabel.Text = "";
                                    phoneLabel.Text = "";
                                    emailLinkLabel.Text = "";
                                    personRemarkRichTextBox.Text = "";

                                    persons1ToolStripButton.Enabled = false;
                                    persons2ToolStripButton.Enabled = false;
                                    persons3ToolStripButton.Enabled = false;
                                    persons4ToolStripButton.Enabled = false;
                                    persons5ToolStripButton.Enabled = false;
                                    persons6ToolStripButton.Enabled = false;
                                    if (result == 0)
                                    {
                                        personLabel.Text = "Keine Personen vorhanden!";
                                        usernameLabel.Text = "Bitte erst mindestens eine Person erstellen.";
                                    }
                                    else if (result == 1)
                                    {
                                        persons5ToolStripButton.Enabled = true;
                                        persons6ToolStripButton.Enabled = true;
                                    }
                                    else if (result >= 2)
                                    {
                                        persons5ToolStripButton.Enabled = true;
                                        persons6ToolStripButton.Enabled = true;
                                        // First element
                                        if (person_ids[person_id] == person_ids.First())
                                        {
                                            persons3ToolStripButton.Enabled = true;
                                            persons4ToolStripButton.Enabled = true;
                                        }
                                        // Last element
                                        else if (person_ids[person_id] == person_ids.Last())
                                        {
                                            persons1ToolStripButton.Enabled = true;
                                            persons2ToolStripButton.Enabled = true;
                                        }
                                        else
                                        {
                                            persons1ToolStripButton.Enabled = true;
                                            persons2ToolStripButton.Enabled = true;
                                            persons3ToolStripButton.Enabled = true;
                                            persons4ToolStripButton.Enabled = true;
                                        }
                                    }
                                    break;
                                case 6:
                                    tablename = "Goals";
                                    if (goal_id >= 0 && goal_ids != null)
                                        idParam.Value = goal_ids[goal_id];
                                    else
                                        idParam.Value = -1;
                                    //if (goal_ids != null)
                                    //    idParam.Value = goal_ids[goal_id]; ;
                                    cnt_goals = result;
                                    goalsTitleLabel.Text = "Ziele";
                                    goalTitleLabel.Text = "";
                                    goalCreatedLabel.Text = "";
                                    goalDateTimePicker.Value = DateTime.Now;
                                    doneCheckBox.Checked = false;
                                    goalRemarkRichTextBox.Text = "";

                                    goals1ToolStripButton.Enabled = false;
                                    goals2ToolStripButton.Enabled = false;
                                    goals3ToolStripButton.Enabled = false;
                                    goals4ToolStripButton.Enabled = false;
                                    goals5ToolStripButton.Enabled = false;
                                    goals6ToolStripButton.Enabled = false;
                                    if (result == 0)
                                    {
                                        goalsTitleLabel.Text = "Keine Ziele vorhanden!";
                                        goalTitleLabel.Text = "Bitte erst mindestens ein Ziel erstellen.";
                                    }
                                    else if (result == 1)
                                    {
                                        goals5ToolStripButton.Enabled = true;
                                        goals6ToolStripButton.Enabled = true;
                                    }
                                    else if (result >= 2)
                                    {
                                        goals5ToolStripButton.Enabled = true;
                                        goals6ToolStripButton.Enabled = true;
                                        // First element
                                        if (goal_ids[goal_id] == goal_ids.First())
                                        {
                                            goals3ToolStripButton.Enabled = true;
                                            goals4ToolStripButton.Enabled = true;
                                        }
                                        // Last element
                                        else if (goal_ids[goal_id] == goal_ids.Last())
                                        {
                                            goals1ToolStripButton.Enabled = true;
                                            goals2ToolStripButton.Enabled = true;
                                        }
                                        else
                                        {
                                            goals1ToolStripButton.Enabled = true;
                                            goals2ToolStripButton.Enabled = true;
                                            goals3ToolStripButton.Enabled = true;
                                            goals4ToolStripButton.Enabled = true;
                                        }
                                    }
                                    break;
                                case 7:
                                    tablename = "Notes";
                                    if (note_id >= 0 && note_ids != null)
                                        idParam.Value = note_ids[note_id];
                                    else
                                        idParam.Value = -1;
                                    cnt_notes = result;
                                    notesTitleLabel.Text = "Notizen";
                                    noteTitleLabel.Text = "";
                                    notesRemarkRichTextBox.Text = "";

                                    notes1ToolStripButton.Enabled = false;
                                    notes2ToolStripButton.Enabled = false;
                                    notes3ToolStripButton.Enabled = false;
                                    notes4ToolStripButton.Enabled = false;
                                    notes5ToolStripButton.Enabled = false;
                                    notes6ToolStripButton.Visible = true;
                                    notes6ToolStripButton.Enabled = false;
                                    if (result == 0)
                                    {
                                        notesTitleLabel.Text = "Keine Notizen vorhanden!";
                                        noteTitleLabel.Text = "Bitte erst mindestens eine Notiz erstellen.";
                                    }
                                    else if (result == 1)
                                    {
                                        notes5ToolStripButton.Enabled = true;
                                        notes6ToolStripButton.Enabled = true;
                                    }
                                    else if (result >= 2)
                                    {
                                        notes5ToolStripButton.Enabled = true;
                                        notes6ToolStripButton.Enabled = true;
                                        // First element
                                        if (note_ids[note_id] == note_ids.First())
                                        {
                                            notes3ToolStripButton.Enabled = true;
                                            notes4ToolStripButton.Enabled = true;
                                        }
                                        // Last element
                                        else if (note_ids[note_id] == note_ids.Last())
                                        {
                                            notes1ToolStripButton.Enabled = true;
                                            notes2ToolStripButton.Enabled = true;
                                        }
                                        else
                                        {
                                            notes1ToolStripButton.Enabled = true;
                                            notes2ToolStripButton.Enabled = true;
                                            notes3ToolStripButton.Enabled = true;
                                            notes4ToolStripButton.Enabled = true;
                                        }
                                    }
                                    break;
                                case 8:
                                    tablename = "BirthdateView";
                                    idParam.Value = -1;
                                    break;
                            }
                            sqlquery += tablename;
                            if (tabPage != 8)   // && Convert.ToInt32(idParam.Value) != -1
                            {
                                if (tabPage == 4)
                                {
                                    whereclause += "Id = @id";
                                }
                                else
                                {
                                    whereclause += "Id = @id AND [User] = @user";
                                }
                                myCommand.Parameters.Add("@id", SqlDbType.Int).Value = idParam.Value;
                            }
                            else if (tabPage == 8)
                            {
                                whereclause += " [User] = @user";
                            }
                            if (tabPage == 1)
                            {
                                whereclause += " ORDER BY Date";
                            }
                            myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                            sqlquery += whereclause;
                            myCommand.CommandText = sqlquery;
                            myCommand.CommandType = CommandType.Text;
                            myCommand.Connection = myConnection;
                            DateTime dt = DateTime.Now;
                            DateTime[] monthArray = [dt];
                            for (int i = 0; i < mainCalendar.CalendarDimensions.Width; i++)
                            {
                                dt = dt.AddMonths(1);
                                monthArray.Append(dt);
                            }
                            mainCalendar.BoldedDates = [];
                            birthdayListBox.Items.Clear();

                            using SqlDataReader reader = myCommand.ExecuteReader();
                            while (reader.Read())
                            {
                                if (reader[0].ToString() == idParam.Value.ToString() && tabPage > 0 && tabPage < 8)
                                {
                                    switch (tabPage)
                                    {
                                        case 1:
                                            dt = Convert.ToDateTime(reader[1]);
                                            string route = "";
                                            string routeImage = "";
                                            string vehicle = "";
                                            string alt = "";
                                            string maxAlt = "";

                                            if (reader[8].ToString() != "0") alt = reader[8].ToString() + " m";
                                            if (reader[9].ToString() != "0") maxAlt = reader[9].ToString() + " m";

                                            // Route
                                            if (reader[2].ToString() != String.Empty)
                                            {
                                                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                                {
                                                    con1.Open();
                                                    using (SqlCommand com1 = new())
                                                    {
                                                        com1.CommandText = @"SELECT RouteName, Image FROM Routes WHERE Id = " + reader[2].ToString();
                                                        com1.CommandType = CommandType.Text;
                                                        com1.Connection = con1;
                                                        using SqlDataReader reader1 = com1.ExecuteReader();
                                                        while (reader1.Read())
                                                        {
                                                            route = reader1[0].ToString();
                                                            routeImage = reader1[1].ToString();
                                                        }
                                                        reader1.Close();
                                                    }
                                                    con1.Close();
                                                }
                                            }

                                            // Vehicle
                                            if (reader[3].ToString() != String.Empty)
                                            {
                                                vehicle = GetDatabaseEntry("Vehicles", "VehicleName", (int)reader[3]);
                                            }

                                            dataTitleLabel.Text = "Tagestour vom " + dt.ToString("dd. MMMM yyyy");
                                            dataRouteLabel.Text = route;
                                            dataVehicleLabel.Text = vehicle;
                                            dataKmLabel.Text = reader[4].ToString();
                                            dataTimeLabel.Text = reader[5].ToString();
                                            dataAvgLabel.Text = reader[6].ToString();
                                            dataVmaxLabel.Text = reader[7].ToString();
                                            dataAltLabel.Text = alt;
                                            dataMaxAltLabel.Text = maxAlt;
                                            dataRemarkRichTextBox.Text = reader[10].ToString();
                                            if (!reader.IsDBNull(11))
                                            {
                                                galleryToolStripSeparator.Visible = true;
                                                showGalleryToolStripButton.Visible = true;
                                                current_gallery = Convert.ToInt32(reader[11]);
                                            }
                                            else
                                            {
                                                galleryToolStripSeparator.Visible = false;
                                                showGalleryToolStripButton.Visible = false;
                                                current_gallery = -1;
                                            }
                                            if (!reader.IsDBNull(12))
                                            {
                                                string[] tmp_pers = reader[12].ToString().Split(';');
                                                foreach (string person in tmp_pers)
                                                {
                                                    if (person.Length > 0)
                                                    {
                                                        int p = Convert.ToInt32(person);
                                                        Person pers = new(p);
                                                        personsRichTextBox.Text += pers.ToString() + "; ";
                                                    }
                                                }
                                            }
                                            if (routeImage != String.Empty)
                                                dataPictureBox.Image = Image.FromFile(routeImage);
                                            else
                                                dataPictureBox.Image = null;
                                            break;
                                        case 2:
                                            routeTitleLabel.Text = reader[1].ToString();

                                            string city1 = "";
                                            string city2 = "";
                                            string city3 = "";
                                            int int_city1 = -1;
                                            int int_city2 = -1;
                                            int int_city3 = -1;

                                            if (reader[2].ToString() != String.Empty)
                                            {
                                                int_city1 = Convert.ToInt32(reader[2]);
                                            }
                                            if (reader[3].ToString() != String.Empty)
                                            {
                                                int_city2 = Convert.ToInt32(reader[3]);
                                            }
                                            if (reader[4].ToString() != String.Empty)
                                            {
                                                int_city3 = Convert.ToInt32(reader[4]);
                                            }
                                            using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                            {
                                                con1.Open();
                                                using (SqlCommand com1 = new())
                                                {
                                                    com1.CommandText = @"SELECT Id, CityName FROM Cities";
                                                    com1.CommandType = CommandType.Text;
                                                    com1.Connection = con1;
                                                    using SqlDataReader reader1 = com1.ExecuteReader();
                                                    while (reader1.Read())
                                                    {
                                                        if (reader1[0].ToString() == int_city1.ToString())
                                                            city1 = reader1[1].ToString();
                                                        if (reader1[0].ToString() == int_city2.ToString())
                                                            city2 = reader1[1].ToString();
                                                        if (reader1[0].ToString() == int_city3.ToString())
                                                            city3 = reader1[1].ToString();
                                                    }
                                                    reader1.Close();
                                                }
                                                con1.Close();
                                            }

                                            cityLabel.Text = city1;
                                            cityStartLabel.Text = city2;
                                            cityEndLabel.Text = city3;
                                            citiesLabel.Text = reader[5].ToString();

                                            routeTypeLabel.Text = GetDatabaseEntry("RouteTypes", "RouteType", (int)reader[6]);

                                            if (reader[7].ToString() != String.Empty && reader[7].ToString() != "0")
                                            {
                                                maxAltLabel.Text = reader[7].ToString() + " m";
                                            }
                                            else
                                            {
                                                maxAltLabel.Text = "";
                                            }
                                            if (reader[8].ToString() != String.Empty && reader[8].ToString() != "0")
                                            {
                                                altLabel.Text = reader[7].ToString() + " m";
                                            }
                                            else
                                            {
                                                altLabel.Text = "";
                                            }

                                            routeRemarkRichTextBox.Text = reader[9].ToString();

                                            if (reader[10].ToString() != String.Empty)
                                            {
                                                altProfilePictureBox.Image = Image.FromFile(reader[10].ToString());
                                            }
                                            else altProfilePictureBox.Image = null;
                                            if (reader[11].ToString() != String.Empty)
                                            {
                                                routePictureBox.Image = Image.FromFile(reader[11].ToString());
                                            }
                                            else routePictureBox.Image = null;
                                            break;
                                        case 3:
                                            string manufacturer = "";
                                            string type = "";
                                            if (reader[2].ToString() != String.Empty)
                                            {
                                                manufacturer = GetDatabaseEntry("Companies", "CompanyName", (int)reader[2]);
                                                if (manufacturer == "Kein Hersteller")
                                                    manufacturer = "";
                                            }
                                            vehicleTitleLabel.Text = manufacturer + " " + reader[1].ToString();
                                            if (reader[5].ToString() != "")
                                            {
                                                vehicleTitleLabel.Text += " (" + reader[5].ToString() + ")";
                                            }
                                            if (reader[4].ToString() != "" && reader[6].ToString() != "")
                                            {
                                                dt = Convert.ToDateTime(reader[4]);
                                                boughtLabel.Text = "Gekauft am " + dt.ToString("dd. MMMM yyyy") +
                                                    " für " + (Convert.ToDecimal(reader[6])).ToString("C") + ".";
                                            }
                                            else boughtLabel.Text = "";

                                            if (reader[3].ToString() != String.Empty)
                                            {
                                                type = GetDatabaseEntry("VehicleTypes", "VehicleType", (int)reader[3]);
                                            }
                                            typeLabel.Text = type;
                                            equipRichTextBox.Text = reader[7].ToString();
                                            if (reader[8].ToString() != String.Empty)
                                            {
                                                vehiclePictureBox.Image = Image.FromFile(reader[8].ToString());
                                            }
                                            else vehiclePictureBox.Image = null;
                                            if (reader[9].ToString() != null && reader[9].ToString() != "")
                                            {
                                                if (Convert.ToInt32(reader[9]) != -1)
                                                {
                                                    entfaltungButton.Enabled = true;
                                                    current_entfaltung = Convert.ToInt32(reader[9]);
                                                    noEntfaltungLabel.Text = "";
                                                }
                                            }
                                            else
                                            {
                                                entfaltungButton.Enabled = false;
                                                current_entfaltung = -1;
                                                noEntfaltungLabel.Text = "Noch nicht vorhanden";
                                            }
                                            break;
                                        case 4:
                                            string bland = "";
                                            string land = "";
                                            cityNameLabel.Text = reader[1].ToString();
                                            if (reader[3].ToString() != String.Empty)
                                            {
                                                bland = GetDatabaseEntry("Bundeslaender", "Bundesland", (int)reader[3]);
                                            }
                                            if (reader[2].ToString() != String.Empty)
                                            {
                                                land = GetDatabaseEntry("Countries", "Country", (int)reader[2]);
                                            }
                                            countryLabel.Text = land;
                                            bundeslandLabel.Text = bland;
                                            postCodeLabel.Text = reader[4].ToString();
                                            prefixLabel.Text = reader[5].ToString();
                                            cityLinkLabel.Text = reader[6].ToString();
                                            kfzLabel.Text = reader[7].ToString();
                                            if (reader[8].ToString() != String.Empty)
                                            {
                                                heightLabel.Text = reader[8].ToString() + " m ü. NHN";
                                            }
                                            else { heightLabel.Text = ""; }
                                            cityRemarkRichTextBox.Text = reader[9].ToString();
                                            if (reader[10].ToString() != String.Empty)
                                            {
                                                cityPictureBox.Image = Image.FromFile(reader[10].ToString());
                                            }
                                            else cityPictureBox.Image = cityPictureBox.ErrorImage;
                                            gpsLabel.Text = GetGPSviaProperty(reader[11].ToString());
                                            SwitchMapProvider(cityMapControl);
                                            if (reader[11].ToString() != String.Empty)
                                            {
                                                string loc = reader[11].ToString();
                                                GpsCoordinate map_loc = new(loc);
                                                cityMapControl.Position = map_loc.GetMapPosition();
                                                cityMapControl.Refresh();
                                            }
                                            break;
                                        case 5:
                                            string city = "";
                                            land = "";
                                            if (reader[4].ToString() != String.Empty)
                                            {
                                                city = GetDatabaseEntry("Cities", "CityName", (int)reader[4]);
                                            }
                                            if (reader[12].ToString() != String.Empty)
                                            {
                                                land = GetDatabaseEntry("Countries", "Country", (int)reader[12]);
                                            }
                                            string roles = "";
                                            if (Convert.ToInt32(reader[19]) == 1) roles = "Anwender";
                                            if (Convert.ToInt32(reader[20]) == 1) roles = "Administrator";
                                            personLabel.Text = reader[3].ToString() + " " + reader[2].ToString();
                                            if (roles.Length > 0) usernameLabel.Text = reader[1].ToString() + " (" + roles + ")";
                                            else usernameLabel.Text = reader[1].ToString();
                                            plzCityLabel.Text = reader[9].ToString() + " " + city;
                                            if (reader[5].ToString().Length > 0)
                                                birthdateLabel.Text = Convert.ToDateTime(reader[5]).ToString("dd. MMMM yyyy");
                                            else
                                                birthdateLabel.Text = "";
                                            if (reader[6].ToString().Length > 0 && reader[5].ToString().Length > 0)   //Deathdate
                                            {
                                                birthdayLabel.Text = "Geburtstag / Todestag";
                                                birthdateLabel.Text = Convert.ToDateTime(reader[5]).ToString("dd. MMMM yyyy") + " / " +
                                                    Convert.ToDateTime(reader[6]).ToString("dd. MMMM yyyy");
                                            }
                                            else
                                                birthdayLabel.Text = "Geburtstag";
                                            phoneLabel.Text = reader[7].ToString();
                                            emailLinkLabel.Text = reader[8].ToString();
                                            str1Label.Text = reader[10].ToString();
                                            str2Label.Text = reader[11].ToString();
                                            userCountryLabel.Text = land;
                                            if (reader[13].ToString() != String.Empty)
                                            {
                                                personPictureBox.Image = Image.FromFile(reader[13].ToString());
                                            }
                                            else cityPictureBox.Image = null;
                                            personRemarkRichTextBox.Text = reader[14].ToString();
                                            break;
                                        case 6:
                                            goalsTitleLabel.Text = "Ziele";
                                            goalTitleLabel.Text = reader[1].ToString();
                                            goalRemarkRichTextBox.Text = reader[2].ToString();
                                            goalDateTimePicker.Value = Convert.ToDateTime(reader[3]);
                                            goalDateTimePicker.Enabled = false;
                                            if (Convert.ToInt32(reader[4]) == 1)
                                                doneCheckBox.Checked = true;
                                            else doneCheckBox.Checked = false;
                                            doneCheckBox.Enabled = false;
                                            goalCreatedLabel.Text = Convert.ToDateTime(reader[5]).ToString("dd.MM.yyyy");
                                            break;
                                        case 7:
                                            notesTitleLabel.Text = "Notizen";
                                            noteTitleLabel.Text = reader[1].ToString();
                                            notesRemarkRichTextBox.Text = reader[2].ToString();
                                            break;
                                    }
                                }
                                else if (tabPage == 8)
                                {
                                    mainCalendar.AnnuallyBoldedDates = [];

                                    if (reader[2].ToString() != String.Empty)
                                    {
                                        foreach (DateTime mon in monthArray)
                                        {
                                            int year = DateTime.Now.Year;
                                            int month = Convert.ToDateTime(reader[2]).Month;
                                            int day = Convert.ToDateTime(reader[2]).Day;
                                            dt = new DateTime(year, month, day);
                                            if (mon.Month == month || mon.Month == month + 1)
                                            {
                                                string date = "";
                                                if (day.ToString().Length == 1) date += "0" + day.ToString() + ".";
                                                else date += day.ToString() + ".";
                                                if (month.ToString().Length == 1) date += "0" + month.ToString() + ".";
                                                else date += month.ToString() + ".";
                                                birthdayListBox.Items.Add(date + ": " + reader[0].ToString() + " " + reader[1].ToString() + " (" + reader[3].ToString() + ")");
                                                birthdayListBox.Sorted = true;
                                            }
                                            mainCalendar.AddAnnuallyBoldedDate(dt);

                                        }
                                    }
                                    mainCalendar.UpdateBoldedDates();
                                }
                            }
                            reader.Close();
                            if (tabPage == 8)
                            {
                                // Birthday of the current user
                                if (Convert.IsDBNull(GetDatabaseEntry("Persons", "Birthdate", "Id = " +
                                        Properties.Settings.Default.CurrentUserID.ToString())) == false
                                        && GetDatabaseEntry("Persons", "Birthdate", "Id = " +
                                        Properties.Settings.Default.CurrentUserID.ToString()) != "")
                                {
                                    MessageBox.Show(GetDatabaseEntry("Persons", "Birthdate", "Id = " +
                                        Properties.Settings.Default.CurrentUserID.ToString()));
                                    DateTime bd = Convert.ToDateTime(GetDatabaseEntry("Persons", "Birthdate", "Id = " +
                                        Properties.Settings.Default.CurrentUserID.ToString()));
                                    if (bd != null)
                                    {
                                        mainCalendar.AddAnnuallyBoldedDate(bd);
                                        foreach (DateTime mon in monthArray)
                                        {
                                            int year = DateTime.Now.Year;
                                            int month = bd.Month;
                                            int day = bd.Day;
                                            int nextMon = mon.Month + 1;
                                            if (nextMon > 12) { nextMon -= 12; year += 1; }
                                            if (mon.Month == month || nextMon == month)
                                            {
                                                string date = "";
                                                if (day.ToString().Length == 1) date += "0" + day.ToString() + ".";
                                                else date += day.ToString() + ".";
                                                if (month.ToString().Length == 1) date += "0" + month.ToString() + ".";
                                                else date += month.ToString() + ".";
                                                birthdayListBox.Items.Add(date + ": Du hast " + (year - bd.Year).ToString() + ". Geburtstag!!");
                                                birthdayListBox.Sorted = true;
                                            }
                                        }
                                    }
                                }
                                using (SqlCommand com1 = new())
                                {
                                    com1.CommandText = @"SELECT * FROM DateView WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                                    com1.CommandType = CommandType.Text;
                                    com1.Connection = myConnection;
                                    using SqlDataReader reader1 = com1.ExecuteReader();
                                    while (reader1.Read())
                                    {
                                        dt = Convert.ToDateTime(reader1[0]);
                                        mainCalendar.AddBoldedDate(dt);
                                    }
                                    reader1.Close();
                                }
                                using (SqlCommand com1 = new())
                                {
                                    com1.CommandText = @"SELECT * FROM Goals WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                                    com1.CommandType = CommandType.Text;
                                    com1.Connection = myConnection;
                                    using SqlDataReader reader1 = com1.ExecuteReader();
                                    while (reader1.Read())
                                    {
                                        dt = Convert.ToDateTime(reader1[3]);
                                        if (!mainCalendar.BoldedDates.Contains(dt))
                                            mainCalendar.AddBoldedDate(dt);
                                    }
                                    reader1.Close();
                                }
                                mainCalendar.UpdateBoldedDates();
                            }
                        }
                        myConnection.Close();
                    }
                }
                catch (System.Exception ex)
                {
                    ShowErrorMessage(ex.Message, "BG-Worker Completed...");
                }
            }
        }

        /// <summary>
        /// When data has been changed the combo boxes need to be refreshed and background worker runs again.
        /// </summary>
        private void RefreshComboBoxes()
        {
            //MessageBox.Show("Refresh Combo");
            //this.vehiclesTableAdapter.Fill(this.dataSet.Vehicles);
            //vehiclesTableAdapter.ClearBeforeFill = true;
            //this.vehiclesTableAdapter.Fill(this.dataSet.Vehicles);
            LoadVehicles();
            LoadRoutes();
            //this.routesTableAdapter.Fill(this.dataSet.Routes);
            this.tourTableAdapter.Fill(this.dataSet.Tour);
            //vehiclesTableAdapter.Update(dataSet);
            //vehicleComboBox.DataSource = null;
            //vehicleComboBox.Items.Clear();
            //vehiclesBindingSource.ResetBindings(true);
            //vehicleComboBox.DataSource = vehiclesBindingSource;
            //vehicleComboBox.ValueMember = "Id";
            //vehicleComboBox.DisplayMember = "VehicleName";
            showDataBackgroundWorker.RunWorkerAsync();
            GetPersons();
            //tblTableAdapter.Fill(dsMy.tbl);
            //vehicleComboBox.Refresh();
        }
        #endregion

        #region Daten
        private void FirstToolStripButton_Click(object sender, EventArgs e)
        {
            tour_id = 0;
            current_tour = tour_ids[tour_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void PreviousToolStripButton_Click(object sender, EventArgs e)
        {
            tour_id--;
            current_tour = tour_ids[tour_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void NextToolStripButton_Click(object sender, EventArgs e)
        {
            tour_id++;
            current_tour = tour_ids[tour_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void LastToolStripButton_Click(object sender, EventArgs e)
        {
            tour_id = cnt_tours - 1;
            current_tour = tour_ids[tour_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void EditToolStripButton_Click(object sender, EventArgs e)
        {
            SqlConnection con1;
            try
            {
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new())
                    {
                        com1.CommandText = $"SELECT * FROM Tour WHERE Id = " + current_tour.ToString();
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using SqlDataReader reader1 = com1.ExecuteReader();
                        while (reader1.Read())
                        {
                            tourDateTimePicker.Value = Convert.ToDateTime(reader1[1]);
                            routeComboBox.SelectedValue = (int)reader1[2];
                            vehicleComboBox.SelectedValue = (int)reader1[3];
                            kmTextBox.Text = FillDecimalForTextbox((decimal)reader1[4], 3);
                            timeTextBox.Text = reader1[5].ToString();
                            avgTextBox.Text = FillDecimalForTextbox((decimal)reader1[6], 3);
                            vmaxTextBox.Text = FillDecimalForTextbox((decimal)reader1[7], 3);
                            if (reader1[8].ToString() != "0") altitudeTextBox.Text = reader1[8].ToString();
                            else altitudeTextBox.Text = "";
                            if (reader1[9].ToString() != "0") maxAltLabel.Text = reader1[9].ToString();
                            else maxAltLabel.Text = "";
                            remarkRichTextBox.Text = reader1[10].ToString();
                            personsListBox.SelectedIndices.Clear();
                            if (!reader1.IsDBNull(11))
                            {
                                string[] tmp_pers = reader1[11].ToString().Split(';');
                                foreach (string person_id in tmp_pers)
                                {
                                    if (person_id.Length > 0)
                                    {
                                        int id = Convert.ToInt32(person_id);
                                        Person pers = new(id);

                                        int index = personsListBox.FindStringExact(pers.GetLastnameFirst());
                                        if (index != -1)
                                        {
                                            //TODO: Bug: listbox entries are not selected
                                            personsListBox.SelectedIndices.Add(index);
                                            //personsListBox.SetSelected(index, true);break;
                                        }
                                    }
                                }
                                //personsListBox.Focus();
                            }
                        }
                        reader1.Close();
                    }
                    con1.Close();
                }
                edit_data = true;
                addButton.Text = "Bearbeiten";
                mainTabControl.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Datenbankzugriff");
            }
        }

        /// <summary>
        /// Delete button might be removed...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteToolStripButton_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Strecken
        private void RouteToolStripButton0_Click(object sender, EventArgs e)
        {
            route_id = 0;
            current_route = route_ids[route_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void RouteToolStripButton1_Click(object sender, EventArgs e)
        {
            route_id--;
            current_route = route_ids[route_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void RouteToolStripButton2_Click(object sender, EventArgs e)
        {
            route_id++;
            current_route = route_ids[route_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void RouteToolStripButton3_Click(object sender, EventArgs e)
        {
            route_id = cnt_routes - 1;
            current_route = route_ids[route_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void RouteToolStripButton4_Click(object sender, EventArgs e)
        {
            RouteForm routeForm = new()
            {
                Edit = true,
                RouteId = current_route
            };
            if (routeForm.ShowDialog() == DialogResult.OK)
            {
                showDataBackgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Delete button might be removed...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RouteToolStripButton5_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Fahrzeuge
        private void VecToolStripButton0_Click(object sender, EventArgs e)
        {
            vehicle_id = 0;
            current_vehicle = vehicle_ids[vehicle_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void VecToolStripButton1_Click(object sender, EventArgs e)
        {
            vehicle_id--;
            current_vehicle = vehicle_ids[vehicle_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void VecToolStripButton2_Click(object sender, EventArgs e)
        {
            vehicle_id++;
            current_vehicle = vehicle_ids[vehicle_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void VecToolStripButton3_Click(object sender, EventArgs e)
        {
            vehicle_id = cnt_vehicles - 1;
            current_vehicle = vehicle_ids[vehicle_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Edit current vehicle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VecToolStripButton4_Click(object sender, EventArgs e)
        {
            VehicleForm vehicleForm = new()
            {
                Edit = true,
                VehicleId = current_vehicle
            };
            if (vehicleForm.ShowDialog() == DialogResult.OK)
            {
                showDataBackgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Delete button might be removed...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VecToolStripButton5_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Cities
        private void City1ToolStripButton_Click(object sender, EventArgs e)
        {
            city_id = 0;
            current_city = city_ids[city_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void City2ToolStripButton_Click(object sender, EventArgs e)
        {
            city_id--;
            current_city = city_ids[city_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void City3ToolStripButton_Click(object sender, EventArgs e)
        {
            city_id++;
            current_city = city_ids[city_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void City4ToolStripButton_Click(object sender, EventArgs e)
        {
            city_id = cnt_cities - 1;
            current_city = city_ids[city_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Edit current city.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void City5ToolStripButton_Click(object sender, EventArgs e)
        {
            CityForm cityForm = new()
            {
                Edit = true,
                CityId = current_city
            };
            if (cityForm.ShowDialog() == DialogResult.OK)
            {
                showDataBackgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Delete button might be removed...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void City6ToolStripButton_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Persons
        private void Persons1ToolStripButton_Click(object sender, EventArgs e)
        {
            person_id = 0;
            current_person = person_ids[person_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void Persons2ToolStripButton_Click(object sender, EventArgs e)
        {
            person_id--;
            current_person = person_ids[person_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void Persons3ToolStripButton_Click(object sender, EventArgs e)
        {
            person_id++;
            current_person = person_ids[person_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void Persons4ToolStripButton_Click(object sender, EventArgs e)
        {
            person_id = cnt_persons - 1;
            current_person = person_ids[person_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Edit current person.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Persons5ToolStripButton_Click(object sender, EventArgs e)
        {
            PersonsForm personsForm = new()
            {
                Edit = true,
                EditId = current_person
            };
            if (personsForm.ShowDialog() == DialogResult.OK)
            {
                showDataBackgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Delete button might be removed...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Persons6ToolStripButton_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Goals
        private void Goals1ToolStripButton_Click(object sender, EventArgs e)
        {
            goal_id = 0;
            current_goal = goal_ids[goal_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void Goals2ToolStripButton_Click(object sender, EventArgs e)
        {
            goal_id--;
            current_goal = goal_ids[goal_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void Goals3ToolStripButton_Click(object sender, EventArgs e)
        {
            goal_id++;
            current_goal = goal_ids[goal_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void Goals4ToolStripButton_Click(object sender, EventArgs e)
        {
            goal_id = cnt_goals - 1;
            current_goal = goal_ids[goal_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Edit current goal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Goals5ToolStripButton_Click(object sender, EventArgs e)
        {
            GoalsForm goalsForm = new()
            {
                Edit = true,
                EditId = current_goal
            };
            if (goalsForm.ShowDialog() == DialogResult.OK)
            {
                showDataBackgroundWorker.RunWorkerAsync();
                MainCalendar_ShowDetails();
            }
        }

        /// <summary>
        /// Delete button might be removed...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Goals6ToolStripButton_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Notes
        private void Notes1ToolStripButton_Click(object sender, EventArgs e)
        {
            note_id = 0;
            current_note = note_ids[note_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void Notes2ToolStripButton_Click(object sender, EventArgs e)
        {
            note_id--;
            current_note = note_ids[note_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void Notes3ToolStripButton_Click(object sender, EventArgs e)
        {
            note_id++;
            current_note = note_ids[note_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        private void Notes4ToolStripButton_Click(object sender, EventArgs e)
        {
            note_id = cnt_notes - 1;
            current_note = note_ids[note_id];
            showDataBackgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Edit current note.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Notes5ToolStripButton_Click(object sender, EventArgs e)
        {
            NotesForm notesForm = new()
            {
                Edit = true,
                EditId = current_note
            };
            if (notesForm.ShowDialog() == DialogResult.OK)
            {
                showDataBackgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Delete button might be removed...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Notes6ToolStripButton_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Export/Import
        private void ExportierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportForm exportForm = new();
            exportForm.ShowDialog();
        }

        private void ImportierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportForm importForm = new();
            if (importForm.ShowDialog() == DialogResult.OK)
            {
                RefreshComboBoxes();
            }
        }
        #endregion

        #region Context menu on images
        /// <summary>
        /// Load the image from a picturebox into the ImageViewerForm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BildbetrachterContextMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            var contextMenu = menuItem.GetCurrentParent() as ContextMenuStrip;
            PictureBox pictureBox = (PictureBox)contextMenu.SourceControl;
            ImageViewerForm viewerForm = new(pictureBox.Image);
            viewerForm.Show();
        }
        #endregion

        #region Image Editor
        private void ImageEditorButton_Click(object sender, EventArgs e)
        {
            if (openImageEditorDialog.ShowDialog() == DialogResult.OK)
            {
                imageEditorPath.Text = openImageEditorDialog.FileName;
                Properties.Settings.Default.ImageEditorPath = imageEditorPath.Text;
                imageEditorTextBox.Text = openImageEditorDialog.SafeFileName.Substring(0, openImageEditorDialog.SafeFileName.Length - 4);
                Properties.Settings.Default.ImageEditorName = imageEditorTextBox.Text;
                bildbearbeitungToolStripMenuItem.Text = imageEditorTextBox.Text;
            }
        }

        private void BildbearbeitungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.ImageEditorPath != "")
                {
                    Process.Start(new ProcessStartInfo(Properties.Settings.Default.ImageEditorPath));
                }
                else
                {
                    mainStatusLabel.Text = "Ein Bildeditor muss erst installiert oder in den Einstellungen angepasst werden!";
                }
            }
            catch (Exception exception) { ShowErrorMessage(exception.Message, "Fehlerhafte Konfiguration Bildeditor"); }
        }

        private void ImageEditorTextBox_TextChanged(object sender, EventArgs e)
        {
            if (imageEditorTextBox.Text != String.Empty)
            {
                bildbearbeitungToolStripMenuItem.Text = imageEditorTextBox.Text;
                Properties.Settings.Default.ImageEditorName = imageEditorTextBox.Text;
            }
        }
        #endregion

        #region Statistics
        private void FahrzeugToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowStatistics();
        }

        private void StatisticsToolStripButton_Click(object sender, EventArgs e)
        {
            ShowStatistics();
        }

        private void ShowStatistics()
        {
            StatisticsForm statisticsForm = new();
            statisticsForm.Show();
        }
        #endregion

        /// <summary>
        /// Load current vehicle and entfaltung into EntfaltungForm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntfaltungButton_Click(object sender, EventArgs e)
        {
            EntfaltungForm ent = new(GetDatabaseEntry("Vehicles", "VehicleName", current_vehicle), current_vehicle, current_entfaltung);
            if (ent.ShowDialog() == DialogResult.OK)
            {
                showDataBackgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.UserLoggedIn = true;
                logged_in = true;
                this.Refresh();
                CheckLogin();
                LoadNotifyIcon();
            }
        }

        /// <summary>
        /// Logout.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogoutForm logoutForm = new();
            if (logoutForm.ShowDialog() == DialogResult.OK)
            {
                logged_in = false;
                Properties.Settings.Default.CurrentUserName = "";
                Properties.Settings.Default.CurrentUserID = -1;
                this.Refresh();
                CheckLogin();
            }
        }

        #region Show PersonsForm
        private void PersonsToolStripButton_Click(object sender, EventArgs e)
        {
            ShowPersons();
        }

        private void PersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPersons();
        }

        /// <summary>
        /// Shows PersonsForm.
        /// </summary>
        private void ShowPersons()
        {
            PersonsForm personsForm = new();
            if (personsForm.ShowDialog() == DialogResult.OK)
            {
                RefreshComboBoxes();
            }
        }
        #endregion

        #region Show Administration
        private void AdministrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAdmin();
        }

        private void AdminToolStripButton_Click(object sender, EventArgs e)
        {
            ShowAdmin();
        }

        /// <summary>
        /// Shows AdminForm.
        /// </summary>
        private void ShowAdmin()
        {
            AdminForm adminForm = new(Properties.Settings.Default.CurrentUserID, Properties.Settings.Default.CurrentUserName);
            adminForm.Show();
        }
        #endregion

        #region Show Goals
        private void GoalsToolStripButton_Click(object sender, EventArgs e)
        {
            ShowGoals();
        }

        private void ZielToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowGoals();
        }

        /// <summary>
        /// Shows GoalsForm.
        /// </summary>
        private void ShowGoals()
        {
            GoalsForm goalsForm = new();
            if (goalsForm.ShowDialog() == DialogResult.OK)
            {
                showDataBackgroundWorker.RunWorkerAsync();
            }
        }
        #endregion

        #region Show Notes
        private void NotesToolStripButton_Click(object sender, EventArgs e)
        {
            ShowNotes();
        }

        private void NotizToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNotes();
        }

        /// <summary>
        /// Shows NotesForm.
        /// </summary>
        private void ShowNotes()
        {
            NotesForm notesForm = new();
            if (notesForm.ShowDialog() == DialogResult.OK) 
            {
                showDataBackgroundWorker.RunWorkerAsync();
            }
        }
        #endregion

        #region Costs
        private void AusgabenUndKostenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCosts();
        }

        private void CostsToolStripButton_Click(object sender, EventArgs e)
        {
            ShowCosts();
        }

        /// <summary>
        /// Shows CostsForm.
        /// </summary>
        private void ShowCosts()
        {
            CostsForm costForm = new();
            if (costForm.ShowDialog() == DialogResult.OK)
            {
                // Currently not needed: costs are displayed in statistics
                //showDataBackgroundWorker.RunWorkerAsync();
            }
        }
        #endregion

        #region NotifyIcon
        NotifyMessage notifyMessage = new(Properties.Settings.Default.NotifyTime);

        /// <summary>
        /// Context menu on NotifyIcon: Birthdays
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeburtstageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCalendar();
        }

        /// <summary>
        /// Context menu on NotifyIcon: Goals
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZieleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowGoalTab();
        }

        /// <summary>
        /// Setting: should the NotifyIcon be loaded with the application or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MitAnwendungStartenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowNotifyIcon = !Properties.Settings.Default.ShowNotifyIcon;
            CheckNotifyIcon(Properties.Settings.Default.ShowNotifyIcon);
        }

        /// <summary>
        /// Closes the notification icon until the next start.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SchließenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainNotifyIcon.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadNotifyIcon()
        {
            if (Properties.Settings.Default.ShowNotifyIcon)
            {
                if (notifyMessage.MyNotifyIcon == null)
                    notifyMessage = new NotifyMessage(Properties.Settings.Default.NotifyTime);
                notifyMessage.MyNotifyIcon = mainNotifyIcon;
                mainNotifyIcon.Visible = true;
                mainTimer.Enabled = true;
                notifyMessage.NewDay();
            }
            else
            {
                mainNotifyIcon.Visible = false;
                mainTimer.Enabled = false;
            }
        }

        /// <summary>
        /// Personal setting: load NotifyIcon with application Yes/No.
        /// </summary>
        /// <param name="show"></param>
        private void CheckNotifyIcon(bool show = true)
        {
            Properties.Settings.Default.ShowNotifyIcon = show;
            if (Properties.Settings.Default.ShowNotifyIcon)
            {
                mitAnwendungStartenToolStripMenuItem.Checked = true;
            }
            else
            {
                mitAnwendungStartenToolStripMenuItem.Checked = false;
            }
            LoadNotifyIcon();
        }
        
        /// <summary>
        /// Check every minute for balloon tip events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainTimer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            if (now.Hour == 0 && now.Minute == 0)
            {
                // Midnight
                if (Properties.Settings.Default.ShowNotifyIcon &&  mainNotifyIcon.Visible)
                {
                    notifyMessage.NewDay();
                }
            }
        }
        
        /// <summary>
        /// Settings: Notification enabled/disabled. If enabled, textbox for notification time is enabled as well.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotificationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckNotifyIcon(notificationCheckBox.Checked);
            timerMaskedTextBox.Enabled = notificationCheckBox.Checked;
        }

        /// <summary>
        /// Change duration of balloon tip in the settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerMaskedTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int test = Int32.Parse(timerMaskedTextBox.Text.Trim());
                if (test > 0 && test < 100)
                {
                    notifyMessage.Time = test * 1000;
                    Properties.Settings.Default.NotifyTime = test * 1000;
                }
                else
                {
                    ShowErrorMessage("Benachrichtungsdauer muss zwischen 1 und 99 Sekunden lang sein!", "Ungültige Benachrichtungsdauer");
                }
            }
            catch
            {
                ShowErrorMessage("Benachrichtungsdauer muss zwischen 1 und 99 Sekunden lang sein!", "Falsches Zahlenformat");
            }
        }
        #endregion

        /// <summary>
        /// Password change of the current user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswortÄndernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordForm pwform = new();
            if (pwform.ShowDialog() == DialogResult.OK)
            {
                string password = pwform.Password;
                var sql = @"UPDATE Persons SET Password = @pwd " +
                        "WHERE Id = " + Properties.Settings.Default.CurrentUserID.ToString();
                try
                {
                    using var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString);
                    using var myCommand = new SqlCommand(sql, connection);
                    myCommand.Parameters.Add("@pwd", SqlDbType.NVarChar).Value = password;

                    connection.Open();
                    myCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, "Fehler beim Passwort-Update");
                }
            }
        }

        /// <summary>
        /// Show Docking Station Form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SigmaConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SigmaDsForm ds = new();
            if (ds.ShowDialog() == DialogResult.OK)
            {
                CreateDirectoryWatcher();
            }
        }

        /// <summary>
        /// If km and time are known for a tour, the average speed can directly be calculated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (kmTextBox.Text != kmTextBox.Mask)
                {
                    DateTime time = Convert.ToDateTime(timeTextBox.Text);
                    int total = time.Hour * 3600 + time.Minute * 60 + time.Second;
                    decimal avg = Convert.ToDecimal(kmTextBox.Text) / total * 3600;
                    avgTextBox.Text = FillDecimalForTextbox(Math.Round(avg, 2), 3);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Bug fix: there was an error if no vmax was given...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VmaxTextBox_Leave(object sender, EventArgs e)
        {
            vmaxTextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (vmaxTextBox.Text == string.Empty)
            {
                vmaxTextBox.Text = FillDecimalForTextbox(Math.Round(0.00M, 2), 3); 
            }
            vmaxTextBox.TextMaskFormat = MaskFormat.IncludeLiterals;
        }

        /// <summary>
        /// Steps can be used if no vehicle is used (jogging or hiking). For vehicles it's the maximum speed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VehicleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (vehicleComboBox.Text == "Joggen" || vehicleComboBox.Text == "Wandern/Zu Fuß")
            {
                vmaxStepsLabel.Text = "Schritte";
                vmaxTextBox.Mask = "999000";
                vehicleLabel.Text = "Zu Fuß";
            }
            else
            {
                vmaxStepsLabel.Text = "Vmax";
                vmaxTextBox.Mask = "990.09";
                vehicleLabel.Text = "Fahrzeug";
            }
        }

        #region Show FlightDB
        /// <summary>
        /// Show FlightDB.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FlightDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowFlightDb();
        }

        private void FlightDbToolStripButton_Click(object sender, EventArgs e)
        {
            ShowFlightDb();
        }

        private void ShowFlightDb()
        {
            FlightDBForm form = new();
            form.Show();
        }
        #endregion

        /// <summary>
        /// Change visibility of certain objects on main tab pages.
        /// Attention: logic is !NotShown = visible (value 0), NotShown = not visible (value 1).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// Route
        private void ShowToolStripSplitButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "checkedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.ROUTE, false, current_route);
            }
            else if (e.ClickedItem.Name == "notCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.ROUTE, true, current_route);
            }
            showToolStripSplitButton.Image = e.ClickedItem.Image;
        }

        // Vehicle
        private void ShowVecToolStripSplitButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "showVecCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.VEHICLE, false, current_vehicle);
            }
            else if (e.ClickedItem.Name == "showVecNotCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.VEHICLE, true, current_vehicle);
            }
            showVecToolStripSplitButton.Image = e.ClickedItem.Image;
        }

        // City
        private void ShowCityToolStripSplitButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "showCityCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.CITY, false, current_city);
            }
            else if (e.ClickedItem.Name == "showCityNotCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.CITY, true, current_city);
            }
            showCityToolStripSplitButton.Image = e.ClickedItem.Image;
        }

        // Person
        private void PersonToolStripSplitButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "showPersonCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.PERSON, false, current_person);
            }
            else if (e.ClickedItem.Name == "showPersonNotCheckedToolStripMenuItem")
            {
                ChangeComboBoxVisibility(VisibilityObject.PERSON, true, current_person);
            }
            personToolStripSplitButton.Image = e.ClickedItem.Image;
        }

        #region Show image galleries
        private void GalleryToolStripButton_Click(object sender, EventArgs e)
        {
            ShowGalleries();
        }

        private void BildergalerienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowGalleries();
        }

        private void ShowGalleries()
        {
            ImageGalleryForm imageGalleryForm = new();
            imageGalleryForm.Show();
        }       

        /// <summary>
        /// Show the image gallery if current_gallery != -1, i.e. tour data has a gallery Id associated with it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowGalleryToolStripButton_Click(object sender, EventArgs e)
        {
            if (current_gallery != -1)
            {
                ImageGalleryForm imageGallery = new()
                {
                    Id = current_gallery
                };
                imageGallery.Show();
            }
        }
        #endregion

        /// <summary>
        /// Open a QRForm on a link (context menu).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QrCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem menuItem)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                if (menuItem.Owner is ContextMenuStrip owner)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    if (sourceControl.Text != "")
                    {
                        QRForm qRForm = new(sourceControl.Text);
                        qRForm.Show();
                    }
                }
            }
        }

        private void jSONImportierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importJson();
        }

        private void importJsonButton_Click(object sender, EventArgs e)
        {
            importJson();
        }

        private void importJson()
        {
            ImportJsonForm importJsonForm = new();
            if (importJsonForm.ShowDialog() == DialogResult.OK)
            {
                // Json import 
                string json = importJsonForm.SelectedFilePath;
                if (json != "")
                {
                    try
                    {
                        RideData rideData = JsonSerializer.Deserialize<RideData>(File.ReadAllText(json));
                        if (rideData != null)
                        {
                            // Create a new tour with the data from the JSON file
                            if (importJsonForm.ImportDate)
                            {
                                tourDateTimePicker.Value = rideData.Timestamp;
                            }
                            if (importJsonForm.ImportBike)
                            {
                                if (vehicleComboBox.Items.Contains(rideData.BikeName))
                                {
                                    vehicleComboBox.SelectedItem = rideData.BikeName;
                                }
                                else
                                {
                                    ShowErrorMessage($"Fahrrad '{rideData.BikeName}' ist nicht in der Datenbank vorhanden. Bitte zuerst in der Verwaltung hinzufügen.", "Fahrrad nicht gefunden");
                                }
                            }
                            kmTextBox.Text = FillDecimalForTextbox(Convert.ToDecimal(rideData.DistanceKm), 3);
                            timeTextBox.Text = rideData.Duration.ToString(@"hh\:mm\:ss");
                            avgTextBox.Text = FillDecimalForTextbox(Convert.ToDecimal(rideData.MeanSpeedKmh), 3);
                            vmaxTextBox.Text = FillDecimalForTextbox(Convert.ToDecimal(rideData.MaxSpeedKmh), 3);
                            minAltTextBox.Text = FillDecimalForTextbox(Convert.ToDecimal(rideData.MinAltitudeMeters), 3);
                            maxAltTextBox.Text = FillDecimalForTextbox(Convert.ToDecimal(rideData.MaxAltitudeMeters), 3);
                            if (Properties.Settings.Default.UseCadence)
                            {
                                cadenceTextBox.Text = rideData.Cadence.ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage(ex.Message, "Fehler beim JSON-Import");
                    }
                }
            }
        }

        private void gPXDateiumwandelnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
