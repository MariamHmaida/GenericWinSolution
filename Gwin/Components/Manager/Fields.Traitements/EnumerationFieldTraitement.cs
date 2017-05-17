using App.Gwin.Exceptions.Gwin;
using App.Gwin.Fields;
using App.Gwin.Components.Manager.Fields.Traitements.Params;
using App.Shared.AttributesManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Gwin.FieldsTraitements
{
    public class EnumerationFieldTraitement : BaseFieldTraitement, IFieldTraitements
    {

       


        public object GetTestValue(PropertyInfo propertyInfo)
        {
            return Activator.CreateInstance(propertyInfo.PropertyType);
        }


        #region EntryForm
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
            // Create Field
            ComboBoxField comboBoxField = new ComboBoxField();
            comboBoxField.StopAutoSizeConfig();
            comboBoxField.Name = param.PropertyInfo.Name;
            comboBoxField.Location = param.Location;
            comboBoxField.OrientationField = param.OrientationField;
            comboBoxField.SizeLabel = param.SizeLabel;
            comboBoxField.SizeControl = param.SizeControl;
            comboBoxField.TabIndex = param.TabIndex;
            comboBoxField.Text_Label = param.ConfigProperty.DisplayProperty.Title;
            comboBoxField.ConfigSizeField();

            // DataSource
            comboBoxField.DataSource = Enum.GetValues(param.PropertyInfo.PropertyType).Cast<object>().ToList<object>();

            // Insert field in Form
            param.ConteneurFormulaire.Controls.Add(comboBoxField);
            return comboBoxField;
        }
      
        public void ShowEntity_To_EntryForm(WriteEntity_To_EntryForm_Param param)
        {
            var valeur = param.Entity.GetType().GetProperty(param.ConfigProperty.PropertyInfo.Name).GetValue(param.Entity);

            // Use Filter Value
            if (param.CritereRechercheFiltre != null && param.CritereRechercheFiltre.ContainsKey(param.ConfigProperty.PropertyInfo.Name))
                throw new GwinNotImplementedException("Enumeation in Filter not yet implmented");

            // Find baseField control in ConteneurFormulaire
            // And Set Value
            Control[] recherche = param.FromContainer.Controls.Find(param.ConfigProperty.PropertyInfo.Name, true);
            if (recherche.Count() > 0)
            {
                BaseField baseField = (BaseField)recherche.First();
                if (baseField == null) throw new GwinException("The field " + param.ConfigProperty.PropertyInfo.Name + "not exit in EntryForm");
                baseField.Value = valeur;
            }

        }
        #endregion

        #region Filter 
        /// <summary>
        /// Create Field in Filter
        /// </summary>
        /// <param name="param"></param>
        /// <returns>Created field instance</returns>
        public BaseField CreateField_In_Filter(CreateField_In_Filter_Params param)
        {
            // Create Field
            ComboBoxField comboBoxField = new ComboBoxField();
            comboBoxField.StopAutoSizeConfig();
            comboBoxField.Name = param.ConfigProperty.PropertyInfo.Name;
            comboBoxField.SizeLabel = param.SizeLabel;
            comboBoxField.SizeControl = param.SizeControl;
            comboBoxField.OrientationField = Orientation.Horizontal;
            comboBoxField.TabIndex = param.TabIndex;
            comboBoxField.Text_Label = param.ConfigProperty.DisplayProperty.Title;
            comboBoxField.ConfigSizeField();

            // DataSource
            comboBoxField.DataSource = Enum.GetValues(param.ConfigProperty.PropertyInfo.PropertyType).Cast<object>().ToList<object>();

            // Insert Field in Filter
            param.FilterContainer.Controls.Add(comboBoxField);

            return comboBoxField;
        }


        public object GetFieldValue_From_Filter(Control FilterContainer, ConfigProperty ConfigProperty)
        {
            ComboBoxField comboBoxField = (ComboBoxField)FilterContainer.Controls.Find(ConfigProperty.PropertyInfo.Name, true).First();

            var empty_instance = Activator.CreateInstance(ConfigProperty.PropertyInfo.PropertyType);
            if (comboBoxField.Value != empty_instance)
                return comboBoxField.Value;
            else
                return null;
        }
        #endregion

        #region EntityDataGrid
        /// <summary>
        /// Create Field Colomn in Entity DataGrid
        /// </summary>
        /// <param name="param"></param>
        public void ConfigFieldColumn_In_EntityDataGrid(CreateFieldColumns_In_EntityDataGrid param)
        {
            param.Column.ValueType = param.ConfigProperty.PropertyInfo.PropertyType;
            param.Column.DataPropertyName = param.ConfigProperty.PropertyInfo.Name;
            param.Column.HeaderText = param.ConfigProperty.DisplayProperty.Title;
            param.Column.Name = param.ConfigProperty.PropertyInfo.Name;
            param.Column.ReadOnly = true;
            if (param.ConfigProperty.DataGrid?.WidthColonne != 0)
                param.Column.Width = param.ConfigProperty.DataGrid.WidthColonne;

        }
        #endregion
    }
}
