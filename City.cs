using System;
using System.Data;
using System.Data.SqlClient;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    internal class City : Location
    {
        #region Properties
        public string Bundesland { get; set; }
        public string Kfz { get; set; }
        public string Prefix { get; set; }
        public string Code { get; set; }    // Postleitzahl Varchar 20
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        public City(int id)
        {
            Id = id;
            load();
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
        /// Loads the city from the database. Is called internally by City(id).
        /// </summary>
        private void load()
        {
            Country = GetDatabaseEntry("Countries", "Country", Convert.ToInt32(
                GetDatabaseEntry("Cities", "Country", Id)));
            Bundesland = GetDatabaseEntry("Bundeslaender", "Bundesland", Convert.ToInt32(
                GetDatabaseEntry("Cities", "Bundesland", Id)));
            NotShown = false;

            SqlConnection myConnection;
            string sqlquery = @"SELECT * FROM Cities WHERE Id = @id";
            
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
                                Code = reader[4].ToString() != "" ? reader.GetString(4) : "";
                                Prefix = reader[5].ToString() != "" ? reader.GetString(5) : "";
                                Kfz = reader[7].ToString() != "" ? reader.GetString(7) : "";
                                Height = reader[8].ToString() != "" ? reader.GetInt32(8) : -1; 
                                Image = reader[10].ToString() != "" ? reader.GetString(10) : "";
                                Gps = reader[11].ToString() != "" ? reader.GetString(11) : "";
                                NotShown = GetBoolFromTinyInt(reader.GetByte(12));
                            }
                        }
                    }
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Helpers.ShowErrorMessage(ex.Message, "Fehler in City.cs");
            }
        }
    }
}
