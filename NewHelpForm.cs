using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BikeDB2024
{
    public partial class NewHelpForm : Form
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NewHelpForm()
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

        /// <summary>
        /// Select help page to be displayed in browser window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string curDir = Directory.GetCurrentDirectory();
            
            switch (helpTreeView.SelectedNode.Text)
            {
                case "Inhalt":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Content.html", curDir));
                    break;
                case "Motivation":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Motivation.html", curDir));
                    break;
                case "Ansicht":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Ansicht.html", curDir));
                    break;
                case "Bildbetrachter":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Bildbetrachter.html", curDir));
                    break;
                case "Daten eingeben":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Daten_eingeben.html", curDir));
                    break;
                case "Einstellungen":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Einstellungen.html", curDir));
                    break;
                case "Entfaltung":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Entfaltung.html", curDir));
                    break;
                case "Fahrzeuge":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Fahrzeuge.html", curDir));
                    break;
                case "Google Earth":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/GoogleEarth.html", curDir));
                    break;
                case "Hilfsmittel":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Hilfsmittel.html", curDir));
                    break;
                case "Länder":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Laender.html", curDir));
                    break;
                case "Städte":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Städte.html", curDir));
                    break;
                case "Speicherorte":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Speicherorte.html", curDir));
                    break;
                case "Statistiken":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Statistiken.html", curDir));
                    break;
                case "Strecken":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Strecken.html", curDir));
                    break;
                case "Streckentypen":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Streckentypen.html", curDir));
                    break;
                case "Tagestouren":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Tagestour.html", curDir));
                    break;
                case "Export":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Export.html", curDir));
                    break;
                case "Import":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Import.html", curDir));
                    break;
                case "Versionsgeschichte":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Versionsgeschichte.html", curDir));
                    break;
                case "Bildbearbeitung":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Bildbearbeitung.html", curDir));
                    break;
                case "Drucken":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/StatistikDrucken.html", curDir));
                    break;
                case "Ziele (Kalender)":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/ZieleKalender.html", curDir));
                    break;
                case "Touren":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/TourenKalender.html", curDir));
                    break;
                case "Geburtstage":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Geburtstage.html", curDir));
                    break;
                case "Kalender":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Kalender.html", curDir));
                    break;
                case "Notizen":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Notizen.html", curDir));
                    break;
                case "Ziele":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Ziele.html", curDir));
                    break;
                case "Personen":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Personen.html", curDir));
                    break;
                case "Einzelplatz-Anwendung":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Einzelplatz.html", curDir));
                    break;
                case "Sicherheitshinweis":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Sicherheitshinweis.html", curDir));
                    break;
                case "Administratoren":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Administratoren.html", curDir));
                    break;
                case "Neuen Benutzer anlegen":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/NeuerBenutzer.html", curDir));
                    break;
                case "Multi-User":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Multiuser.html", curDir));
                    break;
                case "Passwort ändern":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Multiuser.html", curDir));
                    break;
                case "Kosten":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Kosten.html", curDir));
                    break;
                case "GPS-Koordinaten":
                    this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/GPS.html", curDir));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// When loading the form, restore last position.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewHelpForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.HelpLocation != new Point(0, 0))
            {
                this.Location = Properties.Settings.Default.HelpLocation;
            }
            if (Properties.Settings.Default.HelpSize != new Size(500, 400))
            {
                this.Size = Properties.Settings.Default.HelpSize;
            }
        }

        /// <summary>
        /// When closing the form, store current position.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewHelpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.HelpLocation = this.Location;
            Properties.Settings.Default.HelpSize = this.Size;
        }

        /// <summary>
        /// Called when a link is clicked and the browser navigates to a new page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (helpBrowser.Url.LocalPath.Contains("Content.html"))
            {
                helpTreeView.SelectedNode = helpTreeView.Nodes[0];
            }
            else if (helpBrowser.Url.LocalPath.Contains("Motivation.html"))
            {
                helpTreeView.Nodes[0].Expand();
                helpTreeView.SelectedNode = helpTreeView.Nodes[0].Nodes[0];
            }
            helpTreeView.Focus();
        }
    }
}
