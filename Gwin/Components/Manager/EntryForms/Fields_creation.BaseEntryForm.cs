using App.Shared.AttributesManager;
using App.Gwin.Attributes;
using App.Gwin.Fields;
using App.Gwin.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Gwin.Shared.Resources;
using System.Resources;
using App.WinForm.Fields;
using App.Gwin.FieldsTraitements;
using App.Gwin.Exceptions.Gwin;
using App.Gwin.Components.Manager.Fields.Traitements.Params;

namespace App.Gwin
{
    /// <summary>
    /// Field Creation
    /// </summary>
    public partial class BaseEntryForm
    {

        /// <summary>
        /// Field Creation
        /// </summary>
        private void CreateFieldIfNotGenerated()
        {
            // Create Field if not yet Created
            if (!this.AutoGenerateField || this.isGeneratedForm)
                return;
            this.isGeneratedForm = true;

            #region Default Positions and  Size
            int y_field = 0;
            int x_field = 0;
            int width_label = 100;
            int height_label = 10;
            int width_control = 200;
            int height_control = 25;
            int width_groueBox = 100;
            int height_groueBox = 200; // il ne sera pas utilisé 
            Orientation orientation = Orientation.Vertical;
            #endregion

            // Init interface with TabControl 
            this.InitTabPageInterface();

            // Create GroupBoxes
            Dictionary<string, Control> GroupesBoxMainContainers = new Dictionary<string, Control>();
            this.CreateGroupesBoxes(GroupesBoxMainContainers, width_groueBox, height_groueBox);

            // L'index de la touche Entrer
            int TabIndex = 0;

            // Get Properties to show in Entry Form
            var listeProprite = from i in this.EntityBLO.TypeEntity.GetProperties()
                                where i.GetCustomAttribute(typeof(EntryFormAttribute)) != null
                                orderby ((EntryFormAttribute)i.GetCustomAttribute(typeof(EntryFormAttribute))).Ordre
                                select i;

            // Create Field per Properites
            foreach (PropertyInfo item in listeProprite)
            {

                ConfigProperty configProperty = new ConfigProperty(item, this.ConfigEntity);

                // Field Size
                int width_control_config = width_control;
                if (configProperty.EntryForm?.WidthControl != 0)
                    width_control_config = configProperty.EntryForm.WidthControl;

                // Orientation
                Orientation orientation_config = orientation;
                if (configProperty.EntryForm?.UseOrientationField == true)
                    orientation_config = configProperty.EntryForm.OrientationField;

                // FieldContainner
                Control FieldContainner = this.ConteneurFormulaire;
                if (configProperty.EntryForm?.GroupeBox != null && configProperty.EntryForm?.GroupeBox != string.Empty)
                    FieldContainner = GroupesBoxMainContainers[configProperty.EntryForm?.GroupeBox];

                BaseField baseField = null;

                // Params to Create Fields
                CreateFieldParams param = new CreateFieldParams();
                param.PropertyInfo = item;
                param.Location = new System.Drawing.Point(x_field, y_field);
                param.OrientationField = orientation_config;
                param.SizeLabel = new Size(width_label, height_label);
                param.SizeControl = new Size(width_control_config, height_control);
                param.ConfigProperty = configProperty;
                param.TabIndex = ++TabIndex;
                param.EntityBLO = this.EntityBLO; //  used per ManyToOne Field
                param.TabControlForm = this.tabControlForm; //  used per ManyToMany Field
                param.Entity = this.Entity;
                param.ConteneurFormulaire = FieldContainner;
                param.errorProvider = errorProvider;

                // Create FieldTraitement Instance
                IFieldTraitements fieldTraitement = BaseFieldTraitement.CreateInstance(configProperty);

                // Invok Create Field Method
                baseField = fieldTraitement.CreateField_In_EntryForm(param);

                // Create Value Changed to Apply Business Role
                if (configProperty.BusinesRole != null)
                    baseField.ValueChanged += ControlPropriete_ValueChanged;

                // [Bug] Validation per FieldNature
                if (configProperty.EntryForm?.isRequired == true)
                {
                    baseField.ValidatingField += textBoxString_Validating;
                    GwinApp.Instance.Theme.RequiredField(baseField);
                }
                    

            }// Fin de for

            // TabControl for Save and Cancel button
            this.btEnregistrer.TabIndex = ++TabIndex;
            this.btAnnuler.TabIndex = ++TabIndex;

            // GroupeBox Style
            foreach (GroupBox item in this.ConteneurFormulaire.Controls.Cast<Control>().Where(c => c.GetType() == typeof(GroupBox))){
                // item.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
                item.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            }
            foreach (FlowLayoutPanel item in GroupesBoxMainContainers.Values){
                item.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            }
        }


        /// <summary>
        /// Create Group Box
        /// </summary>
        /// <param name="groupesBoxMainContainers"></param>
        private void CreateGroupesBoxes(Dictionary<string, Control> groupesBoxMainContainers, int width, int height)
        {
            // Determine a list of groupe box
            var listeProprite = from i in this.EntityBLO.TypeEntity.GetProperties()
                                where i.GetCustomAttribute(typeof(EntryFormAttribute)) != null
                                && ((EntryFormAttribute)i.GetCustomAttribute(typeof(EntryFormAttribute))).GroupeBox != string.Empty
                                 && ((EntryFormAttribute)i.GetCustomAttribute(typeof(EntryFormAttribute))).GroupeBox != null
                                orderby ((EntryFormAttribute)i.GetCustomAttribute(typeof(EntryFormAttribute))).GroupeBoxOrder
                                select ((EntryFormAttribute)i.GetCustomAttribute(typeof(EntryFormAttribute))).GroupeBox;


            if (listeProprite.Distinct().Count() > 0)
            {
                foreach (var item in listeProprite.Distinct())
                {
                    //
                    // CombBox
                    //
                    GroupBox groupeBox = new GroupBox();
                    groupeBox.Text = ConfigEntity.Translate(item);
                    groupeBox.AutoSize = true;
                    groupeBox.Size = new Size(width, height);
                    groupeBox.Padding = new Padding(20);
                    this.ConteneurFormulaire.Controls.Add(groupeBox);


                    // FlowLayout
                    FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                    flowLayoutPanel.Dock = DockStyle.Fill;
                    flowLayoutPanel.AutoSize = true;
                    flowLayoutPanel.FlowDirection = FlowDirection.TopDown;

                    groupeBox.Controls.Add(flowLayoutPanel);
                    groupesBoxMainContainers[item] = flowLayoutPanel;


                }
            }
            else
            {
                GroupBox groupeBox = new GroupBox();
                groupeBox.Text = this.ConfigEntity.DisplayEntity.SingularName;
                groupeBox.AutoSize = true;
                groupeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));

                //groupeBox.Size = new Size(width, height);
                this.ConteneurFormulaire.Controls.Add(groupeBox);


                // FlowLayout
                FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                flowLayoutPanel.Dock = DockStyle.Fill;
                flowLayoutPanel.AutoSize = true;
                flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
                flowLayoutPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));

                groupeBox.Controls.Add(flowLayoutPanel);

                this.ConteneurFormulaire.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));

                this.ConteneurFormulaire = flowLayoutPanel;
            }

        }

        /// <summary>
        /// Préparation de conteneurs pour une interface qui contient une relation 
        /// ManytoMany
        /// </summary>
        private void InitTabPageInterface()
        {
            var listeProprite = from i in this.EntityBLO.TypeEntity.GetProperties()
                                where i.GetCustomAttribute(typeof(EntryFormAttribute)) != null
                                && ((EntryFormAttribute)i.GetCustomAttribute(typeof(EntryFormAttribute))).TabPage
                                select i;

            // Si l'interface contient des Relation ManyToMany avec TabPage = true
            if (listeProprite.Count() > 0)
            {

                flowLayoutPanelForm.Parent.Controls.Remove(flowLayoutPanelForm);
                flowLayoutPanelForm.Dock = DockStyle.Fill;
                tabControlForm.TabPages["TabPageForm"].Controls.Add(flowLayoutPanelForm);
                tabControlForm.Dock = DockStyle.Fill;
                tabControlForm.TabPages["TabPageForm"].Text = this.ConfigEntity.DisplayEntity.SingularName;

            }
            // Si l'interface ne contient pas des relation ManyToMany
            else
            {
                tabControlForm.Parent.Controls.Remove(tabControlForm);
            }

            flowLayoutPanelForm.Dock = DockStyle.Fill;
            flowLayoutPanelForm.Padding = new Padding(10);

        }

        /// <summary>
        /// Exécuter aprés le changement de chaque propriété 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ControlPropriete_ValueChanged(object sender, EventArgs e)
        {
            if (!this.isStepInitializingValues)
            {
                BaseField field = sender as BaseField;
                // Lecture informations
                this.ReadEntity();
                this.EntityBLO.ApplyBusinessRolesAfterValuesChanged(field.Name, this.Entity);
                this.isStepInitializingValues = true;
                this.ShowEntity();
                this.isStepInitializingValues = false;
                // Re-Initialisation des valeurs
            }
        }

        #region Validation
        protected void ComboBox_Validating(object sender, CancelEventArgs e)
        {
            // déja le combobox propose le premiere élément séléctioné
        }

       

        protected void TextBoxInt32_Validating(object sender, CancelEventArgs e)
        {
            this.MessageValidation.TextBoxInt32(sender, e);
        }

        protected void textBoxString_Validating(object sender, CancelEventArgs e)
        {
            this.MessageValidation.TextBoxString(sender, e);
        }
        #endregion

    }
}
