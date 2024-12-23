using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace BikeDB2024
{
    public partial class ImageViewerForm : Form
    {
        #region Private variables 
        private string filename;
        // The image's original size.
        private int imageWidth, imageHeight;
        private int imageWidth_orig = 0;
        private int imageHeight_orig = 0;

        // The current scale.
        private float imageScale = 1.0f;
        const float scale_per_delta = 0.1f / 120;
        const float scale_per_click = 120;
        #endregion

        #region Public properties
        // The file that is loaded into the picture box.
        public string Filename { get => filename; set => filename = value; }
        #endregion

        #region Constructors.
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
        #endregion
 
        /// <summary>
        /// Prepare the form when it is loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Create the image.
        /// </summary>
        private void createImage()
        {
            if (pictureBox.Image != null)
            {
                imageWidth = pictureBox.Image.Width;
                imageHeight = pictureBox.Image.Height;
                if (imageHeight_orig == 0 && imageWidth_orig == 0)
                {
                    imageWidth_orig = imageWidth; 
                    imageHeight_orig = imageHeight;
                }
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Size = new Size(imageWidth, imageHeight);
                this.MouseWheel += new MouseEventHandler(pictureBox_MouseWheel);
            }
        }

        /// <summary>
        /// Save Settings when the form is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.ImageViewerLocation = this.Location;
            Properties.Settings.Default.ImageViewerSize = this.Size;
        }

        /// <summary>
        /// Zoom in or out via mouse wheel event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Reset the image to original size and location.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void originalSizeToolStripButton_Click(object sender, EventArgs e)
        {
            loadFile(filename);
            pictureBox.Width = imageWidth_orig;
            pictureBox.Height = imageHeight_orig;
            pictureBox.Top = 0;
            pictureBox.Left = 0;
        }

        #region Zoom in
        private void zoomInToolStripButton_Click(object sender, EventArgs e)
        {
            zoomIn();
        }

        /// <summary>
        /// Zoom in, either by pressing '+' or via Toolstrip Menu item.
        /// </summary>
        private void zoomIn()
        {
            imageScale += scale_per_click * scale_per_delta;
            if (imageScale < 0) imageScale = 0;
            scaleToolStripStatusLabel.Text = imageScale.ToString("p0");
            if ((pictureBox.Width < (15 * this.Width)) && (pictureBox.Height < (15 * this.Height)))
            {
                // Change the size of the picturebox, multiply it by the ZOOMFACTOR
                pictureBox.Width = (int)(pictureBox.Width * 1.25);
                pictureBox.Height = (int)(pictureBox.Height * 1.25);

                pictureBox.Top = 0;
                pictureBox.Left = 0;
            }
        }
        #endregion

        #region Zoom out
        private void zoomOutToolStripButton_Click(object sender, EventArgs e)
        {
            zoomOut();
        }

        /// <summary>
        /// Zoom out, either by pressing '-' or via Toolstrip Menu item.
        /// </summary>
        private void zoomOut()
        {
            imageScale -= scale_per_click * scale_per_delta;
            if (imageScale < 0) imageScale = 0;
            scaleToolStripStatusLabel.Text = imageScale.ToString("p0");
            if ((pictureBox.Width > 0) && (pictureBox.Height > 0))
            {
                // Change the size of the picturebox, divide it by the ZOOMFACTOR
                pictureBox.Width = (int)(pictureBox.Width / 1.25);
                pictureBox.Height = (int)(pictureBox.Height / 1.25);

                pictureBox.Top = 0;
                pictureBox.Left = 0;
            }
        }
        #endregion

        /// <summary>
        /// Load file into the image viewer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadFileToolStripButton_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() == DialogResult.OK)
            {
                loadFile(openImageDialog.FileName);
            }
        }

        /// <summary>
        /// Move image to Location (0, 0).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            pictureBox.Location = new Point(0, 0);
        }

        #region Show Help
        private void showHelp()
        {
            ImageViewerHelpForm helpForm = new ImageViewerHelpForm();
            helpForm.ShowDialog();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            showHelp();
        }
        #endregion

        /// <summary>
        /// Keyboard events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageViewerForm_KeyUp(object sender, KeyEventArgs e)
        {
            const int delta = 50;
            switch (e.KeyValue)
            {
                case (char)Keys.Down:
                    pictureBox.Top += delta;
                    break;
                case (char)Keys.Up:
                    pictureBox.Top -= delta;
                    break;
                case (char)Keys.Left:
                    pictureBox.Left -= delta;
                    break;
                case (char)Keys.Right:
                    pictureBox.Left += delta;
                    break;
                case (char)Keys.Escape:
                    Close();
                    break;
                case (char)Keys.F1:
                    showHelp();
                    break;
                case (char)Keys.Enter:
                    pictureBox.Location = new Point(0, 0);
                    break;
                case (char)Keys.OemMinus:
                    zoomOut();
                    break;
                case (char)Keys.Oemplus:
                    zoomIn();
                    break;
                default:
                    break;
            }
            e.Handled = true;
        }

        /// <summary>
        /// Save the image with the current dimensions to a new file. PNG and JPEG output formats are possible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            pictureBox.Location = new Point(0, 0);
            Bitmap bmp = new Bitmap(pictureBox.Image, pictureBox.Width, pictureBox.Height);
            pictureBox.DrawToBitmap(bmp, new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));

            if (saveImageDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveImageDialog.FilterIndex == 1)
                {
                    bmp.Save(saveImageDialog.FileName, ImageFormat.Jpeg);
                }
                else
                {
                    bmp.Save(saveImageDialog.FileName, ImageFormat.Png);
                }
            }
        }

        /// <summary>
        /// When the picture box is resized the current size is shown in the status strip.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            sizeToolStripStatusLabel.Text = " [ Breite: " + pictureBox.Width.ToString() + " - Höhe: " + pictureBox.Height.ToString() + " ]";
        }

        /// <summary>
        /// Load the given file into the picture box.
        /// </summary>
        /// <param name="file"></param>
        private void loadFile(string file)
        {
            filename = file;
            filenameToolStripStatusLabel.Text = filename;
            pictureBox.Image = Image.FromFile(filename);
            if (imageHeight_orig == 0 && imageWidth_orig == 0)
            {
                imageWidth = imageWidth_orig;
                imageHeight = imageHeight_orig;
            }
            else
            {
                imageWidth = pictureBox.Image.Width;
                imageHeight = pictureBox.Image.Height;
            }
            imageScale = 1.0f;
            scaleToolStripStatusLabel.Text = imageScale.ToString("p0");
        }
    }
}
