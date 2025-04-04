using System;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json.Linq;

namespace BikeDB2024
{
    internal class Marker : IEquatable<Marker>, IComparable<Marker>
    {
        #region Variables
        public int Value { get; set; }
        public string MarkerType { get; set; }
        public GMarkerGoogleType Type { get; set; }
        #endregion

        #region Constructors
        public Marker(GMarkerGoogleType type) 
        { 
            Type = type;
            Value = Convert.ToInt32(type);
            MarkerType = getGMarkerTypeString(Type);
        }

        public Marker(int value) 
        {
            Type = (GMarkerGoogleType)value;
            Value = value;
            MarkerType = getGMarkerTypeString(Type);
        }
        #endregion

        public override string ToString() => MarkerType;

        /// <summary>
        /// Change the type by providing a new value.
        /// </summary>
        /// <param name="value"></param>
        public void ChangeType(int value)
        {
            Type = (GMarkerGoogleType)value;
            Value = value;
            MarkerType = getGMarkerTypeString(Type);
        }

        /// <summary>
        /// Change the type by providing a new GMarkerGoogleType.
        /// </summary>
        /// <param name="value"></param>
        public void ChangeType(GMarkerGoogleType type)
        {
            Type = type;
            Value = Convert.ToInt32(type);
            MarkerType = getGMarkerTypeString(Type);
        }

        /// <summary>
        /// Get the German string for a GMarkerGoogleType which can be used e.g. for Comboboxes (Marker.cs).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string getGMarkerTypeString(GMarkerGoogleType type)
        {

            switch (type)
            {
                case GMarkerGoogleType.none:
                    return "Kein";
                case GMarkerGoogleType.arrow:
                    return "Pfeil";
                case GMarkerGoogleType.blue:
                    return "Blau";
                case GMarkerGoogleType.blue_small:
                    return "Blau klein";
                case GMarkerGoogleType.blue_dot:
                    return "Blauer Punkt";
                case GMarkerGoogleType.blue_pushpin:
                    return "Blaue Makiernadel";
                case GMarkerGoogleType.brown_small:
                    return "Braun klein";
                case GMarkerGoogleType.gray_small:
                    return "Grau klein";
                case GMarkerGoogleType.green:
                    return "Grün";
                case GMarkerGoogleType.green_small:
                    return "Grün klein";
                case GMarkerGoogleType.green_dot:
                    return "Grüner Punkt";
                case GMarkerGoogleType.green_pushpin:
                    return "Grüne Makiernadel";
                case GMarkerGoogleType.green_big_go:
                    return "Grün Big_go";       ///
                case GMarkerGoogleType.yellow:
                    return "Gelb";
                case GMarkerGoogleType.yellow_small:
                    return "Gelb klein";
                case GMarkerGoogleType.yellow_dot:
                    return "Gelber Punkt";
                case GMarkerGoogleType.yellow_big_pause:
                    return "Gelbe Große Pause";       ///
                case GMarkerGoogleType.yellow_pushpin:
                    return "Gelbe Makiernadel";
                case GMarkerGoogleType.lightblue:
                    return "Hellblau";
                case GMarkerGoogleType.lightblue_dot:
                    return "Hellblauer Punkt";
                case GMarkerGoogleType.lightblue_pushpin:
                    return "Hellblaue Makiernadel";
                case GMarkerGoogleType.orange:
                    return "Orange";
                case GMarkerGoogleType.orange_small:
                    return "Orange klein";
                case GMarkerGoogleType.orange_dot:
                    return "Orangener Punkt";
                case GMarkerGoogleType.pink:
                    return "Rosa";
                case GMarkerGoogleType.pink_dot:
                    return "Rosa Punkt";
                case GMarkerGoogleType.pink_pushpin:
                    return "Rosa Makiernadel";
                case GMarkerGoogleType.purple:
                    return "Lila";
                case GMarkerGoogleType.purple_small:
                    return "Lila klein";
                case GMarkerGoogleType.purple_dot:
                    return "Lila Punkt";
                case GMarkerGoogleType.purple_pushpin:
                    return "Lila Makiernadel";
                case GMarkerGoogleType.red:
                    return "Rot Punkt";
                case GMarkerGoogleType.red_small:
                    return "Rot klein";
                case GMarkerGoogleType.red_dot:
                    return "Roter Punkt";
                case GMarkerGoogleType.red_pushpin:
                    return "Rote Makiernadel";
                case GMarkerGoogleType.red_big_stop:
                    return "Roter Großer Stop";       ///
                case GMarkerGoogleType.black_small:
                    return "Schwarz klein";
                case GMarkerGoogleType.white_small:
                    return "Weiß klein";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Make the class objects sortable.
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        /// <returns></returns>
        public int SortByNameAscending(string name1, string name2)
        {
            return name1.CompareTo(name2);
        }

        /// <summary>
        /// Make the class objects combarable.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Marker other)
        {
            // A null value means that this object is greater.
            if (other == null)
                return 1;

            else
                return this.MarkerType.CompareTo(other.MarkerType);
        }

        /// <summary>
        /// Make the class objects combarable.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Marker other)
        {
            if (other == null) return false;
            return (this.MarkerType.Equals(other.MarkerType));
        }
    }
}
