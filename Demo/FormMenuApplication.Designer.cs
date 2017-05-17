namespace GenericWinForm.Demo
{
    partial class FormMenuApplication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenuApplication));
            this.btProjectManager = new MetroFramework.Controls.MetroTile();
            this.btTaskManager = new MetroFramework.Controls.MetroTile();
            this.SuspendLayout();
            // 
            // btProjectManager
            // 
            this.btProjectManager.ActiveControl = null;
            resources.ApplyResources(this.btProjectManager, "btProjectManager");
            this.btProjectManager.Name = "btProjectManager";
            this.btProjectManager.UseSelectable = true;
            this.btProjectManager.Click += new System.EventHandler(this.btProjectManager_Click);
            // 
            // btTaskManager
            // 
            this.btTaskManager.ActiveControl = null;
            resources.ApplyResources(this.btTaskManager, "btTaskManager");
            this.btTaskManager.Name = "btTaskManager";
            this.btTaskManager.UseSelectable = true;
            this.btTaskManager.Click += new System.EventHandler(this.btTaskManager_Click);
            // 
            // FormMenuApplication
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.btTaskManager);
            this.Controls.Add(this.btProjectManager);
            this.Name = "FormMenuApplication";
            this.Load += new System.EventHandler(this.FormMenuApplication_Load);
            this.Controls.SetChildIndex(this.btProjectManager, 0);
            this.Controls.SetChildIndex(this.btTaskManager, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTile btProjectManager;
        private MetroFramework.Controls.MetroTile btTaskManager;
    }
}
