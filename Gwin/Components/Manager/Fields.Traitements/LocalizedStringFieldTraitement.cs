using App.Gwin.Entities.MultiLanguage;
using App.Gwin.Exceptions.Gwin;
using App.Gwin.Fields;
using App.Gwin.Components.Manager.Fields.Traitements.Params;
using System;
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
    public class LocalizedStringFieldTraitement : BaseFieldTraitement, IFieldTraitements
    {

        public override object ConvertValue(BaseFieldTraitementParam param)
        {
            LocalizedString localizedString = (LocalizedString)param
                .Entity.GetType()
                .GetProperty(param.ConfigProperty.PropertyInfo.Name)
                .GetValue(param.Entity);

            if (localizedString == null) localizedString = new LocalizedString();

            localizedString.Current = param.BaseField.Value.ToString();
            return localizedString;
        }


        public object GetTestValue(PropertyInfo propertyInfo)
        {
            LocalizedString valeur = new LocalizedString();
            valeur.Arab = "قيمة بالعربية";
            valeur.English = "English value";
            valeur.French = "Valeur en français";
            return valeur;
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
            // Set ErrorProvider Instance
            this.errorProvider = param.errorProvider;

            StringField stringField = new StringField();
            stringField.StopAutoSizeConfig();
            stringField.Name = param.PropertyInfo.Name;
            stringField.Location = param.Location;
            stringField.OrientationField = param.OrientationField;
            stringField.SizeLabel = param.SizeLabel;
            stringField.SizeControl = param.SizeControl;
            stringField.IsMultiline = param.ConfigProperty.EntryForm.MultiLine;
            stringField.NombreLigne = param.ConfigProperty.EntryForm.NumberLine;
            if (param.ConfigProperty.EntryForm.WidthControl != 0)
                stringField.Width = param.ConfigProperty.EntryForm.WidthControl;

            stringField.TabIndex = param.TabIndex;
            stringField.Text_Label = param.ConfigProperty.DisplayProperty.Titre;
            stringField.ConfigSizeField();

            if (param.ConfigProperty.EntryForm.isRequired)
                stringField.Validating += StringField_Validating;
            // Insertion à l'interface
            param.ConteneurFormulaire.Controls.Add(stringField);
            return stringField;
        }

        private void StringField_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StringField stringField = (StringField)sender;
            string message = Glossary.Entering_this_field_is_mandatory;

            // à vérifer avec les second
            if (stringField.isEmpty)
            {
                errorProvider.Clear();
                errorProvider.SetError(stringField, message);
                errorProvider.SetIconPadding(stringField, -20);
                errorProvider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
                e.Cancel = true;
            }
        }

        public void ShowEntity_To_EntryForm(WriteEntity_To_EntryForm_Param param)
        {
            LocalizedString valeur = (LocalizedString)param.Entity.GetType().GetProperty(param.ConfigProperty.PropertyInfo.Name).GetValue(param.Entity);

            // Use Filter Value
            if (param.CritereRechercheFiltre != null && param.CritereRechercheFiltre.ContainsKey(param.ConfigProperty.PropertyInfo.Name))
                valeur.Current = param.CritereRechercheFiltre[param.ConfigProperty.PropertyInfo.Name].ToString();

            // Find baseField control in ConteneurFormulaire
            // And Set Value
            Control[] recherche = param.FromContainer.Controls.Find(param.ConfigProperty.PropertyInfo.Name, true);
            if (recherche.Count() > 0)
            {
                BaseField baseField = (BaseField)recherche.First();
                if (baseField == null) throw new GwinException("The field " + param.ConfigProperty.PropertyInfo.Name + "not exit in EntryForm");

                if (valeur != null)
                    baseField.Value = valeur.Current;
            }

        }

        #region Filter 
        /// <summary>
        /// Create Field in Filter
        /// </summary>
        /// <param name="param">Parameters</param>
        /// <returns>The created field</returns>
        public BaseField CreateField_In_Filter(CreateField_In_Filter_Params param)
        {
            StringField stringFiled = new StringField();
            stringFiled.StopAutoSizeConfig();
            stringFiled.Name = param.ConfigProperty.PropertyInfo.Name;
            stringFiled.SizeLabel = param.SizeLabel;
            stringFiled.SizeControl = param.SizeControl;
            stringFiled.OrientationField = Orientation.Horizontal;
            stringFiled.TabIndex = param.TabIndex;
            stringFiled.Text_Label = param.ConfigProperty.DisplayProperty.Titre;

            stringFiled.ConfigSizeField();
            param.FilterContainer.Controls.Add(stringFiled);

            return stringFiled;
        }

        /// <summary>
        /// Get Field Value from Filter
        /// </summary>
        /// <param name="FilterContainer">Filter container</param>
        /// <param name="ConfigProperty">Config property instance</param>
        /// <returns>Value of Field in Filter</returns>
        public object GetFieldValue_From_Filter(Control FilterContainer, ConfigProperty ConfigProperty)
        {
            StringField stringFiled = (StringField)FilterContainer.Controls.Find(ConfigProperty.PropertyInfo.Name, true).First();
            if (stringFiled.Value.ToString() != "")
                return stringFiled.Value;
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
            param.Column.ValueType = typeof(LocalizedString);
            param.Column.DataPropertyName = param.ConfigProperty.PropertyInfo.Name;
            param.Column.HeaderText = param.ConfigProperty.DisplayProperty.Titre;
            param.Column.Name = param.ConfigProperty.PropertyInfo.Name;
            param.Column.ReadOnly = true;
            if (param.ConfigProperty.DataGrid?.WidthColonne != 0)
                param.Column.Width = param.ConfigProperty.DataGrid.WidthColonne;

        }
        #endregion
    }
}
