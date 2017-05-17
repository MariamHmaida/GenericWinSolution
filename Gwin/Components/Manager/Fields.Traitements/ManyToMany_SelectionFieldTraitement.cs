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
using App.Gwin.Application.BAL;

namespace App.Gwin.FieldsTraitements
{
    public class ManyToMany_SelectionFieldTraitement : BaseFieldTraitement, IFieldTraitements
    {
        public override object ConvertValue(BaseFieldTraitementParam param)
        {
            List<BaseEntity> ls = null;
            ls = param.BaseField.Value as List<BaseEntity>;

 
            IGwinBaseBLO ServicesEntity = param.EntityBLO
                .CreateServiceBLOInstanceByTypeEntityAndContext(
                    param.ConfigProperty.PropertyInfo.PropertyType.GetGenericArguments()[0],
                    param.EntityBLO.Context
                );


            Type TypeListeObjetValeur = typeof(List<>).MakeGenericType(
                param.ConfigProperty.PropertyInfo.PropertyType.GetGenericArguments()[0]);

            IList ls_valeur = (IList)Activator.CreateInstance(TypeListeObjetValeur);

            foreach (BaseEntity b in ls)
            {
                var entity_valeur = ServicesEntity.GetBaseEntityByID(b.Id);
                ls_valeur.Add(entity_valeur);

            }

            return ls_valeur;
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

            if (param.ConfigProperty.EntryForm?.TabPage == true)
            {
                TabPage tabPage = new TabPage();
                tabPage.Name = "tabPage" + param.PropertyInfo.Name;
                tabPage.Text = param.ConfigProperty.DisplayProperty.Title;
                param.TabControlForm.TabPages.Add(tabPage);
                param.ConteneurFormulaire = tabPage;
            }


            //Default Value
            List<BaseEntity> ls_default_value = null;
            if (param.Entity != null)
            {
                IList ls_obj = param.PropertyInfo.GetValue(param.Entity) as IList;

                if (ls_obj != null) ls_default_value = ls_obj.Cast<BaseEntity>().ToList();
            }



            ManyToManyField manyToManyField = new ManyToManyField(param.PropertyInfo,
                                                param.OrientationField,
                                                param.SizeLabel,
                                                param.SizeControl,
                                                param.ConfigProperty.ConfigEntity,
                                                param.ConteneurFormulaire,
                                                param.EntityBLO);
            manyToManyField.Name = param.PropertyInfo.Name;

           

            if (param.ConfigProperty.EntryForm?.TabPage == true)
            {
                manyToManyField.Dock = DockStyle.Fill;
            }

            // Inert in to Interface
            param.ConteneurFormulaire.Controls.Add(manyToManyField);
            return manyToManyField;
        }

        public BaseField CreateField_In_Filter(CreateField_In_Filter_Params param)
        {
            throw new GwinException("Create Field ManyToMany not yet implemented in Filter");
        }

        public object GetFieldValue_From_Filter(Control FilterContainer, ConfigProperty ConfigProperty)
        {
            throw new GwinException("Create Field ManyToMany not yet implemented in Filter");
        }

        public void ShowEntity_To_EntryForm(WriteEntity_To_EntryForm_Param param)
        {
            IList v_ls_object = param.Entity.GetType().GetProperty(param.ConfigProperty.PropertyInfo.Name).GetValue(param.Entity) as IList;
            if (v_ls_object == null) return;
            List<object> ls_object = v_ls_object.Cast<Object>().ToList();

            List<BaseEntity> ls_valeur = ls_object.Cast<BaseEntity>().ToList();
            if (ls_valeur == null) return;




            // Use Filter Value
            if (param.CritereRechercheFiltre != null && param.CritereRechercheFiltre.ContainsKey(param.ConfigProperty.PropertyInfo.Name))
                throw new NotImplementedException();

            // Find baseField control in ConteneurFormulaire
            // And Set Value
            Control[] recherche = param.FromContainer.Controls.Find(param.ConfigProperty.PropertyInfo.Name, true);
            if (recherche.Count() > 0)
            {
                BaseField baseField = (BaseField)recherche.First();
                if (baseField == null) throw new GwinException("The field " + param.ConfigProperty.PropertyInfo.Name + "not exit in EntryForm");
                baseField.Value = ls_valeur;
            }

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
