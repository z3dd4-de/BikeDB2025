using System;
using System.Data;
using System.Data.SqlClient;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    internal class Country
    {
        #region Properties
        public int Id { get; set; }
        public string Iso3166 { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public bool NotShown { get; set; }
        public string Phone { get; set; }
        public int Value { get => GetId(); }
        public string Text { get => ToString(); }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        public Country(int id)
        {
            Id = id;
            load();
        }

        /// <summary>
        /// Value (e.g. for ListBox).
        /// </summary>
        /// <returns></returns>
        public int GetId()
        {
            return Id;
        }

        /// <summary>
        /// For general purposes.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Loads the Country from the database. Is called internally by Country(id).
        /// </summary>
        private void load()
        {
            Continent = GetDatabaseEntry("Continents", "Continent", Convert.ToInt32(
                GetDatabaseEntry("Countries", "Continent", Id)));

            SqlConnection myConnection;
            string sqlquery = @"SELECT * FROM Countries WHERE Id = @id";
            
            try
            {
                using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand())
                    {
                        myCommand.CommandText = sqlquery;
                        myCommand.CommandType = CommandType.Text;
                        myCommand.Connection = myConnection;
                        myCommand.Parameters.Add("@id", SqlDbType.Int).Value = Id;

                        using (SqlDataReader reader = myCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Name = reader.GetString(1);
                                Iso3166 = reader.GetString(2);
                                Phone = reader.GetString(3);
                                NotShown = GetBoolFromTinyInt(reader.GetString(5));
                            }
                        }
                    }
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Helpers.ShowErrorMessage(ex.Message, "Fehler in Country.cs");
            }
        }
    }
}
