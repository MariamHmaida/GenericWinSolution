using App;
using GenericWinForm.Demo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Gwin.Entities;
using App.Gwin.GwinApplication.Security.Attributes;
using App.Gwin.FieldsTraitements;
using App.Shared.AttributesManager;
using App.Gwin.Logging;
using GenericWinForm.Demo.Entities.ProjectManager;

namespace GenericWinForm.Demo.BAL
{
    /// <summary>
    /// This class must be named as : EntityName + BLO
    /// Withe the same NameSpace of BaseBLO
    /// because it is loaded dynamicly to executed Business Role in Gwin Application
    /// </summary>
    [Authorize]
   
    public class TaskProjectBLO : BaseBLO<TaskProject>
    {


        public override void ApplyBusinessRolesAfterValuesChanged(object sender, BaseEntity entity)
        {
            TaskProject entityMiniConfig = entity as TaskProject;
            string field_name = (string)sender;

            switch (field_name)
            {
                //[BL] the name mut be UperCase
                case nameof(TaskProject.Title):
                    {
                        entityMiniConfig.Title.Current = entityMiniConfig.Title.Current.ToUpper();
                    }
                    break;
            }
        }

        /// <summary>
        /// Create Instance with Test Data
        /// </summary>
        /// <returns>Instance of TaskProject with TestData</returns>
        public TaskProject CreateTestInstance()
        {
            TaskProject instance = new TaskProject();

               


            foreach (var item in instance.GetType().GetProperties())
            {
                ConfigProperty configProperty = new ConfigProperty(item, this.ConfigEntity );
                IFieldTraitements fieldTraitement = BaseFieldTraitement.CreateInstance(configProperty);
                item.SetValue(instance, fieldTraitement.GetTestValue(item));
            }

            return instance;

        }
    }
}
