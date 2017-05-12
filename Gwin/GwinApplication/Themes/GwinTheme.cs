using App.Gwin.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Gwin.GwinApplication.Themes
{
    public interface IGwinTheme
    {
       void RequiredField(BaseField baseField);
    }
}
