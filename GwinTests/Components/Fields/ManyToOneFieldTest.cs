using App.Gwin;
using App.Gwin.Application.BAL;
using App.Gwin.Application.Presentation.MainForm;
using App.Gwin.Attributes;
using App.Gwin.Entities.Secrurity.Authentication;
using App.Gwin.Fields;
using GenericWinForm.Demo.BAL;
using GenericWinForm.Demo.DAL;
using GenericWinForm.Demo.Entities.TraineeManagement;
using GenericWinForm.Demo.Entities.TrainingManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GwinTest.Components.Fields
{
    [TestClass()]
    public class ManyToOneFieldTest
    {
        [TestInitialize]
        public void GwinAppStart()
        {
            User user = User.CreateRootUser(new ModelContext());
            GwinApp.Start(typeof(ModelContext), typeof(BaseBLO<>), new FormApplication(), user);
        }


        [TestMethod()]
        public void ManyToOneField_WithFilter()
        {
            // Model Data : Trainee, Groupe, Speciality
            using (ModelContext db = new ModelContext())
            {
                Panel Container = new Panel();
                Size SizeLabel = new Size(100, 20);
                Size SizeControl = new Size(100, 20);

                IGwinBaseBLO TraineeBLO = new GwinBaseBLO<Trainee>(db);
                PropertyInfo groupePropertyInfo = typeof(Trainee).GetProperty(nameof(Trainee.Group));
                ConfigEntity configEntity = ConfigEntity.CreateConfigEntity(typeof(Trainee));
                Trainee trainee = new Trainee();



                ManyToOneField manyToOneField = new ManyToOneField(
                    TraineeBLO,
                    groupePropertyInfo,
                    Container,
                    Orientation.Vertical,
                    SizeLabel,
                    SizeControl,
                    0,
                    configEntity,
                    trainee);

                // Selected the first speciality
                // selectedindex = 1 , index 0 for EmptyData
                manyToOneField
                    .SelectionFilterManager
                    .ListeComboBox[typeof(Specialty).Name]
                    .SelectedIndex = 1;

                // Selected the first Groupe
                manyToOneField.SelectedIndex = 0;
            }
        }

        [TestMethod()]
        public void ManyToOneField_WithOutFilter()
        {
            // Model Data : Trainee, Groupe, Speciality
            using (ModelContext db = new ModelContext())
            {
                Panel Container = new Panel();
                Size SizeLabel = new Size(100, 20);
                Size SizeControl = new Size(100, 20);

                IGwinBaseBLO GroupeBLO = new GwinBaseBLO<Group>(db);
                PropertyInfo SpecialtyPropertyInfo = typeof(Group).GetProperty(nameof(Group.Specialty));
                ConfigEntity configEntity = ConfigEntity.CreateConfigEntity(typeof(Group));
                Group trainee = new Group();


                Specialty Specialty = db.Specialtys.Where(s => s.Reference == "TDI").SingleOrDefault();

                ManyToOneField manyToOneField = new ManyToOneField(
                    GroupeBLO,
                    SpecialtyPropertyInfo,
                    Container,
                    Orientation.Vertical,
                    SizeLabel,
                    SizeControl,
                    Specialty.Id,
                    configEntity,
                    trainee);



                // Selected the first Groupe
                manyToOneField.SelectedIndex = 0;
            }
        }


        [TestMethod()]
        public void ManyToOneField_Form()
        {

            using (ModelContext db = new ModelContext())
            {
                IGwinBaseBLO GroupeBLO = new GwinBaseBLO<Group>(db);
                Dictionary<string, object> filter = new Dictionary<string, object>();

                Specialty Specialty = db.Specialtys.Where(s => s.Reference == "TDI").SingleOrDefault();
                filter.Add(nameof(Specialty),Specialty.Id);
                BaseEntryForm GroupeEntryForm = new BaseEntryForm(GroupeBLO,null, filter,true);
                
            }
              

        }


    }
}

