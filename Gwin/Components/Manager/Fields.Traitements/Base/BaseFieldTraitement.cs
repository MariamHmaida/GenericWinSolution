using App.Gwin.Attributes;
using App.Gwin.Components.Manager.Fields.Traitements.Params;
using App.Gwin.Exceptions.Gwin;
using App.Gwin.FieldsTraitements.Enumerations;
using App.Shared.AttributesManager;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace App.Gwin.FieldsTraitements
{

    public class BaseFieldTraitement
    {
        /// <summary>
        /// ErrorProvider Instance
        /// </summary>
        protected ErrorProvider errorProvider { set; get; }

        public virtual object ConvertValue(BaseFieldTraitementParam param)
        {

            object value = null;
            if (param.ConfigProperty.PropertyInfo.PropertyType.IsPrimitive)
            {
                value = Convert.ChangeType(param.BaseField.Value, param.ConfigProperty.PropertyInfo.PropertyType);
            }
            else
            {
                value = param.BaseField.Value;
            }
            return value;
        }

        /// <summary>
        /// Create Instance of FieldTraitement 
        /// </summary>
        /// <param name="configProperty">Config Property instance</param>
        /// <returns>The FieldTraitement Instance</returns>
        public static IFieldTraitements CreateInstance(ConfigProperty configProperty)
        {

            // Get TypeFieldTraitement
            string field_nature = configProperty.FieldNature.ToString();
            Assembly assembly = typeof(DefaultFieldTraitement).Assembly;
            string TypeFieldTraitement_Name = nameof(App) + "." + nameof(App.Gwin) + "." + nameof(App.Gwin.FieldsTraitements) + "." + field_nature + "FieldTraitement";
            Type TypeFieldTraitement = assembly.GetType(TypeFieldTraitement_Name);
            if (TypeFieldTraitement == null) throw new GwinException("The class " + TypeFieldTraitement_Name + " not exist ");

            // Create FieldTraitement Instance
            IFieldTraitements fieldTraitement = (IFieldTraitements)Activator.CreateInstance(TypeFieldTraitement);
            return fieldTraitement;

        }

        /// <summary>
        /// Determine FieldNature in ConfigProperty
        /// </summary>
        public static FieldsNatures DetermineFieldNature(ConfigProperty configProperty)
        {
            // Default Value of FieldNature is Default
            FieldsNatures fieldNature = FieldsNatures.Default;

            if (configProperty.PropertyInfo.PropertyType.Name == "String" && configProperty.DataSource == null)
            {
                fieldNature = FieldsNatures.Default;

            }
            if (configProperty.PropertyInfo.PropertyType.Name == "String" && configProperty.DataSource != null)
            {
                fieldNature = FieldsNatures.StringWithDataSource;

            }
            if (configProperty.PropertyInfo.PropertyType.Name == "LocalizedString")
            {
                fieldNature = FieldsNatures.LocalizedString;

            }

            // 
            // Use Default Field
            //
            //if (configProperty.PropertyInfo.PropertyType.Name == "Int32")
            //{
            //    fieldNature = FieldsNatures.Int32;

            //}
            //if (configProperty.PropertyInfo.PropertyType.Name == "Int64")
            //{
            //    fieldNature = FieldsNatures.Int64;

            //}

            if (configProperty.PropertyInfo.PropertyType.Name == "DateTime")
            {
                fieldNature = FieldsNatures.DateTime;
            }
            if (configProperty.PropertyInfo.PropertyType.Name == "bool" || configProperty.PropertyInfo.PropertyType.Name == "Boolean")
            {
                fieldNature = FieldsNatures.Bool;
            }
            if (configProperty.PropertyInfo.PropertyType.IsEnum)
            {
                fieldNature = FieldsNatures.Enumeration;

            }
            if (configProperty.Relationship?.Relation == RelationshipAttribute.Relations.ManyToOne)
            {
                fieldNature = FieldsNatures.ManyToOne;

            }
            if (configProperty.Relationship?.Relation == RelationshipAttribute.Relations.ManyToMany_Selection)
            {
                fieldNature = FieldsNatures.ManyToMany_Selection;

            }
            if (configProperty.Relationship?.Relation == RelationshipAttribute.Relations.ManyToMany_Creation)
            {
                fieldNature = FieldsNatures.ManyToMany_Creation;

            }

            return fieldNature;
        }



    }
}