using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace BikeDB2024
{
    public partial class SigmaDsForm : Form
    {
        public SigmaDsForm()
        {
            InitializeComponent();
            
            LoadSettings();
        }

        private void LoadSettings()
        {
            changeStatusLabel("");
            dsComboBox.SelectedIndex = 0;
            dsComboBox.Enabled = false;
            saveButton.Enabled = false;
            dsInstalledCheckBox.Checked = Properties.Settings.Default.SigmaDsEnabled;
            pathTextBox.Text = Properties.Settings.Default.SigmaDirectory;
        }

        private void dsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Not implemented because dsComboBox is disabled
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SigmaDsVersion = dsComboBox.SelectedItem.ToString();
        }

        private void installLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(installLinkLabel.Text);
        }

        private void directoryButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                pathTextBox.Text = folderBrowserDialog.SelectedPath;
                Properties.Settings.Default.SigmaDirectory = pathTextBox.Text;
            }
        }

        private void changeStatusLabel(string text)
        {
            statusLabel.Text = text;
        }

        private void dsInstalledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (dsInstalledCheckBox.Checked)
            {
                Properties.Settings.Default.SigmaDsEnabled = true;
            }
            else
            {
                Properties.Settings.Default.SigmaDsEnabled = false;
            }
        }

        private void SigmaDsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.SigmaDsVersion = dsComboBox.SelectedItem.ToString();
            Properties.Settings.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
