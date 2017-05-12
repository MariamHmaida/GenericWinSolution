using App.Gwin.Attributes;
using App.Gwin.Entities;
using App.Gwin.Entities.Application;
using App.Gwin.Entities.MultiLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Gwin.Entities.ContactInformations
{
    [GwinEntity(Localizable =true, isMaleName = false,DisplayMember = "Name")]
    [Menu(Group = nameof(MenuItemApplication.ParentsMenuItem.Configuration))]
    [SelectionCriteria(typeof(Country))]
    public class City : BaseEntity
    {
        [DisplayProperty(isInGlossary = true)]
        [EntryForm(isRequired =true)]
        [Filter]
        [DataGrid]
        public LocalizedString Name { set; get; }

        [DisplayProperty(isInGlossary = true)]
        [EntryForm(MultiLine = true)]
        public LocalizedString Description { set; get; }

        [DisplayProperty(isInGlossary = true)]
        [EntryForm]
        [Filter(isValeurFiltreVide =true)]
        [DataGrid]
        [Relationship(Relation = RelationshipAttribute.Relations.ManyToOne)]
        public virtual Country Country { set; get; }
}
}
