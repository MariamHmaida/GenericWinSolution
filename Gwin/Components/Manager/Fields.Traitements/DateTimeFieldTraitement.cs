using App.Gwin.Exceptions.Gwin;
using App.Gwin.Fields;
using App.Gwin.Components.Manager.Fields.Traitements.Params;
using App.Shared.AttributesManager;
using App.Components.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace App.Gwin.FieldsTraitements
{
    public class DateTimeFieldTraitement : BaseFieldTraitement, IFieldTraitements
    {
        private readonly object DateTime2;
       

        public object GetTestValue(PropertyInfo propertyInfo)
        {
            return DateTimeField.GetTestValue();
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

            this.errorProvider = param.errorProvider;

            DateTimeField dateTimeField = new DateTimeField();
            dateTimeField.StopAutoSizeConfig();
            dateTimeField.Name = param.PropertyInfo.Name;
            dateTimeField.Location = param.Location;
            dateTimeField.OrientationField = param.OrientationField;
            dateTimeField.SizeLabel = param.SizeLabel;
            dateTimeField.SizeControl = param.SizeControl;

            dateTimeField.TabIndex = param.TabIndex;
            dateTimeField.Text_Label = param.ConfigProperty.DisplayProperty.Title;
            dateTimeField.ConfigSizeField();

            // Validation
            // We can not check validation of DateTime Control because we can not knew the default value 
            //dateTimeField.Validating += DateTimeField_Validating;

            // Insertion à l'interface
            param.ConteneurFormulaire.Controls.Add(dateTimeField);
            return dateTimeField;
        }

        // We can not check validation of DateTime Control because we can not knew the default value 
        //private void DateTimeField_Validating(object sender, CancelEventArgs e)
        //{
        //    DateTimeField dateTimeField = (DateTimeField)sender;
        //    string message = "La saisie de ce champs est oblégatoir";

        //    // à vérifer avec les second
        //    DateTime value = (DateTime)dateTimeField.Value;
        //    if (value.Year == DateTime.Now.Year)
        //    {
        //        errorProvider.SetError(controle, message);
        //        e.Cancel = true;
        //    }
        //    else
        //        errorProvider.SetError(controle, "");
        //}
       


        public void ShowEntity_To_EntryForm(WriteEntity_To_EntryForm_Param param)
        {
            DateTime value = (DateTime)param.Entity.GetType().GetProperty(param.ConfigProperty.PropertyInfo.Name).GetValue(param.Entity);

            // Use Filter Value
            if (param.CritereRechercheFiltre != null && param.CritereRechercheFiltre.ContainsKey(param.ConfigProperty.PropertyInfo.Name))
                value = Convert.ToDateTime(param.CritereRechercheFiltre[param.ConfigProperty.PropertyInfo.Name]);

            // Find baseField control in ConteneurFormulaire
            // And Set Value
            Control[] recherche = param.FromContainer.Controls.Find(param.ConfigProperty.PropertyInfo.Name, true);
            if (recherche.Count() > 0)
            {
                BaseField baseField = (BaseField)recherche.First();
                if (baseField == null) throw new GwinException("The field " + param.ConfigProperty.PropertyInfo.Name + "not exit in EntryForm");


                if (value.Year == 1)
                    // [DataBase] Imcompatiblite with other DataBase
                    baseField.Value =( DateTime)  System.Data.SqlTypes.SqlDateTime.MinValue    ;
                else
                    baseField.Value = value;
            }

        }

        public BaseField CreateField_In_Filter(CreateField_In_Filter_Params param)
        {
            DateTimeField dateTimeField = new DateTimeField();
            dateTimeField.StopAutoSizeConfig();
            dateTimeField.Name = param.ConfigProperty.PropertyInfo.Name;
            dateTimeField.SizeLabel = param.SizeLabel;
            dateTimeField.SizeControl = param.SizeControl;
            dateTimeField.OrientationField = Orientation.Horizontal;
            dateTimeField.TabIndex = param.TabIndex;
            dateTimeField.Text_Label = param.ConfigProperty.DisplayProperty.Title;

            dateTimeField.ConfigSizeField();
            param.FilterContainer.Controls.Add(dateTimeField);

            return dateTimeField;
        }

        public object GetFieldValue_From_Filter(Control FilterContainer, ConfigProperty ConfigProperty)
        {
            DateTimeField dateTimeField = (DateTimeField)FilterContainer.Controls.Find(ConfigProperty.PropertyInfo.Name, true).First();
            if ((DateTime)dateTimeField.Value != DateTime.MinValue)
                return dateTimeField.Value;
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
            param.Column.ValueType = typeof(DateTime);
             
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
