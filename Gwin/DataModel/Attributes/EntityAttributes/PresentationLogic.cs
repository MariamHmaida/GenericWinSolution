using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Gwin.Attributes
{
    public class PresentationLogicAttribute : Attribute
    {
        /// <summary>
        /// Presentation logic object type
        /// </summary>
        public Type TypePLO { get; set; }
    }
}
