using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Gwin.Attributes
{
    [AttributeUsage(AttributeTargets.Property,  AllowMultiple = false)]
    public class DisplayPropertyAttribute : Attribute
    {
        public DisplayPropertyAttribute()
        {
            
        }


        #region Display

        /// <summary>
        /// Indicates whether the property name exists in the glossary
        /// </summary>
        public bool isInGlossary;


        /// <summary>
        /// Displayed name
        /// </summary>
        public string Title { set; get; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// DisplayMember in ComBoBox
        /// </summary>
        public string DisplayMember { get; set; }
        #endregion

       

      

      

       
    }
}
