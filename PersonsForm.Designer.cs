namespace BikeDB2024
{
    partial class PersonsForm
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
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.plzTextBox = new System.Windows.Forms.TextBox();
            this.cityComboBox = new System.Windows.Forms.ComboBox();
            this.citiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet = new BikeDB2024.DataSet();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.imageTextBox = new System.Windows.Forms.TextBox();
            this.fileOpenButton = new System.Windows.Forms.Button();
            this.remarkRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.isUserCheckBox = new System.Windows.Forms.CheckBox();
            this.isAdminCheckBox = new System.Windows.Forms.CheckBox();
            this.addButton = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.phoneTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.countryComboBox = new System.Windows.Forms.ComboBox();
            this.countriesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.street2TextBox = new System.Windows.Forms.TextBox();
            this.street1TextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.birthdateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.deathDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.notShowCheckBox = new System.Windows.Forms.CheckBox();
            this.deadCheckBox = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.pwdButton = new System.Windows.Forms.Button();
            this.imageOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.countriesTableAdapter = new BikeDB2024.DataSetTableAdapters.CountriesTableAdapter();
            this.citiesTableAdapter = new BikeDB2024.DataSetTableAdapters.CitiesTableAdapter();
            this.NotShownToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countriesBindingSource)).BeginInit();
            this.flowLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.errorToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(484, 22);
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel4, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.remarkRichTextBox, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.isUserCheckBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.isAdminCheckBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.addButton, 1, 15);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.emailTextBox, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.phoneTextBox, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.countryComboBox, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.street2TextBox, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.street1TextBox, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.birthdateTimePicker, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.deathDateTimePicker, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel5, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 16;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 540);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.plzTextBox);
            this.flowLayoutPanel3.Controls.Add(this.cityComboBox);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(123, 263);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(358, 29);
            this.flowLayoutPanel3.TabIndex = 12;
            // 
            // plzTextBox
            // 
            this.plzTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.plzTextBox.Location = new System.Drawing.Point(3, 3);
            this.plzTextBox.MaxLength = 10;
            this.plzTextBox.Name = "plzTextBox";
            this.plzTextBox.Size = new System.Drawing.Size(72, 20);
            this.plzTextBox.TabIndex = 12;
            // 
            // cityComboBox
            // 
            this.cityComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cityComboBox.DataSource = this.citiesBindingSource;
            this.cityComboBox.DisplayMember = "CityName";
            this.cityComboBox.FormattingEnabled = true;
            this.cityComboBox.Location = new System.Drawing.Point(81, 3);
            this.cityComboBox.Name = "cityComboBox";
            this.cityComboBox.Size = new System.Drawing.Size(272, 21);
            this.cityComboBox.TabIndex = 13;
            this.cityComboBox.ValueMember = "Id";
            this.cityComboBox.SelectedIndexChanged += new System.EventHandler(this.cityComboBox_SelectedIndexChanged);
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
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.imageTextBox);
            this.flowLayoutPanel4.Controls.Add(this.fileOpenButton);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(123, 388);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(358, 29);
            this.flowLayoutPanel4.TabIndex = 17;
            // 
            // imageTextBox
            // 
            this.imageTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.imageTextBox.Location = new System.Drawing.Point(3, 3);
            this.imageTextBox.Name = "imageTextBox";
            this.imageTextBox.Size = new System.Drawing.Size(265, 20);
            this.imageTextBox.TabIndex = 17;
            // 
            // fileOpenButton
            // 
            this.fileOpenButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.fileOpenButton.Location = new System.Drawing.Point(274, 3);
            this.fileOpenButton.Name = "fileOpenButton";
            this.fileOpenButton.Size = new System.Drawing.Size(75, 20);
            this.fileOpenButton.TabIndex = 18;
            this.fileOpenButton.Text = "Suchen";
            this.fileOpenButton.UseVisualStyleBackColor = true;
            this.fileOpenButton.Click += new System.EventHandler(this.fileOpenButton_Click);
            // 
            // remarkRichTextBox
            // 
            this.remarkRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remarkRichTextBox.Location = new System.Drawing.Point(123, 423);
            this.remarkRichTextBox.Name = "remarkRichTextBox";
            this.remarkRichTextBox.Size = new System.Drawing.Size(358, 84);
            this.remarkRichTextBox.TabIndex = 19;
            this.remarkRichTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Benutzername";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ist Benutzer?";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ist Administrator?";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 30);
            this.label4.TabIndex = 3;
            this.label4.Text = "Vorname, Nachname";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.nameTextBox);
            this.flowLayoutPanel2.Controls.Add(this.lastNameTextBox);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(123, 88);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(358, 24);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.nameTextBox.Location = new System.Drawing.Point(3, 3);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(172, 20);
            this.nameTextBox.TabIndex = 4;
            // 
            // lastNameTextBox
            // 
            this.lastNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lastNameTextBox.Location = new System.Drawing.Point(181, 3);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.Size = new System.Drawing.Size(172, 20);
            this.lastNameTextBox.TabIndex = 5;
            // 
            // isUserCheckBox
            // 
            this.isUserCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.isUserCheckBox.AutoSize = true;
            this.isUserCheckBox.Location = new System.Drawing.Point(123, 38);
            this.isUserCheckBox.Name = "isUserCheckBox";
            this.isUserCheckBox.Size = new System.Drawing.Size(134, 19);
            this.isUserCheckBox.TabIndex = 2;
            this.isUserCheckBox.Text = "Anwender von BikeDB";
            this.isUserCheckBox.UseVisualStyleBackColor = true;
            this.isUserCheckBox.CheckedChanged += new System.EventHandler(this.isUserCheckBox_CheckedChanged);
            // 
            // isAdminCheckBox
            // 
            this.isAdminCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.isAdminCheckBox.AutoSize = true;
            this.isAdminCheckBox.Location = new System.Drawing.Point(123, 63);
            this.isAdminCheckBox.Name = "isAdminCheckBox";
            this.isAdminCheckBox.Size = new System.Drawing.Size(146, 19);
            this.isAdminCheckBox.TabIndex = 3;
            this.isAdminCheckBox.Text = "Administrator von BikeDB";
            this.isAdminCheckBox.UseVisualStyleBackColor = true;
            this.isAdminCheckBox.CheckedChanged += new System.EventHandler(this.isAdminCheckBox_CheckedChanged);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(123, 513);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 20;
            this.addButton.Text = "Hinzufügen";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 420);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Anmerkung";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 385);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 35);
            this.label12.TabIndex = 11;
            this.label12.Text = "Bild";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // emailTextBox
            // 
            this.emailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.emailTextBox.Location = new System.Drawing.Point(123, 358);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(353, 20);
            this.emailTextBox.TabIndex = 16;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 355);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 30);
            this.label11.TabIndex = 10;
            this.label11.Text = "Email";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // phoneTextBox
            // 
            this.phoneTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.phoneTextBox.Location = new System.Drawing.Point(123, 328);
            this.phoneTextBox.Name = "phoneTextBox";
            this.phoneTextBox.Size = new System.Drawing.Size(353, 20);
            this.phoneTextBox.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 325);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 30);
            this.label10.TabIndex = 9;
            this.label10.Text = "Telefon";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 295);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 30);
            this.label9.TabIndex = 8;
            this.label9.Text = "Land";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // countryComboBox
            // 
            this.countryComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.countryComboBox.DataSource = this.countriesBindingSource;
            this.countryComboBox.DisplayMember = "Country";
            this.countryComboBox.FormattingEnabled = true;
            this.countryComboBox.Location = new System.Drawing.Point(123, 298);
            this.countryComboBox.Name = "countryComboBox";
            this.countryComboBox.Size = new System.Drawing.Size(353, 21);
            this.countryComboBox.TabIndex = 14;
            this.countryComboBox.ValueMember = "Id";
            // 
            // countriesBindingSource
            // 
            this.countriesBindingSource.DataMember = "Countries";
            this.countriesBindingSource.DataSource = this.dataSet;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 260);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 35);
            this.label8.TabIndex = 7;
            this.label8.Text = "PLZ, Stadt";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // street2TextBox
            // 
            this.street2TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.street2TextBox.Location = new System.Drawing.Point(123, 233);
            this.street2TextBox.Name = "street2TextBox";
            this.street2TextBox.Size = new System.Drawing.Size(353, 20);
            this.street2TextBox.TabIndex = 11;
            // 
            // street1TextBox
            // 
            this.street1TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.street1TextBox.Location = new System.Drawing.Point(123, 203);
            this.street1TextBox.Name = "street1TextBox";
            this.street1TextBox.Size = new System.Drawing.Size(353, 20);
            this.street1TextBox.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 30);
            this.label7.TabIndex = 6;
            this.label7.Text = "Straße (2)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 30);
            this.label6.TabIndex = 5;
            this.label6.Text = "Straße (1)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 30);
            this.label5.TabIndex = 4;
            this.label5.Text = "Geburtsdatum";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // birthdateTimePicker
            // 
            this.birthdateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.birthdateTimePicker.Location = new System.Drawing.Point(123, 143);
            this.birthdateTimePicker.Name = "birthdateTimePicker";
            this.birthdateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.birthdateTimePicker.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 170);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 30);
            this.label14.TabIndex = 42;
            this.label14.Text = "Sterbedatum";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deathDateTimePicker
            // 
            this.deathDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.deathDateTimePicker.Checked = false;
            this.deathDateTimePicker.Enabled = false;
            this.deathDateTimePicker.Location = new System.Drawing.Point(123, 173);
            this.deathDateTimePicker.Name = "deathDateTimePicker";
            this.deathDateTimePicker.ShowCheckBox = true;
            this.deathDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.deathDateTimePicker.TabIndex = 9;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 115);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 25);
            this.label15.TabIndex = 44;
            this.label15.Text = "Anzeigen";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.notShowCheckBox);
            this.flowLayoutPanel5.Controls.Add(this.deadCheckBox);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(123, 118);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(358, 19);
            this.flowLayoutPanel5.TabIndex = 6;
            // 
            // notShowCheckBox
            // 
            this.notShowCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.notShowCheckBox.AutoSize = true;
            this.notShowCheckBox.Location = new System.Drawing.Point(3, 3);
            this.notShowCheckBox.Name = "notShowCheckBox";
            this.notShowCheckBox.Size = new System.Drawing.Size(48, 17);
            this.notShowCheckBox.TabIndex = 6;
            this.notShowCheckBox.Text = "Nein";
            this.NotShownToolTip.SetToolTip(this.notShowCheckBox, "Wenn \"nein\" angeklickt wurde, werden Personen nicht mehr bei täglichen Touren ang" +
        "ezeigt.");
            this.notShowCheckBox.UseVisualStyleBackColor = true;
            // 
            // deadCheckBox
            // 
            this.deadCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.deadCheckBox.AutoSize = true;
            this.deadCheckBox.Location = new System.Drawing.Point(57, 3);
            this.deadCheckBox.Name = "deadCheckBox";
            this.deadCheckBox.Size = new System.Drawing.Size(83, 17);
            this.deadCheckBox.TabIndex = 7;
            this.deadCheckBox.Text = "Verstorben?";
            this.deadCheckBox.UseVisualStyleBackColor = true;
            this.deadCheckBox.CheckedChanged += new System.EventHandler(this.deadCheckBox_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.userNameTextBox);
            this.flowLayoutPanel1.Controls.Add(this.pwdButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(123, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(358, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.userNameTextBox.Enabled = false;
            this.userNameTextBox.Location = new System.Drawing.Point(3, 3);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(265, 20);
            this.userNameTextBox.TabIndex = 0;
            // 
            // pwdButton
            // 
            this.pwdButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pwdButton.Location = new System.Drawing.Point(274, 3);
            this.pwdButton.Name = "pwdButton";
            this.pwdButton.Size = new System.Drawing.Size(75, 20);
            this.pwdButton.TabIndex = 1;
            this.pwdButton.Text = "Passwort";
            this.pwdButton.UseVisualStyleBackColor = true;
            this.pwdButton.Click += new System.EventHandler(this.pwdButton_Click);
            // 
            // imageOpenFileDialog
            // 
            this.imageOpenFileDialog.Filter = "PNG-Bilddatei|*.png|JPG-Bilddatei|*.jpg|GIF-Bilddatei|*.gif|Alle Dateien (*.*)|*." +
    "*";
            this.imageOpenFileDialog.Title = "Bild für Person auswählen";
            // 
            // countriesTableAdapter
            // 
            this.countriesTableAdapter.ClearBeforeFill = true;
            // 
            // citiesTableAdapter
            // 
            this.citiesTableAdapter.ClearBeforeFill = true;
            // 
            // NotShownToolTip
            // 
            this.NotShownToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.NotShownToolTip.ToolTipTitle = "Sichtbarkeit";
            // 
            // PersonsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 562);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PersonsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Personen";
            this.Load += new System.EventHandler(this.PersonsForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countriesBindingSource)).EndInit();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.CheckBox isUserCheckBox;
        private System.Windows.Forms.CheckBox isAdminCheckBox;
        private System.Windows.Forms.DateTimePicker birthdateTimePicker;
        private System.Windows.Forms.TextBox street1TextBox;
        private System.Windows.Forms.TextBox street2TextBox;
        private System.Windows.Forms.ComboBox countryComboBox;
        private System.Windows.Forms.TextBox phoneTextBox;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.OpenFileDialog imageOpenFileDialog;
        private DataSet dataSet;
        private System.Windows.Forms.BindingSource countriesBindingSource;
        private DataSetTableAdapters.CountriesTableAdapter countriesTableAdapter;
        private System.Windows.Forms.BindingSource citiesBindingSource;
        private DataSetTableAdapters.CitiesTableAdapter citiesTableAdapter;
        private System.Windows.Forms.Button pwdButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.TextBox plzTextBox;
        private System.Windows.Forms.ComboBox cityComboBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.TextBox imageTextBox;
        private System.Windows.Forms.Button fileOpenButton;
        private System.Windows.Forms.RichTextBox remarkRichTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker deathDateTimePicker;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.CheckBox notShowCheckBox;
        private System.Windows.Forms.CheckBox deadCheckBox;
        private System.Windows.Forms.ToolTip NotShownToolTip;
    }
}