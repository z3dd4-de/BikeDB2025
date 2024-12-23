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
    public partial class AirlineForm : Form
    {
        public int EditId { get; set; }
        public bool Edit { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public AirlineForm()
        {
            InitializeComponent();
        }

        private void AirlineForm_Load(object sender, EventArgs e)
        {

        }
    }
}
