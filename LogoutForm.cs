using System;
using System.Windows.Forms;

namespace BikeDB2024
{
    public partial class LogoutForm : Form
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public LogoutForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Cancel logout.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Confirm logout.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.UserLoggedIn = false;
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
