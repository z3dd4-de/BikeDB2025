using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class AdminForm : Form
    {
        public int AdminID { get; set; }
        public string AdminName { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public AdminForm(int id, string name)
        {
            AdminID = id;
            AdminName = name;
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Clients". Sie können sie bei Bedarf verschieben oder entfernen.
            this.clientsTableAdapter.Fill(this.dataSet.Clients);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Goals". Sie können sie bei Bedarf verschieben oder entfernen.
            this.goalsTableAdapter.Fill(this.dataSet.Goals);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Notes". Sie können sie bei Bedarf verschieben oder entfernen.
            this.notesTableAdapter.Fill(this.dataSet.Notes);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.CostCategories". Sie können sie bei Bedarf verschieben oder entfernen.
            this.costCategoriesTableAdapter.Fill(this.dataSet.CostCategories);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.VehicleTypes". Sie können sie bei Bedarf verschieben oder entfernen.
            this.vehicleTypesTableAdapter.Fill(this.dataSet.VehicleTypes);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Vehicles". Sie können sie bei Bedarf verschieben oder entfernen.
            this.vehiclesTableAdapter.Fill(this.dataSet.Vehicles);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Routes". Sie können sie bei Bedarf verschieben oder entfernen.
            this.routesTableAdapter.Fill(this.dataSet.Routes);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Countries". Sie können sie bei Bedarf verschieben oder entfernen.
            this.countriesTableAdapter.Fill(this.dataSet.Countries);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Companies". Sie können sie bei Bedarf verschieben oder entfernen.
            this.companiesTableAdapter.Fill(this.dataSet.Companies);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Entfaltung". Sie können sie bei Bedarf verschieben oder entfernen.
            this.entfaltungTableAdapter.Fill(this.dataSet.Entfaltung);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Cities". Sie können sie bei Bedarf verschieben oder entfernen.
            this.citiesTableAdapter.Fill(this.dataSet.Cities);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Log". Sie können sie bei Bedarf verschieben oder entfernen.
            this.logTableAdapter.Fill(this.dataSet.Log);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Persons". Sie können sie bei Bedarf verschieben oder entfernen.
            this.personsTableAdapter.Fill(this.dataSet.Persons);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Tour". Sie können sie bei Bedarf verschieben oder entfernen.
            this.tourTableAdapter.Fill(this.dataSet.Tour);
            if (Properties.Settings.Default.AdminLocation != new Point(100, 100))
                this.Location = Properties.Settings.Default.AdminLocation;
            else this.Location = new Point(100, 100);
            if (Properties.Settings.Default.AdminSize != new Size(800, 500))
                this.Size = Properties.Settings.Default.AdminSize;
            else this.Size = new Size(800, 500);

            userNameToolStripStatusLabel.Text = AdminName + " (" + AdminID.ToString() + ")";

            mailserverGroupBox.Enabled = false;

            installationToolStripStatusLabel.Text = Properties.Settings.Default.InstallationType.ToString();
        }

        #region Close Form
        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.AdminLocation = this.Location;
            Properties.Settings.Default.AdminSize = this.Size;
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void closeToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        private void clipboardButton_Click(object sender, EventArgs e)
        {
            string textbox = "";
            switch(tablesTabControl.SelectedIndex)
            {
                case 0:
                    textbox = blRichTextBox.Text; 
                    break;
                case 1:
                    textbox = citiesTableRichTextBox.Text;
                    break;
                case 2:
                    textbox = companiesRichTextBox.Text;
                    break;
                case 3:
                    textbox = continentsRichTextBox.Text;
                    break;
                case 4:
                    textbox = countriesRichTextBox.Text;
                    break;
                case 5:
                    textbox = entfaltungRichTextBox.Text;
                    break;
                case 6:
                    textbox = goalsRichTextBox.Text;
                    break;
                case 7:
                    textbox = logRichTextBox.Text;
                    break;
                case 8:
                    textbox = notesRichTextBox.Text;
                    break;
                case 9:
                    textbox = personsRichTextBox.Text;
                    break;
                case 10:
                    textbox = routesRichTextBox.Text;
                    break;
                case 11:
                    textbox = routeTypesRichTextBox.Text;
                    break;
                case 12:
                    textbox = tourRichTextBox.Text;
                    break;
                case 13:
                    textbox = vehiclesRichTextBox.Text;
                    break;
                case 14:
                    textbox = vecTypesRichTextBox.Text;
                    break;
            }
            Clipboard.SetData(DataFormats.Text, (Object)textbox);
        }

        private void dashboardToolStripButton_Click(object sender, EventArgs e)
        {
            adminTabControl.SelectedIndex = 0;
        }

        private void userToolStripButton_Click(object sender, EventArgs e)
        {
            adminTabControl.SelectedIndex = 1;
        }

        private void passwordToolStripButton_Click(object sender, EventArgs e)
        {
            adminTabControl.SelectedIndex = 2;
        }

        private void databaseToolStripButton_Click(object sender, EventArgs e)
        {
            adminTabControl.SelectedIndex = 3;
        }

        private void serverToolStripButton_Click(object sender, EventArgs e)
        {
            adminTabControl.SelectedIndex = 4;
        }

        private void settingsToolStripButton_Click(object sender, EventArgs e)
        {
            adminTabControl.SelectedIndex = 5;
        }

        private void logToolStripButton_Click(object sender, EventArgs e)
        {
            adminTabControl.SelectedIndex = 6;
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void infoToolStripButton_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void leaveAdminToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void usernameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usernameComboBox.Text != "")
            {

            }
        }

        private void pwdButton_Click(object sender, EventArgs e)
        {
            PasswordForm pf = new PasswordForm();
            if (pf.ShowDialog() == DialogResult.OK)
            {
                pwdTextBox.Text = pf.Password;
            }
            else
            {
                pwdTextBox.Text = "";
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(CreateRandomPassword(8));
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            SmtpClient client = new SmtpClient(smtpTextBox.Text);
            MailAddress from = new MailAddress(fromTextBox.Text,
               "Admin BikeDB 2025",
            Encoding.UTF8);
            MailAddress to = new MailAddress(emailTextBox.Text);
            MailMessage message = new MailMessage(from, to);
            message.Body = messageRichTextBox.Text;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = subjectTextBox.Text;
            message.SubjectEncoding = Encoding.UTF8;
            client.SendCompleted += new
            SendCompletedEventHandler(SendCompletedCallback);
            // The userState can be any object that allows your callback
            // method to identify this send operation.
            // For this example, the userToken is a string constant.
            string userState = "test message1";
            client.SendAsync(message, userState);
            Console.WriteLine("Sending message... press c to cancel mail. Press any other key to exit.");
            string answer = Console.ReadLine();
            // If the user canceled the send, and mail hasn't been sent yet,
            // then cancel the pending operation.
            if (answer.StartsWith("c") && mailSent == false)
            {
                client.SendAsyncCancel();
            }
            // Clean up.
            message.Dispose();
            Console.WriteLine("Goodbye.");
        }

        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }

        private void useMailCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (useMailCheckBox.Checked == true)
            {
                sendButton.Enabled = true;
                mailserverGroupBox.Enabled = true;
            }
            else 
            { 
                sendButton.Enabled = false;
                mailserverGroupBox.Enabled = false;
            }
        }

        private void singleUserRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            loadDescription();
        }

        private void multiuserRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            loadDescription();
        }

        private void quickLoginRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            loadDescription();
        }

        private void strictRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            loadDescription();
        }

        private void singleAdminRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            loadDescription();
        }

        private void loadDescription()
        {
            Installation installation = Installation.MULTI_USER;
            if (singleUserRadioButton.Checked)
            {
                descRichTextBox.Text = "Nur eine Person nutzt diese Installation von BikeDB. Sie ist gleichzeitig Administrator, neue Benutzer " +
                    "oder Administratoren können aber nicht angelegt werden. Gleichzeitig entfällt das Login-Form: der aktuelle Administrator " +
                    "wird automatisch beim Starten der Anmeldung angemeldet. ";
                installation = Installation.SINGLE_USER;
            }
            else if (multiuserRadioButton.Checked)
            {
                descRichTextBox.Text = "Die Installation wird zur Multi-User-Anwendung (Standard). Es kann mehrere Administratoren und beliebig " +
                    " viele Benutzer geben. Die Datenbank kann mit mehreren Clients und/oder auf einem Server betrieben werden.";
                installation = Installation.MULTI_USER;
            }
            else if (quickLoginRadioButton.Checked)
            {
                descRichTextBox.Text = "Im Login-Formular werden Name und Passwort direkt ausgefüllt. Beides wird im aktuellen Windows-Anwender-" +
                    "Profil gespeichert. Nicht zu empfehlen bei lokalen Installationen oder wenn sich mehrere Anwender einen Windows-Account " +
                    "teilen. Sicherheitsrisiko: auch Administrator-Accounts werden lokal gespeichert. Wenn Anwender sich ausloggen, müssen beim " +
                    "nächsten Login Username und Passwort eingetragen werden.";
                installation = Installation.QUICK_LOGIN;
            }
            else if (strictRadioButton.Checked) 
            {
                descRichTextBox.Text = "Die Datenbank muss auf einem Server installiert werden. Clients, die zugreifen dürfen, müssen in einer " +
                    "Whitelist mit fester IP/Computernamen eingetragen sein. Beim Schließen der Anwendung erfolgt automatisch ein Logout. Username " +
                    "und Passwort müssen bei jedem Login eingetragen werden.";
                installation = Installation.STRICT;
            }
            else if (singleAdminRadioButton.Checked)
            {
                descRichTextBox.Text = "Lokale Installation mit mehreren Anwendern, es kann aber nur einen Administrator-Account geben. Wenn " +
                    "bereits mehrere Administratoren erstellt wurden, steht diese Option nicht mehr zur Verfügung.";
                installation = Installation.SINGLE_ADMIN;
            }
            else descRichTextBox.Text = "";
            //MessageBox.Show(installation.ToString());
            Properties.Settings.Default.InstallationType = installation.ToString();
            installationToolStripStatusLabel.Text = installation.ToString();
        }
    }
}
