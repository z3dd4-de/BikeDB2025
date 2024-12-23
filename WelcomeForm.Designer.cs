namespace BikeDB2024
{
    partial class WelcomeForm
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
            this.showAtStartCheckBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.showAtStartCheckBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.okButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.webBrowser, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(706, 469);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // showAtStartCheckBox
            // 
            this.showAtStartCheckBox.AutoSize = true;
            this.showAtStartCheckBox.Checked = true;
            this.showAtStartCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showAtStartCheckBox.Location = new System.Drawing.Point(2, 415);
            this.showAtStartCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.showAtStartCheckBox.Name = "showAtStartCheckBox";
            this.showAtStartCheckBox.Size = new System.Drawing.Size(143, 17);
            this.showAtStartCheckBox.TabIndex = 0;
            this.showAtStartCheckBox.Text = "Bei jedem Start anzeigen";
            this.showAtStartCheckBox.UseVisualStyleBackColor = true;
            this.showAtStartCheckBox.CheckedChanged += new System.EventHandler(this.showAtStartCheckBox_CheckedChanged);
            // 
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.okButton.Location = new System.Drawing.Point(601, 445);
            this.okButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(103, 19);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "Schließen";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(2, 2);
            this.webBrowser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.webBrowser.MinimumSize = new System.Drawing.Size(15, 16);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(702, 409);
            this.webBrowser.TabIndex = 2;
            // 
            // WelcomeForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 469);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WelcomeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Willkommen bei BikeDB 2025";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.WelcomeForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox showAtStartCheckBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.WebBrowser webBrowser;
    }
}