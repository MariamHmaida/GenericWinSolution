using App.Gwin.Application.BAL;
using App.Gwin.Exceptions.Gwin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace App.Gwin.Application.Presentation.EntityManagement
{
    /// <summary>
    /// ManageForm, CRUD operation form for Entity Framework 
    /// </summary>
    public partial class ManagerForm : BaseForm
    {
        /// <summary>
        /// ManagerFormControl Instance
        /// </summary>
        public ManagerFormControl managerFormControl;

     
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="EntityBLO">Business object instance</param>
        /// <param name="EntryFormInstance">Entry form instance</param>
        /// <param name="FilterValues">Values of Filter</param>
        /// <param name="MdiForm">Mdi form instance</param>
        public ManagerForm(
            IGwinBaseBLO EntityBLO,
            BaseEntryForm EntryFormInstance,
            Dictionary<string, object> FilterValues,
            Form MdiForm)
        {

            InitializeComponent();

            managerFormControl = new ManagerFormControl(EntityBLO,
                EntryFormInstance,
                null,null,
                FilterValues, MdiForm);
            managerFormControl.Dock = DockStyle.Fill;

            this.Name = EntityBLO.GetType().ToString();
            this.Text = EntityBLO.ConfigEntity.ManagementForm?.FormTitle;
            this.Controls.Add(managerFormControl);


            // Confirm RightToLeft
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = GwinApp.isRightToLeft;

            // Confib Width and Height
            if (EntityBLO.ConfigEntity.ManagementForm.Width != 0)
                this.Width = EntityBLO.ConfigEntity.ManagementForm.Width;
            if (EntityBLO.ConfigEntity.ManagementForm.Height != 0)
                this.Height = EntityBLO.ConfigEntity.ManagementForm.Height;

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="EntityBLO">Business object instance</param>
        /// <param name="FilterValues">Values of Filter</param>
        /// <param name="MdiForm">Mdi form instance</param>
        public ManagerForm(IGwinBaseBLO EntityBLO,
            Dictionary<string, object> FilterValues, Form MdiForm)
            :this(EntityBLO, null, FilterValues, MdiForm)
        {
        }
 

        /// <summary>
        /// You can not use this construcotor, per code
        /// </summary>
        public ManagerForm()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
                throw new GwinUsageModeException("This constructor is used just for DesineMode in Visal Studio, you can not use it per Code");
            InitializeComponent();

        }
    }
}
