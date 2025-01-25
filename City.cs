using System;
using System.Data;
using System.Data.SqlClient;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    internal class City : Location
    {
        #region Properties
        //public int Id { get; set; }
        //public string Country { get; set; }
        //public string Name { get; set; }
        public string Bundesland { get; set; }
        public string Kfz { get; set; }
        public string Prefix { get; set; }
        //public int Height { get; set; }
        //public string Image { get; set; }
        //public string Gps { get; set; }
        //public bool NotShown { get; set; }

        //public int Value { get => GetId(); }
        //public string Text { get => ToString(); }
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
        /// Value (e.g. for ListBox).
        /// </summary>
        /// <returns></returns>
        /*public int GetId()
        {
            return Id;
        }*/

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
                                Kfz = reader.GetString(6);
                                Prefix = reader.GetString(4);
                                Height = reader.GetInt32(7);
                                Image = reader.GetString(9);
                                Gps = reader.GetString(10);
                                NotShown = GetBoolFromTinyInt(reader.GetString(11));
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
