namespace BikeDB2024
{
    partial class PasswordForm
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
            this.pwd1TextBox = new System.Windows.Forms.TextBox();
            this.pwd2TextBox = new System.Windows.Forms.TextBox();
            this.showPwdCheckBox = new System.Windows.Forms.CheckBox();
            this.changePwdButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pwd1TextBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pwd2TextBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.showPwdCheckBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.changePwdButton, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(268, 118);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pwd1TextBox
            // 
            this.pwd1TextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pwd1TextBox.Location = new System.Drawing.Point(3, 3);
            this.pwd1TextBox.Name = "pwd1TextBox";
            this.pwd1TextBox.Size = new System.Drawing.Size(262, 20);
            this.pwd1TextBox.TabIndex = 0;
            this.pwd1TextBox.UseSystemPasswordChar = true;
            this.pwd1TextBox.TextChanged += new System.EventHandler(this.pwd1TextBox_TextChanged);
            // 
            // pwd2TextBox
            // 
            this.pwd2TextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pwd2TextBox.Location = new System.Drawing.Point(3, 33);
            this.pwd2TextBox.Name = "pwd2TextBox";
            this.pwd2TextBox.Size = new System.Drawing.Size(262, 20);
            this.pwd2TextBox.TabIndex = 1;
            this.pwd2TextBox.UseSystemPasswordChar = true;
            this.pwd2TextBox.TextChanged += new System.EventHandler(this.pwd2TextBox_TextChanged);
            // 
            // showPwdCheckBox
            // 
            this.showPwdCheckBox.AutoSize = true;
            this.showPwdCheckBox.Location = new System.Drawing.Point(3, 63);
            this.showPwdCheckBox.Name = "showPwdCheckBox";
            this.showPwdCheckBox.Size = new System.Drawing.Size(115, 17);
            this.showPwdCheckBox.TabIndex = 2;
            this.showPwdCheckBox.Text = "Passwort anzeigen";
            this.showPwdCheckBox.UseVisualStyleBackColor = true;
            this.showPwdCheckBox.CheckedChanged += new System.EventHandler(this.showPwdCheckBox_CheckedChanged);
            // 
            // changePwdButton
            // 
            this.changePwdButton.Enabled = false;
            this.changePwdButton.Location = new System.Drawing.Point(3, 88);
            this.changePwdButton.Name = "changePwdButton";
            this.changePwdButton.Size = new System.Drawing.Size(75, 23);
            this.changePwdButton.TabIndex = 3;
            this.changePwdButton.Text = "Setzen";
            this.changePwdButton.UseVisualStyleBackColor = true;
            this.changePwdButton.Click += new System.EventHandler(this.changePwdButton_Click);
            // 
            // PasswordForm
            // 
            this.AcceptButton = this.changePwdButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 118);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PasswordForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Passwort";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox pwd1TextBox;
        private System.Windows.Forms.TextBox pwd2TextBox;
        private System.Windows.Forms.CheckBox showPwdCheckBox;
        private System.Windows.Forms.Button changePwdButton;
    }
}