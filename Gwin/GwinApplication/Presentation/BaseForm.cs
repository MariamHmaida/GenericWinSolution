
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Gwin.Application.Presentation
{
    public class BaseForm :Form, IBaseForm
    {
        
        List<string> Skins;

        public BaseForm()
        {
            
            Skins = Directory.GetFiles(System.Windows.Forms.Application.StartupPath + @"\IrisSkin4\Skins\", "*.ssk").ToList();

            GwinApp.SkinEngine.SkinFile = Skins[1];
            GwinApp.SkinEngine.Active = true;
            


            //var materialSkinManager = MaterialSkinManager.Instance;
            //materialSkinManager.AddFormToManage(this);
            //materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            //materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        /// <summary>
        /// Reload the form after language change
        /// </summary>
       public virtual void Reload() { } 
    }
}
