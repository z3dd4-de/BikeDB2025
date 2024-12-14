namespace BikeDB2024
{
    partial class ExportForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tourCheckBox = new System.Windows.Forms.CheckBox();
            this.countryCheckBox = new System.Windows.Forms.CheckBox();
            this.cityCheckBox = new System.Windows.Forms.CheckBox();
            this.routeCheckBox = new System.Windows.Forms.CheckBox();
            this.vehicleCheckBox = new System.Windows.Forms.CheckBox();
            this.manufacturerCheckBox = new System.Windows.Forms.CheckBox();
            this.entfaltungCheckBox = new System.Windows.Forms.CheckBox();
            this.vectypeCheckBox = new System.Windows.Forms.CheckBox();
            this.routetypeCheckBox = new System.Windows.Forms.CheckBox();
            this.personsCheckBox = new System.Windows.Forms.CheckBox();
            this.notesCheckBox = new System.Windows.Forms.CheckBox();
            this.goalsCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.exportButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.folderTextBox = new System.Windows.Forms.TextBox();
            this.openButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.allCheckBox = new System.Windows.Forms.CheckBox();
            this.openFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.exportButton, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(477, 159);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.tourCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.countryCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.cityCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.routeCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.vehicleCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.manufacturerCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.entfaltungCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.vectypeCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.routetypeCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.personsCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.notesCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.goalsCheckBox);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(100, 2);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(375, 70);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // tourCheckBox
            // 
            this.tourCheckBox.AutoSize = true;
            this.tourCheckBox.Location = new System.Drawing.Point(2, 2);
            this.tourCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.tourCheckBox.Name = "tourCheckBox";
            this.tourCheckBox.Size = new System.Drawing.Size(86, 17);
            this.tourCheckBox.TabIndex = 0;
            this.tourCheckBox.Text = "Tagestouren";
            this.tourCheckBox.UseVisualStyleBackColor = true;
            this.tourCheckBox.CheckedChanged += new System.EventHandler(this.tourCheckBox_CheckedChanged);
            // 
            // countryCheckBox
            // 
            this.countryCheckBox.AutoSize = true;
            this.countryCheckBox.Location = new System.Drawing.Point(92, 2);
            this.countryCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.countryCheckBox.Name = "countryCheckBox";
            this.countryCheckBox.Size = new System.Drawing.Size(59, 17);
            this.countryCheckBox.TabIndex = 1;
            this.countryCheckBox.Text = "Länder";
            this.countryCheckBox.UseVisualStyleBackColor = true;
            this.countryCheckBox.CheckedChanged += new System.EventHandler(this.countryCheckBox_CheckedChanged);
            // 
            // cityCheckBox
            // 
            this.cityCheckBox.AutoSize = true;
            this.cityCheckBox.Location = new System.Drawing.Point(155, 2);
            this.cityCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.cityCheckBox.Name = "cityCheckBox";
            this.cityCheckBox.Size = new System.Drawing.Size(57, 17);
            this.cityCheckBox.TabIndex = 2;
            this.cityCheckBox.Text = "Städte";
            this.cityCheckBox.UseVisualStyleBackColor = true;
            this.cityCheckBox.CheckedChanged += new System.EventHandler(this.cityCheckBox_CheckedChanged);
            // 
            // routeCheckBox
            // 
            this.routeCheckBox.AutoSize = true;
            this.routeCheckBox.Location = new System.Drawing.Point(216, 2);
            this.routeCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.routeCheckBox.Name = "routeCheckBox";
            this.routeCheckBox.Size = new System.Drawing.Size(69, 17);
            this.routeCheckBox.TabIndex = 3;
            this.routeCheckBox.Text = "Strecken";
            this.routeCheckBox.UseVisualStyleBackColor = true;
            this.routeCheckBox.CheckedChanged += new System.EventHandler(this.routeCheckBox_CheckedChanged);
            // 
            // vehicleCheckBox
            // 
            this.vehicleCheckBox.AutoSize = true;
            this.vehicleCheckBox.Location = new System.Drawing.Point(289, 2);
            this.vehicleCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.vehicleCheckBox.Name = "vehicleCheckBox";
            this.vehicleCheckBox.Size = new System.Drawing.Size(76, 17);
            this.vehicleCheckBox.TabIndex = 4;
            this.vehicleCheckBox.Text = "Fahrzeuge";
            this.vehicleCheckBox.UseVisualStyleBackColor = true;
            this.vehicleCheckBox.CheckedChanged += new System.EventHandler(this.vehicleCheckBox_CheckedChanged);
            // 
            // manufacturerCheckBox
            // 
            this.manufacturerCheckBox.AutoSize = true;
            this.manufacturerCheckBox.Location = new System.Drawing.Point(2, 23);
            this.manufacturerCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.manufacturerCheckBox.Name = "manufacturerCheckBox";
            this.manufacturerCheckBox.Size = new System.Drawing.Size(70, 17);
            this.manufacturerCheckBox.TabIndex = 5;
            this.manufacturerCheckBox.Text = "Hersteller";
            this.manufacturerCheckBox.UseVisualStyleBackColor = true;
            this.manufacturerCheckBox.CheckedChanged += new System.EventHandler(this.manufacturerCheckBox_CheckedChanged);
            // 
            // entfaltungCheckBox
            // 
            this.entfaltungCheckBox.AutoSize = true;
            this.entfaltungCheckBox.Location = new System.Drawing.Point(76, 23);
            this.entfaltungCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.entfaltungCheckBox.Name = "entfaltungCheckBox";
            this.entfaltungCheckBox.Size = new System.Drawing.Size(74, 17);
            this.entfaltungCheckBox.TabIndex = 6;
            this.entfaltungCheckBox.Text = "Entfaltung";
            this.entfaltungCheckBox.UseVisualStyleBackColor = true;
            this.entfaltungCheckBox.CheckedChanged += new System.EventHandler(this.entfaltungCheckBox_CheckedChanged);
            // 
            // vectypeCheckBox
            // 
            this.vectypeCheckBox.AutoSize = true;
            this.vectypeCheckBox.Location = new System.Drawing.Point(155, 24);
            this.vectypeCheckBox.Name = "vectypeCheckBox";
            this.vectypeCheckBox.Size = new System.Drawing.Size(96, 17);
            this.vectypeCheckBox.TabIndex = 7;
            this.vectypeCheckBox.Text = "Fahrzeugtypen";
            this.vectypeCheckBox.UseVisualStyleBackColor = true;
            this.vectypeCheckBox.CheckedChanged += new System.EventHandler(this.vectypeCheckBox_CheckedChanged);
            // 
            // routetypeCheckBox
            // 
            this.routetypeCheckBox.AutoSize = true;
            this.routetypeCheckBox.Location = new System.Drawing.Point(257, 24);
            this.routetypeCheckBox.Name = "routetypeCheckBox";
            this.routetypeCheckBox.Size = new System.Drawing.Size(95, 17);
            this.routetypeCheckBox.TabIndex = 8;
            this.routetypeCheckBox.Text = "Streckentypen";
            this.routetypeCheckBox.UseVisualStyleBackColor = true;
            this.routetypeCheckBox.CheckedChanged += new System.EventHandler(this.routetypeCheckBox_CheckedChanged);
            // 
            // personsCheckBox
            // 
            this.personsCheckBox.AutoSize = true;
            this.personsCheckBox.Location = new System.Drawing.Point(3, 47);
            this.personsCheckBox.Name = "personsCheckBox";
            this.personsCheckBox.Size = new System.Drawing.Size(71, 17);
            this.personsCheckBox.TabIndex = 9;
            this.personsCheckBox.Text = "Personen";
            this.personsCheckBox.UseVisualStyleBackColor = true;
            this.personsCheckBox.CheckedChanged += new System.EventHandler(this.personsCheckBox_CheckedChanged);
            // 
            // notesCheckBox
            // 
            this.notesCheckBox.AutoSize = true;
            this.notesCheckBox.Location = new System.Drawing.Point(80, 47);
            this.notesCheckBox.Name = "notesCheckBox";
            this.notesCheckBox.Size = new System.Drawing.Size(62, 17);
            this.notesCheckBox.TabIndex = 10;
            this.notesCheckBox.Text = "Notizen";
            this.notesCheckBox.UseVisualStyleBackColor = true;
            this.notesCheckBox.CheckedChanged += new System.EventHandler(this.notesCheckBox_CheckedChanged);
            // 
            // goalsCheckBox
            // 
            this.goalsCheckBox.AutoSize = true;
            this.goalsCheckBox.Location = new System.Drawing.Point(148, 47);
            this.goalsCheckBox.Name = "goalsCheckBox";
            this.goalsCheckBox.Size = new System.Drawing.Size(49, 17);
            this.goalsCheckBox.TabIndex = 11;
            this.goalsCheckBox.Text = "Ziele";
            this.goalsCheckBox.UseVisualStyleBackColor = true;
            this.goalsCheckBox.CheckedChanged += new System.EventHandler(this.goalsCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tabellen";
            // 
            // exportButton
            // 
            this.exportButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.exportButton.Enabled = false;
            this.exportButton.Location = new System.Drawing.Point(100, 128);
            this.exportButton.Margin = new System.Windows.Forms.Padding(2);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(80, 24);
            this.exportButton.TabIndex = 10;
            this.exportButton.Text = "Exportieren";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.folderTextBox);
            this.flowLayoutPanel2.Controls.Add(this.openButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(100, 100);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(375, 24);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // folderTextBox
            // 
            this.folderTextBox.Location = new System.Drawing.Point(2, 2);
            this.folderTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.folderTextBox.Name = "folderTextBox";
            this.folderTextBox.Size = new System.Drawing.Size(306, 20);
            this.folderTextBox.TabIndex = 8;
            this.folderTextBox.TextChanged += new System.EventHandler(this.folderTextBox_TextChanged);
            // 
            // openButton
            // 
            this.openButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.openButton.Location = new System.Drawing.Point(312, 2);
            this.openButton.Margin = new System.Windows.Forms.Padding(2);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(56, 20);
            this.openButton.TabIndex = 9;
            this.openButton.Text = "Öffnen...";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 98);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Speicherort";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.allCheckBox);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(101, 77);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(373, 18);
            this.flowLayoutPanel3.TabIndex = 11;
            // 
            // allCheckBox
            // 
            this.allCheckBox.AutoSize = true;
            this.allCheckBox.Location = new System.Drawing.Point(2, 2);
            this.allCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.allCheckBox.Name = "allCheckBox";
            this.allCheckBox.Size = new System.Drawing.Size(43, 17);
            this.allCheckBox.TabIndex = 8;
            this.allCheckBox.Text = "Alle";
            this.allCheckBox.UseVisualStyleBackColor = true;
            this.allCheckBox.CheckedChanged += new System.EventHandler(this.allCheckBox_CheckedChanged);
            // 
            // openFolderBrowserDialog
            // 
            this.openFolderBrowserDialog.Description = "Speicherort für Export-Dateien.";
            // 
            // ExportForm
            // 
            this.AcceptButton = this.exportButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 159);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Exportieren...";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox tourCheckBox;
        private System.Windows.Forms.CheckBox countryCheckBox;
        private System.Windows.Forms.CheckBox cityCheckBox;
        private System.Windows.Forms.CheckBox routeCheckBox;
        private System.Windows.Forms.CheckBox vehicleCheckBox;
        private System.Windows.Forms.CheckBox manufacturerCheckBox;
        private System.Windows.Forms.CheckBox entfaltungCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TextBox folderTextBox;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog openFolderBrowserDialog;
        private System.Windows.Forms.CheckBox vectypeCheckBox;
        private System.Windows.Forms.CheckBox routetypeCheckBox;
        private System.Windows.Forms.CheckBox personsCheckBox;
        private System.Windows.Forms.CheckBox notesCheckBox;
        private System.Windows.Forms.CheckBox goalsCheckBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.CheckBox allCheckBox;
    }
}