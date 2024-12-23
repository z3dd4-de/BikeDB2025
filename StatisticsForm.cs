using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class StatisticsForm : Form
    {
        private DateTime[] date;
        private decimal[] kmValue;
        private decimal[] avgValue;
        private decimal[] vmaxValue;
        int arraySize = 0;
        int vehicleId = 0;
        int page = 0;
        string curDir = Directory.GetCurrentDirectory();

        /// <summary>
        /// Constructor.
        /// </summary>
        public StatisticsForm()
        {
            InitializeComponent();
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

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Vehicles". Sie können sie bei Bedarf verschieben oder entfernen.
            this.vehiclesTableAdapter.Fill(this.dataSet.Vehicles);
            if (Properties.Settings.Default.StatLocation != new Point(50,50))
            {
                this.Location = Properties.Settings.Default.StatLocation;
            }
            if (Properties.Settings.Default.StatSize != new Size(500,400))
            {
                this.Size = Properties.Settings.Default.StatSize;
            }
            deleteVehicleStatistics();
        }

        private void StatisticsForm_Shown(object sender, EventArgs e)
        {
            showTotalStatistics();
        }

        private void showTotalStatistics()
        {
            tourenLabel.Text = CountIds("Tour").ToString();
            vehiclesCountLabel.Text = CountIds("Vehicles").ToString();
            countriesCountLabel.Text = CountIds("Countries").ToString();
            citiesCountLabel.Text = CountIds("Cities").ToString();
            routesCountLabel.Text = CountIds("Routes").ToString();
            vmaxTotalLabel.Text = GetDatabaseEntry("Tour", "MAX(MaxSpeed)") + " km/h";
            kmTotalLabel.Text = GetDatabaseEntry("Tour", "SUM(Km)") + " km";
            timeTotalLabel.Text = GetDatabaseEntry("Tour", "sum(cast(datepart(hour, [Time])*60 + (datepart(minute, [Time])*60 + (datepart(second, [Time]))) / 3600.0 as decimal(8, 3))) ").ToString() + " h";
        }

        /// <summary>
        /// Save current location when form closes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatisticsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.StatLocation = this.Location;
            Properties.Settings.Default.StatSize = this.Size;
        }

        private void showChartSeries()
        {
            statChart.Series[0].Enabled = kmCheckBox.Checked;
            statChart.Series[1].Enabled = avgCheckBox.Checked;
            statChart.Series[2].Enabled = vmaxCheckBox.Checked;
        }

        private void allCheckBox_Click(object sender, EventArgs e)
        {
            if (allCheckBox.Checked)
            {
                kmCheckBox.Checked = true;
                avgCheckBox.Checked = true;
                vmaxCheckBox.Checked = true;

                kmCheckBox.Enabled = false;
                avgCheckBox.Enabled = false;
                vmaxCheckBox.Enabled = false;
            }
            else
            {
                kmCheckBox.Enabled = true;
                avgCheckBox.Enabled = true;
                vmaxCheckBox.Enabled = true;
            }
            showChartSeries();
        }

        private void kmCheckBox_Click(object sender, EventArgs e)
        {
            showChartSeries();
        }

        private void avgCheckBox_Click(object sender, EventArgs e)
        {
            showChartSeries();
        }

        private void vmaxCheckBox_Click(object sender, EventArgs e)
        {
            showChartSeries();
        }

        private void statBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (arraySize > 0)
            {
                date = new DateTime[arraySize];
                kmValue = new decimal[arraySize];
                avgValue = new decimal[arraySize];
                vmaxValue = new decimal[arraySize];

                SqlConnection con1;
                int i = 0;
                try
                {
                    using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        con1.Open();
                        using (SqlCommand com1 = new SqlCommand())
                        {
                            com1.CommandText = $"SELECT Date, Km, AverageSpeed, MaxSpeed FROM Tour WHERE Vehicle = " + vehicleId.ToString();
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;
                            using (SqlDataReader reader1 = com1.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    date[i] = Convert.ToDateTime(reader1[0]);
                                    kmValue[i] = Convert.ToDecimal(reader1[1]);
                                    avgValue[i] = Convert.ToDecimal(reader1[2]);
                                    vmaxValue[i] = Convert.ToDecimal(reader1[3]);
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
        }

        private void statBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string manufacturer = GetDatabaseEntry("Companies", "CompanyName", Convert.ToInt32(
                GetDatabaseEntry("Vehicles", "Manufacturer", vehicleId)));
            statChart.Titles[0].Text = "Statistiken für " + manufacturer + " " + vehicleComboBox.Text;
            statChart.Series[0].Points.Clear();
            statChart.Series[1].Points.Clear();
            statChart.Series[2].Points.Clear();
            if (arraySize > 0)
            {
                for (int i = 0; i < date.Length; i++)
                {
                    statChart.Series[0].Points.AddXY(date[i], kmValue[i]);
                    statChart.Series[1].Points.AddXY(date[i], avgValue[i]);
                    statChart.Series[2].Points.AddXY(date[i], vmaxValue[i]);
                }
            }
        }

        private void vehicleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (vehicleComboBox.SelectedValue != null)
            {
                vehicleId = Convert.ToInt32(vehicleComboBox.SelectedValue);
                showVehicleStatistics(vehicleId);
            }
        }

        private void showVehicleStatistics(int id)
        {
            vecTotalToursLabel.Text = GetDatabaseEntry("Tour", "COUNT(Id)", "Vehicle = " + id.ToString());
            vecTotalKmLabel.Text = GetDatabaseEntry("Tour", "SUM(Km)", "Vehicle = " + id.ToString()) + " km";
            vecKmLabel.Text = MinAvgMax("Tour", "Km", "Vehicle = " + id.ToString()) + " km";
            vecTotalTimeLabel.Text = GetDatabaseEntry("Tour",
                "sum(cast(datepart(hour, [Time])*60 + (datepart(minute, [Time])*60 + (datepart(second, [Time]))) / 3600.0 as decimal(8, 3))) ",
                "Vehicle = " + id.ToString()) + " h";
            vecVmaxLabel.Text = MinAvgMax("Tour", "MaxSpeed", "Vehicle = " + id.ToString()) + " km/h";
            vecAvgLabel.Text = MinAvgMax("Tour", "AverageSpeed", "Vehicle = " + id.ToString()) + " km/h";
            vecRoutesLabel.Text = GetDatabaseEntry("Tour", "COUNT(DISTINCT Route)", "Vehicle = " + id.ToString());

            // Chart
            arraySize = Convert.ToInt32(GetDatabaseEntry("Tour", "COUNT(Id)", "Vehicle = " + vehicleComboBox.SelectedValue.ToString()));
            statBackgroundWorker.RunWorkerAsync();
        }

        private void deleteVehicleStatistics()
        {
            vehicleComboBox.Text = string.Empty;
            vecTotalToursLabel.Text = string.Empty;
            vecTotalKmLabel.Text = string.Empty;
            vecKmLabel.Text = string.Empty;
            vecTotalTimeLabel.Text = string.Empty;
            vecVmaxLabel.Text = string.Empty;
            vecAvgLabel.Text = string.Empty;
            vecCitiesLabel.Text = "noch nicht implementiert";
            vecRoutesLabel.Text = string.Empty;
        }
        #region Printing
        private PrintDocument docToPrint =
            new PrintDocument();

        private void printCurrentPageToolStripButton_Click(object sender, EventArgs e)
        {
            statisticsPrintDialog.AllowSomePages = true;

            // Show the help button.
            statisticsPrintDialog.ShowHelp = true;

            // Set the Document property to the PrintDocument for 
            // which the PrintPage Event has been handled. To display the
            // dialog, either this property or the PrinterSettings property 
            // must be set 
            statisticsPrintDialog.Document = docToPrint;

            DialogResult result = statisticsPrintDialog.ShowDialog();

            // If the result is OK then print the document.
            if (result == DialogResult.OK)
            {
                docToPrint.Print();
            }
        }

        private void printAllToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            statisticsPrintPreviewDialog.ClientSize = new Size(600, 400);
            statisticsPrintPreviewDialog.Name = "PrintPreviewDialog1";

            // Associate the event-handling method with the 
            // document's PrintPage event.
            this.docToPrint.PrintPage +=
                new PrintPageEventHandler
                (document_PrintPage);

            statisticsPrintPreviewDialog.UseAntiAlias = true;

            // Set the PrintPreviewDialog.Document property to
            // the PrintDocument object selected by the user.
            statisticsPrintPreviewDialog.Document = docToPrint;

            // Call the ShowDialog method. This will trigger the document's
            //  PrintPage event.
            statisticsPrintPreviewDialog.ShowDialog();
        }

        private void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Insert code to render the page here.
            // This code will be called when the PrintPreviewDialog.Show 
            // method is called.

            // The following code will render a simple
            // message on the document in the dialog.
            string text = "In document_PrintPage method.";
            Font printFont_title =
                new Font("Arial", 25,
                FontStyle.Regular);
            Font printFont_small =
                new Font("Arial", 11,
                FontStyle.Regular);
            string manufacturer = GetDatabaseEntry("Companies", "CompanyName", Convert.ToInt32(
                        GetDatabaseEntry("Vehicles", "Manufacturer", vehicleId)));
            string vehicle = manufacturer + " " + vehicleComboBox.Text;
            switch (page)
            {
                default:
                case 0:
                    int loc_x = 60;
                    text = "Datenbank Übersicht";
                    e.Graphics.DrawString(text, printFont_title,
                        Brushes.Black, 50, 50);
                    e.Graphics.DrawString("Tagestouren gesamt: \t\t" + tourenLabel.Text, printFont_small,
                        Brushes.Black, loc_x, 120);
                    e.Graphics.DrawString("Gesamtkilometer: \t\t" + kmTotalLabel.Text, printFont_small,
                        Brushes.Black, loc_x, 140);
                    e.Graphics.DrawString("Gesamtfahrzeit: \t\t\t" + timeTotalLabel.Text, printFont_small,
                        Brushes.Black, loc_x, 160);
                    e.Graphics.DrawString("Höchstgeschwindigkeit: \t\t" + vmaxTotalLabel.Text, printFont_small,
                        Brushes.Black, loc_x, 180);
                    e.Graphics.DrawString("Fahrzeuge gesamt: \t\t" + vehiclesCountLabel.Text, printFont_small,
                        Brushes.Black, loc_x, 200);
                    e.Graphics.DrawString("Länder: \t\t\t\t" + countriesCountLabel.Text, printFont_small,
                        Brushes.Black, loc_x, 220);
                    e.Graphics.DrawString("Städte: \t\t\t\t" + citiesCountLabel.Text, printFont_small,
                        Brushes.Black, loc_x, 240);
                    e.Graphics.DrawString("Strecken: \t\t\t" + routesCountLabel.Text, printFont_small,
                        Brushes.Black, loc_x, 260);

                    Image img = Image.FromFile(curDir + "/HelpPages/logo_bikeDB.png");
                    Point loc = new Point(650, 40);
                    e.Graphics.DrawImage(img, loc);
                    break;
                case 1:
                    text = "Fahrzeug Statistiken für " + vehicle;
                    e.Graphics.DrawString(text, printFont_title,
                        Brushes.Black, 0, 0);
                    break;
                case 2:
                    text = "Chart";
                    statChart.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.All;
                    statChart.Printing.Print(true);
                    break;
            }
        }

        private void printSetupToolStripButton_Click(object sender, EventArgs e)
        {
            statisticsPageSetupDialog.Document = docToPrint;
            // Sets the print document's color setting to false,  
            // so that the page will not be printed in color.  
            statisticsPageSetupDialog.Document.DefaultPageSettings.Color = false;
            statisticsPageSetupDialog.ShowDialog();
        }
        #endregion

        private void statisticsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = statisticsTabControl.SelectedIndex;
        }
    }
}
