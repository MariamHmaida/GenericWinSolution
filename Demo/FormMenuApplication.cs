using App;
using App.Gwin;
using App.Gwin.Application.BAL;
using App.Gwin.Application.Presentation.EntityManagement;
using App.Gwin.Application.Presentation.MainForm;
using App.Gwin.Entities;
using App.Gwin.Entities.Application;
using App.Gwin.Entities.Secrurity.Authentication;
using GenericWinForm.Demo.BAL;
using GenericWinForm.Demo.DAL;
using GenericWinForm.Demo.Entities;
using SplashScreen;
using System;
using System.Windows.Forms;

namespace GenericWinForm.Demo
{
    public partial class FormMenuApplication : FormApplication
    {
        public FormMenuApplication()
        {
            InitializeComponent();
        }

        private void FormMenuApplication_Load(object sender, EventArgs e)
        {
            User user = null;
            user = User.CreateAdminUser(new ModelContext());
            // user = User.CreateGuestUser(new ModelContext());
            user = User.CreateRootUser(new ModelContext());
            user.Language = GwinApp.Languages.ar;

            // Start Gwin Application with Authentification
            GwinApp.Start(typeof(ModelContext), typeof(BaseBLO<>), this, user);


        }

        /// <summary>
        /// Reload the form after language change
        /// </summary>
        public override void Reload()
        {
            base.Reload();
            InitializeComponent();
        }

        private void btProjectManager_Click(object sender, EventArgs e)
        {
            CreateAndShowManagerFormHelper ShowManagementFormHelper = new CreateAndShowManagerFormHelper(GwinApp.Instance.TypeDBContext, this);
            ShowManagementFormHelper.ShowManagerForm(typeof(Project));
        }

        private void btTaskManager_Click(object sender, EventArgs e)
        {
            CreateAndShowManagerFormHelper ShowManagementFormHelper = new CreateAndShowManagerFormHelper(GwinApp.Instance.TypeDBContext, this);
            ShowManagementFormHelper.ShowManagerForm(typeof(TaskProject));
        }
    }
}
