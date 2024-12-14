using System;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    public partial class LoginForm : Form
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            loginButton.Enabled = false;
        }

        /// <summary>
        /// Check password and set user status (admin or normal user).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginButton_Click(object sender, EventArgs e)
        {
            string pwd = GetDatabaseEntry("Persons", "Password", "Username = '" + usernameTextBox.Text + "'");
            if (pwd == GetPasswordHash(passwordTextBox.Text))
            {
                if (adminCheckBox.Checked && GetDatabaseEntry("Persons", "IsAdmin", "Username = '" + usernameTextBox.Text + "'") == "1")
                {
                    // Admin logged in
                    SetLoginStatus(true, true);
                }
                else
                {
                    // User logged in
                    SetLoginStatus(true);
                }
                Properties.Settings.Default.CurrentUserName = usernameTextBox.Text;
                Properties.Settings.Default.CurrentUserID = Convert.ToInt32(GetDatabaseEntry("Persons", "Id", "Username = '" + usernameTextBox.Text + "'"));
                Properties.Settings.Default.UserLoggedIn = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                DialogResult = DialogResult.Cancel;
                SetLoginStatus(false);
                Close();
            }
        }

        #region Check Button
        private void usernameTextBox_TextChanged(object sender, EventArgs e)
        {
            checkButton();
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            checkButton();
        }

        /// <summary>
        /// Check if Button is enabled or not.
        /// </summary>
        private void checkButton()
        {
            if (usernameTextBox.Text.Length > 0 && passwordTextBox.Text.Length > 0)
            {
                loginButton.Enabled = true;
            }
            else
            {
                loginButton.Enabled = false;
            }
        }
        #endregion
    }
}
