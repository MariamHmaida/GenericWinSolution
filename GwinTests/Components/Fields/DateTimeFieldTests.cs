using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using App.Components.Fields;

namespace GwinTest.Components.Fields
{
    [TestClass()]
    public class DateTimeFieldTests
    {
        public DateTime ExpectedDate { get;  set; }

        [TestMethod()]
        public void DateTimeField_HorizontalTest()
        {
            Size SizeLabel = new Size(101, 21);
            Size SizeControl = new Size(101, 21);

            DateTimeField DateTimeField_Horizontal = new DateTimeField();
            DateTimeField_Horizontal.SizeLabel = SizeLabel;
            DateTimeField_Horizontal.SizeControl = SizeControl;
            DateTimeField_Horizontal.OrientationField = Orientation.Horizontal;
            DateTimeField_Horizontal.ValueChanged += DateTimeField_FieldChanged;
            DateTimeField_Horizontal.Value = DateTime.Now;

            Assert.AreEqual(SizeLabel.Height + SizeControl.Height, DateTimeField_Horizontal.Height);
            Assert.AreEqual (SizeControl.Width, DateTimeField_Horizontal.Width);

            //Assert.AreEqual(DateTimeField_Horizontal.DateTimeControl.Size, SizeControl);
            Assert.AreEqual(this.ExpectedDate.Year, DateTime.Now.AddYears(5).Year);

        }

        private void DateTimeField_FieldChanged(object sender, EventArgs e)
        {
            DateTimeField Field = sender as DateTimeField;
            this.ExpectedDate = (DateTime) Field.Value  ;
            this.ExpectedDate = this.ExpectedDate.AddYears(5);
        }

        [TestMethod()]
        public void DateTimeField_Vertical_Test()
        {
            Size SizeLabel = new Size(101, 21);
            Size SizeControl = new Size(101, 21);

            DateTimeField DateTimeField_Vertical = new DateTimeField();
            DateTimeField_Vertical.StopAutoSizeConfig();
            DateTimeField_Vertical.SizeLabel = SizeLabel;
            DateTimeField_Vertical.SizeControl = SizeControl;
            DateTimeField_Vertical.OrientationField = Orientation.Vertical;
            DateTimeField_Vertical.ConfigSizeField();
            DateTimeField_Vertical.ValueChanged += DateTimeField_FieldChanged;
            DateTimeField_Vertical.Value = DateTime.Now;

            Assert.AreEqual(SizeControl.Height, DateTimeField_Vertical.Height);

            Assert.AreEqual(SizeLabel.Width + SizeControl.Width, DateTimeField_Vertical.Width);
        }
    }
}