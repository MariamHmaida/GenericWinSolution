using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using App.Gwin.Fields;

namespace GwinTest.Components.Fields
{
    [TestClass()]
    public class Int32FiledTests
    {
        private int ValueAfterEvent = 0;

        [TestMethod()]
        public void Int32Filed_Horizontal_Virtical_FieldChanged_Test()
        {
            Size SizeLabel = new Size(100, 20);
            Size SizeControl = new Size(100, 20);

            Int32Filed int32Filed_Horizontal = new Int32Filed();
            int32Filed_Horizontal.SizeLabel = SizeLabel;
            int32Filed_Horizontal.SizeControl = SizeControl;
            int32Filed_Horizontal.OrientationField = Orientation.Horizontal;
            int32Filed_Horizontal.ValueChanged += Int32Filed_FieldChanged;
            int32Filed_Horizontal.Value = 5;


            Int32Filed int32Filed_Vertical = new Int32Filed();
            int32Filed_Horizontal.StopAutoSizeConfig();
            int32Filed_Horizontal.SizeLabel = SizeLabel;
            int32Filed_Horizontal.SizeControl = SizeControl;
            int32Filed_Horizontal.OrientationField = Orientation.Vertical;
            int32Filed_Horizontal.ConfigSizeField();

            int32Filed_Vertical.ValueChanged += Int32Filed_FieldChanged;
            int32Filed_Vertical.Value = 5;

           
            Assert.AreEqual(int32Filed_Vertical.Value, 5);
            Assert.AreEqual(int32Filed_Horizontal.Value, 5);
            Assert.AreEqual(ValueAfterEvent, 10);

        }

        private void Int32Filed_FieldChanged(object sender, EventArgs e)
        {
            StringField stringField = sender as StringField;
            ValueAfterEvent += 5;
        }
    }
}