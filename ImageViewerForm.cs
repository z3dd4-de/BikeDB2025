using System;
using System.Drawing;
using System.Windows.Forms;

namespace BikeDB2024
{
    public partial class ImageViewerForm : Form
    {
        private string filename;
        // The image's original size.
        private int imageWidth, imageHeight;

        // The current scale.
        private float imageScale = 1.0f;

        public ImageViewerForm()
        {
            InitializeComponent();            
        }

        public ImageViewerForm(Image img)
        {
            InitializeComponent();
            this.pictureBox.Image = img;
            createImage();
        }

        public string Filename { get => filename; set => filename = value; }

        private void ImageViewerForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ImageViewerLocation != new Point(150, 150))
            {
                this.Location = Properties.Settings.Default.ImageViewerLocation;
            }
            if (Properties.Settings.Default.ImageViewerSize != new Size(750, 600))
            {
                this.Size = Properties.Settings.Default.ImageViewerSize;
            }

            if (filename != null)
            {
                filenameToolStripStatusLabel.Text = filename;
                pictureBox.Image = Image.FromFile(filename);
                createImage();
            }
        }

        private void createImage()
        {
            if (pictureBox.Image != null)
            {
                imageWidth = pictureBox.Image.Width;
                imageHeight = pictureBox.Image.Height;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Size = new Size(imageWidth, imageHeight);
                this.MouseWheel += new MouseEventHandler(pictureBox_MouseWheel);
            }
        }

        private void ImageViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.ImageViewerLocation = this.Location;
            Properties.Settings.Default.ImageViewerSize = this.Size;
        }

        private void pictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            const float scale_per_delta = 0.1f / 120;
            imageScale += e.Delta * scale_per_delta;
            if (imageScale < 0) imageScale = 0;
            scaleToolStripStatusLabel.Text = imageScale.ToString("p0");
            // If the mouse wheel is moved forward (Zoom in)
            if (e.Delta > 0)
            {
                // Check if the pictureBox dimensions are in range (15 is the minimum and maximum zoom level)
                if ((pictureBox.Width < (15 * this.Width)) && (pictureBox.Height < (15 * this.Height)))
                {
                    // Change the size of the picturebox, multiply it by the ZOOMFACTOR
                    pictureBox.Width = (int)(pictureBox.Width * 1.25);
                    pictureBox.Height = (int)(pictureBox.Height * 1.25);

                    // Formula to move the picturebox, to zoom in the point selected by the mouse cursor
                    pictureBox.Top = (int)(e.Y - 1.25 * (e.Y - pictureBox.Top));
                    pictureBox.Left = (int)(e.X - 1.25 * (e.X - pictureBox.Left));
                }
            }
            else
            {
                // Check if the pictureBox dimensions are in range (15 is the minimum and maximum zoom level)
                if ((pictureBox.Width > 0) && (pictureBox.Height > 0))
                {
                    // Change the size of the picturebox, divide it by the ZOOMFACTOR
                    pictureBox.Width = (int)(pictureBox.Width / 1.25);
                    pictureBox.Height = (int)(pictureBox.Height / 1.25);

                    // Formula to move the picturebox, to zoom in the point selected by the mouse cursor
                    pictureBox.Top = (int)(e.Y - 0.80 * (e.Y - pictureBox.Top));
                    pictureBox.Left = (int)(e.X - 0.80 * (e.X - pictureBox.Left));
                }
            }
            /*// The amount by which we adjust scale per wheel click.
            const float scale_per_delta = 0.1f / 120;

            // Update the drawing based upon the mouse wheel scrolling.
            imageScale += e.Delta * scale_per_delta;
            if (imageScale < 0) imageScale = 0;

            // Size the image.
            //pictureBox.Size = new Size((int)(imageWidth * imageScale),
            //    (int)(imageHeight * imageScale));
            imageWidth = (int)(imageWidth * imageScale);
            imageHeight = (int)(imageHeight * imageScale);
            pictureBox.Top = (int) (e.Y - scale_per_delta * (e.Y - pictureBox.Top));
            pictureBox.Left = (int) (e.X - scale_per_delta * (e.X - pictureBox.Left));
            pictureBox.Size = new Size(imageWidth, imageHeight);
            //MessageBox.Show(pictureBox.Top.ToString(), pictureBox.Left.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);

            scaleToolStripStatusLabel.Text = imageScale.ToString("p0") + " ("+ pictureBox.Top.ToString() + "/" +pictureBox.Left.ToString()+")";
*/
        }

        /// <summary>
        /// Close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void originalSizeToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void zoomInToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void zoomOutToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void loadFileToolStripButton_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() == DialogResult.OK)
            {
                filename = openImageDialog.FileName;
                filenameToolStripStatusLabel.Text = filename;
                pictureBox.Image = Image.FromFile(filename);
                imageWidth = pictureBox.Image.Width;
                imageHeight = pictureBox.Image.Height;
                imageScale = 1.0f;
                scaleToolStripStatusLabel.Text = imageScale.ToString("p0");
            }
        }
    }
}
