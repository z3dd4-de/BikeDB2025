using System;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class ExportForm : Form
    {
        private string filename;
        private ProgressForm progressForm;
        // Array Size needs to be changed whenever new tables for export are introduced. 
        private Array checkboxes = new bool[12];

        /// <summary>
        /// Constructor.
        /// </summary>
        public ExportForm()
        {
            InitializeComponent();
            progressForm = new ProgressForm();
            progressForm.JobType = JobType.EXPORT;
        }

        #region Folder
        /// <summary>
        /// Open the folder where the backup file will be stored.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openButton_Click(object sender, EventArgs e)
        {
            openFolderBrowserDialog.SelectedPath = Properties.Settings.Default.ImageFolder;
            if (openFolderBrowserDialog.ShowDialog() == DialogResult.OK )
            {
                folderTextBox.Text = openFolderBrowserDialog.SelectedPath;
            }
        }

        private void folderTextBox_TextChanged(object sender, EventArgs e)
        {
            progressForm.SaveFolder = folderTextBox.Text;
            checkButtonState();
        }
        #endregion

        #region Checkboxes
        /// <summary>
        /// Switch between disabled and checked or not checked and enabled checkboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (allCheckBox.Checked)
            {
                tourCheckBox.Checked = true;
                cityCheckBox.Checked = true;
                entfaltungCheckBox.Checked = true;
                manufacturerCheckBox.Checked = true;
                countryCheckBox.Checked = true;
                vehicleCheckBox.Checked = true;
                routeCheckBox.Checked = true;
                vectypeCheckBox.Checked = true;
                routetypeCheckBox.Checked = true;
                personsCheckBox.Checked = true;
                goalsCheckBox.Checked = true;
                notesCheckBox.Checked = true;

                tourCheckBox.Enabled = false;
                cityCheckBox.Enabled = false;
                entfaltungCheckBox.Enabled = false;
                manufacturerCheckBox.Enabled = false;
                countryCheckBox.Enabled = false;
                vehicleCheckBox.Enabled = false;
                routeCheckBox.Enabled = false;
                vectypeCheckBox.Enabled = false;
                routetypeCheckBox.Enabled = false;
                personsCheckBox.Enabled = false;
                goalsCheckBox.Enabled = false;
                notesCheckBox.Enabled = false;
            }
            else
            {
                tourCheckBox.Checked = false;
                cityCheckBox.Checked = false;
                entfaltungCheckBox.Checked = false;
                manufacturerCheckBox.Checked = false;
                countryCheckBox.Checked = false;
                vehicleCheckBox.Checked = false;
                routeCheckBox.Checked = false;
                vectypeCheckBox.Checked = false;
                routetypeCheckBox.Checked = false;
                personsCheckBox.Checked = false;
                goalsCheckBox.Checked = false;
                notesCheckBox.Checked = false;

                tourCheckBox.Enabled = true;
                cityCheckBox.Enabled = true;
                entfaltungCheckBox.Enabled = true;
                manufacturerCheckBox.Enabled = true;
                countryCheckBox.Enabled = true;
                vehicleCheckBox.Enabled = true;
                routeCheckBox.Enabled = true;
                vectypeCheckBox.Enabled = true;
                routetypeCheckBox.Enabled = true;
                personsCheckBox.Enabled = true;
                goalsCheckBox.Enabled = true;
                notesCheckBox.Enabled = true;
            }
        }

        private void tourCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        private void countryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        private void cityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        private void routeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        private void vehicleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        private void manufacturerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        private void entfaltungCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        private void vectypeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        private void routetypeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        private void personsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        private void notesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        private void goalsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            checkButtonState();
        }

        /// <summary>
        /// At least one checkbox needs to be checked and a folder is chosen for the button to be enabled.
        /// </summary>
        private void checkButtonState()
        {
            if ((tourCheckBox.Checked || countryCheckBox.Checked || cityCheckBox.Checked || routeCheckBox.Checked
                || vehicleCheckBox.Checked || manufacturerCheckBox.Checked || entfaltungCheckBox.Checked || vectypeCheckBox.Checked
                || routetypeCheckBox.Checked || personsCheckBox.Checked || goalsCheckBox.Checked || notesCheckBox.Checked) 
                && folderTextBox.Text != "")
            {
                exportButton.Enabled = true;
            }
            else 
            { 
                exportButton.Enabled = false; 
            }
        }
        #endregion

        /// <summary>
        /// Check checkboxes and start ProgressForm which does all the work.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportButton_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string today = dt.ToString("yyyyMMdd");
            filename = @"bike_db_export_" + today + ".xls";
            progressForm.FileName = filename;

            checkboxes.SetValue(tourCheckBox.Checked, 0);
            checkboxes.SetValue(countryCheckBox.Checked, 1);
            checkboxes.SetValue(cityCheckBox.Checked, 2);
            checkboxes.SetValue(routeCheckBox.Checked, 3);
            checkboxes.SetValue(vehicleCheckBox.Checked, 4);
            checkboxes.SetValue(manufacturerCheckBox.Checked, 5);
            checkboxes.SetValue(entfaltungCheckBox.Checked, 6);
            checkboxes.SetValue(vectypeCheckBox.Checked, 7);
            checkboxes.SetValue(routetypeCheckBox.Checked, 8);
            checkboxes.SetValue(personsCheckBox.Checked, 9);
            checkboxes.SetValue(notesCheckBox.Checked, 10);
            checkboxes.SetValue(goalsCheckBox.Checked, 11);

            progressForm.Checkboxes = checkboxes;

            if (progressForm.ShowDialog() == DialogResult.OK)
            {
                Close();
            }
        }
    }
}
