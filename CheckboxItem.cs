using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static BikeDB2024.Helpers;

namespace BikeDB2024
{
    /// <summary>
    /// Class to control the visibility of several items in ComboBoxes.
    /// Usage:
    /// Create an item, e.g.: 
    /// CheckboxItem item = new CheckboxItem(VisibilityObject.COUNTRY, countryToolStripSplitButton, checked_image, not_checked_image);
    /// One item per table is sufficient. Change Id for each country:
    /// item.Id = current_country;
    /// item.CheckVisibility();
    /// If one of the DropDownItems is clicked:
    /// item.DropDownItemClick(bool);
    /// If the SplitButton is clicked:
    /// item.ReverseVisibility();
    /// </summary>
    internal class CheckboxItem
    {
        #region Class variables and properties
        public int Id { get; set; }
        public bool Visibility { get; set; }
        public VisibilityObject ObjectType { get; set; }
        private string table;
        private Bitmap checked_img;
        private Bitmap not_checked_img;
        public ToolStripSplitButton SplitButton { get; set; }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="but"></param>
        /// <param name="ckd"></param>
        /// <param name="not_ckd"></param>
        public CheckboxItem(VisibilityObject type, ToolStripSplitButton but, Bitmap ckd, Bitmap not_ckd)
        {
            SplitButton = but;
            checked_img = ckd;
            not_checked_img = not_ckd;
            ObjectType = type;
            SetTable(type);
        }

        /// <summary>
        /// Check the current visibility of an item (database entry).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckVisibility(int id)
        {
            bool ret = false;
            string sshown = GetDatabaseEntry(table, "NotShown", id, true);
            if (sshown == "0")
            {
                ret = true;
            }
            SetImage(ret);
            Visibility = ret;
            return ret;
        }

        /// <summary>
        /// Check the current visibility of an item (database entry). Id already set.
        /// </summary>
        /// <returns></returns>
        public bool CheckVisibility()
        {
            return CheckVisibility(Id);
        }

        /// <summary>
        /// Set the database table according to the associated VisibilityObject.
        /// </summary>
        /// <param name="obj"></param>
        public void SetTable(VisibilityObject obj)
        {
            switch (obj)
            {
                case VisibilityObject.AIRLINE:
                    table = "Airlines";
                    break;
                case VisibilityObject.AIRPORT:
                    table = "Airport";
                    break;
                case VisibilityObject.CITY:
                    table = "Cities";
                    break;
                case VisibilityObject.COMPANY:
                    table = "Companies";
                    break;
                case VisibilityObject.COUNTRY:
                    table = "Countries";
                    break;
                case VisibilityObject.PERSON:
                    table = "Persons";
                    break;
                case VisibilityObject.PLANEMANUFACTURER:
                    table = "PlaneManufacturers";
                    break;
                case VisibilityObject.PLANE:
                    table = "Planes";
                    break;
                case VisibilityObject.ROUTE:
                    table = "Routes";
                    break;
                case VisibilityObject.VEHICLE:
                    table = "Vehicles";
                    break;
                default:
                    table = "";
                    break;
            }
        }

        /// <summary>
        /// Set the image of the SplitButton.
        /// </summary>
        /// <param name="check"></param>
        public void SetImage(bool check)
        {
            if (check)
            {
                SplitButton.Image = checked_img;
            }
            else
            {
                SplitButton.Image = not_checked_img;
            }
        }

        /// <summary>
        /// Update the database entry to the current visibility.
        /// </summary>
        public void Update()
        {
            string sql = $"UPDATE @table SET NotShown = @visibility WHERE Id = @id";
            try
            {
                using (var connection = new SqlConnection(Properties.Settings.Default.DataConnectionString))
                {
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = Id;
                        command.Parameters.Add("@table", SqlDbType.NVarChar).Value = table;
                        command.Parameters.Add("@visibility", SqlDbType.TinyInt).Value = GetTinyIntFromBool(Visibility);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, "Fehler beim Update (Visibility CheckboxItem)");
            }
        }

        /// <summary>
        /// Change visibility to the oppisite state.
        /// </summary>
        public void ReverseVisibility()
        {
            Visibility = !Visibility;
            Update();
            SetImage(Visibility);
        }

        /// <summary>
        /// Click event on a dropdown item. Might not change the current state.
        /// </summary>
        /// <param name="check"></param>
        public void DropDownItemClick(bool check)
        {
            Visibility = check;
            SetImage(check);
            Update();
        }
    }
}
