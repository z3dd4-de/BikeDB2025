using System;
using System.Data;
using System.Data.SqlClient;
using static BikeDB2024.Helpers;

namespace BikeDB2024.FlightDB
{
    internal class Plane
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Registration { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public int Seats { get; set; }
        public string Image { get; set; }
        public bool NotShown { get; set; }

        public int Value { get => GetId(); }
        public string Text { get => ToString(); }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        public Plane(int id)
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
        /// Loads the city from the database. Is called internally by Plane(id).
        /// </summary>
        private void load()
        {
            Manufacturer = GetDatabaseEntry("PlaneManufacturers", "Name", Convert.ToInt32(
                GetDatabaseEntry("Planes", "Manufacturer", Id)));
            Category = GetDatabaseEntry("PlaneCategories", "Category", Convert.ToInt32(
                GetDatabaseEntry("Planes", "Category", Id)));
            NotShown = false;

            SqlConnection myConnection;
            string sqlquery = @"SELECT * FROM Planes WHERE Id = @id";

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
                                Name = reader.GetString(2);
                                Type = reader.GetString(3);
                                Registration = reader.GetString(4);
                                Seats = reader.GetInt32(6);
                                Image = reader.GetString(7);
                                NotShown = GetBoolFromTinyInt(reader.GetString(8));
                            }
                        }
                    }
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Helpers.ShowErrorMessage(ex.Message, "Fehler in Planes.cs");
            }
        }
    }
}
