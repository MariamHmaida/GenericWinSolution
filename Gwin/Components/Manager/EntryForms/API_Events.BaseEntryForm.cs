using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Gwin 
{
    public partial class BaseEntryForm
    {
        public virtual void FormBeforInit()
        {
            if (this.EntityPLO != null)
                this.EntityPLO.FormBeforInit(this);
        }
        public virtual void FormAfterInit()
        {
            if (this.EntityPLO != null)
                this.EntityPLO.FormAfterInit(this);
        }


        public virtual void Presentation_ValidatingField(object sender, CancelEventArgs e)
        {
            this.EntityPLO.ValidatingFiled(this,sender);
        }

        public virtual void Presentation_ValueChanged(object sender, EventArgs e)
        {
            this.EntityPLO.ValueChanged(this, sender);
        }

    }
}
