using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Gwin.Fields
{
    public partial class ManyToOneField
    {
        /// <summary>
        /// Chargement des comboBox suivants
        /// il s'exécute aprés le changement du valeur de chaque comboBox
        /// à chaque changement d'un comboBox on charge les données de comboBox suivant
        /// </summary>
        private void Value_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if the selected comboBox is Blank
            bool isBlank = false;

            // n'exécuter pas cette événement, si nous somme à l'étape d'initialisation 
            // des chmpas de critères
            if (this.StopEventSelectedIndexChange) return;


            // Initialisation de ComboBox, Service, Entite qui a ête changé
            ManyToOneField comboBoxChanged = (ManyToOneField)sender;

            // if Blank Value dont do anything
            if (Convert.ToInt64(comboBoxChanged.SelectedValue) == 0) isBlank = true;


            comboBoxChanged.CreateControl();
            int indexComboBoxChanged = ListeComboBox.Values.ToList<ManyToOneField>().IndexOf(comboBoxChanged);
            string keyComboBoxCanged = ListeComboBox.Keys.ElementAt(indexComboBoxChanged);
            IGwinBaseBLO serviceComboBoxActuel = this.Service
                .CreateServiceBLOInstanceByTypeEntity(Criterias[keyComboBoxCanged]);
            BaseEntity EntiteActuel = serviceComboBoxActuel.GetBaseEntityByID(Convert.ToInt64(comboBoxChanged.SelectedValue));

            /// Actualisation de ComboBox suivant s'il existe
            /// Le ComboBox suivant prend les valeurs de l'Entite actuel de comboBox 
            /// car l'entité actuel doit avoir une prorpiété de type type Collection de l'entityé suivant
            /// Le nom de cette propiété égale Nom d'entité suivant + "s"
            /// si cette propiété n'existe pas la méthode lance une exception
            if (comboBoxChanged.SelectedValue != null && (ListeComboBox.Values.Count() - 1) >= (indexComboBoxChanged + 1))
            {

                // [Update] chargement des données par des requête Linq au lieux d'utiliser 
                // les membres Virtuel

                // ComboBox suivant 
                ManyToOneField nextComboBox = ListeComboBox.Values.ElementAt(indexComboBoxChanged + 1);

                string keyNexComboBox = ListeComboBox.Keys.ElementAt(indexComboBoxChanged + 1);

                PropertyInfo PropertyContenantValeursComboSuivant = null;
                if (!isBlank)
                {
                    PropertyContenantValeursComboSuivant = EntiteActuel.GetType()
                                                    .GetProperties()
                                                    .Where(p => p.Name == keyNexComboBox + "s")
                                                    .SingleOrDefault();
                    if (PropertyContenantValeursComboSuivant == null)
                        throw new PropertyNotExistInEntityException(keyNexComboBox + "s");
                }
                else
                {
                    nextComboBox.DataSource = null;
                }
                // Affectation des valeurs au ComboBox suivant
                IList ls_source = null;
                if (PropertyContenantValeursComboSuivant != null)
                {
                    ls_source = PropertyContenantValeursComboSuivant.GetValue(EntiteActuel) as IList;


                    // Initalisation avec la valeur par défaux s'il existe
                    if (this.FilterPreviesValues != null && this.FilterPreviesValues.Keys.Contains(keyNexComboBox))
                    {
                        this.StopEventSelectedIndexChange = true;
                        nextComboBox.DataSource = null;
                        nextComboBox.DataSource = ls_source;
                        this.StopEventSelectedIndexChange = false;
                        nextComboBox.SelectedValue = this.FilterPreviesValues[keyNexComboBox];
                    }
                    else
                    {
                        nextComboBox.DataSource = ls_source;
                    }



                }


                // Si ce Combo n'a pas d'information alors vider les combBobx suivant 
                if (ls_source == null || ls_source.Count == 0)
                    for (int i = (indexComboBoxChanged + 1); i < ListeComboBox.Values.Count(); i++)
                    {
                        ManyToOneField comboBox_suivant2 = ListeComboBox.Values.ElementAt(i);
                        comboBox_suivant2.DataSource = null;
                        comboBox_suivant2.TextCombobox = "";
                    }
            }
        }
#endregion
    }
}
