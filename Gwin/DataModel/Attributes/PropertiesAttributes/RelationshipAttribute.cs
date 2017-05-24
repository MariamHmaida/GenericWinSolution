using System;

namespace App.Gwin.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class RelationshipAttribute : Attribute
    {

        public enum EditingModes
        {
            Creation,
            Selection_With_Check_Box
        }

        public enum Relations
        {
            Empty,
            ManyToOne,
            OneToMany,
            ManyToMany_Creation,
            ManyToMany_Selection,
            OneToOne
        }
        /// <summary>
        /// Edit mode
        /// </summary>
        public EditingModes EditMode { get; set; }

        /// <summary>
        /// Indique le type de la relation, ManyToOne ou ManyToMany
        /// a Member ManyToMany must be a valide Generic List
        /// </summary>
        public Relations Relation { set; get; }
        
    }
}