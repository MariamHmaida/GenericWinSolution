using App.Gwin.Attributes;
using App.Gwin.EntityManagement;
using App.Gwin.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Gwin.Application.BAL;

namespace App.Gwin.Fields
{
    /// <summary>
    /// Filtrer du champs ManyToOne
    /// Ce filtre est ajouter avant le champs dans le même conteneur, 
    /// </summary>
    public partial class ManyToOneField
    {
        #region InitInterface
        /// <summary>
        /// Affichage du filtre dans l'interface
        /// Remplissage de ListeComboBox
        /// </summary>
        private void InitAndLoadData()
        {


            this.DisplayMember = "";
            this.ValueMember = "Id";


            if (this.PropertyInfo != null)
            {

                // DisplayMember de combobox actuel
                // Annotation : Affichage de l'objet

                GwinEntityAttribute MetaAffichageClasse = this.ConfigEntity.DisplayEntity;

                this.DisplayMember = MetaAffichageClasse.DisplayMember;
                this.Text_Label = MetaAffichageClasse.SingularName;

                Attribute getAffichagePropriete = PropertyInfo.GetCustomAttribute(typeof(DisplayPropertyAttribute));
                DisplayPropertyAttribute AffichagePropriete = (DisplayPropertyAttribute)getAffichagePropriete;

                #region Annotatin
                // Annotation : Critère de filtre
                Attribute metaSelectionCriteriaAttribute = this.PropertyInfo.PropertyType
                    .GetCustomAttribute(typeof(SelectionCriteriaAttribute));


                if (metaSelectionCriteriaAttribute != null)
                {

                    SelectionCriteriaAttribute MetaSelectionCriteria =
                      (SelectionCriteriaAttribute)metaSelectionCriteriaAttribute;

                    #endregion


                    int index = 10;

                    // Si un objet du critère de selection exite dans la classe 
                    // Nous cherchons sa valeur pour l'utiliser
                    foreach (Type item in MetaSelectionCriteria.Criteria)
                    {
                        // Meta information d'affichage du de Critère
                        GwinEntityAttribute gwinEntity = (GwinEntityAttribute)item.GetCustomAttribute(typeof(GwinEntityAttribute));


                        ManyToOneField manyToOneFilter = new ManyToOneField(this.Service, item, null, null,
                            this.orientationField,
                             this.SizeLabel,
                            this.SizeControl, 0, ConfigEntity
                            );
                        manyToOneFilter.Name = item.Name;
                        //manyToOneFilter.Size = new System.Drawing.Size(this.widthField, this.HeightField);

                        manyToOneFilter.TabIndex = ++index;
                        manyToOneFilter.Text_Label = ConfigEntity.CreateConfigEntity(item).DisplayEntity.SingularName;

                        manyToOneFilter.ValueMember = "Id";
                        manyToOneFilter.DisplayMember = gwinEntity.DisplayMember;
                        // pour le chargement de comboBox Suivant
                        manyToOneFilter.ValueChanged += Value_SelectedIndexChanged;

                        manyToOneFilter.Visible = true;

                        // [bug] Le contôle ne s'affiche pas dans le formilaire ??
                        //Form f = new Form();
                        //f.Controls.Add(manyToOneFilter);
                        //f.Show();

                        this.MainContainner.Controls.Add(manyToOneFilter);

                        ListeComboBox.Add(item.Name, manyToOneFilter);
                        LsiteTypeObjetCritere.Add(item.Name, item);
                    }




                }

                // Insertion du ComBox Actuel pour qu'il sera remplit par les données
                ListeComboBox.Add(this.PropertyInfo.PropertyType.Name, this);
                LsiteTypeObjetCritere.Add(this.PropertyInfo.PropertyType.Name, this.PropertyInfo.PropertyType);

            }
            else
            {
                // Cas des sous ComBobox de filtre

                // Insertion du ComBox Actuel pour qu'il sera remplit par les données
                ListeComboBox.Add(this.TypeOfObject.Name, this);
                LsiteTypeObjetCritere.Add(this.TypeOfObject.Name, this.TypeOfObject);

            }
            this.ValueChanged += Value_SelectedIndexChanged;









        }

        /// <summary>
        ///  Calcule des valeurs initiaux
        /// </summary>
        private void CalculeValeursInitiaux(Int64 Value)
        {

            if (Value == 0) return;


            /// Le ComboBox précédent prend les valeurs de l'Entite actuel de comboBox 
            /// car l'entité actuel doit avoir une prorpiété de type type  de l'entityé précédent
            /// Le nom de cette propiété égale Nom d'entité 
            /// si cette propiété n'existe pas la méthode lance une exception
            /// 

            // Initialisation de la liste des valeurs par défaux
            if (ListeValeursInitiaux.Count() < this.LsiteTypeObjetCritere.Count())
                for (int i = 0; i < this.LsiteTypeObjetCritere.Count(); i++)
                {
                    ListeValeursInitiaux.Add(ListeComboBox.Keys.ElementAt(i), 0);
                }
            // Init la de la vlaeur de comboBox Actuel
            ListeValeursInitiaux[ListeValeursInitiaux.Last().Key] = Value;

            IGwinBaseBLO curentService = this.Service
                  .CreateServiceBLOInstanceByTypeEntity(LsiteTypeObjetCritere[ListeValeursInitiaux.Last().Key]);
            BaseEntity curentEntity = curentService.GetBaseEntityByID(Value);

            BaseEntity previousEntity = null;
            for (int i = this.LsiteTypeObjetCritere.Count() - 1; i >= 1; i--)
            {

                string curentKey = LsiteTypeObjetCritere.Keys.ElementAt(i);
                string Previouskey = ListeComboBox.Keys.ElementAt(i - 1);

                PropertyInfo PropertyPrevious = curentEntity.GetType()
                                                .GetProperties()
                                                .Where(p => p.Name == Previouskey)
                                                .SingleOrDefault();
                if (PropertyPrevious == null)
                    throw new PropertyNotExistInEntityException();

                // Affectation des valeurs au ComboBox précédent
                previousEntity = PropertyPrevious.GetValue(curentEntity) as BaseEntity;
                ListeValeursInitiaux[Previouskey] = previousEntity.Id;

                curentEntity = previousEntity;
            }






        }


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
                .CreateServiceBLOInstanceByTypeEntity(LsiteTypeObjetCritere[keyComboBoxCanged]);
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
                    if (this.ListeValeursInitiaux != null && this.ListeValeursInitiaux.Keys.Contains(keyNexComboBox))
                    {
                        this.StopEventSelectedIndexChange = true;
                        nextComboBox.DataSource = null;
                        nextComboBox.DataSource = ls_source;
                        this.StopEventSelectedIndexChange = false;
                        nextComboBox.SelectedValue = this.ListeValeursInitiaux[keyNexComboBox];
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

        #region Affichage des données dans les comboBox
        /// <summary>
        /// Affichage des données dans les ComboBox
        /// </summary>
        protected void ViewingData()
        {
            // Affichage des données du premiere comboBox
            // les autres comboBox sont afficher par l'événement ValueChange du ComboBox
            if (ListeComboBox.Values.Count() <= 0) return;
            ManyToOneField comboBox = ListeComboBox.Values.ElementAt(0);
            string key = ListeComboBox.Keys.ElementAt(0);
            IGwinBaseBLO service = this.Service
                .CreateServiceBLOInstanceByTypeEntity(LsiteTypeObjetCritere[key]);




            // Initalisation avec la valeur par défaux s'il existe
            if (this.ListeValeursInitiaux != null && this.ListeValeursInitiaux.Keys.Contains(key))
            {
                this.StopEventSelectedIndexChange = true;

                // Add Black Value
                List<Object> ls = service.GetAll();
                if (configProperty != null && configProperty.Filter != null)
                    if (configProperty.Filter.isValeurFiltreVide)
                        ls.Insert(0, new EmptyEntity());
                comboBox.DataSource = ls;
                this.StopEventSelectedIndexChange = false;
                comboBox.SelectedValue = this.ListeValeursInitiaux[key];
            }
            else
            {
                // Add Black Value
                List<Object> ls = service.GetAll();

                if (configProperty?.Filter?.isValeurFiltreVide == true)
                    ls.Insert(0, new EmptyEntity());
                comboBox.DataSource = ls;


            }

        }
        #endregion

    }
}
