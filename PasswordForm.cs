using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace BikeDB2024
{
    public partial class PasswordForm : Form
    {
        private int min_pwd_length = 5;
        public string Password { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PasswordForm()
        {
            InitializeComponent();
            min_pwd_length = Properties.Settings.Default.MinPasswordLength;
        }

        /// <summary>
        /// Show password or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showPwdCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (showPwdCheckBox.Checked)
            {
                pwd1TextBox.UseSystemPasswordChar = false;
                pwd2TextBox.Enabled = false;
            }
            else
            {
                pwd1TextBox.UseSystemPasswordChar = true;
                pwd2TextBox.Enabled = true;
            }
        }

        /// <summary>
        /// Close the form and return new password hash.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changePwdButton_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[pwd1TextBox.Text.Length];
            byte[] result;
            for (int i = 0; i < pwd1TextBox.Text.Length; i++)
            {
                data.SetValue(Convert.ToByte(pwd1TextBox.Text[i]), i);
            }
            using (SHA384 sha = SHA384.Create())
            {
                result = sha.ComputeHash(data);
            }
            string tmp = "";
            for (int i = 0; i < result.Length; i++)
            {
                tmp += result[i].ToString();
            }
            Password = tmp;
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Enable password change button. Minimum length is 5.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pwd1TextBox_TextChanged(object sender, EventArgs e)
        {
            checkPwd();
        }

        private void checkPwd()
        {
            if (pwd1TextBox.Text.Length >= min_pwd_length)
            {
                if (showPwdCheckBox.Checked)
                {
                    changePwdButton.Enabled = true;
                }
                else if (pwd1TextBox.Text == pwd2TextBox.Text)
                {
                    changePwdButton.Enabled = true;
                }
            }
            else if (!showPwdCheckBox.Checked && pwd1TextBox.Text != pwd2TextBox.Text)
            {
                changePwdButton.Enabled = false;
            }
            else changePwdButton.Enabled = false;
        }

        private void pwd2TextBox_TextChanged(object sender, EventArgs e)
        {
            checkPwd();
        }
    }
}
