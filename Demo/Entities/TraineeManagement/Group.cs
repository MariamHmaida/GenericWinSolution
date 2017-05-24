using App.Gwin.Attributes;
using App.Gwin.Entities;
using GenericWinForm.Demo.DAL;
using GenericWinForm.Demo.Entities.ProjectManager;
using GenericWinForm.Demo.Entities.TrainingManagement;
using GenericWinForm.Demo.Presentation.TraineeManagement;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
namespace GenericWinForm.Demo.Entities.TraineeManagement
{

    [GwinEntity(Localizable =true,DisplayMember = "Name")]
    
    [Menu(Group= "Trainee")]
    [PresentationLogic(TypePLO = typeof(GroupPLO))]
    public class Group : BaseEntity
    {
        
        [DisplayProperty(isInGlossary =true)]
        [EntryForm(Ordre = 2)]
        [Filter]
        [DataGrid(WidthColonne = 150)]
        public string Name { set; get; }


        [Relationship(Relation = RelationshipAttribute.Relations.ManyToOne)]
        [EntryForm(Ordre = 3)]
        [Filter(isDefaultIsEmpty =true)]
        [DataGrid(WidthColonne = 100)]
        public virtual Specialty Specialty { set; get; }

        [EntryForm(isDefaultIsEmpty = true)]
        [Relationship(Relation = RelationshipAttribute.Relations.ManyToMany_Selection)]
        public virtual List<TaskProject> TaskProjects { set; get; }


        public override void Seed(DbContext context)
        {

            ModelContext db = context as ModelContext;

            //Specialty speciality = db.Specialtys.Where(s => s.Reference == "TDI").SingleOrDefault();

            //// Test Data
            //db.Groups.AddOrUpdate(
            // r => r.Reference
            //                ,
            //                 new Group
            //                 {
            //                     Name = "Groupe 1",
            //                     Specialty = speciality,
            //                     Reference = "G1",
            //                 }

            //      );
        }

    }
}
