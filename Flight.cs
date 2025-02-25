using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDB2024
{
    internal class Flight : BikeDbObject
    {
        public DateTime FlightDate { get; set; }
        public string FlightDateStr { get; set; }
        public string Airline { get; set; }
        public string Plane { get; set; }
        public string FlightNumber { get; set; }
        public string Takeoff { get; set; }
        public string Landing { get; set; }
        public string Seat { get; set; }
        public string Class { get; set; }

        public Flight(int id) 
        {
            Id = id;
            table = "Flights";
            load();
        }

        /// <summary>
        /// For general purposes.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetDate().ToString("dd. MMM yyyy") + ": " + Takeoff + " - " + Landing;
        }

        public DateTime GetDate()
        {
            DateTime dt = new DateTime();
            if (FlightDate != null)
            {
                dt = FlightDate;
            }
            else if (FlightDateStr != null)
            {
                int len = FlightDateStr.Length;
                bool hasPoint = FlightDateStr.Contains(".");
                if (len == 4 && !hasPoint)
                {
                    // year
                    dt = new DateTime(Convert.ToInt32(FlightDateStr), 7, 1);
                }
                if (hasPoint)
                {
                    string[] tmp = FlightDateStr.Split('.');
                    if (tmp.Length == 2)
                    {
                        if (tmp[1].Length == 4)
                        {
                            // (m)m.yyyy
                            dt = new DateTime(Convert.ToInt32(tmp[1]), Convert.ToInt32(tmp[0]), 15);
                        }
                        else if (tmp[0].Length == 4)
                        {
                            // yyyy.(m)m
                            dt = new DateTime(Convert.ToInt32(tmp[0]), Convert.ToInt32(tmp[1]), 15);
                        }
                    }
                }
            }
            else
            {
                dt = DateTime.Now;      // Fallback
            }
            return dt;
        }

        private void load()
        {

        }
    }
}
