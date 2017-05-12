using App.Shared.AttributesManager;
using App.Gwin.Attributes;
using App.Gwin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Gwin.Application.BAL;
using System.ComponentModel;
using System.Diagnostics;
using App.Gwin.Entities.Resources.Glossary;

namespace App.Gwin
{
    /// <summary>
    /// DataGridControl Events
    /// </summary>
    public partial class ManagerFormControl
    {

        /// <summary>
        /// DataGridControl_EditClick event
        /// </summary>
        private void DataGridControl_EditClick(object sender, EventArgs e)
        {
            BaseEntity entity = (BaseEntity)this.DataGridControl_Instance.SelectedEntity;
            string tabEditerName = "TabEditer-" + entity.Id;

            if (tabControl_MainManager.TabPages.IndexOfKey(tabEditerName) == -1)
            {
                // Creation of Edit Tab page
                TabPage tabEditer = new TabPage();
                tabEditer.Text = Glossary.Update + " : " + entity.ToString();
                tabEditer.Name = tabEditerName;
                tabEditer.Font = this.tabControl_MainManager.TabPages["TabGrid"].Font;
                tabControl_MainManager.TabPages.Add(tabEditer);
                tabControl_MainManager.CausesValidation = false;
                // Creation of EntryForm
                BaseEntryForm form = EntryForm_Instance.CreateInstance(this.BLO_Instance, entity, null);
                form.Name = "EntityForm";
                form.Dock = DockStyle.Fill;

                this.tabControl_MainManager.TabPages[tabEditerName].Controls.Add(form);
                tabControl_MainManager.SelectedTab = tabEditer;
                form.ShowEntity(this.Filter_Instance.GetFilterValues(), BaseEntryForm.EntityActions.Update);
                // Entry Form Events
                form.EnregistrerClick += Form_EditerClick;
                form.AnnulerClick += Form_AnnulerEditerClick;

            }
            else
            {
                TabPage tabEditer = this.tabControl_MainManager.TabPages[tabEditerName];
                tabControl_MainManager.SelectedTab = tabEditer;
            }
        }

        /// <summary>
        /// Edit the collection ManyToMany_Creation
        /// Create Instead Manager form of ManyToMany_Creation collection
        /// </summary>
        private void DataGridControl_EditManyToOneCollection(object sender, EventArgs e)
        {
            // Init Params
            BaseEntity obj = this.DataGridControl_Instance.SelectedEntity;
            PropertyInfo propertyInfo = this.DataGridControl_Instance.SelectedProperty;

            // Cancel if allready in edition
            if (tabControlManagers.TabPages.ContainsKey(obj + propertyInfo.Name))
            {
                tabControlManagers.SelectedTab = tabControlManagers.TabPages[obj + propertyInfo.Name];
                return;
            }

            // Create Business object of the collection
            Type type_objet_of_collection = propertyInfo.PropertyType.GetGenericArguments()[0];
            IGwinBaseBLO service_objet_of_collection = this.BLO_Instance.CreateServiceBLOInstanceByTypeEntity(type_objet_of_collection);

            // Default filter values
            Dictionary<string, object> ValeursFiltre = new Dictionary<string, object>();
            ValeursFiltre[propertyInfo.DeclaringType.Name] = obj.Id;

            // Create ManagerFormControl Instance
            ManagerFormControl form = new ManagerFormControl(service_objet_of_collection, ValeursFiltre, this.MdiParent);

            ConfigEntity configEntity = ConfigEntity.CreateConfigEntity(propertyInfo.DeclaringType);

            string formTitle = Glossary.Update + " : ";
            formTitle += new ConfigProperty(propertyInfo, configEntity).DisplayProperty.Titre; // Entity
            formTitle += " " + Glossary.For + " ";
            formTitle += obj;
            form.ChangeTabGridTitle(formTitle);
            // Not Show In RunTume Mode
            //if (!Debugger.IsAttached)
                form.ShowFilter(false);


            // Insertion de la gestion à l'interface
            this.AddManyToOneManager(form, propertyInfo, obj);

        }

        /// <summary>
        /// Ajouter une gestion ManyToOne à l'interface
        /// </summary>
        /// <param name="form"></param>
        private void AddManyToOneManager(ManagerFormControl form, PropertyInfo item, BaseEntity obj)
        {

            // Annotation de l'propriété
            DisplayPropertyAttribute affichageProperty = new ConfigProperty(item, this.BLO_Instance.ConfigEntity)
                .DisplayProperty;


            // Préparation de l'interface s'il n'est pas encors préparer
            if (this.tabControlManagers.Visible == false)
            {
                this.tabControlManagers.Visible = true;
                this.tabControl_MainManager.Dock = DockStyle.Fill;
                this.tabControlManagers.TabPages["main"].Text = this.BLO_Instance.ConfigEntity.ManagementForm.FormTitle;
                this.tabControlManagers.TabPages["main"].Controls.Add(this.tabControl_MainManager);
                this.tabControlManagers.Dock = DockStyle.Fill;
                this.panelDataGrid.Controls.Add(this.tabControlManagers);
            }

            // Création d'une TabPage dans TabControlManagers
            TabPage TabPageManyToOne = new TabPage();
            TabPageManyToOne.Name = obj + item.Name;
            TabPageManyToOne.Text = affichageProperty.Titre + " : " + obj;

            this.tabControlManagers.TabPages.Add(TabPageManyToOne);

            // Insertion du formulaire
            form.Dock = DockStyle.Fill;
            TabPageManyToOne.Controls.Add(form);
            this.tabControlManagers.SelectedTab = TabPageManyToOne;

        }





    }
}
