using System;
using System.IO;
using System.Windows.Forms;

namespace BikeDB2024
{
    public partial class WelcomeForm : Form
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public WelcomeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the welcome page into the browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            string curDir = Directory.GetCurrentDirectory();

            webBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/Willkommen.html", curDir));
        }

        /// <summary>
        /// If checked, the form is shown at each start of the BikeDB.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showAtStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowWelcomeForm = showAtStartCheckBox.Checked;
        }

        /// <summary>
        /// Close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowWelcomeForm = showAtStartCheckBox.Checked;
            Close();
        }
    }
}
