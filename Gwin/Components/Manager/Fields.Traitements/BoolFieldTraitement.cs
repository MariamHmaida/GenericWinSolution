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
    /// <summary>
    /// Boolean Field Traitement
    /// This class is Dynamicly loaded
    /// You Can not change Traitement suffix, it used to load this Type
    /// </summary>
    public class BoolFieldTraitement : BaseFieldTraitement, IFieldTraitements
    {
        /// <summary>
        /// Converto Value of Object to His Type
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public override object ConvertValue(BaseFieldTraitementParam param)
        {
            object value = null;
            value = param.BaseField.Value;
            return value;
        }



        public object GetTestValue(PropertyInfo propertyInfo)
        {
            bool value = true;
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
            BooleanField booleanField = new BooleanField();
            booleanField.StopAutoSizeConfig();
            booleanField.Name = param.PropertyInfo.Name;
            booleanField.Location = param.Location;
            booleanField.OrientationField = param.OrientationField;
            booleanField.SizeLabel = param.SizeLabel;
            booleanField.SizeControl = param.SizeControl;
            booleanField.TabIndex = param.TabIndex;
            booleanField.Text_Label = param.ConfigProperty.DisplayProperty.Title;
            booleanField.ConfigSizeField();

            // Type of Property
            booleanField.PropertyInfo = param.ConfigProperty.PropertyInfo;

            // Insertion à l'interface
            param.ConteneurFormulaire.Controls.Add(booleanField);
            return booleanField;
        }

        /// <summary>
        /// Step2 : Show Entity to EntryForm
        /// </summary>
        /// <param name="param">Parameters</param>
        public void ShowEntity_To_EntryForm(WriteEntity_To_EntryForm_Param param)
        {
            object valeur = param
                .Entity
                .GetType()
                .GetProperty(param.ConfigProperty.PropertyInfo.Name)
                .GetValue(param.Entity);

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
        #endregion

        #region Filter 
        /// <summary>
        /// Create Field in Filter
        /// </summary>
        /// <param name="param">Parameters</param>
        /// <returns>The created field</returns>
        public BaseField CreateField_In_Filter(CreateField_In_Filter_Params param)
        {
            BooleanField booleanField = new BooleanField();
            booleanField.StopAutoSizeConfig();
            booleanField.Name = param.ConfigProperty.PropertyInfo.Name;
            booleanField.SizeLabel = param.SizeLabel;
            booleanField.SizeControl = param.SizeControl;
            booleanField.OrientationField = Orientation.Horizontal;
            booleanField.TabIndex = param.TabIndex;
            booleanField.Text_Label = param.ConfigProperty.DisplayProperty.Title;

            booleanField.ConfigSizeField();
            param.FilterContainer.Controls.Add(booleanField);

            return booleanField;
        }

        /// <summary>
        /// Get Field Value from Filter
        /// </summary>
        /// <param name="FilterContainer">Filter container</param>
        /// <param name="ConfigProperty">Config property instance</param>
        /// <returns>Value of Field in Filter</returns>
        public object GetFieldValue_From_Filter(Control FilterContainer, ConfigProperty ConfigProperty)
        {
            BooleanField booleanField = (BooleanField)FilterContainer.Controls.Find(ConfigProperty.PropertyInfo.Name, true).First();
            if (booleanField.Value != null)
                return booleanField.Value;
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
