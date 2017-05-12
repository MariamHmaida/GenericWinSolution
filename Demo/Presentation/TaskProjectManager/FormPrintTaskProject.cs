using App.Gwin.Components.Manager.DataGrid;
using App.Gwin.Entities;
using GenericWinForm.Demo.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenericWinForm.Demo.Presentation.TaskProjectManager
{
    public partial class FormPrintTaskProject : Form, IFormSelectedEntityAction
    {
        Project project = null;
        public FormPrintTaskProject()
        {
            InitializeComponent();
        }

        public void SetEntity(BaseEntity entity)
        {
            project = entity as Project;
        }

        private void FormPrintTaskProject_Load(object sender, EventArgs e)
        {
            if (project != null)
                MessageBox.Show(project.Title);
        }
    }
}
