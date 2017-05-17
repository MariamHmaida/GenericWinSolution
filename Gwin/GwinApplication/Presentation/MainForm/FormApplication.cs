using App.Gwin.Application.Presentation.EntityManagement;
using SplashScreen;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace App.Gwin.Application.Presentation.MainForm
{
    public partial class FormApplication : BaseForm, IApplicationMenu
    {

      //  protected EntityManagementCreator entityManagementCreator { set; get; }

        public FormApplication()
        {
            if(LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
               
            }
            InitializeComponent();

        }
        private void FormApplication_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// InitializeComponent
        /// Create Application Menu
        /// Change Direction when language is changed
        /// </summary>
        public override void Reload()
        {
            // Clear All controls in Form Application
            this.ClearAllControls();

            InitializeComponent();

            // Create Menu Application
             // -- entityManagementCreator = new EntityManagementCreator(GwinApp.Instance.TypeDBContext, this);
            new CreateApplicationMenu(this);

            // Change Form Direction  - When  Languauge is changed
            if (GwinApp.Instance.CultureInfo.TwoLetterISOLanguageName == "fr" || GwinApp.Instance.CultureInfo.TwoLetterISOLanguageName == "en")
            {
                this.RightToLeftLayout = false;
                this.RightToLeft = RightToLeft.No;
            }
            else
            {
                this.RightToLeftLayout = true;
                this.RightToLeft = RightToLeft.Yes;
            }

            // [Bug] : Form somtime not good showen when language is changed
            // [Temporary Fix] 
            this.WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Clear All Controls in this form
        /// </summary>
        private void ClearAllControls()
        {
            // [Bug] : Not working with showen child form
            // [Temporary Fix]
            foreach (Control item in this.MdiChildren)
            {
                item.Dispose();
            }
            this.Controls.Clear();
        }

        public MenuStrip getMenuStrip()
        {
            return this.menuStrip1;
        }

        private void frenchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GwinApp.ChangeLanguage(CultureInfo.CreateSpecificCulture("fr"));
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GwinApp.ChangeLanguage(CultureInfo.CreateSpecificCulture("en"));
        }

        private void arabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GwinApp.ChangeLanguage(new CultureInfo("ar"));
        }

        private void languageToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
