namespace App.Gwin.Application.Presentation.MainForm
{
    partial class FormApplication
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormApplication));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.languageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.frenchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem1});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // languageToolStripMenuItem1
            // 
            this.languageToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frenchToolStripMenuItem1,
            this.englishToolStripMenuItem,
            this.arabToolStripMenuItem});
            this.languageToolStripMenuItem1.Name = "languageToolStripMenuItem1";
            resources.ApplyResources(this.languageToolStripMenuItem1, "languageToolStripMenuItem1");
            this.languageToolStripMenuItem1.Click += new System.EventHandler(this.languageToolStripMenuItem1_Click);
            // 
            // frenchToolStripMenuItem1
            // 
            this.frenchToolStripMenuItem1.Name = "frenchToolStripMenuItem1";
            resources.ApplyResources(this.frenchToolStripMenuItem1, "frenchToolStripMenuItem1");
            this.frenchToolStripMenuItem1.Click += new System.EventHandler(this.frenchToolStripMenuItem1_Click);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // arabToolStripMenuItem
            // 
            this.arabToolStripMenuItem.Name = "arabToolStripMenuItem";
            resources.ApplyResources(this.arabToolStripMenuItem, "arabToolStripMenuItem");
            this.arabToolStripMenuItem.Click += new System.EventHandler(this.arabToolStripMenuItem_Click);
            // 
            // FormApplication
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuStrip1);
            this.Name = "FormApplication";
            this.TransparencyKey = System.Drawing.Color.Empty;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormApplication_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem1;
        protected System.Windows.Forms.ToolStripMenuItem frenchToolStripMenuItem1;
        protected System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem arabToolStripMenuItem;
        public System.Windows.Forms.MenuStrip menuStrip1;
    }
}