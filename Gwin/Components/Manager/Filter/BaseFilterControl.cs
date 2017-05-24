using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using App.Gwin.Attributes;
using App.Gwin.Fields;
using App.Shared.AttributesManager;
using App.Gwin.Entities;
using System.Resources;
using App.Gwin.Shared.Resources;
using App.Components.Fields;
using System.Collections;
using App.Gwin.Components.Manager.Fields.Traitements.Params;
using App.Gwin.FieldsTraitements;
using App.Gwin.Exceptions.Gwin;
using App.Gwin.Application.BAL;

namespace App.Gwin.EntityManagement
{
    /// <summary>
    /// Filter control, it is used in 
    /// Management Interface
    /// </summary>
    public partial class BaseFilterControl : UserControl, IBaseFilterControl
    {

        #region Private Properties
        /// <summary>
        /// Get or Set the Filter Values
        /// The filter value can be of several types: String, Int, DateTime
        /// Return Dictionary String, Object
        /// String : the name of entity property
        /// Object : the value of field
        /// </summary>
        public Dictionary<string, object> DefaultFilterValues { set; get; }

        /// <summary>
        /// BLO Object   
        /// </summary>
        protected IGwinBaseBLO BLO { set; get; }


        /// <summary>
        /// The main filter interface container 
        /// </summary>
        protected Control FilterContainer { set; get; }

        /// <summary>
        /// Config Entity Instance
        /// </summary>
        protected ConfigEntity ConfigEntity { get; set; }

        #endregion

        #region Evénement
        /// <summary>
        /// Fired when One field is changed
        /// </summary>
        public event EventHandler RefreshEvent;
        protected void onRefreshEvent(object sender, EventArgs e)
        {
            if (RefreshEvent != null)
                RefreshEvent(sender, e);
        }
        #endregion

        #region Constructor
        public BaseFilterControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// BaseFilterControl Constructor
        /// </summary>
        /// <param name="BAO">Entity Business Object</param>
        /// <param name="ValeursFiltre"></param>
        public BaseFilterControl(IGwinBaseBLO BAO, Dictionary<string, object> ValeursFiltre)
        {
            InitializeComponent();


           
            if (GwinApp.isRightToLeft)
            {
                this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
                this.groupBoxFiltrage.RightToLeft = RightToLeft.Yes;
            }

            else
            {
                this.flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
                this.groupBoxFiltrage.RightToLeft = RightToLeft.No;
            }
                


            this.FilterContainer = this.flowLayoutPanel1;
            this.BLO = BAO;
            this.DefaultFilterValues = ValeursFiltre;
            this.ConfigEntity = ConfigEntity.CreateConfigEntity(this.BLO.TypeEntity);
            CreatFiltre();
        }

        public BaseFilterControl(IGwinBaseBLO Service) : this(Service, null)
        {
        }

        #endregion

        #region InitializeFilter

        /// <summary>
        /// Initialisation de filtre 
        /// </summary>
        protected void CreatFiltre()
        {
            // Default Size and Positions
            int width_label = 100;
            int height_label = 25;
            int width_control = 100;
            int height_control = 25;

            // List of Properties must be shown in filter
            var propertyListFilter = from i in BLO.TypeEntity.GetProperties()
                                     where i.GetCustomAttribute(typeof(FilterAttribute)) != null
                                     orderby ((FilterAttribute)i.GetCustomAttribute(typeof(FilterAttribute))).Ordre
                                     select i;


            // Create Field in filter
            foreach (PropertyInfo propertyInfo in propertyListFilter)
            {
                // Config Property Instance
                ConfigProperty configProperty = new ConfigProperty(propertyInfo, this.ConfigEntity);

                // WidthControl
                int item_width_control = width_control;
                if (configProperty.Filter?.WidthControl != 0)
                    item_width_control = configProperty.Filter.WidthControl;

                // Params to Create Fields
                CreateField_In_Filter_Params param = new CreateField_In_Filter_Params();
                param.ConfigProperty = configProperty;
                param.SizeLabel = new Size(width_label, height_label);
                param.SizeControl = new Size(item_width_control, height_control);
                param.TabIndex = ++TabIndex;
                param.FilterContainer = FilterContainer;
                param.DefaultFilterValues = DefaultFilterValues;
                param.EntityBLO = BLO;
               
                // Create FieldTraitement Instance
                IFieldTraitements fieldTraitement = BaseFieldTraitement.CreateInstance(configProperty);

                BaseField baseField = null;
                // Invok Create Field in filter Method
                baseField = fieldTraitement.CreateField_In_Filter(param);
                baseField.ValueChanged += Filtre_SelectedValueChanged;

            } // End For
        }

        /// <summary>
        /// Evénement SelectValueChange de ComboBoxs des Propriétés avec Relation :MnayToOne
        /// </summary>
        private void Filtre_SelectedValueChanged(object sender, EventArgs e)
        {
            onRefreshEvent(sender, e);
        }
        //private void Filtre_ComboBox_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    foreach (var item in this.groupBoxFiltrage.Controls.OfType<TextBox>())
        //    {
        //        item.Text = "";
        //    }
        //    onRefreshEvent(sender, e);
        //}


        #endregion

        #region Read & Write
        /// <summary>
        /// Get Filter value
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetFilterValues()
        {
            // Filter value
            Dictionary<string, object> FilterValues = new Dictionary<string, object>();

            // List of Property shwon in Filter
            var PropertyListFilter = from i in BLO.TypeEntity.GetProperties()
                                     where i.GetCustomAttribute(typeof(FilterAttribute)) != null
                                     orderby ((FilterAttribute)i.GetCustomAttribute(typeof(FilterAttribute))).Ordre
                                     select i;

            // Read Values from Fields
            foreach (PropertyInfo propertyInfo in PropertyListFilter)
            {
                ConfigProperty configProperty = new ConfigProperty(propertyInfo, this.ConfigEntity);

                IFieldTraitements fieldTraiement = BaseFieldTraitement.CreateInstance(configProperty);

                object value = fieldTraiement.GetFieldValue_From_Filter(FilterContainer, configProperty);

                if (value != null)
                    FilterValues[propertyInfo.Name] = value;
            }

            return FilterValues;
        }

        #endregion

        private void groupBoxFiltrage_Enter(object sender, EventArgs e)
        {

        }
    }
}
