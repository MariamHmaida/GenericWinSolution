using App.Shared.AttributesManager;
using App.Gwin.Attributes;
using App.Gwin.Entities;
using App.Gwin.Fields.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using App.Gwin.Application.BAL;

namespace App.Gwin.Fields
{
    /// <summary>
    /// Meny to Many Field
    /// 
    /// Private Filter 
    /// It can use Private Filter to selected Data
    /// The Private filter is configured in Property
    /// </summary>
    public partial class ManyToManyField : BaseField
    {

        #region Business variables
        public IGwinBaseBLO EntityBAO { get; set; }
        /// <summary>
        /// Type of The object that use this field
        /// </summary>
        protected Type TypeOfObject { set; get; }
        public SelectionFilterManager SelectionFilterManager { set; get; }
        #endregion

        #region Gwin variables
        /// <summary>
        /// The configuration of the property
        /// </summary>
        protected ConfigProperty configProperty { get; set; }
        protected ConfigEntity ConfigEntity { get; set; }
        #endregion


        #region Set and Get Value
        /// <summary>
        /// Set or Get the SelectedITems
        /// </summary>
        public override object Value
        {
            get
            {
                return listBoxChoices.SelectedItems.Cast<BaseEntity>().ToList<BaseEntity>();
            }

            set
            {
                if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
                {
                    List<BaseEntity> ls_values = value as List<BaseEntity>;
                    // Update Filter selection
                    if (this.SelectionFilterManager != null && ls_values != null && ls_values.Count > 0)
                        this.SelectionFilterManager.Value = ls_values.First().Id;

                    // Update Value
                    foreach (var item in ls_values)
                    {
                        listBoxChoices.SelectedItems.Add(item);
                    
                    }
                }
            }
        }
        #endregion




        public ManyToManyField() : base()
        {
            InitializeComponent();
        }

        public ManyToManyField(
            PropertyInfo propertyInfo,
            Orientation OrientationField,
            Size SizeLabel,
            Size SizeControl,
            ConfigEntity ConfigEntity,
            Control MainContainer,
            IGwinBaseBLO Service)
            : base()
        {
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                // Params
                this.PropertyInfo = propertyInfo;
                this.orientationField = OrientationField;
                this.SizeLabel = SizeLabel;
                this.SizeControl = SizeControl;
                this.ConfigEntity = ConfigEntity;
                this.EntityBAO = Service;

                // Create configProperty instance
                if (PropertyInfo != null)
                    this.configProperty = new ConfigProperty(PropertyInfo, this.ConfigEntity);



                // Create Instance of PrivateFilter
                if (this.configProperty.SelectionCriteria != null)
                    this.SelectionFilterManager = new SelectionFilterManager(this.EntityBAO,
                    this.configProperty,
                    MainContainer,
                    SizeLabel, SizeControl, OrientationField,0);

                // Config Current Field : ManyToManyField
                this.Text_Label = this.configProperty.DisplayProperty.Title;

                 

                // Fill Data
                if (this.SelectionFilterManager != null)
                {
                    // The filter fill listbox data
                    this.SelectionFilterManager.ValueChanged += SelectionFilterManager_ValueChanged;
                }
                else
                {
                    // Fill the listBox data if the filed not have private filter 
                   
                    Type TypeGenericList = this.PropertyInfo.PropertyType.GetGenericArguments()[0];
                    IGwinBaseBLO ServiceTypeGenericList = this.EntityBAO.CreateServiceBLOInstanceByTypeEntity(TypeGenericList);
                    List<Object> ls_possible_value = ServiceTypeGenericList.GetAll();
                    listBoxChoices.Items.AddRange(ls_possible_value.ToArray());
                }
            }

        }

        private void SelectionFilterManager_ValueChanged(object sender, EventArgs e)
        {
            BaseEntity ValueEntity = this.SelectionFilterManager.ValueEntity;

            Type Type_ValueEntity = ObjectContext.GetObjectType(ValueEntity.GetType());
            if (ValueEntity == null) return;

            Type TypeGenericList = this.PropertyInfo.PropertyType.GetGenericArguments()[0];
            IGwinBaseBLO ServiceTypeGenericList = this.EntityBAO.CreateServiceBLOInstanceByTypeEntity(TypeGenericList);
            List<Object> ls_entity_in_filter = ServiceTypeGenericList.Recherche(
                 new Dictionary<string, object>() {
                    { Type_ValueEntity.Name, ValueEntity.Id }
                   });

            listBoxChoices.Items.AddRange(ls_entity_in_filter.ToArray());
        }




    }
}
