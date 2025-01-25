namespace BikeDB2024.FlightDB
{
    partial class GpsDistanceForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.startComboBox = new System.Windows.Forms.ComboBox();
            this.endComboBox = new System.Windows.Forms.ComboBox();
            this.lat1TextBox = new System.Windows.Forms.TextBox();
            this.lat2TextBox = new System.Windows.Forms.TextBox();
            this.lon1TextBox = new System.Windows.Forms.TextBox();
            this.lon2TextBox = new System.Windows.Forms.TextBox();
            this.calcButton = new System.Windows.Forms.Button();
            this.distanceLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.city1RadioButton = new System.Windows.Forms.RadioButton();
            this.airport1RadioButton = new System.Windows.Forms.RadioButton();
            this.city2RadioButton = new System.Windows.Forms.RadioButton();
            this.airport2RadioButton = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.startComboBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.endComboBox, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lat1TextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lat2TextBox, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lon1TextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lon2TextBox, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.calcButton, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.distanceLabel, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(514, 187);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Breitengrad";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Längengrad";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 150);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label5.Size = new System.Drawing.Size(59, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "Entfernung";
            // 
            // startComboBox
            // 
            this.startComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.startComboBox.FormattingEnabled = true;
            this.startComboBox.Location = new System.Drawing.Point(103, 33);
            this.startComboBox.Name = "startComboBox";
            this.startComboBox.Size = new System.Drawing.Size(194, 21);
            this.startComboBox.TabIndex = 5;
            this.startComboBox.SelectedIndexChanged += new System.EventHandler(this.startComboBox_SelectedIndexChanged);
            // 
            // endComboBox
            // 
            this.endComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.endComboBox.FormattingEnabled = true;
            this.endComboBox.Location = new System.Drawing.Point(303, 33);
            this.endComboBox.Name = "endComboBox";
            this.endComboBox.Size = new System.Drawing.Size(194, 21);
            this.endComboBox.TabIndex = 6;
            this.endComboBox.SelectedIndexChanged += new System.EventHandler(this.endComboBox_SelectedIndexChanged);
            // 
            // lat1TextBox
            // 
            this.lat1TextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lat1TextBox.Location = new System.Drawing.Point(103, 65);
            this.lat1TextBox.Name = "lat1TextBox";
            this.lat1TextBox.Size = new System.Drawing.Size(194, 20);
            this.lat1TextBox.TabIndex = 7;
            this.lat1TextBox.TextChanged += new System.EventHandler(this.lat1TextBox_TextChanged);
            // 
            // lat2TextBox
            // 
            this.lat2TextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lat2TextBox.Location = new System.Drawing.Point(303, 65);
            this.lat2TextBox.Name = "lat2TextBox";
            this.lat2TextBox.Size = new System.Drawing.Size(194, 20);
            this.lat2TextBox.TabIndex = 8;
            this.lat2TextBox.TextChanged += new System.EventHandler(this.lat2TextBox_TextChanged);
            // 
            // lon1TextBox
            // 
            this.lon1TextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lon1TextBox.Location = new System.Drawing.Point(103, 95);
            this.lon1TextBox.Name = "lon1TextBox";
            this.lon1TextBox.Size = new System.Drawing.Size(194, 20);
            this.lon1TextBox.TabIndex = 9;
            this.lon1TextBox.TextChanged += new System.EventHandler(this.lon1TextBox_TextChanged);
            // 
            // lon2TextBox
            // 
            this.lon2TextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lon2TextBox.Location = new System.Drawing.Point(303, 95);
            this.lon2TextBox.Name = "lon2TextBox";
            this.lon2TextBox.Size = new System.Drawing.Size(194, 20);
            this.lon2TextBox.TabIndex = 10;
            this.lon2TextBox.TextChanged += new System.EventHandler(this.lon2TextBox_TextChanged);
            // 
            // calcButton
            // 
            this.calcButton.Enabled = false;
            this.calcButton.Location = new System.Drawing.Point(103, 123);
            this.calcButton.Name = "calcButton";
            this.calcButton.Size = new System.Drawing.Size(75, 23);
            this.calcButton.TabIndex = 11;
            this.calcButton.Text = "Berechnen";
            this.calcButton.UseVisualStyleBackColor = true;
            this.calcButton.Click += new System.EventHandler(this.calcButton_Click);
            // 
            // distanceLabel
            // 
            this.distanceLabel.AutoSize = true;
            this.distanceLabel.Location = new System.Drawing.Point(103, 150);
            this.distanceLabel.Name = "distanceLabel";
            this.distanceLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.distanceLabel.Size = new System.Drawing.Size(21, 18);
            this.distanceLabel.TabIndex = 12;
            this.distanceLabel.Text = "km";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.city1RadioButton);
            this.flowLayoutPanel1.Controls.Add(this.airport1RadioButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(103, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(194, 24);
            this.flowLayoutPanel1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Start";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.city2RadioButton);
            this.flowLayoutPanel2.Controls.Add(this.airport2RadioButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(303, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(194, 24);
            this.flowLayoutPanel2.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ziel";
            // 
            // city1RadioButton
            // 
            this.city1RadioButton.AutoSize = true;
            this.city1RadioButton.Checked = true;
            this.city1RadioButton.Location = new System.Drawing.Point(38, 3);
            this.city1RadioButton.Name = "city1RadioButton";
            this.city1RadioButton.Size = new System.Drawing.Size(50, 17);
            this.city1RadioButton.TabIndex = 2;
            this.city1RadioButton.TabStop = true;
            this.city1RadioButton.Text = "Stadt";
            this.city1RadioButton.UseVisualStyleBackColor = true;
            this.city1RadioButton.CheckedChanged += new System.EventHandler(this.city1RadioButton_CheckedChanged);
            // 
            // airport1RadioButton
            // 
            this.airport1RadioButton.AutoSize = true;
            this.airport1RadioButton.Location = new System.Drawing.Point(94, 3);
            this.airport1RadioButton.Name = "airport1RadioButton";
            this.airport1RadioButton.Size = new System.Drawing.Size(72, 17);
            this.airport1RadioButton.TabIndex = 3;
            this.airport1RadioButton.Text = "Flughafen";
            this.airport1RadioButton.UseVisualStyleBackColor = true;
            this.airport1RadioButton.CheckedChanged += new System.EventHandler(this.airport1RadioButton_CheckedChanged);
            // 
            // city2RadioButton
            // 
            this.city2RadioButton.AutoSize = true;
            this.city2RadioButton.Checked = true;
            this.city2RadioButton.Location = new System.Drawing.Point(33, 3);
            this.city2RadioButton.Name = "city2RadioButton";
            this.city2RadioButton.Size = new System.Drawing.Size(50, 17);
            this.city2RadioButton.TabIndex = 4;
            this.city2RadioButton.TabStop = true;
            this.city2RadioButton.Text = "Stadt";
            this.city2RadioButton.UseVisualStyleBackColor = true;
            this.city2RadioButton.CheckedChanged += new System.EventHandler(this.city2RadioButton_CheckedChanged);
            // 
            // airport2RadioButton
            // 
            this.airport2RadioButton.AutoSize = true;
            this.airport2RadioButton.Location = new System.Drawing.Point(89, 3);
            this.airport2RadioButton.Name = "airport2RadioButton";
            this.airport2RadioButton.Size = new System.Drawing.Size(72, 17);
            this.airport2RadioButton.TabIndex = 5;
            this.airport2RadioButton.Text = "Flughafen";
            this.airport2RadioButton.UseVisualStyleBackColor = true;
            this.airport2RadioButton.CheckedChanged += new System.EventHandler(this.airport2RadioButton_CheckedChanged);
            // 
            // GpsDistanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 187);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GpsDistanceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Berechnung der Entfernung auf dem Großkreis";
            this.Load += new System.EventHandler(this.GpsDistanceForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox startComboBox;
        private System.Windows.Forms.ComboBox endComboBox;
        private System.Windows.Forms.TextBox lat1TextBox;
        private System.Windows.Forms.TextBox lat2TextBox;
        private System.Windows.Forms.TextBox lon1TextBox;
        private System.Windows.Forms.TextBox lon2TextBox;
        private System.Windows.Forms.Button calcButton;
        private System.Windows.Forms.Label distanceLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton city1RadioButton;
        private System.Windows.Forms.RadioButton airport1RadioButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton city2RadioButton;
        private System.Windows.Forms.RadioButton airport2RadioButton;
    }
}