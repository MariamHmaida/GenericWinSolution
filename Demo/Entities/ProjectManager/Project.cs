using App.Gwin.Attributes;
using App.Gwin.Entities;
using App.Gwin.Entities.MultiLanguage;
using App.Gwin.GwinApplication.Security.Attributes;
using GenericWinForm.Demo.DAL;
using GenericWinForm.Demo.Presentation.ProjectManager;
using GenericWinForm.Demo.Presentation.TaskProjectManager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericWinForm.Demo.Entities.ProjectManager
{
    /// <summary>
    /// Entity Exemple to be used in ManyToOne RelationShip
    /// </summary>
    [GwinEntity(DisplayMember = "Title")]
    [Authorize]
    [Menu]
    [DataGridSelectedAction(Title = "Print", Description = "Print_Task", TypeOfForm = typeof(FormPrintTaskProject))]
    [GwinForm(FormType = typeof(ProjectForm))]
    public class Project:BaseEntity
    {

        public Project()
        {
            this.Description = new LocalizedString();
        }

        [EntryForm(isRequired = true)]
        [Filter]
        [DataGrid]
        [BusinesRole]
        public string Title { set; get; }


        [EntryForm(MultiLine = true,WidthControl =300, isRequired = true)]
        [Filter]
        [DataGrid]
        public LocalizedString Description { set; get; }


        [Relationship(Relation = RelationshipAttribute.Relations.ManyToMany_Creation)]
        [DataGrid]
        public List<TaskProject> TaskProjects { set; get; }


       

    }
}
