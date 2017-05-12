using App.Gwin.Attributes;
using App.Shared.AttributesManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Gwin.Entities;
using App.Gwin.Application.BAL;
using System.ComponentModel;

namespace App.Gwin.Components.Manager.Fields.Traitements.Params
{
    /// <summary>
    /// Params of CreateField Traitement in EntryForm
    /// </summary>
    public class CreateFieldParams : BaseFieldTraitementParam
    {
        public Control ConteneurFormulaire { get; set; }

        public delegate void ControlPropriete_ValueChanged(object sender1, EventArgs e1);

        public  CancelEventHandler FunctionValidation;
       

        public Point Location { get; set; }
        public Orientation OrientationField { get; set; }
        public PropertyInfo PropertyInfo { set; get; }
        public Size SizeControl { get; set; }
        public Size SizeLabel { get; set; }
        public int TabIndex { get; set; }
        /// <summary>
        /// used per ManyToOne Field
        /// </summary>
        

        /// <summary>
        /// used per ManyToMany Field
        /// </summary>
        public TabControl TabControlForm { get;  set; }

        /// <summary>
        /// ErrorProvider instance
        /// </summary>
        public ErrorProvider errorProvider { get; set; }
    }
}
