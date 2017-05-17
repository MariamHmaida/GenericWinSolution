using App.Gwin.Entities;
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
using App.Gwin.Application.BAL;

namespace App.Gwin.FieldsTraitements
{
    public class ManyToOneFieldTraitement : BaseFieldTraitement, IFieldTraitements
    {

        public override object ConvertValue(BaseFieldTraitementParam param)
        {
            IGwinBaseBLO ServicesEntity = param
                .EntityBLO
                .CreateServiceBLOInstanceByTypeEntity(param.ConfigProperty.PropertyInfo.PropertyType);

            BaseEntity ManyToOneEntity = ServicesEntity.GetBaseEntityByID(Convert.ToInt64(param.BaseField.Value));
            return ManyToOneEntity;
        }
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
            
            Int64 InitValue = 0;

            ManyToOneField manyToOneField = new ManyToOneField(param.EntityBLO, param.PropertyInfo,
               param.ConteneurFormulaire, param.OrientationField,
               param.SizeLabel,
                param.SizeControl, InitValue, param.ConfigProperty.ConfigEntity
                );

            manyToOneField.Location = param.Location;
            manyToOneField.Name = param.PropertyInfo.Name;
            manyToOneField.TabIndex = param.TabIndex;
            manyToOneField.Text_Label = param.ConfigProperty.DisplayProperty.Title;
 
 
            // Inert in to Interface
            param.ConteneurFormulaire.Controls.Add(manyToOneField);
            return manyToOneField;
        }

        public void ShowEntity_To_EntryForm(WriteEntity_To_EntryForm_Param param)
        {
            BaseEntity valeur = (BaseEntity) param.Entity.GetType().GetProperty(param.ConfigProperty.PropertyInfo.Name).GetValue(param.Entity);
            if (valeur == null) return;
            Int64 valeur_id = valeur.Id;
            // Use Filter Value
            if (param.CritereRechercheFiltre != null && param.CritereRechercheFiltre.ContainsKey(param.ConfigProperty.PropertyInfo.Name))
                valeur_id = Convert.ToInt64(param.CritereRechercheFiltre[param.ConfigProperty.PropertyInfo.Name]);

            // Find baseField control in ConteneurFormulaire
            // And Set Value
            Control[] recherche = param.FromContainer.Controls.Find(param.ConfigProperty.PropertyInfo.Name, true);
            if (recherche.Count() > 0)
            {
                BaseField baseField = (BaseField)recherche.First();
                if (baseField == null) throw new GwinException("The field " + param.ConfigProperty.PropertyInfo.Name + "not exit in EntryForm");
                baseField.Value = valeur_id;
            }

        }

        public BaseField CreateField_In_Filter(CreateField_In_Filter_Params param)
        {
            // Default Value
            Int64 default_value = 0;
            if (param.DefaultFilterValues != null && param.DefaultFilterValues.Keys.Contains(param.ConfigProperty.PropertyInfo.PropertyType.Name))
                default_value = (Int64)param.DefaultFilterValues[param.ConfigProperty.PropertyInfo.PropertyType.Name];

            ManyToOneField manyToOneField = new ManyToOneField(param.EntityBLO, param.ConfigProperty.PropertyInfo,
                param.FilterContainer,
                Orientation.Horizontal,
                 param.SizeLabel,
                 param.SizeControl,
                 default_value, param.ConfigProperty.ConfigEntity
                );
            manyToOneField.Name = param.ConfigProperty.PropertyInfo.Name;
            manyToOneField.TabIndex = param.TabIndex;
            manyToOneField.Text_Label = param.ConfigProperty.DisplayProperty.Title;

            param.FilterContainer.Controls.Add(manyToOneField);

            return manyToOneField;
        }

        public object GetFieldValue_From_Filter(Control FilterContainer, ConfigProperty ConfigProperty)
        {
            ManyToOneField ComboBoxEntity = (ManyToOneField)FilterContainer.Controls.Find(ConfigProperty.PropertyInfo.Name, true).First();
            BaseEntity obj = (BaseEntity)ComboBoxEntity.SelectedItem;
            if (obj != null && Convert.ToInt32(obj.Id) != 0)
                return ComboBoxEntity.Value;
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
