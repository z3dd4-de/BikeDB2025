namespace BikeDB2024
{
    partial class RouteForm
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
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.errorToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.routesComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.cityComboBox = new System.Windows.Forms.ComboBox();
            this.citiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet = new BikeDB2024.DataSet();
            this.cityStartComboBox = new System.Windows.Forms.ComboBox();
            this.citiesBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.cityEndComboBox = new System.Windows.Forms.ComboBox();
            this.citiesBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.citiesTextBox = new System.Windows.Forms.TextBox();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.routeTypesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.remarkRichTextBox = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.altProfileTextBox = new System.Windows.Forms.TextBox();
            this.altitudeButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.imageTextBox = new System.Windows.Forms.TextBox();
            this.imageButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.maxAltLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.maxAltTextBox = new System.Windows.Forms.MaskedTextBox();
            this.altTextBox = new System.Windows.Forms.MaskedTextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.notShownCheckBox = new System.Windows.Forms.CheckBox();
            this.routesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.citiesTableAdapter = new BikeDB2024.DataSetTableAdapters.CitiesTableAdapter();
            this.routeTypesTableAdapter = new BikeDB2024.DataSetTableAdapters.RouteTypesTableAdapter();
            this.altitudeOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.imageOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.routesTableAdapter = new BikeDB2024.DataSetTableAdapters.RoutesTableAdapter();
            this.notShownToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.routeTypesBindingSource)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.routesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.errorToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 397);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(403, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // errorToolStripStatusLabel
            // 
            this.errorToolStripStatusLabel.Name = "errorToolStripStatusLabel";
            this.errorToolStripStatusLabel.Size = new System.Drawing.Size(118, 17);
            this.errorToolStripStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.routesComboBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.nameTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cityComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cityStartComboBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cityEndComboBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.citiesTextBox, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.typeComboBox, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.remarkRichTextBox, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel4, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.maxAltLabel, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.maxAltTextBox, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.altTextBox, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.addButton, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 12);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 14;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(403, 397);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vorhandene Strecken";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // routesComboBox
            // 
            this.routesComboBox.DisplayMember = "Id";
            this.routesComboBox.FormattingEnabled = true;
            this.routesComboBox.Location = new System.Drawing.Point(137, 2);
            this.routesComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.routesComboBox.Name = "routesComboBox";
            this.routesComboBox.Size = new System.Drawing.Size(260, 21);
            this.routesComboBox.TabIndex = 0;
            this.routesComboBox.ValueMember = "Id";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 168);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Anmerkung";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 144);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 24);
            this.label6.TabIndex = 5;
            this.label6.Text = "Streckentyp";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 120);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "Städte";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "Stadt (Ende)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 72);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Stadt (Start)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 48);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 24);
            this.label8.TabIndex = 9;
            this.label8.Text = "Stadt";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(137, 26);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.nameTextBox.MaxLength = 50;
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(260, 20);
            this.nameTextBox.TabIndex = 1;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // cityComboBox
            // 
            this.cityComboBox.DataSource = this.citiesBindingSource;
            this.cityComboBox.DisplayMember = "CityName";
            this.cityComboBox.FormattingEnabled = true;
            this.cityComboBox.Location = new System.Drawing.Point(137, 50);
            this.cityComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.cityComboBox.Name = "cityComboBox";
            this.cityComboBox.Size = new System.Drawing.Size(260, 21);
            this.cityComboBox.TabIndex = 2;
            this.cityComboBox.ValueMember = "Id";
            // 
            // citiesBindingSource
            // 
            this.citiesBindingSource.DataMember = "Cities";
            this.citiesBindingSource.DataSource = this.dataSet;
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cityStartComboBox
            // 
            this.cityStartComboBox.DataSource = this.citiesBindingSource1;
            this.cityStartComboBox.DisplayMember = "CityName";
            this.cityStartComboBox.FormattingEnabled = true;
            this.cityStartComboBox.Location = new System.Drawing.Point(137, 74);
            this.cityStartComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.cityStartComboBox.Name = "cityStartComboBox";
            this.cityStartComboBox.Size = new System.Drawing.Size(260, 21);
            this.cityStartComboBox.TabIndex = 3;
            this.cityStartComboBox.ValueMember = "Id";
            // 
            // citiesBindingSource1
            // 
            this.citiesBindingSource1.DataMember = "Cities";
            this.citiesBindingSource1.DataSource = this.dataSet;
            // 
            // cityEndComboBox
            // 
            this.cityEndComboBox.DataSource = this.citiesBindingSource2;
            this.cityEndComboBox.DisplayMember = "CityName";
            this.cityEndComboBox.FormattingEnabled = true;
            this.cityEndComboBox.Location = new System.Drawing.Point(137, 98);
            this.cityEndComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.cityEndComboBox.Name = "cityEndComboBox";
            this.cityEndComboBox.Size = new System.Drawing.Size(260, 21);
            this.cityEndComboBox.TabIndex = 4;
            this.cityEndComboBox.ValueMember = "Id";
            // 
            // citiesBindingSource2
            // 
            this.citiesBindingSource2.DataMember = "Cities";
            this.citiesBindingSource2.DataSource = this.dataSet;
            // 
            // citiesTextBox
            // 
            this.citiesTextBox.Location = new System.Drawing.Point(137, 122);
            this.citiesTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.citiesTextBox.Name = "citiesTextBox";
            this.citiesTextBox.Size = new System.Drawing.Size(260, 20);
            this.citiesTextBox.TabIndex = 5;
            // 
            // typeComboBox
            // 
            this.typeComboBox.DataSource = this.routeTypesBindingSource;
            this.typeComboBox.DisplayMember = "RouteType";
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Location = new System.Drawing.Point(137, 146);
            this.typeComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(260, 21);
            this.typeComboBox.TabIndex = 6;
            this.typeComboBox.ValueMember = "Id";
            // 
            // routeTypesBindingSource
            // 
            this.routeTypesBindingSource.DataMember = "RouteTypes";
            this.routeTypesBindingSource.DataSource = this.dataSet;
            // 
            // remarkRichTextBox
            // 
            this.remarkRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remarkRichTextBox.Location = new System.Drawing.Point(137, 170);
            this.remarkRichTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.remarkRichTextBox.Name = "remarkRichTextBox";
            this.remarkRichTextBox.Size = new System.Drawing.Size(264, 59);
            this.remarkRichTextBox.TabIndex = 7;
            this.remarkRichTextBox.Text = "";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.altProfileTextBox);
            this.flowLayoutPanel3.Controls.Add(this.altitudeButton);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(138, 282);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(262, 26);
            this.flowLayoutPanel3.TabIndex = 10;
            // 
            // altProfileTextBox
            // 
            this.altProfileTextBox.Location = new System.Drawing.Point(2, 2);
            this.altProfileTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.altProfileTextBox.Name = "altProfileTextBox";
            this.altProfileTextBox.Size = new System.Drawing.Size(196, 20);
            this.altProfileTextBox.TabIndex = 10;
            // 
            // altitudeButton
            // 
            this.altitudeButton.Location = new System.Drawing.Point(202, 2);
            this.altitudeButton.Margin = new System.Windows.Forms.Padding(2);
            this.altitudeButton.Name = "altitudeButton";
            this.altitudeButton.Size = new System.Drawing.Size(56, 19);
            this.altitudeButton.TabIndex = 11;
            this.altitudeButton.Text = "Öffnen";
            this.altitudeButton.UseVisualStyleBackColor = true;
            this.altitudeButton.Click += new System.EventHandler(this.altitudeButton_Click);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.imageTextBox);
            this.flowLayoutPanel4.Controls.Add(this.imageButton);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(138, 314);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(262, 26);
            this.flowLayoutPanel4.TabIndex = 12;
            // 
            // imageTextBox
            // 
            this.imageTextBox.Location = new System.Drawing.Point(2, 2);
            this.imageTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.imageTextBox.Name = "imageTextBox";
            this.imageTextBox.Size = new System.Drawing.Size(196, 20);
            this.imageTextBox.TabIndex = 12;
            // 
            // imageButton
            // 
            this.imageButton.Location = new System.Drawing.Point(202, 2);
            this.imageButton.Margin = new System.Windows.Forms.Padding(2);
            this.imageButton.Name = "imageButton";
            this.imageButton.Size = new System.Drawing.Size(56, 19);
            this.imageButton.TabIndex = 13;
            this.imageButton.Text = "Öffnen";
            this.imageButton.UseVisualStyleBackColor = true;
            this.imageButton.Click += new System.EventHandler(this.imageButton_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 311);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 32);
            this.label10.TabIndex = 11;
            this.label10.Text = "Bild";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 279);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 32);
            this.label9.TabIndex = 10;
            this.label9.Text = "Höhenprofil";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // maxAltLabel
            // 
            this.maxAltLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.maxAltLabel.AutoSize = true;
            this.maxAltLabel.Location = new System.Drawing.Point(3, 231);
            this.maxAltLabel.Name = "maxAltLabel";
            this.maxAltLabel.Size = new System.Drawing.Size(105, 24);
            this.maxAltLabel.TabIndex = 16;
            this.maxAltLabel.Text = "Höhenmeter maximal";
            this.maxAltLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 255);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 24);
            this.label12.TabIndex = 17;
            this.label12.Text = "Höhenmeter gesamt";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // maxAltTextBox
            // 
            this.maxAltTextBox.Location = new System.Drawing.Point(137, 233);
            this.maxAltTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.maxAltTextBox.Mask = "#9990";
            this.maxAltTextBox.Name = "maxAltTextBox";
            this.maxAltTextBox.Size = new System.Drawing.Size(76, 20);
            this.maxAltTextBox.TabIndex = 8;
            // 
            // altTextBox
            // 
            this.altTextBox.Location = new System.Drawing.Point(137, 257);
            this.altTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.altTextBox.Mask = "99990";
            this.altTextBox.Name = "altTextBox";
            this.altTextBox.Size = new System.Drawing.Size(76, 20);
            this.altTextBox.TabIndex = 9;
            // 
            // addButton
            // 
            this.addButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.addButton.Location = new System.Drawing.Point(137, 369);
            this.addButton.Margin = new System.Windows.Forms.Padding(2);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(91, 26);
            this.addButton.TabIndex = 15;
            this.addButton.Text = "Hinzufügen";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 343);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 24);
            this.label11.TabIndex = 18;
            this.label11.Text = "Anzeigen";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.notShownCheckBox);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(138, 346);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(262, 18);
            this.flowLayoutPanel1.TabIndex = 14;
            // 
            // notShownCheckBox
            // 
            this.notShownCheckBox.AutoSize = true;
            this.notShownCheckBox.Location = new System.Drawing.Point(3, 3);
            this.notShownCheckBox.Name = "notShownCheckBox";
            this.notShownCheckBox.Size = new System.Drawing.Size(48, 17);
            this.notShownCheckBox.TabIndex = 14;
            this.notShownCheckBox.Text = "Nein";
            this.notShownToolTip.SetToolTip(this.notShownCheckBox, "Wenn \"nein\" angeklickt wurde, werden Strecken nicht mehr bei täglichen Touren ang" +
        "ezeigt.");
            this.notShownCheckBox.UseVisualStyleBackColor = true;
            // 
            // routesBindingSource
            // 
            this.routesBindingSource.DataMember = "Routes";
            this.routesBindingSource.DataSource = this.dataSet;
            // 
            // citiesTableAdapter
            // 
            this.citiesTableAdapter.ClearBeforeFill = true;
            // 
            // routeTypesTableAdapter
            // 
            this.routeTypesTableAdapter.ClearBeforeFill = true;
            // 
            // altitudeOpenFileDialog
            // 
            this.altitudeOpenFileDialog.Filter = "PNG-Bilddatei|*.png|JPG-Bilddatei|*.jpg|GIF-Bilddatei|*.gif|Alle Dateien (*.*)|*." +
    "*";
            this.altitudeOpenFileDialog.Title = "Bilddatei für Höhenprofil auswählen";
            // 
            // imageOpenFileDialog
            // 
            this.imageOpenFileDialog.Filter = "PNG-Bilddatei|*.png|JPG-Bilddatei|*.jpg|GIF-Bilddatei|*.gif|Alle Dateien (*.*)|*." +
    "*";
            this.imageOpenFileDialog.Title = "Bilddatei für Bild der Strecke auswählen";
            // 
            // routesTableAdapter
            // 
            this.routesTableAdapter.ClearBeforeFill = true;
            // 
            // notShownToolTip
            // 
            this.notShownToolTip.ToolTipTitle = "Sichtbarkeit";
            // 
            // RouteForm
            // 
            this.AcceptButton = this.addButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 419);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RouteForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Neue Strecke";
            this.Load += new System.EventHandler(this.RouteForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.routeTypesBindingSource)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.routesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel errorToolStripStatusLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox routesComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.ComboBox cityComboBox;
        private System.Windows.Forms.ComboBox cityStartComboBox;
        private System.Windows.Forms.ComboBox cityEndComboBox;
        private System.Windows.Forms.TextBox citiesTextBox;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.RichTextBox remarkRichTextBox;
        private System.Windows.Forms.Button addButton;
        private DataSet dataSet;
        private System.Windows.Forms.BindingSource citiesBindingSource;
        private DataSetTableAdapters.CitiesTableAdapter citiesTableAdapter;
        private System.Windows.Forms.BindingSource routeTypesBindingSource;
        private DataSetTableAdapters.RouteTypesTableAdapter routeTypesTableAdapter;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox altProfileTextBox;
        private System.Windows.Forms.Button altitudeButton;
        private System.Windows.Forms.TextBox imageTextBox;
        private System.Windows.Forms.Button imageButton;
        private System.Windows.Forms.OpenFileDialog altitudeOpenFileDialog;
        private System.Windows.Forms.OpenFileDialog imageOpenFileDialog;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label maxAltLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.MaskedTextBox maxAltTextBox;
        private System.Windows.Forms.MaskedTextBox altTextBox;
        private System.Windows.Forms.BindingSource routesBindingSource;
        private DataSetTableAdapters.RoutesTableAdapter routesTableAdapter;
        private System.Windows.Forms.BindingSource citiesBindingSource1;
        private System.Windows.Forms.BindingSource citiesBindingSource2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox notShownCheckBox;
        private System.Windows.Forms.ToolTip notShownToolTip;
    }
}