using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BikeDB2024
{
    internal class Tour : BikeDbObject
    {
        public DateTime TourDate { get; set; }
        public int Route { get; set; }
        public string RouteName { get; set; }
        public Vehicle MyVehicle { get; set; }
        public decimal Km { get; set; }
        public DateTime MyTime { get; set; }
        public decimal Vavg { get; set; }
        public decimal Vmax { get; set; }
        public int AccHeight { get; set; }
        public int MaxHeight { get; set; }
        public string Remark { get; set; }
        public int ImageGallery { get; set; }
        public string Persons { get; set; }

        public Tour (int id)
        {
            Id = id;
            table = "Tour";
            load();
        }

        /// <summary>
        /// For general purposes.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return TourDate.ToString("dd. MMM yyyy") + ": " + RouteName;
        }

        private void load()
        {

        }
    }
}
