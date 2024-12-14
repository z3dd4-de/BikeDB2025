using System;
using System.Data;
using System.Data.SqlClient;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    internal class Route
    {
        #region Properties
        public int Id { get; set; }
        public string City { get; set; }
        public string CityStart { get; set; }
        public string CityEnd { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool NotShown { get; set; }

        public int Value { get => GetId(); }
        public string Text { get => ToString(); }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        public Route(int id)
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
        /// Loads the route from the database. Is called internally by Route(id).
        /// </summary>
        private void load()
        {
            Type = GetDatabaseEntry("RouteTypes", "RouteType", Convert.ToInt32(
                GetDatabaseEntry("Routes", "RouteType", Id)));
            
            SqlConnection con1, myConnection;
            string sqlquery = @"SELECT * FROM Routes WHERE Id = @id";
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@id";
            
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
                                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                                {
                                    Name = reader.GetString(1);
                                    if (reader.GetByte(12) == 1) NotShown = true;
                                    else NotShown = false;

                                    City = "";
                                    CityStart = "";
                                    CityEnd = "";
                                    int int_city1 = -1;
                                    int int_city2 = -1;
                                    int int_city3 = -1;

                                    if (reader[2].ToString() != String.Empty)
                                    {
                                        int_city1 = Convert.ToInt32(reader[2]);
                                    }
                                    if (reader[3].ToString() != String.Empty)
                                    {
                                        int_city2 = Convert.ToInt32(reader[3]);
                                    }
                                    if (reader[4].ToString() != String.Empty)
                                    {
                                        int_city3 = Convert.ToInt32(reader[4]);
                                    }
                                    con1.Open();
                                    using (SqlCommand com1 = new SqlCommand())
                                    {
                                        com1.CommandText = @"SELECT Id, CityName FROM Cities";
                                        com1.CommandType = CommandType.Text;
                                        com1.Connection = con1;
                                        using (SqlDataReader reader1 = com1.ExecuteReader())
                                        {
                                            while (reader1.Read())
                                            {
                                                if (reader1[0].ToString() == int_city1.ToString())
                                                    City = reader1[1].ToString();
                                                if (reader1[0].ToString() == int_city2.ToString())
                                                    CityStart = reader1[1].ToString();
                                                if (reader1[0].ToString() == int_city3.ToString())
                                                    CityEnd = reader1[1].ToString();
                                            }
                                            reader1.Close();
                                        }
                                    }
                                    con1.Close();
                                }
                            }
                        }
                    }
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Helpers.ShowErrorMessage(ex.Message, "Fehler in Route.cs");
            }
        }
    }
}
