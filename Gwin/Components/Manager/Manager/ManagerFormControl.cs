using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Gwin.Attributes;
using App.Gwin.EntityManagement;
using System.Reflection;
using App.Gwin.Application.BAL;

namespace App.Gwin
{
    /// <summary>
    /// Manager Form : CRUD Operation of GwinApplication with entity frameWork 
    /// </summary>
    public partial class ManagerFormControl : UserControl
    {
        /// <summary>
        /// Business Object Instance
        /// </summary>
        public IGwinBaseBLO BLO_Instance { set; get; }
        /// <summary>
        /// Parent MDI Form
        /// </summary>
        public Form MdiParent { set; get; }
        /// <summary>
        /// DefaultFilterValues to Create Filter an DataGrid componenet
        /// </summary>
        Dictionary<string, object> DefaultFilterValues { set; get; }
        /// <summary>
        /// FilterControl Instance
        /// </summary>
        public BaseFilterControl Filter_Instance { set; get; }
        /// <summary>
        /// EntryFormInstance
        /// </summary>
        protected BaseEntryForm EntryForm_Instance { set; get; }
        /// <summary>
        /// DataGridControl_Instance
        /// </summary>
        public GwinDataGridComponent DataGridControl_Instance { get; private set; }
        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="BLO">Business OBject Instance</param>
        /// <param name="EntryForm_Instance">Entry Form Instance</param>
        /// <param name="Filter_Instance">Filter Instance</param>
        /// <param name="DataGrid_Instance">DataGrid Instance</param>
        /// <param name="DefaultFilterValues">Default Filter Values</param>
        /// <param name="MdiFormParent">Mdi Parent Form</param>
        public ManagerFormControl(
            IGwinBaseBLO BLO,
            BaseEntryForm EntryForm_Instance,
            BaseFilterControl Filter_Instance,
            GwinDataGridComponent DataGrid_Instance,
            Dictionary<string, object> DefaultFilterValues,
            Form MdiFormParent)
        {
            InitializeComponent();
            // Init Properties values
            this.BLO_Instance = BLO;
            this.EntryForm_Instance = EntryForm_Instance;
            this.Filter_Instance = Filter_Instance;
            this.DataGridControl_Instance = DataGrid_Instance;
            this.DefaultFilterValues = DefaultFilterValues;
            this.MdiParent = MdiFormParent;

            // Init Main Contrainner
            this.tabControl_MainManager.Dock = DockStyle.Fill;
            this.tabControlManagers.Visible = false;
            this.panelDataGrid.Controls.Add(this.tabControl_MainManager);

            // Create Entry Form Instance
            if (this.EntryForm_Instance == null) this.EntryForm_Instance = new BaseEntryForm(this.BLO_Instance);
            
            // Create and Init filtre Instance
            if (this.Filter_Instance == null)
                this.Filter_Instance = new BaseFilterControl(this.BLO_Instance, this.DefaultFilterValues);
            this.Filter_Instance.Dock = DockStyle.Fill;
            this.panel_Filtre.Controls.Add(this.Filter_Instance);
            this.Filter_Instance.RefreshEvent += BaseFilterControl_RefreshEvent;

            // Create and Init DataGrid Instance
            if (this.DataGridControl_Instance == null)
                this.DataGridControl_Instance = new GwinDataGridComponent(this.BLO_Instance, this.DefaultFilterValues);
            this.DataGridControl_Instance.Dock = DockStyle.Fill;
            this.tabControl_MainManager.TabPages["TabGrid"].Controls.Add(this.DataGridControl_Instance);
            this.DataGridControl_Instance.EditClick += DataGridControl_EditClick;
            this.DataGridControl_Instance.EditManyToMany_Creation += DataGridControl_EditManyToOneCollection;

            // Update Titles
            this.Name = nameof(ManagerFormControl) + this.BLO_Instance.TypeEntity.ToString();
            this.Text = this.BLO_Instance.ConfigEntity.ManagementForm.FormTitle;
            this.tabPageAdd.ToolTipText = this.BLO_Instance.ConfigEntity.AddButton.Title;
            this.tabControl_MainManager.TabPages["TabGrid"].Text = this.BLO_Instance.ConfigEntity.ManagementForm.TitrePageGridView;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="BLO">Business OBject Instance</param>
        /// <param name="DefaultFilterValues">Default Filter Values</param>
        /// <param name="MdiFormParent">Mdi Parent Form</param>
        public ManagerFormControl(IGwinBaseBLO BLO,
            Dictionary<string, object> DefaultFilterValues, Form MdiFormParent)
            : this(BLO, null,null,null, DefaultFilterValues, MdiFormParent)
        { }
        /// <summary>
        /// RefreshData with filter information
        /// </summary> 
        public void RefreshData()
        {
            this.DataGridControl_Instance.RefreshEntities(this.Filter_Instance.GetFilterValues());
        }
        /// <summary>
        /// Load ManagerForm Event
        /// RefreshData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManagerFormControl_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
                this.RefreshData();

            // Change Direction of TabControlMainManager
            // Change Form Direction  - When  Languauge is changed
            if (GwinApp.Instance.CultureInfo.TwoLetterISOLanguageName == "fr" || GwinApp.Instance.CultureInfo.TwoLetterISOLanguageName == "en")
            {
                tabControl_MainManager.RightToLeftLayout = false;
                tabControl_MainManager.RightToLeft = RightToLeft.No;
            }
            else
            {
                tabControl_MainManager.RightToLeftLayout = true;
                tabControl_MainManager.RightToLeft = RightToLeft.Yes;
            }
        }
        private void EntityManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Fix Problem : Close form with cas validation 
            // bevause the form dont want be closed if the validation is active
            e.Cancel = false;
        }

        private void panelDataGrid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TabGrid_Click(object sender, EventArgs e)
        {

        }

        public void ChangeTabGridTitle(string Title)
        {
            this.tabControl_MainManager.TabPages["TabGrid"].Text = Title;
        }
    }
}
