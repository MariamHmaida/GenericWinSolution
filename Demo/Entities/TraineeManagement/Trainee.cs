using App.Gwin.Attributes;
using App.Gwin.Entities.MultiLanguage;
using App.Gwin.Entities.Persons;
using GenericWinForm.Demo.DAL;
using GenericWinForm.Demo.Entities.TrainingManagement;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
namespace GenericWinForm.Demo.Entities.TraineeManagement
{
    /// <summary>
    /// Used to Test GwinApp
    /// - ManyToOneField
    /// </summary>
    [GwinEntity(Localizable = true, isMaleName = true, DisplayMember = "Name")]
    [Menu(Group = "Trainee")]
    public class Trainee : Person
    {
         
        public int State { set; get; }

        // 
        // Assignments
        //
        [DisplayProperty(DisplayMember = "Nom")]
        [Relationship(Relation = RelationshipAttribute.Relations.ManyToOne)]
        [EntryForm(Ordre = 3,GroupeBox = "Assignments",isDefaultIsEmpty = true,GroupeBoxOrder = 3)]
        [Filter]
        [DataGrid(WidthColonne = 100)]
        [SelectionCriteria(typeof(Specialty))]
        public virtual Group Group { set; get; }


        //public virtual List<MiniGroupe> MiniGroupes { set; get; }


        //// Gestion des tâches
        //public virtual List<Tache> Taches { set; get; }


        public override  void Seed(DbContext context)
        {

            ModelContext db = context as ModelContext;

     
            // Test Data
            db.Trainees.AddOrUpdate(
             r => r.Reference
                            ,
                             new Trainee
                             {
                                 Reference = "madani-ali",
                                 FirstName = new LocalizedString() {Arab = "مدني",French = "Madani",English = "Madani" },
                                 LastName = new LocalizedString() { Arab = "علي", French = "Ali", English = "Ali" },
                                 Group = new Group
                                 {
                                     Id = 1,
                                     Name = "Groupe 1",
                                     Specialty = new Specialty
                                     {
                                         Id = 1,
                                         Code = "TDI",
                                         Title = new LocalizedString() { Arab = "", French = "Développement", English = "Developpement" },
                                         Reference = "TDI"
                                     },
                                     Reference = "G1",
                                 }
                             }

                  );
        }

      

    }
}

