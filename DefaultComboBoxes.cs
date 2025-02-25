using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BikeDB2024.FlightDB;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    internal class DefaultComboBoxes
    {
        #region Properties
        public enum CB_Types { AIRLINES, AIRPORTS, CITIES, COUNTRIES, FLIGHTS, MANUFACTURERS, PERSONS, PLANES, PLANE_TYPES, ROUTES, TOUR, VEHICLES }
        public ComboBox DefaultComboBox { get; set; }
        public object Item { get; set; }
        public CB_Types Type { get; set; }
        public bool Sorted { get; set; }
        #endregion

        #region internal variables
        private string table;
        private string id_field;
        private bool useObject;
        private bool not_shown;
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="defaultComboBox">The ComboBox that should be used.</param>
        /// <param name="type">Type of the ComboBox, e.g. Country or City CB.</param>
        /// <param name="use_not_shown">Use database field NotShown, default: true.</param>
        public DefaultComboBoxes(ComboBox defaultComboBox, CB_Types type, bool use_not_shown = true)
        {
            DefaultComboBox = defaultComboBox;
            Type = type;
            useObject = true;
            Sorted = true;
            not_shown = use_not_shown;

            table = "";
            id_field = "Id";

            initType();
        }

        /// <summary>
        /// Initialisize the type of the ComboBox.
        /// </summary>
        private void initType()
        {
            switch (Type)
            {
                case CB_Types.AIRLINES:
                    table = "Airlines";
                    break;
                case CB_Types.AIRPORTS:
                    table = "Airports";
                    break;
                case CB_Types.CITIES:
                    table = "Cities";
                    break;
                case CB_Types.COUNTRIES:
                    table = "Countries";
                    break;
                case CB_Types.FLIGHTS:
                    table = "Flights";
                    break;
                case CB_Types.MANUFACTURERS:
                    table = "PlaneManufacturers";
                    break;
                case CB_Types.PERSONS:
                    table = "Persons";
                    break;
                case CB_Types.PLANES:
                    table = "Planes";
                    break;
                case CB_Types.PLANE_TYPES:      // the only type without a class object
                    table = "PlaneManufacturers";
                    id_field = "Types";
                    useObject = false;
                    break;
                case CB_Types.ROUTES:
                    table = "Routes";
                    break;
                case CB_Types.TOUR:
                    table = "Tour";
                    break;
                case CB_Types.VEHICLES:
                    table = "Vehicles";
                    break;
                default:
                    ShowErrorMessage("Typ kann nicht initialisiert werden!", "Fehler in DefaultComboBoxes.cs");
                    break;
            }
            LoadComboBoxItems(Type);
        }

        /// <summary>
        /// Used to store all ComboBox items in one string, separated by ";".
        /// </summary>
        /// <returns></returns>
        public string GetItemString()
        {
            string ret = "";
            foreach (string type in DefaultComboBox.Items)
            {
                if (type.Trim().Length > 0)
                    ret += type + ";";
            }
            return ret;
        }

        /// <summary>
        /// Convert a string into a string array.
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public string[] LoadItems(string types)
        {
            string[] tmp = types.Split(';');
            string[] ret = new string[tmp.Length];
            for (int i = 0; i < tmp.Length; i++)
            {
                ret[i] = tmp[i].Trim();
            }
            return ret;
        }

        /// <summary>
        /// Load a string array into a ComboBox.
        /// </summary>
        /// <param name="types"></param>
        public void LoadComboBoxItems(string[] types)
        {
            List<object> data = new List<object>(); 
            DefaultComboBox.DataSource = null;
            DefaultComboBox.Items.Clear();
            DefaultComboBox.DisplayMember = "Text";
            DefaultComboBox.ValueMember = "Text";
            foreach (string type in types)
            {
                data.Add(type);
            }
            DefaultComboBox.DataSource = data;
            DefaultComboBox.Sorted = Sorted;
        }

        /// <summary>
        /// Load items into the ComboBox.
        /// </summary>
        /// <param name="types"></param>
        public void LoadComboBoxItems(CB_Types types)
        {
            List<object> data = new List<object>();
            DefaultComboBox.DataSource = null;
            DefaultComboBox.Items.Clear();
            DefaultComboBox.DisplayMember = "Text";
            DefaultComboBox.ValueMember = "Value";

            SqlConnection myConnection;
            string sqlquery = "";
            try
            {
                using (myConnection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    myConnection.Open();
                    using (SqlCommand myCommand = new SqlCommand())
                    {
                        sqlquery = $"SELECT {id_field} FROM {table}";
                        if (not_shown)
                        {
                            sqlquery += " WHERE NotShown = 0";
                        }
                        myCommand.CommandText = sqlquery;
                        myCommand.CommandType = CommandType.Text;
                        myCommand.Connection = myConnection;
                        
                        using (SqlDataReader reader = myCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (useObject)
                                {
                                    switch (Type)
                                    {
                                        case CB_Types.AIRLINES:
                                            Item = new Airline(reader.GetInt32(0));
                                            break;
                                        case CB_Types.AIRPORTS:
                                            Item = new Airport(reader.GetInt32(0));
                                            break;
                                        case CB_Types.CITIES:
                                            Item = new City(reader.GetInt32(0));
                                            break;
                                        case CB_Types.COUNTRIES:
                                            Item = new Country(reader.GetInt32(0));
                                            break;
                                        case CB_Types.MANUFACTURERS:
                                            Item = new Manufacturer(reader.GetInt32(0));
                                            break;
                                        case CB_Types.PERSONS:
                                            Item = new Person(reader.GetInt32(0));
                                            break;
                                        case CB_Types.PLANES:
                                            Item = new Plane(reader.GetInt32(0));
                                            break;
                                        case CB_Types.ROUTES:
                                            Item = new Route(reader.GetInt32(0));
                                            break;
                                        case CB_Types.VEHICLES:
                                            Item = new Vehicle(reader.GetInt32(0));
                                            break;
                                        default:
                                            break;
                                    }
                                    data.Add(Item);
                                    //DefaultComboBox.Items.Add(Item);
                                }
                                else
                                {
                                    switch (Type)
                                    {
                                        case CB_Types.PLANE_TYPES:
                                            LoadComboBoxItems(LoadItems(reader.GetString(0)));
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    myConnection.Close();
                    DefaultComboBox.DataSource = data;
                    DefaultComboBox.Sorted = Sorted;
                }
            }
            catch (Exception ex)
            {
                Helpers.ShowErrorMessage(ex.Message, "Fehler in DefaultComboBoxes.cs");
            }
        }
    }
}
