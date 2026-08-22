using System;
using System.Windows.Forms;

namespace BikeDB2024
{
    public partial class ImportJsonForm : Form
    {
        public string SelectedFilePath => fileTextBox.Text;
        public bool ImportDate => dateCheckBox.Checked;
        public bool ImportBike => bikeCheckBox.Checked;

        public ImportJsonForm()
        {
            InitializeComponent();
            toolStripStatusLabel.Text = "Wähle eine JSON-Datei zum Importieren.";
            openJsonFileDialog.Filter = "JSON-Dateien (*.json)|*.json|Alle Dateien (*.*)|*.*";
            openJsonFileDialog.Title = "JSON-Datei auswählen";
            openJsonFileDialog.Multiselect = false;
            openJsonFileDialog.InitialDirectory = Helpers.GetSNAFolder();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            if (openJsonFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openJsonFileDialog.FileName;
                fileTextBox.Text = filePath;
                toolStripStatusLabel.Text = $"Datei ausgewählt: {filePath}";
                if (filePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                {
                    importButton.Enabled = true;
                }
                else
                {
                    importButton.Enabled = false;
                    MessageBox.Show("Bitte wähle eine gültige JSON-Datei aus.", "Ungültige Datei", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            ImportJsonPreviewForm preview = new ImportJsonPreviewForm(SelectedFilePath);
            preview.Show();
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
