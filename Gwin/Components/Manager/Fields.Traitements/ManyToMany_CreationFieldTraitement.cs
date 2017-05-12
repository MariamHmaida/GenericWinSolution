using App.Gwin.Entities;
using App.Gwin.Exceptions.Gwin;
using App.Gwin.Fields;
using App.Gwin.Components.Manager.Fields.Traitements.Params;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Shared.AttributesManager;
using System.Reflection;
using App.Gwin.Entities.Resources.Glossary;

namespace App.Gwin.FieldsTraitements
{
    public class ManyToMany_CreationFieldTraitement : BaseFieldTraitement, IFieldTraitements
    {

        public object GetTestValue(PropertyInfo propertyInfo)
        {
            return null;
        }

        /// <summary>
        /// CreateField in EntryForm
        /// 
        /// </summary>
        /// <param name="param">
        /// param.PropertyInfo
        /// param.Location 
        /// param.OrientationField 
        /// param.SizeLabel 
        /// param.SizeControl 
        /// param.ConfigProperty 
        /// param.TabIndex 
        /// param.Service 
        /// param.ConfigEntity
        /// param.TabControlForm
        /// param.Entity
        /// param.ConteneurFormulaire
        /// </param>
        /// <returns>the created field</returns>
        public BaseField CreateField_In_EntryForm(CreateFieldParams param)
        {
            throw new GwinException("Field Many to Many Creation in EntryForm not yet implemented in Gwin Application");
        }

        public BaseField CreateField_In_Filter(CreateField_In_Filter_Params param)
        {
            throw new GwinException("Field Many to Many Creation in Filter not yet implemented in Gwin Application");
        }

        public object GetFieldValue_From_Filter(Control FilterContainer, ConfigProperty ConfigProperty)
        {
            throw new GwinException("Field Many to Many Creation in Filter not yet implemented in Gwin Application");
        }

        public void ShowEntity_To_EntryForm(WriteEntity_To_EntryForm_Param param)
        {
            throw new GwinException("Field Many to Many Creation in EntryForm not yet implemented in Gwin Application");
        }

        #region EntityDataGrid
        /// <summary>
        /// Create Field Colomn in Entity DataGrid
        /// </summary>
        /// <param name="param"></param>
        public void ConfigFieldColumn_In_EntityDataGrid(CreateFieldColumns_In_EntityDataGrid param)
        {
            DataGridViewButtonColumn ButtonColumn = new DataGridViewButtonColumn();
            ButtonColumn.UseColumnTextForButtonValue = true;
            // [Localize]
            ButtonColumn.Text = Glossary.Update + " : " + param.ConfigProperty.DisplayProperty.Titre;
            param.Column = ButtonColumn;

            param.Column.HeaderText = param.ConfigProperty.DisplayProperty.Titre;
            param.Column.Name = param.ConfigProperty.PropertyInfo.Name;
            param.Column.ReadOnly = true;
            if (param.ConfigProperty.DataGrid?.WidthColonne != 0)
                param.Column.Width = param.ConfigProperty.DataGrid.WidthColonne;

        }
        #endregion
    }
}
