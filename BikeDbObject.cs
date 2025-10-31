using System;
using System.Data;
using System.Data.SqlClient;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    internal class BikeDbObject
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public bool NotShown { get; set; }

        public int Value { get => GetId(); }
        public string Text { get => ToString(); }
        #endregion

        internal string table;
        internal string sqlquery;
        //internal SqlConnection sqlconnection;
        //internal SqlCommand sqlcommand;

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
    }
}
