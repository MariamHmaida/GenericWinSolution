using App.Gwin.Entities;
using GenericWinForm.Demo.Entities;
using GenericWinForm.Demo.Entities.ProjectManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericWinForm.Demo.BAL
{
    public class ProjectBLO : BaseBLO<Project>
    {
        public override void ApplyBusinessRolesAfterValuesChanged(object sender, BaseEntity entity)
        {
            Project entityMiniConfig = entity as Project;
            string field_name = (string)sender;

            switch (field_name)
            {
                //[BL] the name mut be UperCase
                case nameof(Project.Title):
                    {
                        entityMiniConfig.Title  = entityMiniConfig.Title.ToUpper();
                    }
                    break;
            }
        }
    }
}
