using System;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class ImportForm : Form
    {
        #region Private variables
        private string file = "";
        private JobStatus status;
        private ProgressForm progressForm;
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public ImportForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the form is loaded, the ProgressForm is prepared.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportForm_Load(object sender, EventArgs e)
        {
            progressForm = new ProgressForm(Properties.Settings.Default.AdminLoggedIn);
            progressForm.JobType = JobType.IMPORT;
            status = JobStatus.NONE;
            openImportFileDialog.InitialDirectory = Properties.Settings.Default.ImageFolder;
            checkImportButton();
        }

        /// <summary>
        /// Input on fileTextBox which cannot be empty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileTextBox_TextChanged(object sender, EventArgs e)
        {
            testFileInput();
        }

        /// <summary>
        /// Validate fileTextBox.
        /// </summary>
        /// <returns>True if textbox is not empty, else false.</returns>
        private bool testFileInput()
        {
            bool ret = false;
            if (fileTextBox.Text == String.Empty)
            {
                ret = false;
            }
            else ret = true;
            return ret;
        }

        /// <summary>
        /// Open FileDialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openButton_Click(object sender, EventArgs e)
        {
            if (openImportFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileTextBox.Text = openImportFileDialog.FileName;
                checkImportButton();
            }
        }

        /// <summary>
        /// Starts the import within the ProgressForm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importButton_Click(object sender, EventArgs e)
        {
            file = fileTextBox.Text;
            progressForm.FileName = file;
            progressForm.Status = status;
            if (progressForm.ShowDialog() == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK; 
                Close();
            }
        }

        /// <summary>
        /// Calls checkImportButton().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkImportButton();
        }

        /// <summary>
        /// Calls checkImportButton().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            checkImportButton();
        }

        /// <summary>
        /// Calls checkImportButton().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tourRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            checkImportButton();
        }

        /// <summary>
        /// Validate RadioButtons and sets Job type.
        /// </summary>
        /// <returns>True if a mode is set and a radiobutton is selected otherwise false.</returns>
        private bool checkJob()
        {
            bool ret = false;
            if (!allRadioButton.Checked && !tourRadioButton.Checked)
            {
                ret = false;
            }
            // Daten anfügen = 0; Daten überschreiben = 1
            else if (modusComboBox.SelectedIndex == 0 || modusComboBox.SelectedIndex == 1)
            {
                switch (modusComboBox.SelectedIndex)
                {
                    case 0:
                        if (allRadioButton.Checked)
                        {
                            status = JobStatus.APPEND_ALL;
                        }
                        else if (tourRadioButton.Checked)
                        {
                            status = JobStatus.APPEND_TOUR;
                        }
                        if (testFileInput()) ret = true;
                        break;
                    case 1:
                        if (allRadioButton.Checked)
                        {
                            status = JobStatus.DROP_ALL;
                        }
                        else if (tourRadioButton.Checked)
                        {
                            status = JobStatus.DROP_TOUR;
                        }
                        if (testFileInput()) ret = true;
                        break;
                    default:
                        ret = false;
                        break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Enables or disables ImportButton.
        /// </summary>
        private void checkImportButton()
        {
            if (checkJob()) importButton.Enabled = true;
            else importButton.Enabled = false;
        }
    }
}
