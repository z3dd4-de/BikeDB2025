using System;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class CompanyForm : Form
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CompanyForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Prepare the form after loading.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompanyForm_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "dataSet.Companies". Sie können sie bei Bedarf verschieben oder entfernen.
            this.companiesTableAdapter.Fill(this.dataSet.Companies);

            errorToolStripStatusLabel.Text = "";
            nameEmpty();
        }

        /// <summary>
        /// nameTextBox cannot be empty.
        /// </summary>
        private void nameEmpty()
        {
            if (nameTextBox.Text == "") addButton.Enabled = false;
        }

        /// <summary>
        /// Validate nameTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (companiesComboBox.FindStringExact(nameTextBox.Text) >= 0)
            {
                errorToolStripStatusLabel.Text = "Fehler: Hersteller ist schon vorhanden!";
                addButton.Enabled = false;
            }
            else
            {
                addButton.Enabled = true;
                errorToolStripStatusLabel.Text = "";
            }
            nameEmpty();
        }

        /// <summary>
        /// Insert new company into database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                int length = companiesComboBox.Items.Count;
                DataSetTableAdapters.CompaniesTableAdapter adapter = new DataSetTableAdapters.CompaniesTableAdapter();
                adapter.Insert(length, nameTextBox.Text, linkTextBox.Text, DateTime.Now, DateTime.Now, Properties.Settings.Default.CurrentUserID);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Anlegen neuer Hersteller");
                this.DialogResult= DialogResult.Abort;
            }
            Close();
        }
    }
}
