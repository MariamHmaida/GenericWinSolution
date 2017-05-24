using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Gwin.Components.Manager.EntryForms.PLL
{

    /// <summary>
    /// Presentation Logic Object Interface
    /// </summary>
    public interface IGwinPLO
    {
        void FormBeforInit(BaseEntryForm EntryForm);

        void FormAfterInit(BaseEntryForm EntryForm);

        void ValueChanged(BaseEntryForm EntryForm, object sender);

        void ValidatingFiled(BaseEntryForm EntryForm, object sender);
    }
}
