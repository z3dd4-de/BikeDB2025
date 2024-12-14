namespace BikeDB2024
{
    partial class DbNavControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbNavControl));
            this.dbNavToolStrip = new System.Windows.Forms.ToolStrip();
            this.firstToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.previousToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.nextToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.lastToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.editToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.dbNavToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dbNavToolStrip
            // 
            this.dbNavToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.dbNavToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.firstToolStripButton,
            this.previousToolStripButton,
            this.nextToolStripButton,
            this.lastToolStripButton,
            this.toolStripSeparator1,
            this.editToolStripButton,
            this.deleteToolStripButton});
            this.dbNavToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.dbNavToolStrip.Location = new System.Drawing.Point(0, 0);
            this.dbNavToolStrip.Name = "dbNavToolStrip";
            this.dbNavToolStrip.Size = new System.Drawing.Size(238, 39);
            this.dbNavToolStrip.Stretch = true;
            this.dbNavToolStrip.TabIndex = 0;
            this.dbNavToolStrip.Text = "Datenbank Navigation";
            // 
            // firstToolStripButton
            // 
            this.firstToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.firstToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("firstToolStripButton.Image")));
            this.firstToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.firstToolStripButton.Name = "firstToolStripButton";
            this.firstToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.firstToolStripButton.Text = "Erster Datensatz";
            // 
            // previousToolStripButton
            // 
            this.previousToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.previousToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("previousToolStripButton.Image")));
            this.previousToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.previousToolStripButton.Name = "previousToolStripButton";
            this.previousToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.previousToolStripButton.Text = "Letzter Datensatz";
            // 
            // nextToolStripButton
            // 
            this.nextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nextToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("nextToolStripButton.Image")));
            this.nextToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nextToolStripButton.Name = "nextToolStripButton";
            this.nextToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.nextToolStripButton.Text = "Nächster Datensatz";
            // 
            // lastToolStripButton
            // 
            this.lastToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.lastToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("lastToolStripButton.Image")));
            this.lastToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lastToolStripButton.Name = "lastToolStripButton";
            this.lastToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.lastToolStripButton.Text = "Letzter Datensatz";
            // 
            // editToolStripButton
            // 
            this.editToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("editToolStripButton.Image")));
            this.editToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editToolStripButton.Name = "editToolStripButton";
            this.editToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.editToolStripButton.Text = "Bearbeiten";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 36);
            // 
            // deleteToolStripButton
            // 
            this.deleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripButton.Image")));
            this.deleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteToolStripButton.Name = "deleteToolStripButton";
            this.deleteToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.deleteToolStripButton.Text = "Datensatz löschen";
            // 
            // DbNavControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dbNavToolStrip);
            this.Name = "DbNavControl";
            this.Size = new System.Drawing.Size(238, 39);
            this.dbNavToolStrip.ResumeLayout(false);
            this.dbNavToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip dbNavToolStrip;
        private System.Windows.Forms.ToolStripButton firstToolStripButton;
        private System.Windows.Forms.ToolStripButton previousToolStripButton;
        private System.Windows.Forms.ToolStripButton nextToolStripButton;
        private System.Windows.Forms.ToolStripButton lastToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton editToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteToolStripButton;
    }
}
