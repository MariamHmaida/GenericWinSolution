using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using System.Drawing;
using App.Gwin.Fields;

namespace GwinTest.Components.Fields
{
    [TestClass()]
    public class StringFieldTests
    {
       

        private string ValueAfterEvent = "";

        [TestMethod()]
        public void StringField_Horizontal_MultiLigne_FieldChanged_Test()
        {

            Size SizeLabel = new Size(100, 20);
            Size SizeControl = new Size(100, 20);

            StringField stringField_Horizontal = new StringField();
            stringField_Horizontal.OrientationField = Orientation.Horizontal;
            stringField_Horizontal.SizeLabel = SizeLabel;
            stringField_Horizontal.SizeControl = SizeControl;

            stringField_Horizontal.ValueChanged += StringField_FieldChanged;
            stringField_Horizontal.Value = "Hello";

          

            StringField stringField_Horizontal_MultiLigne = new StringField();
            stringField_Horizontal_MultiLigne.StopAutoSizeConfig();
            stringField_Horizontal_MultiLigne.OrientationField = Orientation.Horizontal;
            stringField_Horizontal_MultiLigne.SizeLabel = SizeLabel;
            stringField_Horizontal_MultiLigne.SizeControl = SizeControl;
            stringField_Horizontal_MultiLigne.IsMultiline = true;
            stringField_Horizontal_MultiLigne.ConfigSizeField();


            stringField_Horizontal_MultiLigne.ValueChanged += StringField_FieldChanged;
            stringField_Horizontal_MultiLigne.Value = "Hello";


            Assert.AreNotEqual(stringField_Horizontal.Size.Height, stringField_Horizontal_MultiLigne.Size.Height);
            Assert.AreEqual(ValueAfterEvent, "HelloAfterEventHelloAfterEvent");
           
        }

        private void StringField_FieldChanged(object sender, EventArgs e)
        {
            StringField stringField = sender as StringField;
            ValueAfterEvent += stringField.Value + "AfterEvent";
        }
        [TestMethod()]
        public void StringField_Vertical()
        {
       
            StringField stringFieldVertical = new StringField();
            stringFieldVertical.OrientationField = Orientation.Vertical;
            stringFieldVertical.SizeLabel = new Size(100, 20);
            stringFieldVertical.SizeControl = new Size(100, 20);
        }

        [TestMethod()]
        public void StringField_ChangeOrientation()
        {
            StringField stringFieldVertical = new StringField();
            stringFieldVertical.OrientationField = Orientation.Horizontal;
            stringFieldVertical.OrientationField = Orientation.Vertical;
            stringFieldVertical.OrientationField = Orientation.Horizontal;
        }
        [TestMethod()]
        public void StringField_ChangeMultiLine()
        {
            StringField stringFieldVertical = new StringField();
            stringFieldVertical.IsMultiline = true;
            stringFieldVertical.IsMultiline = false;
            stringFieldVertical.IsMultiline = true;
            stringFieldVertical.NombreLigne = 10;
        }




    }
}