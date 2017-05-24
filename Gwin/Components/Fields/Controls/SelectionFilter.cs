using App.Gwin.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using App.Gwin.Entities;
using System.Collections;
using App.Gwin.Application.BAL;
using App.Gwin.Exceptions.Gwin;
using App.Shared.AttributesManager;

namespace App.Gwin.Fields.Controls
{
    /// <summary>
    /// Manage the the selection filter of Field
    /// </summary>
    public class SelectionFilterManager
    {
        #region Presentation variables
        /// <summary>
        /// Le conteneur de l'interface, our que le Filed ajoute son filtre personnelle à l'interface 
        /// sous Forme des Field avant son Field
        /// </summary>
        public Control MainContainner { set; get; }
        private Orientation OrientationFiled;
        public Size SizeLabel { get; private set; }
        public Size SizeControl { get; private set; }
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }
        #endregion

        #region Business Variables
        private IGwinBaseBLO Service { set; get; }
        /// <summary>
        /// Le champs accepte une des valeurs pardéfaut pour chaque ComboBox de son filtre 
        /// personnel
        /// </summary>
        Int64 DefaultValues { set; get; }
        /// <summary>
        /// Lite des ComboBox 
        /// </summary>
        public Dictionary<string, ManyToOneField> ListeComboBox { set; get; }

        /// <summary>
        /// Liste des Types de critère 
        /// </summary>
        Dictionary<string, Type> LsiteTypeObjetCritere { set; get; }

        Dictionary<string, Int64> ListeValeursInitiaux { set; get; }
        #endregion

        #region WorkFlow variables
        /// <summary>
        /// Indique si le programme en état de changement de la vlaeurs pardéfaut du champs
        /// dans cette étape il aura l'éxécution seulement des événement d'initialisation
        /// et les événement de changement des valeurs ne seront pas exécuter
        /// </summary>
        public bool StopEventSelectedIndexChange { get; private set; }
        #endregion

        #region Gwin variables
        public ConfigProperty ConfigProperty { get; set; }
        #endregion

        #region Events
        public event EventHandler ValueChanged;
        protected void onValueChaned(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(sender, e);
        }
        #endregion

        #region Constructeur
        public SelectionFilterManager(
            IGwinBaseBLO Service,
            ConfigProperty ConfigProperty,
            Control MainContainner,
            Size SizeLabel,
            Size SizeControl,
            Orientation OrientationFiled,
            Int64 DefaultValue
            )
        {
            this.ConfigProperty = ConfigProperty;
            this.MainContainner = MainContainner;
            this.SizeLabel = SizeLabel;
            this.SizeControl = SizeControl;
            this.OrientationFiled = OrientationFiled;

            this.Service = Service;
            this.ListeComboBox = new Dictionary<string, ManyToOneField>();
            this.ListeValeursInitiaux = new Dictionary<string, long>();
            this.LsiteTypeObjetCritere = new Dictionary<string, Type>();

            if (this.ConfigProperty.SelectionCriteria != null)
                InitInterface();

            CalculeValeursInitiaux((Int64)DefaultValue);
            ViewingData();
        }
        #endregion


        #region Get and Set Filter Value
        /// <summary>
        /// Set and Get Filter values
        /// </summary>
        /// 
        public object Value
        {

            get
            {

                return ListeComboBox.Last().Value.Value;
            }

            set
            {
                CalculeValeursInitiaux((Int64)value);
                ViewingData();
            }
        }

        /// <summary>
        /// Get Selected Filter Entity
        /// </summary>
        public BaseEntity ValueEntity
        {
            get
            {
                if (ListeComboBox.Count > 0)

                    return ListeComboBox.Last().Value.SelectedItem as BaseEntity;
                else
                {
                    return null;
                }
            }
        }





        #endregion

        #region InitInterface
        /// <summary>
        /// Affichage du filtre dans l'interface
        /// Remplissage de ListeComboBox
        /// </summary>
        private void InitInterface()
        {

            this.DisplayMember = "";
            this.ValueMember = "Id";


            int index = 10;

            // Si un objet du critère de selection exite dans la classe 
            // Nous cherchons sa valeur pour l'utiliser
            foreach (Type item in this.ConfigProperty.SelectionCriteria.CriteriasTypes)
            {
                // Meta information d'affichage du de Critère
                GwinEntityAttribute DisplayEntityAttributeCritere = (GwinEntityAttribute)item.GetCustomAttribute(typeof(GwinEntityAttribute));

                // Size Filter Item
                Size SizeControlFilter = new Size(this.SizeControl.Width, 25);

                ManyToOneField manyToOneFilter = new ManyToOneField(this.Service, item, null, null,
                    this.OrientationFiled,
                     this.SizeLabel,
                    SizeControlFilter, 0, this.ConfigProperty.ConfigEntity, this.ValueEntity
                    );
                manyToOneFilter.Name = item.Name;
                //manyToOneFilter.Size = new System.Drawing.Size(this.widthField, this.HeightField);

                manyToOneFilter.TabIndex = ++index;
                manyToOneFilter.Text_Label = item.Name;

                manyToOneFilter.ValueMember = "Id";
                manyToOneFilter.DisplayMember = DisplayEntityAttributeCritere.DisplayMember;
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

        #endregion

        #region Calcule des valeurs initiaux
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
            if (ListeValeursInitiaux.Count > 0)
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
        #endregion

        #region Affichage des données
        /// <summary>
        /// Chargement des comboBox suivants
        /// il s'exécute aprés le changement du valeur de chaque comboBox
        /// à chaque changement d'un comboBox on charge les données de comboBox suivant
        /// </summary>
        private void Value_SelectedIndexChanged(object sender, EventArgs e)
        {
            // n'exécuter pas cette événement, si nous somme à l'étape d'initialisation 
            // des chmpas de critères
            if (this.StopEventSelectedIndexChange) return;


            // Initialisation de ComboBox, Service, Entite qui a ête changé
            ManyToOneField comboBoxChanged = (ManyToOneField)sender;
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

                // ComboBox suivant 
                ManyToOneField nextComboBox = ListeComboBox.Values.ElementAt(indexComboBoxChanged + 1);
                string keyNexComboBox = ListeComboBox.Keys.ElementAt(indexComboBoxChanged + 1);

                PropertyInfo PropertyContenantValeursComboSuivant = EntiteActuel.GetType()
                                                .GetProperties()
                                                .Where(p => p.Name == keyNexComboBox + "s")
                                                .SingleOrDefault();
                if (PropertyContenantValeursComboSuivant == null)
                    throw new PropertyNotExistInEntityException(keyNexComboBox + "s");

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
            else
            {
                // si la selection de dernier ComboBox on lance l'événement ValueChanged
                onValueChaned(this, null);
            }
        }



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


            IList ls_source =  service.GetAll();

            // Add Black Value if requorid
            if (this.ConfigProperty?.EntryForm?.isDefaultIsEmpty == true || this.ConfigProperty?.Filter?.isDefaultIsEmpty == true)
                ls_source.Insert(0, new EmptyEntity());

            // Initalisation avec la valeur par défaux s'il existe
            if (this.ListeValeursInitiaux != null && this.ListeValeursInitiaux.Keys.Contains(key))
            {
                this.StopEventSelectedIndexChange = true;
                comboBox.DataSource = ls_source;
                this.StopEventSelectedIndexChange = false;
                comboBox.SelectedValue = this.ListeValeursInitiaux[key];
            }
            else
            {

                comboBox.DataSource = ls_source;
            }

        }
        #endregion
    }
}
