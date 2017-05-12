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
using App.Shared.AttributesManager;
using App.Gwin.Entities;
using System.Resources;
using App.Gwin.Entities.MultiLanguage;
using App.Gwin.Application.BAL;
using App.Gwin.Components.Manager.Fields.Traitements.Params;
using App.Gwin.FieldsTraitements;
using App.Gwin.Exceptions.Gwin;
using App.Gwin.Exceptions.Helpers;
using App.Gwin.Components.Manager.DataGrid;
using App.Gwin.Application.Presentation.EntityManagement;

namespace App.Gwin
{
    /// <summary>
    /// Gwin DataGrid Component
    /// </summary>
    public partial class GwinDataGridComponent : UserControl, IGwinDataGridComponent
    {
        #region Properties
        /// <summary>
        ///  Selected Property : Selected Columns in DataGrid
        /// </summary>
        public PropertyInfo SelectedProperty { set; get; }
        /// <summary>
        /// Business object instance
        /// </summary>
        public IGwinBaseBLO EntityBLO { set; get; }
        /// <summary>
        /// Filter Values
        /// </summary>
        public Dictionary<string, object> FilterValues { set; get; }
        /// <summary>
        ///  List of Shown Entity Properties
        /// </summary>
        public List<PropertyInfo> ShownEntityProperties { set; get; }
        /// <summary>
        /// Get Selected Entity
        /// </summary>
        public BaseEntity SelectedEntity
        {
            get
            {
                return this.ObjetBindingSource.Current as BaseEntity;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Edit Click event
        /// </summary>
        public event EventHandler EditClick;
        protected void onEditClick(object sender, EventArgs e)
        {
            if (EditClick != null)
                EditClick(sender, e);
        }
        /// <summary>
        /// Edit Many to one click event
        /// </summary>
        public event EventHandler EditManyToMany_Creation;
        protected void onEditManyToMany_Creation(object sender, EventArgs e)
        {

            if (EditManyToMany_Creation != null)
                EditManyToMany_Creation(sender, e);
        }
       
        /// <summary>
        /// Refresh Event
        /// </summary>
        public event EventHandler RefreshEvent;
        protected void onRefreshEvent(object sender, EventArgs e)
        {
            RefreshEvent(sender, e);
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Create EntityDataGrid Instance
        /// </summary>
        /// <param name="EtityBLO">Business object instance</param>
        /// <param name="FilterValues">Filter values</param>
        public GwinDataGridComponent(IGwinBaseBLO EtityBLO, Dictionary<string, object> FilterValues)
        {
            CheckPramIsNull.CheckParam_is_NotNull(EtityBLO, this, nameof(EtityBLO));

            InitializeComponent();

            // Init Params values
            this.EntityBLO = EtityBLO;
            this.FilterValues = FilterValues;

            // Init ShownEntityProperties
            var requete = from i in EntityBLO.TypeEntity.GetProperties()
                          where i.GetCustomAttribute(typeof(DataGridAttribute)) != null
                          orderby ((DataGridAttribute)i.GetCustomAttribute(typeof(DataGridAttribute))).Ordre
                          select i;
            this.ShownEntityProperties = requete.ToList<PropertyInfo>();

            _Insert_Column_In_DataGrid();
        }
        /// <summary>
        /// You can not use this constuctor to create EntityDataGrid Instance
        /// It is used en to support DesinMode en Visaul Sutdio
        /// </summary>
        public GwinDataGridComponent()  {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
                throw new GwinUsageModeException("This constructor is used just for DesineMode in Visal Studio, you can not use it per Code");
            InitializeComponent();
        }
        #endregion

        #region Insert Column in DataGrid
        /// <summary>
        /// Insert Column in DataGrid
        /// </summary>
        private void _Insert_Column_In_DataGrid()
        {
            int index_colonne = 0;

            // Create Properties Columns
            foreach (PropertyInfo propertyInfo in this.ShownEntityProperties)
            {
                ConfigProperty configProperty = new ConfigProperty(propertyInfo, this.EntityBLO.ConfigEntity);

                //Insert Column according to its Type 
                DataGridViewColumn colonne = new DataGridViewTextBoxColumn(); ;
                index_colonne++;

                // Params to Creat Fields
                CreateFieldColumns_In_EntityDataGrid param = new CreateFieldColumns_In_EntityDataGrid();
                param.Column = colonne;
                param.ConfigProperty = configProperty;
                // Create FieldTraitement Instance
                IFieldTraitements fieldTraitement = BaseFieldTraitement.CreateInstance(configProperty);

                // Invok Create Column 
                fieldTraitement.ConfigFieldColumn_In_EntityDataGrid(param);

                // Insert Column in DataGriView
                this.dataGridView.Columns.Insert(index_colonne, param.Column);
            }

            // Create SelectedAction Columns in Last of columns
            if(this.EntityBLO.ConfigEntity.ListDataGridSelectedAction != null)
                foreach (DataGridSelectedActionAttribute item in this.EntityBLO.ConfigEntity.ListDataGridSelectedAction)
                {
                    index_colonne++;
                    DataGridViewButtonColumn colonne = new DataGridViewButtonColumn();

                    colonne.HeaderText = item.Title;
                    colonne.Text = item.Title;
                    colonne.Name = item.TypeOfForm.FullName;
                    colonne.Tag = item;
                    colonne.ToolTipText = item.Description;
                    colonne.UseColumnTextForButtonValue = true;
                   
                    // Insert Column in DataGriView
                    this.dataGridView.Columns.Insert(index_colonne, colonne);
                }
        }

        /// <summary>
        /// Clikc Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BaseEntity obj = (BaseEntity)ObjetBindingSource.Current;
            // Delete Clikc
            if (e.ColumnIndex == dataGridView.Columns["Supprimer"].Index && e.RowIndex >= 0)
            {
                if (DialogResult.Yes == MessageBox.Show(
                    "Voullez-vous vraimment supprimer :" + obj.ToString(),
                    "Confirmation de supprision", MessageBoxButtons.YesNo))
                {
                    this.EntityBLO.Delete(obj);
                    this.RefreshEntities();
                }
            }
            // Editer Clikc
            if (e.ColumnIndex == dataGridView.Columns["Editer"].Index && e.RowIndex >= 0)
            {
                onEditClick(this, null);
            }


            // ManytoMany_Creation Clic
            foreach (var item in this.ShownEntityProperties.Where(p => p.PropertyType.Name == "List`1"))
            {
                ConfigProperty attributesOfProperty = new ConfigProperty(item, this.EntityBLO.ConfigEntity);
                if (attributesOfProperty.Relationship?.Relation != RelationshipAttribute.Relations.ManyToMany_Creation)
                    continue;
                if (e.ColumnIndex == dataGridView.Columns[item.Name].Index && e.RowIndex >= 0)
                {
                    this.SelectedProperty = item;
                    onEditManyToMany_Creation(this, e);
                }
            }

            // ActionClikc for DataGridSeletedAction
            if (this.EntityBLO.ConfigEntity.ListDataGridSelectedAction != null)
                foreach (DataGridSelectedActionAttribute item in this.EntityBLO.ConfigEntity.ListDataGridSelectedAction)
                {
                    if (e.ColumnIndex == dataGridView.Columns[item.TypeOfForm.FullName].Index && e.RowIndex >= 0)
                    {
                        // Create TraitementAtionForm Instance
                        IFormSelectedEntityAction Form = Activator.CreateInstance(item.TypeOfForm) as IFormSelectedEntityAction;
                        Form.SetEntity(obj);

                        // Show Form In MDI Application
                        CreateAndShowManagerFormHelper ShowManagerFormHelper = new CreateAndShowManagerFormHelper(GwinApp.Instance.TypeDBContext,GwinApp.Instance.FormApplication);
                        ShowManagerFormHelper.ShwoForm(Form as Form);

                    }
                }

        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                onEditClick(this, null);
            }
        }

        #endregion


        #region Refresh
        /// <summary>
        /// Show Entity List in DataGrid, With Intial FilterValues
        /// </summary> 
        public void RefreshEntities()
        {
            this.RefreshEntities(this.FilterValues);
        }
        /// <summary>
        /// Show Entity List in DataGrid with FilterValues
        /// </summary>
        /// <param name="FilterValues">Filter Values to filter Data in GridView</param>
        public void RefreshEntities(Dictionary<string, object> FilterValues)
        {

            this.FilterValues = FilterValues;
            var ls = EntityBLO.Recherche(FilterValues);

            ObjetBindingSource.Clear();
            foreach (var item in ls)
            {
                ObjetBindingSource.Add(item);
            }


        }
        #endregion
        /// <summary>
        /// Get DataGrid View Instance 
        /// Used to Test Control
        /// </summary>
        public DataGridView GetDataGridViewInstance()
        {
            return this.dataGridView;
        }
    }
}
