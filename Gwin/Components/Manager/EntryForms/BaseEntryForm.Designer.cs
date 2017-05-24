namespace App.Gwin
{
    partial class BaseEntryForm
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseEntryForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel_form = new System.Windows.Forms.Panel();
            this.FlowLayoutContainer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btEnregistrer = new MetroFramework.Controls.MetroButton();
            this.btAnnuler = new MetroFramework.Controls.MetroButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel_form.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.CausesValidation = false;
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.CausesValidation = false;
            this.splitContainer1.Panel1.Controls.Add(this.panel_form);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.CausesValidation = false;
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.TabStop = false;
            // 
            // panel_form
            // 
            this.panel_form.Controls.Add(this.FlowLayoutContainer);
            resources.ApplyResources(this.panel_form, "panel_form");
            this.panel_form.Name = "panel_form";
            this.panel_form.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_form_Paint);
            // 
            // FlowLayoutContainer
            // 
            resources.ApplyResources(this.FlowLayoutContainer, "FlowLayoutContainer");
            this.FlowLayoutContainer.Name = "FlowLayoutContainer";
            // 
            // panel1
            // 
            this.panel1.CausesValidation = false;
            this.panel1.Controls.Add(this.btEnregistrer);
            this.panel1.Controls.Add(this.btAnnuler);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btEnregistrer
            // 
            resources.ApplyResources(this.btEnregistrer, "btEnregistrer");
            this.btEnregistrer.Name = "btEnregistrer";
            this.btEnregistrer.UseSelectable = true;
            this.btEnregistrer.Click += new System.EventHandler(this.Save_Click);
            // 
            // btAnnuler
            // 
            resources.ApplyResources(this.btAnnuler, "btAnnuler");
            this.btAnnuler.CausesValidation = false;
            this.btAnnuler.Name = "btAnnuler";
            this.btAnnuler.UseSelectable = true;
            this.btAnnuler.Click += new System.EventHandler(this.btAnnuler_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            resources.ApplyResources(this.errorProvider, "errorProvider");
            // 
            // BaseEntryForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CausesValidation = false;
            this.Controls.Add(this.splitContainer1);
            this.Name = "BaseEntryForm";
            this.Load += new System.EventHandler(this.BaseEntryForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel_form.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton btAnnuler;
        
        private System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.ErrorProvider errorProvider;
        public System.Windows.Forms.SplitContainer splitContainer1;
        public MetroFramework.Controls.MetroButton btEnregistrer;
     
        public System.Windows.Forms.Panel panel_form;
        protected System.Windows.Forms.Panel FlowLayoutContainer;
    }
}
