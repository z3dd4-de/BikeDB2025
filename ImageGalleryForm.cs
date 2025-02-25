using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Shapes;
using ExcelLibrary.BinaryFileFormat;
using static BikeDB2024.Helpers;
using static BikeDB2024.ImageGalleryForm;

namespace BikeDB2024
{
    public partial class ImageGalleryForm : Form
    {
        #region Properties/Variables
        public int Id { get; set; }
        public string CurrentPath { get; set; }
        public GalleryType Gallery { get; set; }
        private List<Image> images = new List<Image>();
        private List<PictureBox> boxes = new List<PictureBox>();
        Bitmap folder = GalleryResources.icons8_folder_48;
        Bitmap image = GalleryResources.icons8_image_48;
        Bitmap flight = GalleryResources.icons8_airport_32;
        Bitmap route = GalleryResources.icons8_journey_32;
        private ImageList imageList = new ImageList();
        //Thumbnail sizes
        private int twidth = 100;
        private int theight = 100;
        private const int small = 100;
        private const int medium = 200;
        private const int large = 350;
        enum ThumbnailSize { SMALL,  MEDIUM, LARGE }
        private ThumbnailSize currentSize = ThumbnailSize.SMALL;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public ImageGalleryForm()
        {
            InitializeComponent();
            Id = -1;
        }

        /// <summary>
        /// Constructor with given Id loads the corresponding gallery directly.
        /// </summary>
        /// <param name="id"></param>
        public ImageGalleryForm(int id)
        {
            InitializeComponent();
            this.Id = id;
            load();
        }
        #endregion

        /// <summary>
        /// Initialize the form during load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageGaleryForm_Load(object sender, EventArgs e)
        {
            GetImages();
            if (Properties.Settings.Default.ImageGalleryLocation != new System.Drawing.Point(80, 80))
                this.Location = Properties.Settings.Default.ImageGalleryLocation;
            else this.Location = new System.Drawing.Point(80, 80);
            if (Properties.Settings.Default.ImageGallerySize != new System.Drawing.Size(800, 500))
                this.Size = Properties.Settings.Default.ImageGallerySize;
            else this.Size = new System.Drawing.Size(800, 500);
            if (Id == -1) loadTabPage(0);
            else 
            { 
                loadTabPage(1); 
            }
            imageOpenFileDialog.InitialDirectory = Properties.Settings.Default.ImageFolder;
            //galleryFolderBrowserDialog.RootFolder = Environment.SpecialFolder.MyPictures;
            galleryToolStripStatusLabel.Text = "";
            if (Properties.Settings.Default.ImageGalleryViewer == "ImageViewer")
            {
                bildbetrachterToolStripMenuItem.Checked = true;
                systemToolStripMenuItem.Checked = false;
            }
            else
            {
                bildbetrachterToolStripMenuItem.Checked = false;
                systemToolStripMenuItem.Checked = true;
            }
            fillTreeViews();
        }

        #region Close form
        private void ImageGaleryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.ImageGalleryLocation = this.Location;
            Properties.Settings.Default.ImageGallerySize = this.Size;
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void closeToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        /// <summary>
        /// Load the gallery with a given Id. Constructor: ImageGallery(id)
        /// </summary>
        private void load()
        {
            SqlConnection myConnection;
            try
            {
                using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand())
                    {
                        string sql = $"SELECT * FROM ImageGalleries WHERE Id = {Id}";   //[User] = " + Properties.Settings.Default.CurrentUserID;
                        myCommand.CommandText = sql;
                        myCommand.CommandType = CommandType.Text;
                        myCommand.Connection = myConnection;

                        using (SqlDataReader reader = myCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //galleryDetailsTreeView
                                //galleryOverwiewTreeView
                            }
                        }
                    }
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Laden der Bildergalerie");
                galleryToolStripStatusLabel.Text = "Fehler beim Laden!";
            }
            loadTabPage(1);
        }

        private void GetImages()
        {
            imageList.Images.Add("Route", route);
            imageList.Images.Add("Flight", flight);
            imageList.Images.Add("Image", image);
            imageList.Images.Add("Folder", folder); 

            galleryDetailsTreeView.ImageList = imageList;
            galleryOverwiewTreeView.ImageList = imageList;
        }

        private void fillTreeViews()
        {
            galleryDetailsTreeView.Nodes.Clear();
            galleryOverwiewTreeView.Nodes.Clear();

            SqlConnection myConnection;
            try
            {
                using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand())
                    {
                        string sql = $"SELECT * FROM ImageGalleries WHERE [User] = " + Properties.Settings.Default.CurrentUserID +
                            " ORDER BY Date";
                        myCommand.CommandText = sql;
                        myCommand.CommandType = CommandType.Text;
                        myCommand.Connection = myConnection;
                        string key = "";

                        using (SqlDataReader reader = myCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(1))
                                {
                                    if (reader.GetByte(1) == 0)
                                    {
                                        key = "Route";
                                    }
                                    else if (reader.GetByte(1) == 1)
                                    {
                                        key = "Flight";
                                    }
                                }
                                TreeNode node = new TreeNode();
                                node.Text = Convert.ToDateTime(reader[5]).ToString("yyyy-MM-dd");
                                node.ImageKey = key;
                                node.Tag = reader[3].ToString() + ";" + reader[4].ToString();
                                galleryOverwiewTreeView.Nodes.Add(node);
                                galleryDetailsTreeView.Nodes.Add(node);
                                //System.Windows.Forms.MessageBox.Show(Convert.ToDateTime(reader[5]).ToString("yyyy-MM-dd"));
                            }
                        }
                    }
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Laden der Bildergalerie");
                galleryToolStripStatusLabel.Text = "Fehler beim Laden!";
            }
        }

        /// <summary>
        /// Switch tab pages programmatically.
        /// </summary>
        /// <param name="page"></param>
        private void loadTabPage(int page)
        {
            galleryTabControl.SelectedIndex = page;
        }

        private void galleryTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(e.Node.Tag.ToString());
        }

        private void galleryOverwiewTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag.ToString() != "")
            {
                string[] tag = e.Node.Tag.ToString().Split(';');
                CurrentPath = tag[1];
                if (tag[0] == "0")
                {
                    Gallery = GalleryType.FILE;
                }
                else if (tag[0] == "1")
                {
                    Gallery = GalleryType.FOLDER;
                }
                ClearGalleryOverview();
                Console.WriteLine(CurrentPath);
                Console.WriteLine(Gallery.ToString());
                galleryBackgroundWorker.RunWorkerAsync();
            }
        }

        private void lastButton_Click(object sender, EventArgs e)
        {

        }

        private void nextButton_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker_Leave(object sender, EventArgs e)
        {
            string type = linkComboBox.Text;
            SqlConnection myConnection;
            try
            {
                using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand())
                    {
                        string sql = "";
                        if (type == "Flug")
                        {
                            sql = "SELECT * FROM Flights WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                        }
                        else //if (type == "Tagestour")
                        {
                            sql = "SELECT * FROM Tour WHERE [User] = " + Properties.Settings.Default.CurrentUserID;
                        }
                        myCommand.CommandText = sql;
                        myCommand.CommandType = CommandType.Text;
                        myCommand.Connection = myConnection;

                        using (SqlDataReader reader = myCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(1))
                                {
                                    DateTime dateTime = reader.GetDateTime(1);
                                    if (dateTime == dateTimePicker.Value)
                                    {

                                    }
                                }
                            }
                        }
                    }
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Laden von Daten (Bildergallerie)");
                galleryToolStripStatusLabel.Text = "Fehler beim Laden!";
            }
        }

        public enum GalleryType { FILE, FOLDER }

        private GalleryType getPathType()
        {
            GalleryType galleryType = GalleryType.FILE;
            if (singleRadioButton.Checked)
            {
                galleryType = GalleryType.FILE;
            }
            else if (folderRadioButton.Checked)
            {
                galleryType = GalleryType.FOLDER;
            }
            return galleryType;
        }

        /// <summary>
        /// Depending on either file or folder the respective dialog is opened.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openButton_Click(object sender, EventArgs e)
        {
            if (singleRadioButton.Checked)
            {
                if (imageOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pathTextBox.Text = imageOpenFileDialog.FileName;
                }
            }
            else if (folderRadioButton.Checked)
            {
                if (galleryFolderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    pathTextBox.Text = galleryFolderBrowserDialog.SelectedPath;
                }
            }
        }

        private void dataComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Create a new gallery.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createGalleryButton_Click(object sender, EventArgs e)
        {
            SqlConnection myConnection;
            try
            {
                using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand())
                    { 
                        int id = NextId("ImageGalleries");
                        string sqlquery = "INSERT INTO ImageGalleries" +
                        " (Id, LinkType, Type, PathType, Path, Date, LinkId, Remark, Created, LastChanged, [User]) " +
                        "VALUES (@id, @link, @type, @patht, @path, @date, @linkid, @remark, @created, @last, @user)";
                        myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        myCommand.Parameters.Add("@link", SqlDbType.TinyInt).Value = linkComboBox.SelectedIndex;
                        myCommand.Parameters.Add("@type", SqlDbType.NVarChar).Value = typeComboBox.Text;
                        myCommand.Parameters.Add("patht", SqlDbType.TinyInt).Value = getPathType();
                        myCommand.Parameters.Add("@path", SqlDbType.NVarChar).Value = pathTextBox.Text;
                        myCommand.Parameters.Add("date", SqlDbType.Date).Value = dateTimePicker.Value;
                        myCommand.Parameters.Add("@linkid", SqlDbType.Int).Value = 1; // dataComboBox.SelectedValue;
                        myCommand.Parameters.Add("@remark", SqlDbType.NVarChar).Value = remarkRichTextBox.Text;
                        myCommand.Parameters.Add("@created", SqlDbType.DateTime).Value = DateTime.Now;
                        myCommand.Parameters.Add("@last", SqlDbType.DateTime).Value = DateTime.Now;
                        myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                        myCommand.CommandText = sqlquery;
                        myCommand.CommandType = CommandType.Text;
                        myCommand.Connection = myConnection;
                        myCommand.ExecuteNonQuery();
                    }
                }
                myConnection.Close();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Anlegen einer neuen Bildergalerie");
                galleryToolStripStatusLabel.Text = "Fehler in den eingegebenen Daten!";
            }
            finally { clearFields(); }
        }

        /// <summary>
        /// After a new gallery has been created, all entries are reset.
        /// </summary>
        private void clearFields()
        {
            linkComboBox.Text = "";
            typeComboBox.Text = "";
            singleRadioButton.Checked = false;
            folderRadioButton.Checked = true;
            deletePathTextBox();
            dateTimePicker.Value = DateTime.Now;
            dataComboBox.Items.Clear();
            remarkRichTextBox.Text = "";
        }

        #region link and type depend on each other
        private void linkComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (linkComboBox.Text != "")
            {
                typeComboBox.Text = linkComboBox.Text;
            }
        }

        private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeComboBox.Text == "Flug")
            {
                linkComboBox.Text = typeComboBox.Text;
            }
        }
        #endregion

        #region If RadioButtons change, the path textbox content is deleted.
        private void singleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            deletePathTextBox();
        }

        private void folderRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            deletePathTextBox();
        }

        private void deletePathTextBox()
        {
            pathTextBox.Text = "";
        }
        #endregion

        #region BackgroundWorker
        /// <summary>
        /// Create thumbnail image gallery as a background worker job (creates List<PictureBox>).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void galleryBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            createGallery(Gallery, CurrentPath);
        }

        /// <summary>
        /// When the background worker finishes, add picture boxes to the flowlayout panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void galleryBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (PictureBox box in boxes)
            {
                box.Parent = pictureBoxesFlowLayoutPanel;
                pictureBoxesFlowLayoutPanel.Controls.Add(box);
            }
            pictureBoxesFlowLayoutPanel.ResumeLayout(true);
        }
        #endregion

        /// <summary>
        /// Clear the gallery flowlayout to load a new gallery.
        /// </summary>
        private void ClearGalleryOverview()
        {
            if (boxes.Count > 0)
            {
                foreach (PictureBox box in boxes)
                {
                    box.DoubleClick -= pictureBox_DoubleClick;
                    box.Dispose();
                }
            }
            pictureBoxesFlowLayoutPanel.Controls.Clear();
        }

        /// <summary>
        /// Takes a file or directory and create a gallery with thumbnail images.
        /// http://www.csharphelper.com/howtos/howto_show_picture_thumbnails.html
        /// </summary>
        /// <param name="galleryType"></param>
        /// <param name="path"></param>
        private void createGallery(GalleryType galleryType, string path)
        {
            boxes = new List<PictureBox>();
            List<string> filenames = new List<string>();

            switch (galleryType)
            {
                case GalleryType.FILE:
                    if (!File.Exists(path)) return;
                    filenames.Add(path);
                    break;
                case GalleryType.FOLDER:
                    if (!Directory.Exists(path)) return;
                    string[] patterns = { "*.png", "*.gif", "*.jpg", "*.tif", "*.bmp" };
                    foreach (string pattern in patterns)
                    {
                        filenames.AddRange(Directory.GetFiles(path, pattern, SearchOption.AllDirectories));
                    }
                    break;
                default:
                    break;
            }
            filenames.Sort();

            foreach (string filename in filenames)
            {
                PictureBox box = new PictureBox();
                // Create a thumbnail if needed
                CreateThumbnail(filename);
                box.Image = new Bitmap(GetThumbnailFilename(filename));
                if (filenames.Count > 1)
                {
                    box.ClientSize = new System.Drawing.Size(twidth, theight);
                    if (box.Image.Width > twidth || box.Image.Height > theight)
                    {
                        box.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        box.SizeMode = PictureBoxSizeMode.CenterImage;
                    }
                }
                else
                {
                    // Use original image if there is only one image in the gallery
                    box.Image = new Bitmap(filename);
                    box.Dock = DockStyle.Fill;
                    box.SizeMode = PictureBoxSizeMode.Zoom;
                }
                // Add the DoubleClick Event handler
                box.DoubleClick += pictureBox_DoubleClick;
                // Add a tooltip
                FileInfo fileInfo = new FileInfo(filename);
                if (fileInfo.Exists)
                {
                    using (Bitmap img = new Bitmap(filename))
                    {
                        string tooltip = fileInfo.Name +
                            "\nErstellt: " + fileInfo.CreationTime.ToShortDateString() +
                            "\n(" + box.Image.Width + " x " + box.Image.Height + ") " +
                            ToFileSize(fileInfo.Length);
                        pictureBoxToolTip.SetToolTip(box, tooltip);
                        box.Tag = fileInfo;
                    }
                    boxes.Add(box);
                }
            }
        }

        /// <summary>
        /// DoubleClick event on a thumbnail image launches either the build-in image viewer or the system application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            FileInfo fileInfo = pictureBox.Tag as FileInfo;
            if (fileInfo != null)
            {
                if (Properties.Settings.Default.ImageGalleryViewer == "ImageViewer")
                {
                    ImageViewerForm imageViewerForm = new ImageViewerForm();
                    imageViewerForm.Filename = fileInfo.FullName;
                    imageViewerForm.Show();
                }
                else
                {
                    Process.Start(fileInfo.FullName);
                }
            }
        }

        #region Tool strip menus to launch an image
        /// <summary>
        /// Launch an image with the built-in image viewer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bildbetrachterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bildbetrachterToolStripMenuItem.Checked = true;
            systemToolStripMenuItem.Checked = false;
            Properties.Settings.Default.ImageGalleryViewer = "ImageViewer";
        }

        /// <summary>
        /// Launch an image with the built-in systen image viewer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void systemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bildbetrachterToolStripMenuItem.Checked = false;
            systemToolStripMenuItem.Checked = true;
            Properties.Settings.Default.ImageGalleryViewer = "System";
        }
        #endregion

        #region Change thumbnail size
        private void kleinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeThumbnailSize(ThumbnailSize.SMALL);
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeThumbnailSize(ThumbnailSize.MEDIUM);
        }

        private void großToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeThumbnailSize(ThumbnailSize.LARGE);
        }

        /// <summary>
        /// Change Thumbnailsize: small = 100, medium = 200, large = 350; selected via Option menu.
        /// </summary>
        /// <param name="size"></param>
        private void changeThumbnailSize(ThumbnailSize size)
        {
            kleinToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = false;
            großToolStripMenuItem.Checked = false;
            currentSize = size;
            Properties.Settings.Default.ThumbnailSize = (byte)size;
            switch (size)
            {
                case ThumbnailSize.SMALL:
                    twidth = small;
                    theight = small;
                    kleinToolStripMenuItem.Checked = true;
                    break;
                case ThumbnailSize.MEDIUM:
                    twidth = medium;
                    theight = medium;
                    mediumToolStripMenuItem.Checked = true;
                    break;
                case ThumbnailSize.LARGE:
                    twidth = large;
                    theight = large;
                    großToolStripMenuItem.Checked = true;
                    break;
                default:
                    break;
            }
            galleryBackgroundWorker.RunWorkerAsync();
        }
        #endregion
    }
}
