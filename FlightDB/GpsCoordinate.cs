// Source: https://www.csharphelper.com/howtos/howto_great_circle_distance.html
using System;
using System.Linq;
using static BikeDB2024.Helpers;
using GMap.NET;
using static BikeDB2024.SexagesimalAngle;

namespace BikeDB2024.FlightDB
{
    internal class GpsCoordinate
    {
        #region Variables.
        public string Coordinate;
        public string Latitude;
        public string Longitude;
        public double LatitudeValue;
        public double LongitudeValue;
        public CoordinateType Type;
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="coord"></param>
        public GpsCoordinate(string coord) 
        { 
            Coordinate = coord;
            TestCoordinateType(coord);
            if (Type == CoordinateType.DECIMAL || Type == CoordinateType.DEGREE)
            {
                getParts();
            }
            else
            {
                Latitude = "";
                Longitude = "";
                LatitudeValue = 0;
                LongitudeValue = 0;
            }
        }

        /// <summary>
        /// Get a GMap.NET.PointLatLng-Coordinate.
        /// </summary>
        /// <returns>Position on a GMap.</returns>
        public PointLatLng GetMapPosition()
        {
            return new PointLatLng(LatitudeValue, LongitudeValue);
        }

        /// <summary>
        /// Get the coordinate in decimal representation.
        /// </summary>
        /// <returns></returns>
        public string GetDecimalCoordinate()
        {
            return LatitudeValue.ToString() + ", " + LongitudeValue.ToString();
        }

        /// <summary>
        /// Get the coordinate in degree representation.
        /// </summary>
        /// <returns></returns>
        public string GetDegreeCoordinate()
        {
            return Latitude + ", " + Longitude;
        }

        /// <summary>
        /// Check the type of a coordinate string (decimal or degree are valid strings).
        /// </summary>
        /// <param name="orig"></param>
        public void TestCoordinateType(string orig)
        {
            if (orig != String.Empty)
            {
                string[] parts = orig.Split(',');
                if (parts.Length != 2)
                {
                    Type = CoordinateType.INCOMPLETE;
                    return;
                }
                if (orig.Contains("°"))
                {
                    Type = CoordinateType.DEGREE;
                }
                else if (orig.Contains("."))
                {
                    Type = CoordinateType.DECIMAL;
                }
            }
            else
            {
                Type = CoordinateType.NULL;
            }
        }

        /// <summary>
        /// Extract latitude and longitude from GPS Coordinate. Both are stored as decimal and degree values.
        /// </summary>
        private void getParts()
        {
            string[] tmp = Coordinate.Split(',');
            if (Type == CoordinateType.DECIMAL)
            {
                Latitude = FromDouble(Convert.ToDouble(tmp[0])).ToString();
                LatitudeValue = Convert.ToDouble(tmp[0]);
                Longitude = FromDouble(Convert.ToDouble(tmp[1])).ToString();
                LongitudeValue = Convert.ToDouble(tmp[1]);
            }
            else if (Type == CoordinateType.DEGREE)
            {
                Latitude = tmp[0];
                LatitudeValue = ParseLatLon(tmp[0]);
                Longitude = tmp[1];
                LongitudeValue = ParseLatLon(tmp[1]);
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
