﻿using App.Gwin.Components.Manager.EntryForms.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Gwin
{

    public partial class BaseEntryForm
    {

        #region Enregistrer et Annuler
        /// <summary>
        /// Le button d'enregistremnet du formulaire de saisie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btEnregistrer_Click(object sender, EventArgs e)
        {
            // Check is All controls en Form are validate

            if (ValidationManager.hasValidationErrors(this.Controls))
                return;

            this.ReadFormToEntity();


            if (EntityBLO.Save(this.Entity) > 0)
            {
                MessageBox.Show(string.Format(ResourceEntryForm.Entity_has_been_properly_registered, this.Entity.ToString()));
                onEnregistrerClick(this, e);
            }
            else
            {
                MessageBox.Show(
                    string.Format(ResourceEntryForm.The_information_is_not_saved_because_there_are_no_changes
                    , this.Entity.ToString())
                    , ResourceEntryForm.There_are_no_changes

                    );
            }


        }

        private void btAnnuler_Click(object sender, EventArgs e)
        {

            onAnnulerClick(this, e);

        }

        void IBaseEntryForm.Lire()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
