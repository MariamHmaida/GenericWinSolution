using System;
using System.Runtime.CompilerServices;

namespace App.Gwin.Attributes
{
    public class FilterAttribute : Attribute
    {

        /// <summary>
        /// Check if the value is empty in filtre
        /// </summary>
        public bool isDefaultIsEmpty { get; set; }

        /// <summary>
        /// Filed order in filter
        /// </summary>
        public int Ordre { get; set; }

        /// <summary>
        /// Control width in filter
        /// </summary>
        public int WidthControl { set; get; }


        public FilterAttribute([CallerMemberName] string propertyName = null)
        {
            
        }
    }
}