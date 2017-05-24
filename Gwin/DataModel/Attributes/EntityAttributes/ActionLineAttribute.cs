using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Gwin.Attributes
{
    /// <summary>
    /// Can do Action for Seletected Entity
    /// </summary>
    public class DataGridSelectedActionAttribute : Attribute
    {
        /// <summary>
        /// Type of Form to Show that contain Traitement
        /// Type must extend Interface : IFormSelectedEntityAction
        /// Type must be unique ,Entity must not have multiple Action fom the same Form
        /// </summary>
        public Type TypeOfForm {
            set; 
            get;
        }

        /// <summary>
        /// Title to show in DataGrid Line
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// Description to show as tooltip
        /// </summary>
        public string Description { set; get; }
    }
}
