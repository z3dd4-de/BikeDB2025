using System;
using System.IO;
using System.Windows.Forms;

namespace BikeDB2024
{
    public partial class ImageViewerHelpForm : Form
    {
        public ImageViewerHelpForm()
        {
            InitializeComponent();
        }

        private void closeToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ImageViewerHelpForm_Load(object sender, EventArgs e)
        {
            string curDir = Directory.GetCurrentDirectory();
            this.helpBrowser.Url = new Uri(String.Format("file:///{0}/HelpPages/BildbetrachterHilfe.html", curDir));
        }

        private void ImageViewerHelpForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case (char)Keys.Escape:
                    Close();
                    break;
                default:
                    break;
            }
            e.Handled = true;
        }
    }
}
