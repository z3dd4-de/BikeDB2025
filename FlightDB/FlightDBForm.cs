using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BikeDB2024.FlightDB;

namespace BikeDB2024
{
    public partial class FlightDBForm : Form
    {
        public int CurrentUserId { get; set; }
        public string CurrentUserName { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public FlightDBForm()
        {
            InitializeComponent();
        }

        private void FlightDBForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.FlightDBLocation != new Point(100, 100))
                this.Location = Properties.Settings.Default.FlightDBLocation;
            else this.Location = new Point(100, 100);
            if (Properties.Settings.Default.FlightDBSize != new Size(800, 500))
                this.Size = Properties.Settings.Default.FlightDBSize;
            else this.Size = new Size(800, 500);
        }

        private void FlightDBForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.FlightDBLocation = this.Location;
            Properties.Settings.Default.FlightDBSize = this.Size;
        }

        private void schließenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void closeToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            showHelp();
        }

        private void hilfeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showHelp();
        }

        private void showHelp()
        {

        }

        private void infoToolStripButton_Click(object sender, EventArgs e)
        {
            showInfo();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showInfo();
        }

        private void showInfo()
        {
            AboutFlightDBForm aboutFlightDBForm = new AboutFlightDBForm();
            aboutFlightDBForm.ShowDialog();
        }

        private void flugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showFlightForm();
        }

        private void flightToolStripButton_Click(object sender, EventArgs e)
        {
            showFlightForm();
        }

        private void showFlightForm()
        {
            FlightForm flightForm = new FlightForm();
            if (flightForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void flughafenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showAirportForm();
        }

        private void airportToolStripButton_Click(object sender, EventArgs e)
        {
            showAirportForm();
        }

        private void showAirportForm()
        {
            AirportForm airportForm = new AirportForm();
            if (airportForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void flugzeugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPlaneForm();
        }

        private void planeToolStripButton_Click(object sender, EventArgs e)
        {
            showPlaneForm();
        }

        private void showPlaneForm()
        {
            PlaneForm planeForm = new PlaneForm();
            if (planeForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void kostenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showCosts();
        }

        private void costToolStripButton_Click(object sender, EventArgs e)
        {
            showCosts();
        }

        private void showCosts()
        {

        }

        private void landToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showCountryForm();
        }

        private void countryToolStripButton_Click(object sender, EventArgs e)
        {
            showCountryForm();
        }

        private void showCountryForm()
        {
            CountryForm countryForm = new CountryForm();
            if (countryForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void stadtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showCityForm();
        }

        private void cityToolStripButton_Click(object sender, EventArgs e)
        {
            showCityForm();
        }

        private void showCityForm()
        {
            CityForm cityForm = new CityForm();
            if (cityForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void fluggesellschaftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showAirlineForm();
        }

        private void airlinetoolStripButton_Click(object sender, EventArgs e)
        {
            showAirlineForm();
        }

        private void showAirlineForm()
        {
            AirlineForm airlineForm = new AirlineForm();
            if (airlineForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void flugzeugherstellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showConstructorForm();
        }

        private void manufacturerToolStripButton_Click(object sender, EventArgs e)
        {
            showConstructorForm();
        }

        private void showConstructorForm()
        {
            PlaneManufacturersForm planeManufacturersForm = new PlaneManufacturersForm();
            if (planeManufacturersForm.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
