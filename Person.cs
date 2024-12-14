using System;
using System.Data;
using System.Data.SqlClient;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    /// <summary>
    /// Helper object used in MainForm.
    /// </summary>
    internal class Person
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsUser { get; set; }

        public int Value { get => GetId(); }
        public string Text { get => GetLastnameFirst(); }

        #region Constructors
        public Person(int id) 
        {
            Id = id;
            Load();
        }

        public Person(int id, string lastname, string firstname)
        {
            Id = id;
            UserName = "";
            LastName = lastname;
            FirstName = firstname;
            IsAdmin = false; 
            IsUser = false;
        }
        #endregion

        /// <summary>
        /// Value (e.g. for ListBox).
        /// </summary>
        /// <returns></returns>
        public int GetId()
        {
            return Id;
        }

        /// <summary>
        /// Text (e.g. for ListBox).
        /// </summary>
        /// <returns></returns>
        public string GetLastnameFirst()
        {
            return LastName + ", " + FirstName;
        }

        /// <summary>
        /// For general purposes.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }

        /// <summary>
        /// Loads the person from the database. Is called internally by Person(id).
        /// </summary>
        private void Load()
        {
            try
            {
                SqlConnection con1;

                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        com1.CommandText = @"SELECT * FROM Persons WHERE Id = " + Id.ToString();
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                UserName = reader1.GetString(1);
                                LastName = reader1.GetString(2);
                                FirstName = reader1.GetString(3);
                                if (reader1.GetByte(20) == 1)
                                {
                                    IsAdmin = true;
                                }
                                else
                                {
                                    IsAdmin = false;
                                }
                                if (reader1.GetByte(19) == 1)
                                {
                                    IsUser = true;
                                }
                                else
                                {
                                    IsUser = false;
                                }
                            }
                            reader1.Close();
                        }
                    }
                    con1.Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Person.cs: Fehler beim Laden der Person");
            }
        }
    }
}
