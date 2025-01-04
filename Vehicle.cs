using System;
using System.Data;
using System.Data.SqlClient;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    internal class Vehicle
    {
        #region Properties
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public VehicleType Vehiclemode { get; set; }
        public bool NotShown { get; set; }
        public int Value { get => GetId(); }
        public string Text { get => ToString(); }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        public Vehicle(int id)
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
            string tmp = string.Empty;
            if (Manufacturer == "") tmp += Name;
            else tmp += Manufacturer + " " + Name;
            return tmp;
        }

        /// <summary>
        /// Loads the vehicle from the database. Is called internally by Vehicle(id).
        /// </summary>
        private void load()
        {
            Manufacturer = GetDatabaseEntry("Companies", "CompanyName", Convert.ToInt32(
                                GetDatabaseEntry("Vehicles", "Manufacturer", Id)));
            if (Manufacturer == "Kein Hersteller")
            {
                Manufacturer = "";
            }
            Name = GetDatabaseEntry("Vehicles", "VehicleName", Id);
            int vt = Convert.ToInt32(GetDatabaseEntry("Vehicles", "VehicleType", Id));
            Type = GetDatabaseEntry("VehicleTypes", "VehicleType", vt);
            NotShown = GetBoolFromTinyInt(GetDatabaseEntry("Vehicles", "NotShown", Id));

            try
            {
                SqlConnection con1;
                
                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        com1.CommandText = @"SELECT Electric, Engine FROM VehicleTypes WHERE Id = " + vt.ToString();
                        com1.CommandType = CommandType.Text;
                        com1.Connection = con1;
                        using (SqlDataReader reader1 = com1.ExecuteReader())
                        {
                            bool electric = false;
                            bool fossil = false;
                            while (reader1.Read())
                            {
                                if (!reader1.IsDBNull(0))
                                {
                                    electric = true;
                                    Vehiclemode = VehicleType.ELECTRIC;
                                }
                                if (!reader1.IsDBNull(1))
                                {
                                    fossil = true;
                                    Vehiclemode = VehicleType.FOSSIL;
                                }
                            }
                            reader1.Close();
                            if (!electric && !fossil) { Vehiclemode = VehicleType.BIKE; }
                        }
                    }
                    con1.Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Laden des Fahrzeugtyps");
            }
        }
    }
}
