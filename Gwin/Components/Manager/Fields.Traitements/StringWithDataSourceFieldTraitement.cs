using App.Gwin.Exceptions.Gwin;
using App.Gwin.Fields;
using App.Gwin.Components.Manager.Fields.Traitements.Params;
using App.Shared.AttributesManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Gwin.Entities;

namespace App.Gwin.FieldsTraitements
{

    class StringWithDataSourceFieldTraitement : BaseFieldTraitement, IFieldTraitements
    {

        public override object ConvertValue(BaseFieldTraitementParam param)
        {
            object value = null;

               
                value = param.BaseField.Value;
             
            return value;
        }

        public object GetTestValue(PropertyInfo propertyInfo)
        {
            return "String DataSource Value";
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
            //var DataObject = Activator.CreateInstance(param.ConfigProperty.DataSource.TypeObject);
            //IList ls_data = (IList)DataObject.GetType().GetMethod(param.ConfigProperty.DataSource.MethodeName).Invoke(DataObject, null);
            IList ls_data = param.ConfigProperty.DataSource.GetData();

            List<object> ls_data_object = ls_data.Cast<string>().ToList<object>();
           
         
            comboBoxField.DataSource = ls_data_object;

            // Insertion à l'interface
            param.ConteneurFormulaire.Controls.Add(comboBoxField);
            return comboBoxField;
        }

        public void ShowEntity_To_EntryForm(WriteEntity_To_EntryForm_Param param)
        {
            string valeur = (string)param.Entity.GetType().GetProperty(param.ConfigProperty.PropertyInfo.Name).GetValue(param.Entity);

            // Use Filter Value
            if (param.CritereRechercheFiltre != null && param.CritereRechercheFiltre.ContainsKey(param.ConfigProperty.PropertyInfo.Name))
                valeur = param.CritereRechercheFiltre[param.ConfigProperty.PropertyInfo.Name].ToString();

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

        public BaseField CreateField_In_Filter(CreateField_In_Filter_Params param)
        {
            ComboBoxField stringFiled = new ComboBoxField();
            stringFiled.StopAutoSizeConfig();
            stringFiled.Name = param.ConfigProperty.PropertyInfo.Name;
            stringFiled.SizeLabel = param.SizeLabel;
            stringFiled.SizeControl = param.SizeControl;
            stringFiled.OrientationField = Orientation.Horizontal;
            stringFiled.TabIndex = param.TabIndex;
            stringFiled.Text_Label = param.ConfigProperty.DisplayProperty.Title;

            stringFiled.ConfigSizeField();

            // DataSource
            IList ls_data = param.ConfigProperty.DataSource.GetData();
            List<string> ls_data_string = ls_data.Cast<Object>().Select(o => o.ToString()).ToList<string>();

            // Add Blank Data 
            if (param.ConfigProperty.Filter.isValeurFiltreVide)
                ls_data_string.Insert(0, "");
            stringFiled.DataSource = ls_data_string.ToList<object>();



            param.FilterContainer.Controls.Add(stringFiled);

            return stringFiled;
        }

        public object GetFieldValue_From_Filter(Control FilterContainer, ConfigProperty ConfigProperty)
        {
            ComboBoxField stringFiled = (ComboBoxField)FilterContainer.Controls.Find(ConfigProperty.PropertyInfo.Name, true).First();
            if (stringFiled.Value?.ToString() != "")
                return stringFiled.Value;
            else
                return null;
        }

        #region EntityDataGrid
        /// <summary>
        /// Create Field Colomn in Entity DataGrid
        /// </summary>
        /// <param name="param"></param>
        public void ConfigFieldColumn_In_EntityDataGrid(CreateFieldColumns_In_EntityDataGrid param)
        {
            param.Column.ValueType = typeof(String);
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
