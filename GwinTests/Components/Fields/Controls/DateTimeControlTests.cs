using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.Gwin.Fields.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GwinTest.Components.Fields.Controls
{
    [TestClass()]
    public class DateTimeControlTests
    {
        int Increment = 0;
        DateTimeControl dateTimeControl;
        [TestInitialize()]
        public void CreateInstanceDateTimeControlTest()
        {
            dateTimeControl = new DateTimeControl();
            dateTimeControl.ValueChanged += DateTimeControl_ValueChanged;
        }


        [TestMethod()]
        public void DateTimeControl_UseValue_ValueChangedTest()
        {
            dateTimeControl.Value = DateTime.Now;
            Assert.AreEqual(dateTimeControl.Value.AddYears(5).Year, DateTime.Now.AddYears(this.Increment).Year);
        }

        private void DateTimeControl_ValueChanged(object sender, EventArgs e)
        {
            this.Increment = 5;
        }

        [TestMethod()]
        public void DateTimeControl_ChangeSizeTest()
        {
            dateTimeControl.ChangeSizeControl(new Size(150, 40));
            Assert.AreEqual(dateTimeControl.Size, new Size(150, 40));
        }


    }
}