namespace BikeDB2024
{
    partial class ImageViewerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageViewerForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.imageToolStrip = new System.Windows.Forms.ToolStrip();
            this.exitToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.loadFileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.originalSizeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.zoomInToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.zoomOutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.imageStatusStrip = new System.Windows.Forms.StatusStrip();
            this.filenameToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.scaleToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sizeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.imageToolStrip.SuspendLayout();
            this.imageStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.pictureBox);
            this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(734, 434);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(734, 473);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.imageToolStrip);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(723, 410);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.SizeChanged += new System.EventHandler(this.pictureBox_SizeChanged);
            this.pictureBox.DoubleClick += new System.EventHandler(this.pictureBox_DoubleClick);
            // 
            // imageToolStrip
            // 
            this.imageToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.imageToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.imageToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFileToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator1,
            this.originalSizeToolStripButton,
            this.zoomInToolStripButton,
            this.zoomOutToolStripButton,
            this.toolStripSeparator2,
            this.helpToolStripButton,
            this.toolStripSeparator3,
            this.exitToolStripButton});
            this.imageToolStrip.Location = new System.Drawing.Point(3, 0);
            this.imageToolStrip.Name = "imageToolStrip";
            this.imageToolStrip.Size = new System.Drawing.Size(282, 39);
            this.imageToolStrip.TabIndex = 0;
            // 
            // exitToolStripButton
            // 
            this.exitToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exitToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripButton.Image")));
            this.exitToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitToolStripButton.Name = "exitToolStripButton";
            this.exitToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.exitToolStripButton.Text = "Schließen";
            this.exitToolStripButton.Click += new System.EventHandler(this.exitToolStripButton_Click);
            // 
            // loadFileToolStripButton
            // 
            this.loadFileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.loadFileToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("loadFileToolStripButton.Image")));
            this.loadFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadFileToolStripButton.Name = "loadFileToolStripButton";
            this.loadFileToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.loadFileToolStripButton.Text = "Datei laden";
            this.loadFileToolStripButton.Click += new System.EventHandler(this.loadFileToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // originalSizeToolStripButton
            // 
            this.originalSizeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.originalSizeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("originalSizeToolStripButton.Image")));
            this.originalSizeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.originalSizeToolStripButton.Name = "originalSizeToolStripButton";
            this.originalSizeToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.originalSizeToolStripButton.Text = "100% Größe";
            this.originalSizeToolStripButton.Click += new System.EventHandler(this.originalSizeToolStripButton_Click);
            // 
            // zoomInToolStripButton
            // 
            this.zoomInToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomInToolStripButton.Image")));
            this.zoomInToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInToolStripButton.Name = "zoomInToolStripButton";
            this.zoomInToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.zoomInToolStripButton.Text = "Vergrößern";
            this.zoomInToolStripButton.Click += new System.EventHandler(this.zoomInToolStripButton_Click);
            // 
            // zoomOutToolStripButton
            // 
            this.zoomOutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomOutToolStripButton.Image")));
            this.zoomOutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutToolStripButton.Name = "zoomOutToolStripButton";
            this.zoomOutToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.zoomOutToolStripButton.Text = "Verkleinern";
            this.zoomOutToolStripButton.Click += new System.EventHandler(this.zoomOutToolStripButton_Click);
            // 
            // imageStatusStrip
            // 
            this.imageStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filenameToolStripStatusLabel,
            this.scaleToolStripStatusLabel,
            this.toolStripStatusLabel1,
            this.sizeToolStripStatusLabel});
            this.imageStatusStrip.Location = new System.Drawing.Point(0, 451);
            this.imageStatusStrip.Name = "imageStatusStrip";
            this.imageStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.imageStatusStrip.Size = new System.Drawing.Size(734, 22);
            this.imageStatusStrip.TabIndex = 1;
            this.imageStatusStrip.Text = "statusStrip1";
            // 
            // filenameToolStripStatusLabel
            // 
            this.filenameToolStripStatusLabel.Name = "filenameToolStripStatusLabel";
            this.filenameToolStripStatusLabel.Size = new System.Drawing.Size(118, 17);
            this.filenameToolStripStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // scaleToolStripStatusLabel
            // 
            this.scaleToolStripStatusLabel.Name = "scaleToolStripStatusLabel";
            this.scaleToolStripStatusLabel.Size = new System.Drawing.Size(35, 17);
            this.scaleToolStripStatusLabel.Text = "100%";
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "PNG-Bilddatei|*.png|JPG-Bilddatei|*.jpg|GIF-Bilddatei|*.gif|Alle Dateien (*.*)|*." +
    "*";
            this.openImageDialog.Title = "Bilddatei laden";
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.helpToolStripButton.Text = "Hilfe";
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.saveToolStripButton.Text = "Speichern";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.DefaultExt = "*.png";
            this.saveImageDialog.Filter = "PNG-Dateien|*.png|JPEG-Dateien|*.jpg|Alle Dateien|*.*";
            this.saveImageDialog.Title = "Bild speichern...";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // sizeToolStripStatusLabel
            // 
            this.sizeToolStripStatusLabel.Name = "sizeToolStripStatusLabel";
            this.sizeToolStripStatusLabel.Size = new System.Drawing.Size(87, 17);
            this.sizeToolStripStatusLabel.Text = "Width x Height";
            this.sizeToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sizeToolStripStatusLabel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // ImageViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(734, 473);
            this.Controls.Add(this.imageStatusStrip);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ImageViewerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Bildbetrachter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageViewerForm_FormClosing);
            this.Load += new System.EventHandler(this.ImageViewerForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ImageViewerForm_KeyUp);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.imageToolStrip.ResumeLayout(false);
            this.imageToolStrip.PerformLayout();
            this.imageStatusStrip.ResumeLayout(false);
            this.imageStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip imageStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel filenameToolStripStatusLabel;
        private System.Windows.Forms.ToolStrip imageToolStrip;
        private System.Windows.Forms.ToolStripButton exitToolStripButton;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripButton loadFileToolStripButton;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.ToolStripStatusLabel scaleToolStripStatusLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton originalSizeToolStripButton;
        private System.Windows.Forms.ToolStripButton zoomInToolStripButton;
        private System.Windows.Forms.ToolStripButton zoomOutToolStripButton;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel sizeToolStripStatusLabel;
    }
}