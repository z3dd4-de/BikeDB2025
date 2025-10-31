namespace BikeDB2024
{
    partial class QRForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QRForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.qrPictureBox = new System.Windows.Forms.PictureBox();
            this.linkQrLabel = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qrPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.qrPictureBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.linkQrLabel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(441, 379);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // qrPictureBox
            // 
            this.qrPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qrPictureBox.Location = new System.Drawing.Point(3, 3);
            this.qrPictureBox.Name = "qrPictureBox";
            this.qrPictureBox.Size = new System.Drawing.Size(435, 343);
            this.qrPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.qrPictureBox.TabIndex = 0;
            this.qrPictureBox.TabStop = false;
            // 
            // linkQrLabel
            // 
            this.linkQrLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.linkQrLabel.AutoSize = true;
            this.linkQrLabel.Location = new System.Drawing.Point(3, 357);
            this.linkQrLabel.Name = "linkQrLabel";
            this.linkQrLabel.Size = new System.Drawing.Size(55, 13);
            this.linkQrLabel.TabIndex = 1;
            this.linkQrLabel.TabStop = true;
            this.linkQrLabel.Text = "linkLabel1";
            this.linkQrLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkQrLabel_LinkClicked);
            // 
            // QRForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 379);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QRForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "QR-Code";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qrPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox qrPictureBox;
        private System.Windows.Forms.LinkLabel linkQrLabel;
    }
}