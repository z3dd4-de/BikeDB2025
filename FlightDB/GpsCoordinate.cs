// Source: https://www.csharphelper.com/howtos/howto_great_circle_distance.html
using System;
using System.Linq;

namespace BikeDB2024.FlightDB
{
    internal class GpsCoordinate
    {
        public string Coordinate { get; set; }
        public string Latitude;
        public string Longitude;
        public double LatitudeValue;
        public double LongitudeValue;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="coord"></param>
        public GpsCoordinate(string coord) 
        { 
            Coordinate = coord;
            getParts();
        }

        /// <summary>
        /// Extract latitude and longitude from GPS Coordinate.
        /// </summary>
        private void getParts()
        {
            if (Coordinate != String.Empty)
            {
                if (Coordinate.Contains(","))
                {
                    string[] tmp = Coordinate.Split(',');
                    if (tmp.Length == 2)
                    {
                        Latitude = tmp[0];
                        LatitudeValue = ParseLatLon(tmp[0]);
                        Longitude = tmp[1];
                        LongitudeValue = ParseLatLon(tmp[1]);
                    }
                }
            }
        }

        private const string Deg = "°";

        /// <summary>
        /// Parse a latitude or longitude.
        /// Allowed values:
        /// 1° 14' N
        /// 1 14' 0" N   
        /// 1° 14 0N
        /// 1 14 0 N
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public double ParseLatLon(string str)
        {
            str = str.Replace("′", "'").Replace("″", "\"");
            str = str.ToUpper().Replace(Deg, " ").Replace("'", " ").Replace("\"", " ");
            str = str.Replace("S", " S").Replace("N", " N");
            str = str.Replace("E", " E").Replace("W", " W");
            str = str.Replace("O", " O");
            char[] separators = { ' ' };
            string[] fields = str.Split(separators,
                StringSplitOptions.RemoveEmptyEntries);

            double result =             // Degrees.
                double.Parse(fields[0]);
            if (fields.Length > 2)      // Minutes.
                result += double.Parse(fields[1]) / 60;
            if (fields.Length > 3)      // Seconds.
                result += double.Parse(fields[2]) / 3600;
            if (str.Contains('S') || str.Contains('W')) result *= -1;
            return result;
        }

        /// <summary>
        /// Convert degrees to radians.
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        /// <summary>
        /// Calculate the great circle distance between two points.
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        public double GreatCircleDistance(
            double lat1, double lon1, double lat2, double lon2)
        {
            const double radius = 6371; // Radius of the Earth in km.
            lat1 = DegreesToRadians(lat1);
            lon1 = DegreesToRadians(lon1);
            lat2 = DegreesToRadians(lat2);
            lon2 = DegreesToRadians(lon2);
            double d_lat = lat2 - lat1;
            double d_lon = lon2 - lon1;
            double h = Math.Sin(d_lat / 2) * Math.Sin(d_lat / 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Sin(d_lon / 2) * Math.Sin(d_lon / 2);
            return 2 * radius * Math.Asin(Math.Sqrt(h));
        }
    }
}
