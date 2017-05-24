using App;
using App.Gwin;
using App.Gwin.Application.Presentation;
using App.Gwin.Application.Presentation.EntityManagement;
using App.Gwin.Entities.Secrurity.Authentication;
using GenericWinForm.Demo.BAL;
using GenericWinForm.Demo.DAL;
using GenericWinForm.Demo.Entities;
using GenericWinForm.Demo.Entities.ProjectManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenericWinForm.Demo
{
    public partial class PrivateApplicationMenu : BaseForm
    {
        public PrivateApplicationMenu()
        {
            InitializeComponent();
        }



        private void projectManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagerForm form = new ManagerForm(new BaseBLO<Project>(), null, this);
            form.MdiParent = this;
            form.Show();
        }

        private void ProjectFormMenu_Load(object sender, EventArgs e)
        {
            // Application User
            User user = new User();
            user.Language = GwinApp.Languages.ar;

            // Start Gwin Application
            GwinApp.Start(typeof(ModelContext), typeof(BaseBLO<>),null, user);
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }
    }
}
