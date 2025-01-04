using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static BikeDB2024.Helpers;
using System.Data;

namespace BikeDB2024.FlightDB
{
    internal class Airline
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }
        public bool NotShown { get; set; }

        public int Value { get => GetId(); }
        public string Text { get => ToString(); }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        public Airline(int id)
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
        /// Loads the city from the database. Is called internally by Airline(id).
        /// </summary>
        private void load()
        {
            Country = GetDatabaseEntry("Countries", "Country", Convert.ToInt32(
                GetDatabaseEntry("Airlines", "Country", Id)));
            NotShown = false;

            SqlConnection myConnection;
            string sqlquery = @"SELECT * FROM Airlines WHERE Id = @id";

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
                                Image = reader.GetString(4);
                                NotShown = GetBoolFromTinyInt(reader.GetString(5));
                            }
                        }
                    }
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Helpers.ShowErrorMessage(ex.Message, "Fehler in Airline.cs");
            }
        }
    }
}
