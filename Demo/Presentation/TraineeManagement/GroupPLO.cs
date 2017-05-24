using App.Gwin.Components.Manager.EntryForms.PLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Gwin;
using GenericWinForm.Demo.Entities.TraineeManagement;

namespace GenericWinForm.Demo.Presentation.TraineeManagement
{
    public class GroupPLO : IGwinPLO
    {
        public void FormAfterInit(BaseEntryForm EntryForm)
        {
            EntryForm.Fields[nameof(Group.Specialty)].Hide();
        }

        public void FormBeforInit(BaseEntryForm EntryForm)
        {
           
        }

        public void ValidatingFiled(BaseEntryForm EntryForm, object sender)
        {
          
        }

        public void ValueChanged(BaseEntryForm EntryForm, object sender)
        {
            if ((string)EntryForm.Fields[nameof(Group.Name)].Value != String.Empty)
                EntryForm.Fields[nameof(Group.Specialty)].Show();
            else
            {
                EntryForm.Fields[nameof(Group.Specialty)].Hide();
            }
        }
    }
}
