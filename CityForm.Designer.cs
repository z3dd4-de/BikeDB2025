namespace BikeDB2024
{
    partial class CityForm
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
            this.remarkRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.citiesComboBox = new System.Windows.Forms.ComboBox();
            this.citiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet = new BikeDB2024.DataSet();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.countryComboBox = new System.Windows.Forms.ComboBox();
            this.countriesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bundeslandComboBox = new System.Windows.Forms.ComboBox();
            this.bundeslaenderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.prefixTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.linkTextBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.imageTextBox = new System.Windows.Forms.TextBox();
            this.openImageButton = new System.Windows.Forms.Button();
            this.kfzTextBox = new System.Windows.Forms.TextBox();
            this.heightTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.gpsTextBox = new System.Windows.Forms.TextBox();
            this.citiesTableAdapter = new BikeDB2024.DataSetTableAdapters.CitiesTableAdapter();
            this.countryDataSet = new BikeDB2024.CountryDataSet();
            this.countryDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.countriesTableAdapter = new BikeDB2024.DataSetTableAdapters.CountriesTableAdapter();
            this.bundeslaenderTableAdapter = new BikeDB2024.DataSetTableAdapters.BundeslaenderTableAdapter();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countriesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bundeslaenderBindingSource)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countryDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.errorToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 382);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(473, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // errorToolStripStatusLabel
            // 
            this.errorToolStripStatusLabel.Name = "errorToolStripStatusLabel";
            this.errorToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.remarkRichTextBox, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.citiesComboBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.nameTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.countryComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.bundeslandComboBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.prefixTextBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.linkTextBox, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.kfzTextBox, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.heightTextBox, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.addButton, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.gpsTextBox, 1, 9);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(473, 382);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // remarkRichTextBox
            // 
            this.remarkRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remarkRichTextBox.Location = new System.Drawing.Point(138, 260);
            this.remarkRichTextBox.Name = "remarkRichTextBox";
            this.remarkRichTextBox.Size = new System.Drawing.Size(332, 89);
            this.remarkRichTextBox.TabIndex = 11;
            this.remarkRichTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vorhandene Städte";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 48);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Land";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 72);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Bundesland (D)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 96);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Vorwahl";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 120);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Kfz-Kennzeichen";
            // 
            // citiesComboBox
            // 
            this.citiesComboBox.DataSource = this.citiesBindingSource;
            this.citiesComboBox.DisplayMember = "CityName";
            this.citiesComboBox.FormattingEnabled = true;
            this.citiesComboBox.Location = new System.Drawing.Point(137, 2);
            this.citiesComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.citiesComboBox.Name = "citiesComboBox";
            this.citiesComboBox.Size = new System.Drawing.Size(188, 21);
            this.citiesComboBox.TabIndex = 0;
            this.citiesComboBox.ValueMember = "Id";
            // 
            // citiesBindingSource
            // 
            this.citiesBindingSource.DataMember = "Cities";
            this.citiesBindingSource.DataSource = this.dataSet;
            this.citiesBindingSource.Sort = "CityName";
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(137, 26);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.nameTextBox.MaxLength = 50;
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(188, 20);
            this.nameTextBox.TabIndex = 1;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // countryComboBox
            // 
            this.countryComboBox.DataSource = this.countriesBindingSource;
            this.countryComboBox.DisplayMember = "Country";
            this.countryComboBox.FormattingEnabled = true;
            this.countryComboBox.Location = new System.Drawing.Point(137, 50);
            this.countryComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.countryComboBox.Name = "countryComboBox";
            this.countryComboBox.Size = new System.Drawing.Size(188, 21);
            this.countryComboBox.TabIndex = 2;
            this.countryComboBox.ValueMember = "Id";
            this.countryComboBox.SelectedIndexChanged += new System.EventHandler(this.countryComboBox_SelectedIndexChanged);
            // 
            // countriesBindingSource
            // 
            this.countriesBindingSource.DataMember = "Countries";
            this.countriesBindingSource.DataSource = this.dataSet;
            this.countriesBindingSource.Sort = "Country";
            // 
            // bundeslandComboBox
            // 
            this.bundeslandComboBox.DataSource = this.bundeslaenderBindingSource;
            this.bundeslandComboBox.DisplayMember = "Bundesland";
            this.bundeslandComboBox.FormattingEnabled = true;
            this.bundeslandComboBox.Location = new System.Drawing.Point(137, 74);
            this.bundeslandComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.bundeslandComboBox.Name = "bundeslandComboBox";
            this.bundeslandComboBox.Size = new System.Drawing.Size(188, 21);
            this.bundeslandComboBox.TabIndex = 3;
            this.bundeslandComboBox.ValueMember = "Id";
            // 
            // bundeslaenderBindingSource
            // 
            this.bundeslaenderBindingSource.DataMember = "Bundeslaender";
            this.bundeslaenderBindingSource.DataSource = this.dataSet;
            // 
            // prefixTextBox
            // 
            this.prefixTextBox.Location = new System.Drawing.Point(137, 98);
            this.prefixTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.prefixTextBox.MaxLength = 10;
            this.prefixTextBox.Name = "prefixTextBox";
            this.prefixTextBox.Size = new System.Drawing.Size(188, 20);
            this.prefixTextBox.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Höhe (Stadtmitte)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Bild";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 203);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Link";
            // 
            // linkTextBox
            // 
            this.linkTextBox.Location = new System.Drawing.Point(137, 205);
            this.linkTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.linkTextBox.Name = "linkTextBox";
            this.linkTextBox.Size = new System.Drawing.Size(325, 20);
            this.linkTextBox.TabIndex = 9;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.imageTextBox);
            this.flowLayoutPanel1.Controls.Add(this.openImageButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(138, 171);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(332, 29);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // imageTextBox
            // 
            this.imageTextBox.Location = new System.Drawing.Point(3, 3);
            this.imageTextBox.Name = "imageTextBox";
            this.imageTextBox.Size = new System.Drawing.Size(240, 20);
            this.imageTextBox.TabIndex = 7;
            // 
            // openImageButton
            // 
            this.openImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openImageButton.Location = new System.Drawing.Point(249, 3);
            this.openImageButton.Name = "openImageButton";
            this.openImageButton.Size = new System.Drawing.Size(75, 20);
            this.openImageButton.TabIndex = 8;
            this.openImageButton.Text = "Öffnen";
            this.openImageButton.UseVisualStyleBackColor = true;
            this.openImageButton.Click += new System.EventHandler(this.openImageButton_Click);
            // 
            // kfzTextBox
            // 
            this.kfzTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.kfzTextBox.Location = new System.Drawing.Point(138, 123);
            this.kfzTextBox.MaxLength = 3;
            this.kfzTextBox.Name = "kfzTextBox";
            this.kfzTextBox.Size = new System.Drawing.Size(187, 20);
            this.kfzTextBox.TabIndex = 5;
            // 
            // heightTextBox
            // 
            this.heightTextBox.Location = new System.Drawing.Point(138, 147);
            this.heightTextBox.MaxLength = 5;
            this.heightTextBox.Name = "heightTextBox";
            this.heightTextBox.Size = new System.Drawing.Size(187, 20);
            this.heightTextBox.TabIndex = 6;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(137, 354);
            this.addButton.Margin = new System.Windows.Forms.Padding(2);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(91, 26);
            this.addButton.TabIndex = 12;
            this.addButton.Text = "Hinzufügen";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 257);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Anmerkung";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 227);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "GPS-Koordinaten";
            // 
            // gpsTextBox
            // 
            this.gpsTextBox.Location = new System.Drawing.Point(138, 230);
            this.gpsTextBox.Name = "gpsTextBox";
            this.gpsTextBox.Size = new System.Drawing.Size(187, 20);
            this.gpsTextBox.TabIndex = 10;
            // 
            // citiesTableAdapter
            // 
            this.citiesTableAdapter.ClearBeforeFill = true;
            // 
            // countryDataSet
            // 
            this.countryDataSet.DataSetName = "CountryDataSet";
            this.countryDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // countryDataSetBindingSource
            // 
            this.countryDataSetBindingSource.DataSource = this.countryDataSet;
            this.countryDataSetBindingSource.Position = 0;
            this.countryDataSetBindingSource.Sort = "";
            // 
            // countriesTableAdapter
            // 
            this.countriesTableAdapter.ClearBeforeFill = true;
            // 
            // bundeslaenderTableAdapter
            // 
            this.bundeslaenderTableAdapter.ClearBeforeFill = true;
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "PNG-Bilddatei|*.png|JPG-Bilddatei|*.jpg|GIF-Bilddatei|*.gif|Alle Dateien (*.*)|*." +
    "*";
            this.openImageDialog.Title = "Bild der Stadt auswählen";
            // 
            // CityForm
            // 
            this.AcceptButton = this.addButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 404);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CityForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Neue Stadt";
            this.Load += new System.EventHandler(this.CityForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countriesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bundeslaenderBindingSource)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countryDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryDataSetBindingSource)).EndInit();
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
        private System.Windows.Forms.ComboBox citiesComboBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ComboBox countryComboBox;
        private System.Windows.Forms.ComboBox bundeslandComboBox;
        private System.Windows.Forms.TextBox prefixTextBox;
        private System.Windows.Forms.TextBox linkTextBox;
        private DataSet dataSet;
        private System.Windows.Forms.BindingSource citiesBindingSource;
        private DataSetTableAdapters.CitiesTableAdapter citiesTableAdapter;
        private System.Windows.Forms.BindingSource countryDataSetBindingSource;
        private CountryDataSet countryDataSet;
        private System.Windows.Forms.BindingSource countriesBindingSource;
        private DataSetTableAdapters.CountriesTableAdapter countriesTableAdapter;
        private System.Windows.Forms.BindingSource bundeslaenderBindingSource;
        private DataSetTableAdapters.BundeslaenderTableAdapter bundeslaenderTableAdapter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox imageTextBox;
        private System.Windows.Forms.Button openImageButton;
        private System.Windows.Forms.TextBox kfzTextBox;
        private System.Windows.Forms.TextBox heightTextBox;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RichTextBox remarkRichTextBox;
        private System.Windows.Forms.TextBox gpsTextBox;
    }
}