using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024.Admin
{
    public partial class ClientForm : Form
    {
        public int EditId { get; set; }
        public bool Edit { get; set; }

        public ClientForm()
        {
            InitializeComponent();
            ipv4TextBox.ValidatingType = typeof(System.Net.IPAddress);
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            if (Edit)
            {
                addButton.Text = "Bearbeiten";
                this.Text = "Clients bearbeiten";
                load();
            }
            else
            {
                addButton.Enabled = false;
                addButton.Text = "Hinzufügen";
                this.Text = "Clients hinzufügen";
            }
        }

        private void load()
        {
            try
            {
                if (EditId >= 0)
                {
                    SqlConnection con1;

                    using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        con1.Open();
                        using (SqlCommand com1 = new SqlCommand())
                        {
                            com1.CommandText = @"SELECT * FROM Clients WHERE Id = " + EditId.ToString();
                            com1.CommandType = CommandType.Text;
                            com1.Connection = con1;
                            using (SqlDataReader reader1 = com1.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    nameTextBox.Text = reader1.GetString(1);
                                    ipv4TextBox.Text = reader1.GetString(2);
                                    ipv6TextBox.Text = reader1.GetString(4);
                                    lanCheckBox.Checked = GetBoolFromTinyInt(reader1.GetString(3));
                                    restrictUserCheckBox.Checked = GetBoolFromTinyInt(reader1.GetString(4));
                                    usersRichTextBox.Text = reader1.GetString(5);
                                }
                                reader1.Close();
                            }
                        }
                        con1.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Laden des Clients");
            }
        }

        private void ipv4TextBox_Leave(object sender, EventArgs e)
        {
            
        }

        private void restrictUserCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (restrictUserCheckBox.Checked)
            {
                usersRichTextBox.Enabled = true;
            }
            else
            {
                usersRichTextBox.Enabled = false;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            SqlConnection myConnection;
            if (!Edit)
            {
                try
                {
                    using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        myConnection.Open();
                        using (SqlCommand myCommand = new SqlCommand())
                        {
                            int id = NextId("Clients");
                            string sqlquery = "INSERT INTO Clients " +
                            "(Id, ClientName, IPv4, IPv6, LAN, RestrictUser, AllowedUser, [User], Created, LastChanged) " +
                            "VALUES (@id, @name, @ip4, @ip6, @lan, @restrict, @allowed, @user, @created, @lastchanged)";
                            myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            myCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = nameTextBox.Text;
                            myCommand.Parameters.Add("@ip4", SqlDbType.NChar).Value = ipv4TextBox.Text;
                            myCommand.Parameters.Add("@ip6", SqlDbType.NVarChar).Value = ipv6TextBox.Text;
                            myCommand.Parameters.Add("@lan", SqlDbType.TinyInt).Value = lanCheckBox.Checked;
                            myCommand.Parameters.Add("@restrict", SqlDbType.TinyInt).Value = restrictUserCheckBox.Checked;
                            myCommand.Parameters.Add("@allowed", SqlDbType.NVarChar).Value = usersRichTextBox.Text;
                            myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                            myCommand.Parameters.Add("@created", SqlDbType.DateTime).Value = DateTime.Now;
                            myCommand.Parameters.Add("@lastchanged", SqlDbType.DateTime).Value = DateTime.Now;

                            myCommand.CommandText = sqlquery;
                            myCommand.CommandType = CommandType.Text;
                            myCommand.Connection = myConnection;
                            myCommand.ExecuteNonQuery();
                        }
                        myConnection.Close();
                    }
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, "Fehler beim Speichern des Clients");
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                var sql = @"UPDATE Clients SET ClientName = @name, IPv4 = @ip4, IPv6 = @ip6, LAN = @lan, " +
                        "RestrictUser = @restrict, AllowedUser = @allowed, [User] = @user, LastChanged = @lastchanged " +
                        "WHERE Id = " + EditId.ToString();
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                    {
                        using (var myCommand = new SqlCommand(sql, connection))
                        {
                            myCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = nameTextBox.Text;
                            myCommand.Parameters.Add("@ip4", SqlDbType.NChar).Value = ipv4TextBox.Text;
                            myCommand.Parameters.Add("@ip6", SqlDbType.NVarChar).Value = ipv6TextBox.Text;
                            myCommand.Parameters.Add("@lan", SqlDbType.TinyInt).Value = lanCheckBox.Checked;
                            myCommand.Parameters.Add("@restrict", SqlDbType.TinyInt).Value = restrictUserCheckBox.Checked;
                            myCommand.Parameters.Add("@allowed", SqlDbType.NVarChar).Value = usersRichTextBox.Text;
                            myCommand.Parameters.Add("@user", SqlDbType.Int).Value = Properties.Settings.Default.CurrentUserID;
                            myCommand.Parameters.Add("@lastchanged", SqlDbType.DateTime).Value = DateTime.Now;

                            connection.Open();
                            myCommand.ExecuteNonQuery();
                            connection.Close();
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message, "Fehler beim Update des Clients");
                    this.DialogResult = DialogResult.Abort;
                }
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
