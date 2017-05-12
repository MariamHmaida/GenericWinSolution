namespace App.Gwin.EntityManagement
{
    partial class BaseFilterControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseFilterControl));
            this.groupBoxFiltrage = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxFiltrage.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxFiltrage
            // 
            resources.ApplyResources(this.groupBoxFiltrage, "groupBoxFiltrage");
            this.groupBoxFiltrage.Controls.Add(this.flowLayoutPanel1);
            this.groupBoxFiltrage.Name = "groupBoxFiltrage";
            this.groupBoxFiltrage.TabStop = false;
            this.groupBoxFiltrage.Enter += new System.EventHandler(this.groupBoxFiltrage_Enter);
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // BaseFilterControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxFiltrage);
            this.Name = "BaseFilterControl";
            this.groupBoxFiltrage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.GroupBox groupBoxFiltrage;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
