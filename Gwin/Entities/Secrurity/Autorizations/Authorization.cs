using App.Gwin.Attributes;
using App.Gwin.DataModel.ModelInfo;
using App.Gwin.Entities.Application;
using App.Gwin.GwinApplication.Security.Attributes;
using App.Gwin.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Gwin.Entities.Secrurity.Autorizations

{
    [GwinEntity(DisplayMember = nameof(Authorization.Name), Localizable = true)]
    [Menu(Group = nameof(MenuItemApplication.ParentsMenuItem.Root))]
    public class Authorization : BaseEntity
    {
        [Filter]
        [EntryForm]
        [DataGrid]
        public String Name { set; get; }

        [EntryForm]
        public String Description { set; get; }

        /// <summary>
        /// Entity Name : NameSpace.EntityName
        /// </summary>
        [Filter(WidthControl = 400, isDefaultIsEmpty = true)]
        [EntryForm(WidthControl = 400)]
        [DataGrid(WidthColonne = 400)]
        [ReferencesDataSource(TypeObject = typeof(GwinEntitiesManager),
            MethodeName = nameof(GwinEntitiesManager.GetAll_Reference),
            Param1 =typeof(GwinEntityAttribute))]
        public String BusinessEntity { set; get; }

        /// <summary>
        /// Mathods Authorized Names
        /// if ActionsNames is null or empty the the Access is All
        /// </summary>
        [EntryForm]
        [DataGrid]
        public virtual List<String> ActionsNames { set; get; }


        /// <summary>
        /// Maaping of ActionsNames
        /// </summary>
        public string ActionsNamesAsString
        {
            get {
                if (this.ActionsNames == null)
                    this.ActionsNames = new List<string>();
                return string.Join(",", ActionsNames);
            }
            set {
                if(value != null)
                ActionsNames = value.Split(',').ToList();
            }
        }

        [EntryForm]
        [Relationship(Relation = RelationshipAttribute.Relations.ManyToMany_Selection)]
        public virtual List<Role> Roles { set; get; }
    }
}
