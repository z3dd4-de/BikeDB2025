using System;
using System.Data;
using System.Data.SqlClient;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    internal class CostCategory
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public VehicleType Mode { get; set; }

        public int Value { get => GetId(); }
        public string Text { get => ToString(); }
        public string VehicleMode { get => GetMode(); }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        public CostCategory(int id)
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
        /// Value (e.g. for ListBox).
        /// </summary>
        /// <returns></returns>
        public string GetMode()
        {
            string mode = "";
            switch (Mode)
            {
                default:
                case VehicleType.BIKE:
                    mode = "Fahrrad";
                    break;
                case VehicleType.FOSSIL:
                    mode = "Verbrenner";
                    break;
                case VehicleType.ELECTRIC:
                    mode = "Elektrofahrzeug";
                    break;
            }
            return mode;
        }

        /// <summary>
        /// Loads the category from the database. Is called internally by CostCategory(id).
        /// </summary>
        private void load()
        {
            Name = GetDatabaseEntry("CostCategories", "CategoryName", Id);

            try
            {
                SqlConnection con1;

                using (con1 = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    con1.Open();
                    using (SqlCommand com1 = new SqlCommand())
                    {
                        com1.CommandText = @"SELECT ElectricVehicles, Engines FROM CostCategories WHERE Id = " + Id.ToString();
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
                                    Mode = VehicleType.ELECTRIC;
                                }
                                if (!reader1.IsDBNull(1))
                                {
                                    fossil = true;
                                    Mode = VehicleType.FOSSIL;
                                }
                            }
                            reader1.Close();
                            if (!electric && !fossil) { Mode = VehicleType.BIKE; }
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
