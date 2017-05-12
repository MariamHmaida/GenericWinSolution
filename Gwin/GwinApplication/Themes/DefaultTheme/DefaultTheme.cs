using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Gwin.Fields;
using System.Drawing;
using System.Windows.Forms;

namespace App.Gwin.GwinApplication.Themes
{
    public class DefaultTheme : IGwinTheme
    {
        public void RequiredField(BaseField baseField)
        {
             // baseField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            baseField.Paint += BaseField_Paint;
        }

        private void BaseField_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            BaseField baseField = sender as BaseField;
            // Draw a string on the PictureBox.
            g.DrawString("This is a diagonal line drawn on the control",
                new Font("Arial", 10), System.Drawing.Brushes.Blue, new Point(30, 30));
            // Draw a line in the PictureBox.
            g.DrawLine(System.Drawing.Pens.Red, baseField.Left, baseField.Top,
                baseField.Right, baseField.Bottom);
        }
    }
}
