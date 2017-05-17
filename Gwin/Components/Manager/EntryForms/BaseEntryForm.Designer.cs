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
            this.flowLayoutPanelForm = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControlForm = new System.Windows.Forms.TabControl();
            this.tabPageForm = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btEnregistrer = new MetroFramework.Controls.MetroButton();
            this.btAnnuler = new MetroFramework.Controls.MetroButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlForm.SuspendLayout();
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
            this.splitContainer1.Panel1.CausesValidation = false;
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanelForm);
            this.splitContainer1.Panel1.Controls.Add(this.tabControlForm);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.CausesValidation = false;
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.TabStop = false;
            // 
            // flowLayoutPanelForm
            // 
            resources.ApplyResources(this.flowLayoutPanelForm, "flowLayoutPanelForm");
            this.flowLayoutPanelForm.Name = "flowLayoutPanelForm";
            // 
            // tabControlForm
            // 
            resources.ApplyResources(this.tabControlForm, "tabControlForm");
            this.tabControlForm.Controls.Add(this.tabPageForm);
            this.tabControlForm.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlForm.Multiline = true;
            this.tabControlForm.Name = "tabControlForm";
            this.tabControlForm.SelectedIndex = 0;
            this.tabControlForm.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlForm.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabPageForm
            // 
            resources.ApplyResources(this.tabPageForm, "tabPageForm");
            this.tabPageForm.Name = "tabPageForm";
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
            this.btEnregistrer.Click += new System.EventHandler(this.btEnregistrer_Click);
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
            this.tabControlForm.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton btAnnuler;
        
        private System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.ErrorProvider errorProvider;
        public System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabPageForm;
        protected System.Windows.Forms.TabControl tabControlForm;
        public MetroFramework.Controls.MetroButton btEnregistrer;
        protected System.Windows.Forms.FlowLayoutPanel flowLayoutPanelForm;
    }
}
