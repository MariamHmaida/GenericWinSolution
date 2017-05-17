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
using App.Gwin.Entities.Resources.Glossary;

namespace App.Gwin.FieldsTraitements
{
    /// <summary>
    /// Primitive and Nullable Field Traitement
    /// This class is Dynamicly loaded
    /// You Can not change Traitement suffix, it used to load this Type
    /// </summary>
    public class DefaultFieldTraitement : BaseFieldTraitement, IFieldTraitements
    {
        /// <summary>
        /// Converto Value of Object to His Type
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public override object ConvertValue(BaseFieldTraitementParam param)
        {
            object value = null;
            if (param.ConfigProperty.PropertyInfo.PropertyType.IsPrimitive)
            {
                if (param.BaseField.Value != null)
                    value = Convert.ChangeType(param.BaseField.Value, param.ConfigProperty.PropertyInfo.PropertyType);
            }
            else
            {
                value = param.BaseField.Value;
            }
            return value;
        }



        public object GetTestValue(PropertyInfo propertyInfo)
        {

            if (propertyInfo.PropertyType == typeof(string)) return "String Value";
            var value = Activator.CreateInstance(propertyInfo.PropertyType);



            if (propertyInfo.PropertyType == typeof(int))
                value = 5;
            if (propertyInfo.PropertyType == typeof(Int64))
                value = (Int64)5.5;
            if (propertyInfo.PropertyType == typeof(float))
                value = (float)5.5;


            return value;
        }

        


        #region EntryForm
        /// <summary>
        /// Step1 : CreateField in EntryForm
        /// </summary>
        /// <param name="param">
        /// </param>
        /// <returns>the created field</returns>
        public BaseField CreateField_In_EntryForm(CreateFieldParams param)
        {
            // Set ErrorProvider Instance
            this.errorProvider = param.errorProvider;

            DefaultField defaultField = new DefaultField();
            defaultField.StopAutoSizeConfig();
            defaultField.Name = param.PropertyInfo.Name;
            defaultField.Location = param.Location;
            defaultField.OrientationField = param.OrientationField;
            defaultField.SizeLabel = param.SizeLabel;
            defaultField.SizeControl = param.SizeControl;
            defaultField.TabIndex = param.TabIndex;
            defaultField.Text_Label = param.ConfigProperty.DisplayProperty.Title;
            defaultField.ConfigSizeField();

            //Validating
            if (param.ConfigProperty.EntryForm.isRequired)
                defaultField.Validating += DefaultField_Validating;

            // Type of Property
            defaultField.PropertyInfo = param.ConfigProperty.PropertyInfo;

            // Can not Update Not supported Type , you can Just read it value
            // NB : String is not a primitve type
            if (
                !param.ConfigProperty.PropertyInfo.PropertyType.IsPrimitive
                && !(param.ConfigProperty.PropertyInfo.PropertyType == typeof(string))
                )
                defaultField.Enabled = false;


            // Insertion à l'interface
            param.ConteneurFormulaire.Controls.Add(defaultField);
            return defaultField;
        }

        private void DefaultField_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DefaultField defaultField = (DefaultField)sender;
            string message = Glossary.Entering_this_field_is_mandatory;

            // à vérifer avec les second
            if (defaultField.isEmpty)
            {
                errorProvider.Clear();
                errorProvider.SetError(defaultField, message);
                errorProvider.SetIconPadding(defaultField,  -20);
                errorProvider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
  
                e.Cancel = true;
            }
           

        }

        /// <summary>
        /// Step2 : Show Entity to EntryForm
        /// </summary>
        /// <param name="param">Parameters</param>
        public void ShowEntity_To_EntryForm(WriteEntity_To_EntryForm_Param param)
        {
            object value = null;

            // Determine Value if EntryForm.isShowDefaultValueWhenAdd is true
            if (param.EntityAction != BaseEntryForm.EntityActions.Add ||
                param.EntityAction == BaseEntryForm.EntityActions.Add &&  param.ConfigProperty.EntryForm.isShowDefaultValueWhenAdd)
            {
               value = param
               .Entity
               .GetType()
               .GetProperty(param.ConfigProperty.PropertyInfo.Name)
               .GetValue(param.Entity);
            }
           
            // Use Filter Value if exist
            if (param.CritereRechercheFiltre != null && param.CritereRechercheFiltre.ContainsKey(param.ConfigProperty.PropertyInfo.Name))
                value = param.CritereRechercheFiltre[param.ConfigProperty.PropertyInfo.Name].ToString();

            // Find baseField control in ConteneurFormulaire and set Value 
            if(value != null)
            {
                Control[] recherche = param.FromContainer.Controls.Find(param.ConfigProperty.PropertyInfo.Name, true);
                if (recherche.Count() > 0)
                {
                    BaseField baseField = (BaseField)recherche.First();
                    if (baseField == null) throw new GwinException("The field " + param.ConfigProperty.PropertyInfo.Name + "not exit in EntryForm");
                    baseField.Value = value;
                }
            }
            

        }
        #endregion

        #region Filter 
        /// <summary>
        /// Create Field in Filter
        /// </summary>
        /// <param name="param">Parameters</param>
        /// <returns>The created field</returns>
        public BaseField CreateField_In_Filter(CreateField_In_Filter_Params param)
        {
            DefaultField defaultField = new DefaultField();
            defaultField.StopAutoSizeConfig();
            defaultField.Name = param.ConfigProperty.PropertyInfo.Name;
            defaultField.SizeLabel = param.SizeLabel;
            defaultField.SizeControl = param.SizeControl;
            defaultField.OrientationField = Orientation.Horizontal;
            defaultField.TabIndex = param.TabIndex;
            defaultField.Text_Label = param.ConfigProperty.DisplayProperty.Title;

            defaultField.ConfigSizeField();
            param.FilterContainer.Controls.Add(defaultField);

            return defaultField;
        }

        /// <summary>
        /// Get Field Value from Filter
        /// </summary>
        /// <param name="FilterContainer">Filter container</param>
        /// <param name="ConfigProperty">Config property instance</param>
        /// <returns>Value of Field in Filter</returns>
        public object GetFieldValue_From_Filter(Control FilterContainer, ConfigProperty ConfigProperty)
        {
            DefaultField defaultField = (DefaultField)FilterContainer.Controls.Find(ConfigProperty.PropertyInfo.Name, true).First();
            if (defaultField.Value != null)
                return defaultField.Value;
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
