using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Gwin.Entities;
using App.Shared.AttributesManager;

namespace App.Gwin.Components.Manager.Fields.Traitements.Params
{
    public class WriteEntity_To_EntryForm_Param : BaseFieldTraitementParam
    {
        public Control FromContainer { get;  set; }
        public Dictionary<string, object> CritereRechercheFiltre { get;  set; }
        /// <summary>
        /// Action : Update or Add
        /// </summary>
        public BaseEntryForm.EntityActions EntityAction { get;  set; }
    }
}
