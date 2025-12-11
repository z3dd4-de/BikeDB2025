using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace BikeDB2024
{
    public partial class SigmaDsForm : Form
    {
        public SigmaDsForm()
        {
            InitializeComponent();
            changeStatusLabel("");
            dsComboBox.SelectedIndex = 0;
            dsComboBox.Enabled = false;
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
    }
}
