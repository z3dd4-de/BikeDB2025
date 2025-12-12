using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BikeDB2024
{
    public partial class LoadBikeDataForm : Form
    {
        public RideData data;

        public LoadBikeDataForm(RideData data)
        {
            InitializeComponent();
            this.data = data;
        }

        private void LoadBikeDataForm_Load(object sender, EventArgs e)
        {
            if (data != null)
            {
                bikeLabel.Text = data.BikeName;
                dateTimePicker.Value = data.Timestamp;
                kmLabel.Text = $"{data.DistanceKm:F2} km";
                timeLabel.Text = data.Duration.ToString(@"hh\:mm\:ss");
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (data != null)
            {
                this.data.Timestamp = dateTimePicker.Value;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
