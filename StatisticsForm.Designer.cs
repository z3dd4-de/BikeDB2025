namespace BikeDB2024
{
    partial class StatisticsForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticsForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statisticsTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tourenLabel = new System.Windows.Forms.Label();
            this.kmTotalLabel = new System.Windows.Forms.Label();
            this.timeTotalLabel = new System.Windows.Forms.Label();
            this.vmaxTotalLabel = new System.Windows.Forms.Label();
            this.vehiclesCountLabel = new System.Windows.Forms.Label();
            this.countriesCountLabel = new System.Windows.Forms.Label();
            this.citiesCountLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.routesCountLabel = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.vehicleComboBox = new System.Windows.Forms.ComboBox();
            this.vehiclesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet = new BikeDB2024.DataSet();
            this.vecTotalToursLabel = new System.Windows.Forms.Label();
            this.vecTotalKmLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.vecRoutesLabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.vecCitiesLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.vecAvgLabel = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.vecVmaxLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.vecTotalTimeLabel = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.vecKmLabel = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.allCheckBox = new System.Windows.Forms.CheckBox();
            this.kmCheckBox = new System.Windows.Forms.CheckBox();
            this.avgCheckBox = new System.Windows.Forms.CheckBox();
            this.vmaxCheckBox = new System.Windows.Forms.CheckBox();
            this.statChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.statToolStrip = new System.Windows.Forms.ToolStrip();
            this.printCurrentPageToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printAllToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printPreviewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printSetupToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.vehiclesTableAdapter = new BikeDB2024.DataSetTableAdapters.VehiclesTableAdapter();
            this.statisticsPageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.statisticsPrintDialog = new System.Windows.Forms.PrintDialog();
            this.statisticsPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.statisticsPrintPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statisticsTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vehiclesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statChart)).BeginInit();
            this.statToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.statisticsTabControl);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(789, 451);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(789, 490);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.statToolStrip);
            // 
            // statisticsTabControl
            // 
            this.statisticsTabControl.Controls.Add(this.tabPage1);
            this.statisticsTabControl.Controls.Add(this.tabPage2);
            this.statisticsTabControl.Controls.Add(this.tabPage3);
            this.statisticsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statisticsTabControl.Location = new System.Drawing.Point(0, 0);
            this.statisticsTabControl.Name = "statisticsTabControl";
            this.statisticsTabControl.SelectedIndex = 0;
            this.statisticsTabControl.Size = new System.Drawing.Size(789, 451);
            this.statisticsTabControl.TabIndex = 0;
            this.statisticsTabControl.SelectedIndexChanged += new System.EventHandler(this.statisticsTabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(781, 425);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Übersicht";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tourenLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.kmTotalLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.timeTotalLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.vmaxTotalLabel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.vehiclesCountLabel, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.countriesCountLabel, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.citiesCountLabel, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label28, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.routesCountLabel, 1, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(775, 419);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tagestouren gesamt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Gesamtkilometer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Gesamtfahrzeit";
            // 
            // tourenLabel
            // 
            this.tourenLabel.AutoSize = true;
            this.tourenLabel.Location = new System.Drawing.Point(133, 0);
            this.tourenLabel.Name = "tourenLabel";
            this.tourenLabel.Size = new System.Drawing.Size(63, 13);
            this.tourenLabel.TabIndex = 7;
            this.tourenLabel.Text = "tourenLabel";
            // 
            // kmTotalLabel
            // 
            this.kmTotalLabel.AutoSize = true;
            this.kmTotalLabel.Location = new System.Drawing.Point(133, 30);
            this.kmTotalLabel.Name = "kmTotalLabel";
            this.kmTotalLabel.Size = new System.Drawing.Size(71, 13);
            this.kmTotalLabel.TabIndex = 8;
            this.kmTotalLabel.Text = "kmTotalLabel";
            // 
            // timeTotalLabel
            // 
            this.timeTotalLabel.AutoSize = true;
            this.timeTotalLabel.Location = new System.Drawing.Point(133, 60);
            this.timeTotalLabel.Name = "timeTotalLabel";
            this.timeTotalLabel.Size = new System.Drawing.Size(76, 13);
            this.timeTotalLabel.TabIndex = 9;
            this.timeTotalLabel.Text = "timeTotalLabel";
            // 
            // vmaxTotalLabel
            // 
            this.vmaxTotalLabel.AutoSize = true;
            this.vmaxTotalLabel.Location = new System.Drawing.Point(133, 90);
            this.vmaxTotalLabel.Name = "vmaxTotalLabel";
            this.vmaxTotalLabel.Size = new System.Drawing.Size(82, 13);
            this.vmaxTotalLabel.TabIndex = 10;
            this.vmaxTotalLabel.Text = "vmaxTotalLabel";
            // 
            // vehiclesCountLabel
            // 
            this.vehiclesCountLabel.AutoSize = true;
            this.vehiclesCountLabel.Location = new System.Drawing.Point(133, 120);
            this.vehiclesCountLabel.Name = "vehiclesCountLabel";
            this.vehiclesCountLabel.Size = new System.Drawing.Size(100, 13);
            this.vehiclesCountLabel.TabIndex = 11;
            this.vehiclesCountLabel.Text = "vehiclesCountLabel";
            // 
            // countriesCountLabel
            // 
            this.countriesCountLabel.AutoSize = true;
            this.countriesCountLabel.Location = new System.Drawing.Point(133, 150);
            this.countriesCountLabel.Name = "countriesCountLabel";
            this.countriesCountLabel.Size = new System.Drawing.Size(104, 13);
            this.countriesCountLabel.TabIndex = 12;
            this.countriesCountLabel.Text = "countriesCountLabel";
            // 
            // citiesCountLabel
            // 
            this.citiesCountLabel.AutoSize = true;
            this.citiesCountLabel.Location = new System.Drawing.Point(133, 180);
            this.citiesCountLabel.Name = "citiesCountLabel";
            this.citiesCountLabel.Size = new System.Drawing.Size(85, 13);
            this.citiesCountLabel.TabIndex = 13;
            this.citiesCountLabel.Text = "citiesCountLabel";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Strecken";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Städte";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Länder";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Fahrzeuge";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(3, 90);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(117, 13);
            this.label28.TabIndex = 14;
            this.label28.Text = "Höchstgeschwindigkeit";
            // 
            // routesCountLabel
            // 
            this.routesCountLabel.AutoSize = true;
            this.routesCountLabel.Location = new System.Drawing.Point(133, 210);
            this.routesCountLabel.Name = "routesCountLabel";
            this.routesCountLabel.Size = new System.Drawing.Size(90, 13);
            this.routesCountLabel.TabIndex = 15;
            this.routesCountLabel.Text = "routesCountLabel";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(781, 425);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Fahrzeugstatistik";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.vehicleComboBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.vecTotalToursLabel, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.vecTotalKmLabel, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label12, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.vecRoutesLabel, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.vecCitiesLabel, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.label14, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.vecAvgLabel, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.label30, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.vecVmaxLabel, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.vecTotalTimeLabel, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label15, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.vecKmLabel, 1, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(775, 419);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Gesamtkilometer";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Tagestouren gesamt";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Fahrzeug";
            // 
            // vehicleComboBox
            // 
            this.vehicleComboBox.DataSource = this.vehiclesBindingSource;
            this.vehicleComboBox.DisplayMember = "VehicleName";
            this.vehicleComboBox.FormattingEnabled = true;
            this.vehicleComboBox.Location = new System.Drawing.Point(133, 3);
            this.vehicleComboBox.Name = "vehicleComboBox";
            this.vehicleComboBox.Size = new System.Drawing.Size(206, 21);
            this.vehicleComboBox.TabIndex = 8;
            this.vehicleComboBox.ValueMember = "Id";
            this.vehicleComboBox.SelectedIndexChanged += new System.EventHandler(this.vehicleComboBox_SelectedIndexChanged);
            // 
            // vehiclesBindingSource
            // 
            this.vehiclesBindingSource.DataMember = "Vehicles";
            this.vehiclesBindingSource.DataSource = this.dataSet;
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // vecTotalToursLabel
            // 
            this.vecTotalToursLabel.AutoSize = true;
            this.vecTotalToursLabel.Location = new System.Drawing.Point(133, 30);
            this.vecTotalToursLabel.Name = "vecTotalToursLabel";
            this.vecTotalToursLabel.Size = new System.Drawing.Size(102, 13);
            this.vecTotalToursLabel.TabIndex = 9;
            this.vecTotalToursLabel.Text = "vecTotalToursLabel";
            // 
            // vecTotalKmLabel
            // 
            this.vecTotalKmLabel.AutoSize = true;
            this.vecTotalKmLabel.Location = new System.Drawing.Point(133, 60);
            this.vecTotalKmLabel.Name = "vecTotalKmLabel";
            this.vecTotalKmLabel.Size = new System.Drawing.Size(90, 13);
            this.vecTotalKmLabel.TabIndex = 10;
            this.vecTotalKmLabel.Text = "vecTotalKmLabel";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 240);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Strecken";
            // 
            // vecRoutesLabel
            // 
            this.vecRoutesLabel.AutoSize = true;
            this.vecRoutesLabel.Location = new System.Drawing.Point(133, 240);
            this.vecRoutesLabel.Name = "vecRoutesLabel";
            this.vecRoutesLabel.Size = new System.Drawing.Size(85, 13);
            this.vecRoutesLabel.TabIndex = 16;
            this.vecRoutesLabel.Text = "vecRoutesLabel";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 210);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Städte";
            // 
            // vecCitiesLabel
            // 
            this.vecCitiesLabel.AutoSize = true;
            this.vecCitiesLabel.Location = new System.Drawing.Point(133, 210);
            this.vecCitiesLabel.Name = "vecCitiesLabel";
            this.vecCitiesLabel.Size = new System.Drawing.Size(76, 13);
            this.vecCitiesLabel.TabIndex = 14;
            this.vecCitiesLabel.Text = "vecCitiesLabel";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 180);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "Durchschnitt";
            // 
            // vecAvgLabel
            // 
            this.vecAvgLabel.AutoSize = true;
            this.vecAvgLabel.Location = new System.Drawing.Point(133, 180);
            this.vecAvgLabel.Name = "vecAvgLabel";
            this.vecAvgLabel.Size = new System.Drawing.Size(95, 13);
            this.vecAvgLabel.TabIndex = 13;
            this.vecAvgLabel.Text = "vecCountriesLabel";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(3, 150);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(117, 13);
            this.label30.TabIndex = 15;
            this.label30.Text = "Höchstgeschwindigkeit";
            // 
            // vecVmaxLabel
            // 
            this.vecVmaxLabel.AutoSize = true;
            this.vecVmaxLabel.Location = new System.Drawing.Point(133, 150);
            this.vecVmaxLabel.Name = "vecVmaxLabel";
            this.vecVmaxLabel.Size = new System.Drawing.Size(77, 13);
            this.vecVmaxLabel.TabIndex = 12;
            this.vecVmaxLabel.Text = "vecVmaxLabel";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 120);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Gesamtfahrzeit";
            // 
            // vecTotalTimeLabel
            // 
            this.vecTotalTimeLabel.AutoSize = true;
            this.vecTotalTimeLabel.Location = new System.Drawing.Point(133, 120);
            this.vecTotalTimeLabel.Name = "vecTotalTimeLabel";
            this.vecTotalTimeLabel.Size = new System.Drawing.Size(98, 13);
            this.vecTotalTimeLabel.TabIndex = 11;
            this.vecTotalTimeLabel.Text = "vecTotalTimeLabel";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 90);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(50, 13);
            this.label15.TabIndex = 17;
            this.label15.Text = "Kilometer";
            // 
            // vecKmLabel
            // 
            this.vecKmLabel.AutoSize = true;
            this.vecKmLabel.Location = new System.Drawing.Point(133, 90);
            this.vecKmLabel.Name = "vecKmLabel";
            this.vecKmLabel.Size = new System.Drawing.Size(66, 13);
            this.vecKmLabel.TabIndex = 18;
            this.vecKmLabel.Text = "vecKmLabel";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(781, 425);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Diagramme";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.statChart, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(781, 425);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.allCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.kmCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.avgCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.vmaxCheckBox);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(775, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // allCheckBox
            // 
            this.allCheckBox.AutoSize = true;
            this.allCheckBox.Checked = true;
            this.allCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allCheckBox.Location = new System.Drawing.Point(3, 3);
            this.allCheckBox.Name = "allCheckBox";
            this.allCheckBox.Size = new System.Drawing.Size(43, 17);
            this.allCheckBox.TabIndex = 0;
            this.allCheckBox.Text = "Alle";
            this.allCheckBox.UseVisualStyleBackColor = true;
            this.allCheckBox.Click += new System.EventHandler(this.allCheckBox_Click);
            // 
            // kmCheckBox
            // 
            this.kmCheckBox.AutoSize = true;
            this.kmCheckBox.Checked = true;
            this.kmCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kmCheckBox.Enabled = false;
            this.kmCheckBox.Location = new System.Drawing.Point(52, 3);
            this.kmCheckBox.Name = "kmCheckBox";
            this.kmCheckBox.Size = new System.Drawing.Size(69, 17);
            this.kmCheckBox.TabIndex = 1;
            this.kmCheckBox.Text = "Kilometer";
            this.kmCheckBox.UseVisualStyleBackColor = true;
            this.kmCheckBox.Click += new System.EventHandler(this.kmCheckBox_Click);
            // 
            // avgCheckBox
            // 
            this.avgCheckBox.AutoSize = true;
            this.avgCheckBox.Checked = true;
            this.avgCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.avgCheckBox.Enabled = false;
            this.avgCheckBox.Location = new System.Drawing.Point(127, 3);
            this.avgCheckBox.Name = "avgCheckBox";
            this.avgCheckBox.Size = new System.Drawing.Size(114, 17);
            this.avgCheckBox.TabIndex = 2;
            this.avgCheckBox.Text = "Durchschnitt km/h";
            this.avgCheckBox.UseVisualStyleBackColor = true;
            this.avgCheckBox.Click += new System.EventHandler(this.avgCheckBox_Click);
            // 
            // vmaxCheckBox
            // 
            this.vmaxCheckBox.AutoSize = true;
            this.vmaxCheckBox.Checked = true;
            this.vmaxCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vmaxCheckBox.Enabled = false;
            this.vmaxCheckBox.Location = new System.Drawing.Point(247, 3);
            this.vmaxCheckBox.Name = "vmaxCheckBox";
            this.vmaxCheckBox.Size = new System.Drawing.Size(80, 17);
            this.vmaxCheckBox.TabIndex = 3;
            this.vmaxCheckBox.Text = "Vmax km/h";
            this.vmaxCheckBox.UseVisualStyleBackColor = true;
            this.vmaxCheckBox.Click += new System.EventHandler(this.vmaxCheckBox_Click);
            // 
            // statChart
            // 
            chartArea4.Name = "ChartArea1";
            this.statChart.ChartAreas.Add(chartArea4);
            this.statChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.statChart.Legends.Add(legend4);
            this.statChart.Location = new System.Drawing.Point(3, 38);
            this.statChart.Name = "statChart";
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series10.Legend = "Legend1";
            series10.Name = "Kilometer";
            series11.ChartArea = "ChartArea1";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series11.Legend = "Legend1";
            series11.Name = "Durchschnitt";
            series12.ChartArea = "ChartArea1";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series12.Legend = "Legend1";
            series12.Name = "Vmax";
            this.statChart.Series.Add(series10);
            this.statChart.Series.Add(series11);
            this.statChart.Series.Add(series12);
            this.statChart.Size = new System.Drawing.Size(775, 384);
            this.statChart.TabIndex = 1;
            this.statChart.Text = "chart1";
            title4.Name = "Title1";
            title4.Text = "Statistiken für Fahrzeug XY";
            this.statChart.Titles.Add(title4);
            // 
            // statToolStrip
            // 
            this.statToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printCurrentPageToolStripButton,
            this.printAllToolStripButton,
            this.printPreviewToolStripButton,
            this.printSetupToolStripButton,
            this.toolStripSeparator1,
            this.exitToolStripButton});
            this.statToolStrip.Location = new System.Drawing.Point(3, 0);
            this.statToolStrip.Name = "statToolStrip";
            this.statToolStrip.Size = new System.Drawing.Size(198, 39);
            this.statToolStrip.TabIndex = 0;
            // 
            // printCurrentPageToolStripButton
            // 
            this.printCurrentPageToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printCurrentPageToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printCurrentPageToolStripButton.Image")));
            this.printCurrentPageToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printCurrentPageToolStripButton.Name = "printCurrentPageToolStripButton";
            this.printCurrentPageToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.printCurrentPageToolStripButton.Text = "Aktuelle Seite drucken";
            this.printCurrentPageToolStripButton.Click += new System.EventHandler(this.printCurrentPageToolStripButton_Click);
            // 
            // printAllToolStripButton
            // 
            this.printAllToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printAllToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printAllToolStripButton.Image")));
            this.printAllToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printAllToolStripButton.Name = "printAllToolStripButton";
            this.printAllToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.printAllToolStripButton.Text = "Alles drucken";
            this.printAllToolStripButton.Click += new System.EventHandler(this.printAllToolStripButton_Click);
            // 
            // printPreviewToolStripButton
            // 
            this.printPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printPreviewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripButton.Image")));
            this.printPreviewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            this.printPreviewToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.printPreviewToolStripButton.Text = "Vorschau";
            this.printPreviewToolStripButton.Click += new System.EventHandler(this.printPreviewToolStripButton_Click);
            // 
            // printSetupToolStripButton
            // 
            this.printSetupToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printSetupToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printSetupToolStripButton.Image")));
            this.printSetupToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printSetupToolStripButton.Name = "printSetupToolStripButton";
            this.printSetupToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.printSetupToolStripButton.Text = "Seite einrichten";
            this.printSetupToolStripButton.Click += new System.EventHandler(this.printSetupToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
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
            // statBackgroundWorker
            // 
            this.statBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.statBackgroundWorker_DoWork);
            this.statBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.statBackgroundWorker_RunWorkerCompleted);
            // 
            // vehiclesTableAdapter
            // 
            this.vehiclesTableAdapter.ClearBeforeFill = true;
            // 
            // statisticsPrintDialog
            // 
            this.statisticsPrintDialog.UseEXDialog = true;
            // 
            // statisticsPrintPreviewDialog
            // 
            this.statisticsPrintPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.statisticsPrintPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.statisticsPrintPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.statisticsPrintPreviewDialog.Enabled = true;
            this.statisticsPrintPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("statisticsPrintPreviewDialog.Icon")));
            this.statisticsPrintPreviewDialog.Name = "printPreviewDialog1";
            this.statisticsPrintPreviewDialog.Visible = false;
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 490);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StatisticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Statistiken";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StatisticsForm_FormClosing);
            this.Load += new System.EventHandler(this.StatisticsForm_Load);
            this.Shown += new System.EventHandler(this.StatisticsForm_Shown);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statisticsTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vehiclesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statChart)).EndInit();
            this.statToolStrip.ResumeLayout(false);
            this.statToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip statToolStrip;
        private System.Windows.Forms.ToolStripButton exitToolStripButton;
        private System.Windows.Forms.TabControl statisticsTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label tourenLabel;
        private System.Windows.Forms.Label kmTotalLabel;
        private System.Windows.Forms.Label timeTotalLabel;
        private System.Windows.Forms.Label vmaxTotalLabel;
        private System.Windows.Forms.Label vehiclesCountLabel;
        private System.Windows.Forms.Label countriesCountLabel;
        private System.Windows.Forms.Label citiesCountLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox vehicleComboBox;
        private System.Windows.Forms.Label vecTotalToursLabel;
        private System.Windows.Forms.Label vecTotalKmLabel;
        private System.Windows.Forms.Label vecTotalTimeLabel;
        private System.Windows.Forms.Label vecVmaxLabel;
        private System.Windows.Forms.Label vecAvgLabel;
        private System.Windows.Forms.Label vecCitiesLabel;
        private System.Windows.Forms.TabPage tabPage3;
        private System.ComponentModel.BackgroundWorker statBackgroundWorker;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart statChart;
        private System.Windows.Forms.CheckBox allCheckBox;
        private System.Windows.Forms.CheckBox kmCheckBox;
        private System.Windows.Forms.CheckBox avgCheckBox;
        private System.Windows.Forms.CheckBox vmaxCheckBox;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label routesCountLabel;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label vecRoutesLabel;
        private DataSet dataSet;
        private System.Windows.Forms.BindingSource vehiclesBindingSource;
        private DataSetTableAdapters.VehiclesTableAdapter vehiclesTableAdapter;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label vecKmLabel;
        private System.Windows.Forms.ToolStripButton printCurrentPageToolStripButton;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton printSetupToolStripButton;
        private System.Windows.Forms.ToolStripButton printAllToolStripButton;
        private System.Windows.Forms.PageSetupDialog statisticsPageSetupDialog;
        private System.Windows.Forms.PrintDialog statisticsPrintDialog;
        private System.Drawing.Printing.PrintDocument statisticsPrintDocument;
        private System.Windows.Forms.PrintPreviewDialog statisticsPrintPreviewDialog;
    }
}