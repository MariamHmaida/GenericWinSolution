using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.Gwin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Gwin.Attributes;
using GenericWinForm.Demo.Entities;
using App.Gwin.Application.BAL;
using GenericWinForm.Demo.BAL;
using App.Gwin.Application.Presentation.MainForm;
using App.Gwin.Entities;
using App.Gwin.Exceptions.Gwin;
using App.Gwin.DataModel.ModelInfo;
using App.Shared.AttributesManager;
using App.Gwin.FieldsTraitements;
using GenericWinForm.Demo.DAL;
using GenericWinForm.Demo.Entities.ProjectManager;

namespace App.Gwin.Tests
{
    [TestClass()]
    public class BaseEntryFormTests
    {
        ConfigEntity configEntity = null;
        TaskProject Entity = null;
        IGwinBaseBLO TaskProjectBLO = null;
        [TestInitialize]
        public void GwinAppStart()
        {
            GwinApp.Start(typeof(ModelContext), typeof(BaseBLO<>), new FormApplication(), null);
            configEntity = ConfigEntity.CreateConfigEntity(typeof(TaskProject));
            Entity = new TaskProject();
            TaskProjectBLO = GwinBaseBLO<BaseEntity>.CreateBLO_Instance(typeof(TaskProject), typeof(BaseBLO<>));
        }

        #region Static Test
        //[TestMethod()]
        //[ExpectedException(typeof(GwinUsageModeException))]
        //public void DefaultConstructor_BaseEntryFormTest()
        //{
        //    BaseEntryForm BaseEntryForm = new BaseEntryForm();
        //}

        //[TestMethod()]
        //[ExpectedException(typeof(GwinNullParameterException))]
        //public void Create_BaseEntryForm_with_null_parameterTest()
        //{
        //    BaseEntryForm BaseEntryForm = new BaseEntryForm(null);

        //}

        [TestMethod()]
        public void Create_With_EntityBLO_Instance_BaseEntryFormTest()
        {
            BaseEntryForm BaseEntryForm = new BaseEntryForm(this.TaskProjectBLO);
        }

        [TestMethod()]
        public void Create_With_FilterValues_Instance_BaseEntryFormTest()
        {
            // [ToDo]
        }
        #endregion

        #region Genrtic Test
        /// <summary>
        ///  Add Information with All EntryForm
        /// </summary>
        [TestMethod()]
        public void Add_BaseEntryFormTest()
        {

            // Test All EntryForm
            using (ModelContext db = new ModelContext())
            {
                foreach (var item in new GwinEntitiesManager().GetAll_Entities_Type())
                {
                    IGwinBaseBLO EntityBLO = GwinBaseBLO<BaseEntity>.CreateBLO_Instance(item, GwinApp.Instance.TypeBaseBLO);
                    BaseEntryForm baseEntryForm = new BaseEntryForm(EntityBLO);
                }
            }
        }
        #endregion

        [TestMethod()]
        public void Cancel_BaseEntryFormTest()
        {
            // [ToDo]
        }

        /// <summary>
        /// Show and Read Entity with All Field Natures in Entryform
        /// </summary>
        [TestMethod()]
        public void Show_and_Read_Entity_In_EntryFormTest()
        {
            BaseEntryForm baseEntryForm = new BaseEntryForm(this.TaskProjectBLO);

            TaskProject taskProject = new TaskProjectBLO().CreateTestInstance();
            // Set Default Values
            // Set Values
            foreach (var prorpertyInfo in taskProject.GetType().GetProperties())
            {
                ConfigProperty configProperty = new ConfigProperty(prorpertyInfo, configEntity);
                IFieldTraitements fieldTraitement = BaseFieldTraitement.CreateInstance(configProperty);
                var value = fieldTraitement.GetTestValue(prorpertyInfo);
                // Set Value
                prorpertyInfo.SetValue(taskProject, value);
            }

            


            baseEntryForm.Entity = taskProject;

          


            // Show Entity to View
            baseEntryForm.ShowEntity();


            // Read Entity from View
            baseEntryForm.ReadEntity();
        }
    }
}