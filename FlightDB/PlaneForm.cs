using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BikeDB2024.FlightDB
{
    public partial class PlaneForm : Form
    {
        public int EditId { get; set; }
        public bool Edit { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlaneForm()
        {
            InitializeComponent();
        }

        private void PlaneForm_Load(object sender, EventArgs e)
        {
            modeComboBox.SelectedIndex = 2;
            errorToolStripStatusLabel.Text = "";
        }

        private void load()
        {

        }

        private void imageButton_Click(object sender, EventArgs e)
        {
            if (openImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageTextBox.Text = openImageFileDialog.FileName;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {

        }
    }
}
