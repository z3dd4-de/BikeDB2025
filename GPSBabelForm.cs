using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace BikeDB2024
{
    public partial class GPSBabelForm : Form
    {
        //const string GPSBABEL_PATH = @"C:\Program Files\GPSBabel\gpsbabel.exe";
        string GPSBABEL_PATH = Properties.Settings.Default.GPSBabelDir + "\\gpsbabel.exe";
        const string GPSBABEL_ARGS_TEMPLATE = "-w -r -t -i gpx -f \"{0}\" -o kml -F \"{1}\"";
        string exePlaceholder = "%GPSBABEL_PATH%";

        public GPSBabelForm()
        {
            InitializeComponent();
            openGpxFileDialog.Filter = "GPX-Dateien (*.gpx)|*.gpx|Alle Dateien (*.*)|*.*";
            openGpxFileDialog.Title = "GPX-Datei auswählen";
            openGpxFileDialog.Multiselect = false;
            openGpxFileDialog.CheckFileExists = true;
            openGpxFileDialog.InitialDirectory = Helpers.GetTraxFolder();

            if (!System.IO.File.Exists(GPSBABEL_PATH))
            {
                consoleRichTextBox.Text = $"Die GPSBabel-Executable wurde nicht gefunden: {GPSBABEL_PATH}\nBitte überprüfe die Einstellungen.";
                consoleRichTextBox.ForeColor = Color.Red;
                openButton.Enabled = false;
                convertButton.Enabled = false;
            }
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            if (openGpxFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openGpxFileDialog.FileName;
                originTextBox.Text = filePath;
                string destpath = filePath.Replace("gpx", "kml");
                destinationTextBox.Text = destpath;
                toolStripStatusLabel.Text = $"GPX-Datei ausgewählt: {filePath}";
                convertButton.Enabled = true;
                consoleRichTextBox.Text = $"{exePlaceholder} {string.Format(GPSBABEL_ARGS_TEMPLATE, filePath, destpath)}";
            }
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            if (consoleRichTextBox.Text.Contains(exePlaceholder))
            {
                string command = consoleRichTextBox.Text.Replace(exePlaceholder, "");

                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = GPSBABEL_PATH,
                        Arguments = command,
                        UseShellExecute = false, // Wichtig für Umleitung oder verstecktes Ausführen
                        CreateNoWindow = true    // Verhindert das Aufblitzen des Konsolenfensters
                    };

                    // Prozess starten
                    using (Process process = Process.Start(startInfo))
                    {
                        // Optional: Warten, bis die Konsolenanwendung beendet ist
                        process.WaitForExit(); 
                    }
                    consoleRichTextBox.AppendText("\n\nKonvertierung abgeschlossen.");
                    toolStripStatusLabel.Text = "Konvertierung abgeschlossen.";
                }
                catch (Exception ex)
                {
                    Helpers.ShowErrorMessage($"Fehler beim Starten von GPSBabel: {ex.Message}");
                    toolStripStatusLabel.Text = "Fehler bei der Konvertierung.";
                }
            }
            else
            {
                Helpers.ShowErrorMessage
                    ($"Fehler beim Starten von GPSBabel: {exePlaceholder} darf nicht geändert werden.\nDer Pfad zu GPSBabel wird in den Einstellungen festgelegt.");
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            originTextBox.Clear();
            destinationTextBox.Clear();
            consoleRichTextBox.Clear();
            convertButton.Enabled = false;
            toolStripStatusLabel.Text = "Eingabefelder wurden geleert. Wähle eine neue GPX-Datei aus.";
        }
    }
}
