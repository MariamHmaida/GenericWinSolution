using App.Gwin.Attributes;
using App.Gwin.Entities;
using App.Gwin.Entities.MultiLanguage;
using App.Gwin.GwinApplication.Security.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Gwin.Entities.Application
{
    [GwinEntity(Localizable =true,isMaleName =false,DisplayMember ="Name",PluralName ="Applications",SingularName = "Application")]
    [Menu(Group =nameof(MenuItemApplication.ParentsMenuItem.Root))]
    [Authorize]
    public class ApplicationName : BaseEntity
    {
       
        public ApplicationName()
        {
            this.Name = new LocalizedString();
            this.Description = new LocalizedString();
        }
        [EntryForm]
        [Filter]
        [DataGrid]
        public LocalizedString Name { set; get; }

        [EntryForm]
        [Filter]
        [DataGrid]
        public LocalizedString Description { set; get; }
    }
}
