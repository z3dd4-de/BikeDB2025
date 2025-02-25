using System;
using System.Data;
using System.Data.SqlClient;
using static BikeDB2024.Helpers;

namespace BikeDB2024.FlightDB
{
    internal class Airport : Location
    {
        #region Properties
        public string City { get; set; }
        public string Icao { get; set; }
        public string Iata { get; set; }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        public Airport(int id)
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
        /// Loads the city from the database. Is called internally by Airport(id).
        /// </summary>
        private void load() 
        {
            Country = GetDatabaseEntry("Countries", "Country", Convert.ToInt32(
                GetDatabaseEntry("Cities", "Country", Id)));
            City = GetDatabaseEntry("Cities", "CityName", Convert.ToInt32(
                GetDatabaseEntry("Airports", "City", Id)));
            NotShown = false;

            SqlConnection myConnection;
            string sqlquery = @"SELECT * FROM Airports WHERE Id = @id";

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
                                Icao = reader[4].ToString() != "" ? reader.GetString(4) : "";
                                Iata = reader[5].ToString() != "" ? reader.GetString(5) : "";
                                Gps = reader[6].ToString() != "" ? reader.GetString(6) : "";
                                Height = reader[7].ToString() != "" ? reader.GetInt32(7) : -1;
                                Image = reader[10].ToString() != "" ? reader.GetString(10) : "";
                                NotShown = GetBoolFromTinyInt(reader.GetByte(11));
                            }
                        }
                    }
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Helpers.ShowErrorMessage(ex.Message, "Fehler in Airport.cs");
            }
        }
    }
}
