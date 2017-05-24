using App.Shared.AttributesManager;
using App.Gwin.Attributes;
using App.Gwin.EntityManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using App.Gwin.Application.BAL;
using System.Linq;
using App.Gwin.Entities;
using App.Gwin.Fields.Controls;
using System.Data.Entity.Core.Objects;

namespace App.Gwin.Fields
{
    /// <summary>
    /// ManyToOne Field : Implemented as ComboBox and DataFilter
    /// Filter is implemeted as List of comboBoxes names as FilterCombo
    /// </summary>
    public partial class ManyToOneField : BaseField
    {

        #region Presentation Variables
        /// <summary>
        ///  Field container , used to add DataFilter
        /// </summary>
        public Control MainContainner { set; get; }
        #endregion

        #region Business Variables
        private IGwinBaseBLO Service { set; get; }
        public SelectionFilterManager SelectionFilterManager { set; get; }
        /// <summary>
        /// Type of The object that use this field
        /// </summary>
        protected Type TypeOfObject { set; get; }
        /// <summary>
        /// Default value from Application Filter
        /// </summary>
        Int64 DefaultValue { set; get; }
        /// <summary>
        /// Entity Instance
        /// </summary>
        public BaseEntity EntityInstance { get; set; }
        #endregion

        #region WorkFlow
        /// <summary>
        /// Stop execute ValueCange of DataFilter
        /// </summary>
        public bool StopEventSelectedIndexChange { get; set; }

        /// <summary>
        /// Indicate if the Field is used as FilterItem
        /// </summary>
        public bool isFilterItem { set; get; }
        #endregion

        #region Gwin Variables
        /// <summary>
        /// The configuration of the property
        /// </summary>
        public ConfigProperty ConfigProperty { get; set; }
        public ConfigEntity ConfigEntity { get; set; }
        #endregion


        #region ComboBox Properties

        public string ValueMember
        {
            get { return this.comboBoxManyToOne.ValueMember; }
            set { this.comboBoxManyToOne.ValueMember = value; }
        }
        public string DisplayMember
        {
            get { return this.comboBoxManyToOne.DisplayMember; }
            set { this.comboBoxManyToOne.DisplayMember = value; }
        }

        public object DataSource
        {
            get { return this.comboBoxManyToOne.DataSource; }
            set { this.comboBoxManyToOne.DataSource = value; }
        }

        public int SelectedIndex
        {
            get { return this.comboBoxManyToOne.SelectedIndex; }
            set { this.comboBoxManyToOne.SelectedIndex = value; }
        }

        public Object SelectedValue
        {
            get { return this.comboBoxManyToOne.SelectedValue; }
            set { this.comboBoxManyToOne.SelectedValue = value; }
        }

        public object SelectedItem
        {
            get { return this.comboBoxManyToOne.SelectedItem; }
        }


        public string TextCombobox
        {
            get { return this.comboBoxManyToOne.Text; }
            set { this.comboBoxManyToOne.Text = value; }
        }
        #endregion



        #region Set and Get Values
        /// <summary>
        /// Get ComboBox Selected Value
        /// </summary>
        public override Object Value
        {
            get
            {
                return comboBoxManyToOne.SelectedValue;
            }
            set
            {
               if (this.SelectionFilterManager != null)
                    this.SelectionFilterManager.Value = (Int64)value;
                this.SelectedValue = (Int64)value;
            }
        }
        #endregion


        #region Events
        private void comboBoxManyToOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            onValueChanged(this, e);
        }
        #endregion

        #region Constructeurs

        /// <summary>
        /// ManyTo One Constructor
        /// </summary>
        /// <param name="Service">BLO Instance</param>
        /// <param name="TypeObjet">Type of Object en Entity</param>
        /// <param name="propertyInfo">PropertyInfo instance</param>
        /// <param name="MainContainner">ManyToOne containner</param>
        /// <param name="OrientationFiled">Filed orientation</param>
        /// <param name="SizeLabel">Label size</param>
        /// <param name="SizeControl">Control size</param>
        /// <param name="DefaultValue">Default value from Application Filter</param>
        /// <param name="ConfigEntity">Config entity instance</param>
        public ManyToOneField(IGwinBaseBLO Service,
            Type TypeObjet,
            PropertyInfo propertyInfo,
            Control MainContainner,
            Orientation OrientationFiled,
            Size SizeLabel,
            Size SizeControl,
            Int64 DefaultValue,
            ConfigEntity ConfigEntity,
            BaseEntity EntityInstance)
            : base()
        {
            InitializeComponent();

            // Test to support Designe Mode
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                // Params
                this.TypeOfObject = TypeObjet;
                this.PropertyInfo = propertyInfo;
                this.orientationField = OrientationFiled;
                this.SizeLabel = SizeLabel;
                this.SizeControl = SizeControl;
                this.ConfigEntity = ConfigEntity;
                this.MainContainner = MainContainner;
                this.Service = Service;
                this.EntityInstance = EntityInstance;
                this.DefaultValue = DefaultValue;

                // if PropertyInfo is null that mean the field is used as filter item
                if (PropertyInfo == null)
                    this.isFilterItem = true;

                // Type of Object 
                if (this.TypeOfObject == null && this.PropertyInfo != null)
                    this.TypeOfObject = this.PropertyInfo.PropertyType;

                // Create configProperty instance
                if (PropertyInfo != null)
                    this.ConfigProperty = new ConfigProperty(PropertyInfo, this.ConfigEntity);


                // Create Instance of PrivateFilter
                if (!this.isFilterItem && this.ConfigProperty.SelectionCriteria != null)
                    this.SelectionFilterManager = new SelectionFilterManager(this.Service,
                    this.ConfigProperty,
                    this.MainContainner,
                    this.SizeLabel, this.SizeControl, this.OrientationField, this.DefaultValue);


                // Config Current Field : ManyToOneField 
                this.DisplayMember = this.ConfigEntity.GwinEntity.DisplayMember;
                this.ValueMember = "Id";
                this.Text_Label = this.ConfigEntity.GwinEntity.SingularName;
                // this.ValueChanged += Value_SelectedIndexChanged;


                // Fill Data
                if (this.SelectionFilterManager != null)
                {
                    // The filter fill listbox data
                    this.SelectionFilterManager.ValueChanged += SelectionFilterManager_ValueChanged;
                }
                else
                {
                    if (!isFilterItem)
                    {
                        // Fill the ComboBox data if the field not have private filter 
                        // And DataSource is null
                        if (this.DataSource == null)
                        {
                            IGwinBaseBLO FieldBLO = this.Service.CreateServiceBLOInstanceByTypeEntity(this.PropertyInfo.PropertyType);
                            List<Object> ls_data = FieldBLO.GetAll();

                            // Add Empty Data
                            
                            if (this.ConfigProperty?.EntryForm?.isDefaultIsEmpty == true || this.ConfigProperty?.Filter?.isDefaultIsEmpty == true)
                                ls_data.Insert(0, new EmptyEntity());


                            this.DataSource = ls_data;
                        }
                           
                    }

                }

            }
        }



        public ManyToOneField(IGwinBaseBLO Service,
            PropertyInfo propertyInfo,
            Control MainContainner,
            Orientation OrientationFiled,
            Size SizeLabel, Size SizeControl,
            Int64 DefaultFiltreValues,
            ConfigEntity ConfigEntity, BaseEntity EntityInstance)
          : this(Service, null, propertyInfo, MainContainner, OrientationFiled, SizeLabel, SizeControl, DefaultFiltreValues, ConfigEntity, EntityInstance)
        { }

        private ManyToOneField(IGwinBaseBLO Service, Type TypeObjet, ConfigEntity ConfigEntity, BaseEntity EntityInstance)
           : this(Service, TypeObjet, null, null, Orientation.Horizontal, new Size(50, 20), new Size(50, 20), 0, ConfigEntity, EntityInstance)
        {

        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ManyToOneField()
        {
            InitializeComponent();
            this.SizeLabel = new Size(50, 20);
            this.SizeControl = new Size(50, 20);
            this.orientationField = Orientation.Horizontal;
        }
        #endregion

        private void SelectionFilterManager_ValueChanged(object sender, EventArgs e)
        {
            BaseEntity ValueEntity = this.SelectionFilterManager.ValueEntity;

            Type Type_ValueEntity = ObjectContext.GetObjectType(ValueEntity.GetType());
            if (ValueEntity == null) return;


            IGwinBaseBLO FieldBLO = this.Service.CreateServiceBLOInstanceByTypeEntity(this.PropertyInfo.PropertyType);
            List<Object> ls_data = FieldBLO.Recherche(
                 new Dictionary<string, object>() {
                    { Type_ValueEntity.Name, ValueEntity.Id }
                   });
            this.DataSource = ls_data;
        }

    }
}
