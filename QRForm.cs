using System.Drawing;
using System.Windows.Forms;
using QRCoder;

namespace BikeDB2024
{
    public partial class QRForm : Form
    {
        private string link;

        /// <summary>
        /// Used by a context menu that provides the link which is to be converted to a QR code.
        /// </summary>
        /// <param name="link"></param>
        public QRForm(string link)
        {
            InitializeComponent();
            this.link = link;
            loadLink();
        }

        /// <summary>
        /// Load the link into the form (picture box and linklabel).
        /// </summary>
        private void loadLink()
        {
            if (link != null)
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(link, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                linkQrLabel.Text = link;
                qrPictureBox.Image = qrCodeImage;
            }
            else
            {
                linkQrLabel.Text = "";
                qrPictureBox.Image = null;
            }
        }

        /// <summary>
        /// Open the link in a browser (if valid).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkQrLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //string target = e.Link.LinkData as string;
            if (linkQrLabel.Text.Length > 0 && linkQrLabel.Text != "linkLabel1")
            {
                System.Diagnostics.Process.Start(linkQrLabel.Text);
            }
        }
    }
}
