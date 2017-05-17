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
    /// The Private filter is configured in Entity Classe
    /// </summary>
    public partial class ManyToManyField : BaseField
    {
        #region public Properties
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
                List<BaseEntity> ls_values = value as List<BaseEntity>;
                // Update Filter selection
                if (this.SelectionFilterManager.isHasFilter && ls_values != null && ls_values.Count > 0)
                    this.SelectionFilterManager.Value = ls_values.First().Id;

                // Update Value
                foreach (var item in ls_values)
                {
                    listBoxChoices.SelectedItems.Add(item);
                }
            }
        }
        #endregion

        #region Private Properties
        public IGwinBaseBLO EntityBAO { get; set; }
        /// <summary>
        /// Type of The object that use this field
        /// </summary>
        protected Type TypeOfObject { set; get; }
        /// <summary>
        /// Meta information of the Field
        /// </summary>
        protected PropertyInfo PropertyInfo { set; get; }
        /// <summary>
        /// The configuration of the property
        /// </summary>
        protected ConfigProperty configProperty { get; set; }
        protected ConfigEntity ConfigEntity { get; set; }

        private SelectionFilterManager SelectionFilterManager { set; get; }
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
            Control MainContainer, IGwinBaseBLO Service)
            : base()
        {
            InitializeComponent();

            // Params
            this.PropertyInfo = propertyInfo;
            this.orientationField = OrientationField;
            this.SizeLabel = SizeLabel;
            this.SizeControl = SizeControl;
            this.ConfigEntity = ConfigEntity;
            this.EntityBAO = Service;
            
            // Test Label
            

            // Create Instance of PropertyInfo
            if (PropertyInfo != null)
                this.configProperty = new ConfigProperty(PropertyInfo, this.ConfigEntity);

            this.Text_Label = this.configProperty.DisplayProperty.Title;

            // Create Instance of PrivateFilter
            this.SelectionFilterManager = new SelectionFilterManager(this.EntityBAO,
                this.PropertyInfo,
                MainContainer,
                SizeLabel, SizeControl, OrientationField, ConfigEntity);

            // Fill Listbox Data
            if (this.SelectionFilterManager.isHasFilter)
            {
                // The filter fill listbox data
                this.SelectionFilterManager.ValueChanged += SelectionFilterManager_ValueChanged;
            }else
            {
                // Fill the listBox data if the filed not have private filter 
                Type TypeGenericList = this.PropertyInfo.PropertyType.GetGenericArguments()[0];
                IGwinBaseBLO ServiceTypeGenericList = this.EntityBAO.CreateServiceBLOInstanceByTypeEntity(TypeGenericList);
                List<Object> ls_possible_value = ServiceTypeGenericList.GetAll();
                listBoxChoices.Items.AddRange(ls_possible_value.ToArray());
                ChangeSizeListBox(listBoxChoices.Items.Count);
            }
           

        }

        /// <summary>
        /// Cange the size of LisBox
        /// </summary>
        /// <param name="ItemNumber">Number of Items in ListBox</param>
        private void ChangeSizeListBox(int ItemNumber)
        {
            // Height - min = 60
            int height_control = 20 * listBoxChoices.Items.Count;
            if (height_control == 0) height_control = 60; 

            // Change Size of Field
            this.SizeControl = new Size(this.SizeControl.Width, height_control);
            this.CallConfigSizeField();
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
            this.ChangeSizeListBox(listBoxChoices.Items.Count);
        }

        


    }
}
