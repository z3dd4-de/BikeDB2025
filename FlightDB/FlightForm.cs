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
    public partial class FlightForm : Form
    {
        public int EditId { get; set; }
        public bool Edit { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public FlightForm()
        {
            InitializeComponent();
        }

        private void FlightForm_Load(object sender, EventArgs e)
        {
            classComboBox.SelectedIndex = 2;
        }
    }
}
