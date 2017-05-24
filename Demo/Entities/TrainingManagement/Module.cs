using App.Gwin.Attributes;
using App.Gwin.Entities;
using App.Gwin.Entities.MultiLanguage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GenericWinForm.Demo.Entities.TrainingManagement
{
    public class Module : BaseEntity
    {
        
        public Module()
        {
          
            this.Name = new LocalizedString();
            this.Presentation = new LocalizedString();
            this.Competence = new LocalizedString();
            this.TeachingStrategy = new LocalizedString();
            this.Learning = new LocalizedString();
            this.Evaluation = new LocalizedString();
            this.Description = new LocalizedString();
        }


        // 
        // Informations générale
        //
        [EntryForm(Ordre = 3, GroupeBox = "Module")]
        [DataGrid(WidthColonne = 100)]
        public LocalizedString Name { set; get; }

        [EntryForm(Ordre = 3, GroupeBox = "Module")]
        [DataGrid(WidthColonne = 100)]
        public LocalizedString Competence { set; get; }
        [EntryForm(Ordre = 3, GroupeBox = "Module")]
        [DataGrid(WidthColonne = 100)]


        public String Code { set; get; }

        [EntryForm(Ordre = 3, GroupeBox = "Module")]
        [DataGrid(WidthColonne = 100)]

      
        public LocalizedString Presentation { set; get; }

        // 
        // Description pédagogique
        //
        [EntryForm(Ordre = 3, GroupeBox = "Module")]
        [DataGrid(WidthColonne = 100)]
        public LocalizedString TeachingStrategy { set; get; }
        [EntryForm(Ordre = 3, GroupeBox = "Module")]
        [DataGrid(WidthColonne = 100)]
        public LocalizedString Learning { set; get; }
        [EntryForm(Ordre = 3, GroupeBox = "Module")]
        [DataGrid(WidthColonne = 100)]
        public LocalizedString Evaluation { set; get; }

        // 
        // Planning
        //
        /// <summary>
        /// La duré en Nombre d'heure
        /// </summary>
        ///  
        [DataGrid(WidthColonne = 100)]
        [EntryForm(Ordre = 3, GroupeBox = "Module")]
        public int Duration { set; get; }
        // 
        // Affectation
        //

        //   public virtual Filiere Filiere { set; get; }

        // 
        // Description Technique
        //  

        //public virtual List<Precision> Precisions { set; get; }

        //public virtual List<PrevisionSeance> PrevisionSeances { set; get; }
        //public virtual List<Formation> Formations { set; get; }

        [EntryForm(Ordre = 3, GroupeBox = "Module")]
        [DataGrid(WidthColonne = 100)]
        public LocalizedString Description { set; get; }

       /* [EntryForm(Ordre = 3, GroupeBox = "Module")]
        [DataGrid(WidthColonne = 100)]
        [Relationship(Relation =RelationshipAttribute.Relations.ManyToMany_Selection)]
        //public List<Training> Trainings { get; set; }*/
    }
}