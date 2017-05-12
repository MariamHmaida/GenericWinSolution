using App.Gwin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Gwin
{
    /// <summary>
    /// Add New Entity , Shwo EntryForm 
    /// </summary>
    public partial class ManagerFormControl
    {
        private void tabControl_MainManager_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Name == "tabPageAdd")
                AddEntity_Click(this, e);
        }


        /// <summary>
        /// Add new Entity Click
        /// </summary>
        public void AddEntity_Click(object sender, EventArgs e)
        {
            // Return if Add Entity is Allready shwowen
            if (tabPageAdd.Text != "") return;

            // Show TabAdd Page
            tabPageAdd.Text = this.BLO_Instance.ConfigEntity.AddButton?.Title;
            tabControl_MainManager.CausesValidation = false;
            tabControl_MainManager.SelectedTab = tabPageAdd;

            // Create EntryForm Instance
            BaseEntity Entity = (BaseEntity)this.BLO_Instance.CreateEntityInstance();
            BaseEntryForm form = EntryForm_Instance.CreateInstance(BLO_Instance, Entity, this.Filter_Instance.GetFilterValues());
            form.Name = "Form";
            form.Dock = DockStyle.Fill;
            form.ShowEntity(this.Filter_Instance.GetFilterValues(), BaseEntryForm.EntityActions.Add);
            tabPageAdd.Controls.Add(form);
            form.EnregistrerClick += EntryFormSave_Click;
            form.AnnulerClick += CancelEntryForm_Click;
        }
        /// <summary>
        /// EntryForm Save Click
        /// </summary>
        private void EntryFormSave_Click(object sender, EventArgs e)
        {

            BaseEntryForm form = (BaseEntryForm)tabPageAdd.Controls
                .Find("Form", false).First();
            this._EndAdd();
            this.RefreshData();
        }
        /// <summary>
        /// CancelEntryForm_Click
        /// </summary>
        private void CancelEntryForm_Click(object sender, EventArgs e)
        {
            this._EndAdd();
        }

        /// <summary>
        /// End Add, And Show DataGrid
        /// </summary>
        private void _EndAdd()
        {
            tabPageAdd.Text = "";
            tabPageAdd.Controls.Clear();
            tabControl_MainManager.SelectedTab = TabGrid;

        }
    }
}
