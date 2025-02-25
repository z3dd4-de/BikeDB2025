namespace BikeDB2024
{
    internal class Location
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Height { get; set; }
        public string Image { get; set; }
        protected string Gps { get; set; }
        public bool NotShown { get; set; }

        public int Value { get => GetId(); }
        public string Text { get => ToString(); }
        public string GpsString { get => GetGps(); }
        #endregion

        /// <summary>
        /// Value (e.g. for ListBox).
        /// </summary>
        /// <returns></returns>
        protected int GetId()
        {
            return Id;
        }

        protected string GetGps()
        {
            return Gps;
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
