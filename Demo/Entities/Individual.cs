using App.Gwin.Attributes;
using App.Gwin.Entities;
using App.Gwin.Entities.Persons;
using App.Gwin.GwinApplication.Security.Attributes;
using GenericWinForm.Demo.Entities.ProjectManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericWinForm.Demo.Entities
{
    /// <summary>
    /// Entity Exemple to be used in ManyToMany relationship 
    /// </summary>
    [GwinEntity(DisplayMember ="Title")]
    [Authorize]
    public class Individual : Person
    {
        public List<TaskProject> Histasks { set; get; }
        public List<TaskProject> ResponsibilityFortasks { set; get; }
    }
}
