namespace App.Gwin
{
    partial class ManagerFormControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerFormControl));
            this.panelDataGrid = new System.Windows.Forms.Panel();
            this.splitContainerDataGridAction = new System.Windows.Forms.SplitContainer();
            this.groupBoxDataGrid = new System.Windows.Forms.GroupBox();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.tabControl_MainManager = new MetroFramework.Controls.MetroTabControl();
            this.TabGrid = new MetroFramework.Controls.MetroTabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel_Filtre = new System.Windows.Forms.Panel();
            this.tabPageAdd = new MetroFramework.Controls.MetroTabPage();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panelDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDataGridAction)).BeginInit();
            this.splitContainerDataGridAction.Panel1.SuspendLayout();
            this.splitContainerDataGridAction.Panel2.SuspendLayout();
            this.splitContainerDataGridAction.SuspendLayout();
            this.tabControl_MainManager.SuspendLayout();
            this.TabGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDataGrid
            // 
            this.panelDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDataGrid.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelDataGrid.Controls.Add(this.splitContainerDataGridAction);
            this.panelDataGrid.Location = new System.Drawing.Point(3, 3);
            this.panelDataGrid.Name = "panelDataGrid";
            this.panelDataGrid.Size = new System.Drawing.Size(767, 355);
            this.panelDataGrid.TabIndex = 17;
            this.panelDataGrid.Tag = "";
            this.panelDataGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDataGrid_Paint);
            // 
            // splitContainerDataGridAction
            // 
            this.splitContainerDataGridAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerDataGridAction.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerDataGridAction.Location = new System.Drawing.Point(0, 0);
            this.splitContainerDataGridAction.Name = "splitContainerDataGridAction";
            this.splitContainerDataGridAction.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerDataGridAction.Panel1
            // 
            this.splitContainerDataGridAction.Panel1.Controls.Add(this.groupBoxDataGrid);
            // 
            // splitContainerDataGridAction.Panel2
            // 
            this.splitContainerDataGridAction.Panel2.Controls.Add(this.groupBoxActions);
            this.splitContainerDataGridAction.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainerDataGridAction.Panel2MinSize = 70;
            this.splitContainerDataGridAction.Size = new System.Drawing.Size(767, 355);
            this.splitContainerDataGridAction.SplitterDistance = 281;
            this.splitContainerDataGridAction.SplitterWidth = 1;
            this.splitContainerDataGridAction.TabIndex = 0;
            // 
            // groupBoxDataGrid
            // 
            this.groupBoxDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDataGrid.Location = new System.Drawing.Point(0, 0);
            this.groupBoxDataGrid.Name = "groupBoxDataGrid";
            this.groupBoxDataGrid.Size = new System.Drawing.Size(767, 281);
            this.groupBoxDataGrid.TabIndex = 0;
            this.groupBoxDataGrid.TabStop = false;
            this.groupBoxDataGrid.Text = "Data";
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxActions.Location = new System.Drawing.Point(0, 0);
            this.groupBoxActions.MinimumSize = new System.Drawing.Size(0, 50);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(767, 73);
            this.groupBoxActions.TabIndex = 0;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Actions";
            // 
            // tabControl_MainManager
            // 
            this.tabControl_MainManager.CausesValidation = false;
            this.tabControl_MainManager.Controls.Add(this.TabGrid);
            this.tabControl_MainManager.Controls.Add(this.tabPageAdd);
            this.tabControl_MainManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_MainManager.HotTrack = true;
            this.tabControl_MainManager.ItemSize = new System.Drawing.Size(20, 28);
            this.tabControl_MainManager.Location = new System.Drawing.Point(0, 0);
            this.tabControl_MainManager.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl_MainManager.Multiline = true;
            this.tabControl_MainManager.Name = "tabControl_MainManager";
            this.tabControl_MainManager.SelectedIndex = 0;
            this.tabControl_MainManager.ShowToolTips = true;
            this.tabControl_MainManager.Size = new System.Drawing.Size(787, 487);
            this.tabControl_MainManager.TabIndex = 15;
            this.tabControl_MainManager.TabStop = false;
            this.tabControl_MainManager.UseSelectable = true;
            this.tabControl_MainManager.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl_MainManager_Selecting);
            // 
            // TabGrid
            // 
            this.TabGrid.CausesValidation = false;
            this.TabGrid.Controls.Add(this.splitContainer1);
            this.TabGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabGrid.HorizontalScrollbarBarColor = true;
            this.TabGrid.HorizontalScrollbarHighlightOnWheel = false;
            this.TabGrid.HorizontalScrollbarSize = 10;
            this.TabGrid.Location = new System.Drawing.Point(4, 32);
            this.TabGrid.Name = "TabGrid";
            this.TabGrid.Padding = new System.Windows.Forms.Padding(3);
            this.TabGrid.Size = new System.Drawing.Size(779, 451);
            this.TabGrid.TabIndex = 0;
            this.TabGrid.Text = "Informations";
            this.TabGrid.UseVisualStyleBackColor = true;
            this.TabGrid.VerticalScrollbarBarColor = true;
            this.TabGrid.VerticalScrollbarHighlightOnWheel = false;
            this.TabGrid.VerticalScrollbarSize = 10;
            this.TabGrid.Click += new System.EventHandler(this.TabGrid_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.CausesValidation = false;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel_Filtre);
            this.splitContainer1.Panel1MinSize = 50;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.CausesValidation = false;
            this.splitContainer1.Panel2.Controls.Add(this.panelDataGrid);
            this.splitContainer1.Size = new System.Drawing.Size(773, 445);
            this.splitContainer1.SplitterDistance = 80;
            this.splitContainer1.TabIndex = 15;
            // 
            // panel_Filtre
            // 
            this.panel_Filtre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Filtre.AutoSize = true;
            this.panel_Filtre.Location = new System.Drawing.Point(3, 3);
            this.panel_Filtre.Name = "panel_Filtre";
            this.panel_Filtre.Size = new System.Drawing.Size(767, 74);
            this.panel_Filtre.TabIndex = 16;
            this.panel_Filtre.Tag = "";
            // 
            // tabPageAdd
            // 
            this.tabPageAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageAdd.HorizontalScrollbarBarColor = false;
            this.tabPageAdd.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageAdd.HorizontalScrollbarSize = 0;
            this.tabPageAdd.ImageKey = "(aucun)";
            this.tabPageAdd.Location = new System.Drawing.Point(4, 32);
            this.tabPageAdd.Name = "tabPageAdd";
            this.tabPageAdd.Size = new System.Drawing.Size(564, 315);
            this.tabPageAdd.TabIndex = 1;
            this.tabPageAdd.ToolTipText = "Ajouter";
            this.tabPageAdd.VerticalScrollbarBarColor = false;
            this.tabPageAdd.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageAdd.VerticalScrollbarSize = 0;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "add.png");
            // 
            // ManagerFormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl_MainManager);
            this.Name = "ManagerFormControl";
            this.Size = new System.Drawing.Size(787, 487);
            this.Load += new System.EventHandler(this.ManagerFormControl_Load);
            this.panelDataGrid.ResumeLayout(false);
            this.splitContainerDataGridAction.Panel1.ResumeLayout(false);
            this.splitContainerDataGridAction.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDataGridAction)).EndInit();
            this.splitContainerDataGridAction.ResumeLayout(false);
            this.tabControl_MainManager.ResumeLayout(false);
            this.TabGrid.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDataGrid;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel_Filtre;
        private MetroFramework.Controls.MetroTabControl tabControl_MainManager;
        private MetroFramework.Controls.MetroTabPage TabGrid;
        private MetroFramework.Controls.MetroTabPage tabPageAdd;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.SplitContainer splitContainerDataGridAction;
        private System.Windows.Forms.GroupBox groupBoxDataGrid;
        private System.Windows.Forms.GroupBox groupBoxActions;
    }
}
