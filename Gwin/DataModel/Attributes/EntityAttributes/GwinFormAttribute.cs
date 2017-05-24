using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Gwin.Attributes
{
    public class GwinFormAttribute : Attribute
    {
        /// <summary>
        /// Type of EntryForm
        /// </summary>
        public Type FormType { get; set; }
    }
}
