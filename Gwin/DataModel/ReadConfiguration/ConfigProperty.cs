using App.Gwin;
using App.Gwin.Application;
using App.Gwin.Application.Presentation.Messages;
using App.Gwin.Attributes;
using App.Gwin.Entities;
using App.Gwin.Shared.Resources;
using App.Gwin.Entities.Resources.Glossary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using App.Gwin.FieldsTraitements.Enumerations;
using App.Gwin.Exceptions.Gwin;
using App.Gwin.DataModel.Exceptions;
using App.Gwin.FieldsTraitements;

namespace App.Shared.AttributesManager
{
    /// <summary>
    /// Read Property Configuration
    /// </summary>
    public class ConfigProperty
    {
        /// <summary>
        /// Current Entity Instance
        /// </summary>
        public BaseEntity EntityInstance {set;get;}

        #region Public Properties
        public DisplayPropertyAttribute DisplayProperty { set; get; }
        public RelationshipAttribute Relationship { set; get; }
        public EntryFormAttribute EntryForm { set; get; }
        public FilterAttribute Filter { set; get; }
        public DataGridAttribute DataGrid { set; get; }
        public ReferencesDataSourceAttribute DataSource { set; get; }
        public FieldsNatures FieldNature { set; get; }
        public PropertyInfo PropertyInfo { get; set; }
        public ConfigEntity ConfigEntity { get; set; }
        public Type TypeOfEntity { set; get; }
        public bool Localizable { get; set; }
        public CultureInfo CultureInfo { get; set; }
        public BusinesRoleAttribute BusinesRole { get; set; }
        public SelectionCriteriaAttribute SelectionCriteria { set; get; }


        /// <summary>
        ///  Resource Manager of the Entity and its BaseType
        /// </summary>
        Dictionary<string, ResourceManager> RessoucesManagers = new Dictionary<string, ResourceManager>();
        #endregion

        public ConfigProperty(PropertyInfo propertyInfo, ConfigEntity configEntity)
        {

            this.PropertyInfo = propertyInfo;


            this.ConfigEntity = configEntity;
            //Fill RessouceManager
            this.RessoucesManagers = this.ConfigEntity.RessourcesManagers;


            // Culture Info
            this.CultureInfo = GwinApp.Instance.CultureInfo;

            // Localizable
            this.TypeOfEntity = propertyInfo.ReflectedType;
            this.Localizable = this.ConfigEntity.GwinEntity.Localizable;


           

            //
            // Relationship
            //
            Attribute Relationship = propertyInfo.GetCustomAttribute(typeof(RelationshipAttribute));
            this.Relationship = Relationship as RelationshipAttribute;
            if (this.Relationship != null)
            {
                // Check if Type of Memeber is valide Generic List
                if (
                    (this.Relationship.Relation == RelationshipAttribute.Relations.ManyToMany_Creation
                    || this.Relationship.Relation == RelationshipAttribute.Relations.ManyToMany_Selection
                    || this.Relationship.Relation == RelationshipAttribute.Relations.OneToMany)
                    &&
                    this.PropertyInfo.PropertyType.GetGenericArguments().Count() == 0)
                {
                    string msg_exception = "The Type :" + this.PropertyInfo.PropertyType.Name;
                    msg_exception += " of member " + this.PropertyInfo.Name;
                    msg_exception += " in Entity " + this.PropertyInfo.DeclaringType.Name;
                    msg_exception += " is not a valid generic List";
                    throw new GwinException(msg_exception);
                }
            }

            //
            // DisplayProperty
            //
            #region DisplayProperty
            // Load DisplayProperty
            Attribute DisplayProperty = propertyInfo.GetCustomAttribute(typeof(DisplayPropertyAttribute));
            this.DisplayProperty = DisplayProperty as DisplayPropertyAttribute;
            // If DisplayProperty not exist
            if (this.DisplayProperty == null)
            {
                if (this.Localizable == false)
                {
                    string message = String.Format("The Attribute : {0} not exist", nameof(DisplayPropertyAttribute));
                    message += " with Property :" + propertyInfo.ToString();
                    message += " in Entity " + propertyInfo.ReflectedType.Name;
                    message += " \n Bacause the Entity is not configured as Localazible";
                    throw new AnnotationNotExistException(message);
                }

                this.DisplayProperty = new DisplayPropertyAttribute();
            }
            if (this.DisplayProperty.isInGlossary)
            {
                string GlossaryRessouceFullName = "App.Gwin.Entities.Resources.Glossary.Glossary";
                ResourceManager GlossaryResourceManager = null;
                GlossaryResourceManager = new ResourceManager(GlossaryRessouceFullName, typeof(Glossary).Assembly);
                string title = GlossaryResourceManager.GetString(propertyInfo.Name, this.CultureInfo);
                if (title == null)
                    this.DisplayProperty.Title = this.CultureInfo.Name + "_Glossary_" + propertyInfo.Name;
                else
                    this.DisplayProperty.Title = title;
            }
            else
            {
                //
                // Title
                //
                if (this.DisplayProperty.Title == null)
                {

                    if (this.PropertyInfo.PropertyType.IsSubclassOf(typeof(BaseEntity)))
                        this.DisplayProperty.Title = ConfigEntity.CreateConfigEntity(this.PropertyInfo.PropertyType).GwinEntity.SingularName;
                    else
                        this.DisplayProperty.Title = GetStringFromRessource(propertyInfo.Name);
                }
                else
                    this.DisplayProperty.Title = GetStringFromRessource(propertyInfo.Name);
            }
            #endregion

            //
            // EntryForm
            //
            Attribute EntryForm = propertyInfo.GetCustomAttribute(typeof(EntryFormAttribute));
            this.EntryForm = EntryForm as EntryFormAttribute;



            //
            // DataGrid
            //
            Attribute DataGrid = propertyInfo.GetCustomAttribute(typeof(DataGridAttribute));
            this.DataGrid = DataGrid as DataGridAttribute;
            // Order 
            if (this.EntryForm != null && this.EntryForm.Ordre > 0
                && this.DataGrid != null && this.DataGrid.Ordre == 0)
                this.DataGrid.Ordre = this.EntryForm.Ordre;

            //
            // Filter
            //
            Attribute Filter = propertyInfo.GetCustomAttribute(typeof(FilterAttribute));
            this.Filter = Filter as FilterAttribute;

            //
            // DataSource
            //
            Attribute dataSource = propertyInfo.GetCustomAttribute(typeof(ReferencesDataSourceAttribute));
            this.DataSource = dataSource as ReferencesDataSourceAttribute;

            //
            // Criteria
            //
            Attribute SelectionCriteria_Attribute = propertyInfo.GetCustomAttribute(typeof(SelectionCriteriaAttribute));
            this.SelectionCriteria = SelectionCriteria_Attribute as SelectionCriteriaAttribute;

            //
            //BusinesRoleAttribute
            //

            Attribute aBusinesRole = propertyInfo.GetCustomAttribute(typeof(BusinesRoleAttribute));
            this.BusinesRole = aBusinesRole as BusinesRoleAttribute;


            // Determine FieldNautre
            this.FieldNature = BaseFieldTraitement.DetermineFieldNature(this);

        }




        private string GetStringFromRessource(string key, bool return_null_if_nat_exist = false)
        {
            string msg = null;
            foreach (var item in RessoucesManagers.Values)
            {
                string text;
                text = item.GetString(key, this.CultureInfo);
                if (text != null)
                {
                    msg = text;
                    break;
                }
            }

            if (msg == null && !return_null_if_nat_exist)
                msg = this.CultureInfo.Name + "_" + key;
            return msg;
        }
    }
}
