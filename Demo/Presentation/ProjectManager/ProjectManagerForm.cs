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

namespace GenericWinForm.Demo.Presentation.ProjectManager
{
    [App.Gwin.Attributes.Menu(EntityType = typeof(Project),Order = 10,Title = "ProjectManager1")]
    public partial class ProjectManagerForm : Form
    {

        public ProjectManagerForm()
        {
            InitializeComponent();
        }

        private void ProjectManagerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
