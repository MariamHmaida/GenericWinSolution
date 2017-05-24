namespace App.Gwin.Components.Manager.EntryForms.TabEntryForm
{
    partial class TabEntryForm
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlForm = new System.Windows.Forms.TabControl();
            this.tabPageForm = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel_form.SuspendLayout();
            this.FlowLayoutContainer.SuspendLayout();
            this.tabControlForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // FlowLayoutContainer
            // 
            this.FlowLayoutContainer.Controls.Add(this.tabControlForm);
            this.FlowLayoutContainer.Controls.SetChildIndex(this.tabControlForm, 0);
            // 
            // tabControlForm
            // 
            this.tabControlForm.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlForm.Controls.Add(this.tabPageForm);
            this.tabControlForm.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlForm.ItemSize = new System.Drawing.Size(30, 70);
            this.tabControlForm.Location = new System.Drawing.Point(336, 99);
            this.tabControlForm.Multiline = true;
            this.tabControlForm.Name = "tabControlForm";
            this.tabControlForm.SelectedIndex = 0;
            this.tabControlForm.Size = new System.Drawing.Size(261, 156);
            this.tabControlForm.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlForm.TabIndex = 5;
            // 
            // tabPageForm
            // 
            this.tabPageForm.Location = new System.Drawing.Point(74, 4);
            this.tabPageForm.Name = "tabPageForm";
            this.tabPageForm.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageForm.Size = new System.Drawing.Size(183, 148);
            this.tabPageForm.TabIndex = 0;
            this.tabPageForm.Text = "tabPage1";
            // 
            // TabEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "TabEntryForm";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel_form.ResumeLayout(false);
            this.FlowLayoutContainer.ResumeLayout(false);
            this.tabControlForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl tabControlForm;
        private System.Windows.Forms.TabPage tabPageForm;
    }
}
