using App.Gwin.Attributes;
using App.Gwin.DataModel.ModelInfo;
using App.Gwin.Entities;
using App.Gwin.Entities.MultiLanguage;
using App.Gwin.GwinApplication.Security.Attributes;
using App.Gwin.ModelData;
using GenericWinForm.Demo.Presentation.TaskProjectManager;
using System.Collections.Generic;

namespace GenericWinForm.Demo.Entities
{

    /// <summary>
    /// this class must contrain All Feild with all configuration posibility
    /// this class must contain All C# Type
    /// becuase it is used to test All Gwin Fields and Components
    /// </summary>
    [GwinEntity(Localizable =true,DisplayMember = nameof(TaskProject.Title))]
    [Menu]
    [ManagementForm(FormTitle = "form_title")]
    [BusinesRole]
    public class TaskProject : BaseEntity
    {

        public TaskProject()
        {
            this.Title = new LocalizedString();
            this.Description = new LocalizedString();
        }

        #region Primitive Type 
        /// <summary>
        /// Type : String
        /// </summary>
        [EntryForm(GroupeBox = "Primitive_Type",GroupeBoxOrder = 4,isRequired = true)]
        [Filter]
        [DataGrid]
        public LocalizedString Title { set; get; }

        /// <summary>
        /// Type : String with MultiLine
        /// </summary>
        [EntryForm(MultiLine =true, GroupeBox = "Primitive_Type", GroupeBoxOrder = 4)]
        //[Filter]
        [DataGrid]
        public LocalizedString Description { set; get; }

        /// <summary>
        /// Type : DateTime
        /// </summary>
        [EntryForm(GroupeBox = "Primitive_Type", GroupeBoxOrder = 4,isRequired = true)]
        // [Filter] not yet implemented, Bug : cant show Data, msut eliminate second 
        // from search data
        [DataGrid]
        public System.DateTime StartDate { set; get; }


        /// <summary>
        /// Type : Boolean
        /// </summary>
        [EntryForm(GroupeBox = "Primitive_Type", GroupeBoxOrder = 4 , isRequired = true)]
        [Filter]
        [DataGrid]
        public System.Boolean Valide { set; get; }


        /// <summary>
        /// Type : Int32
        /// </summary>
        [EntryForm(GroupeBox = "Primitive_Type", GroupeBoxOrder = 4,isShowDefaultValueWhenAdd = true)]
        [Filter]
        [DataGrid]
        public System.Int32 DaysNumber { set; get; }

        /// <summary>
        /// Type : Int16
        /// </summary>
        [EntryForm(GroupeBox = "Primitive_Type", GroupeBoxOrder = 4 , isRequired = true)]
        // [Filter]
        [DataGrid]
        public System.Int16 var_Int16 { set; get; }

        /// <summary>
        /// Type : Int64
        /// </summary>
        [EntryForm(GroupeBox = "Primitive_Type", GroupeBoxOrder = 4)]
        //[Filter]
        [DataGrid]
        public System.Int64 var_Int64 { set; get; }

        /// <summary>
        /// Type : var_float
        /// </summary>
        [EntryForm(GroupeBox = "Primitive_Type", GroupeBoxOrder = 4)]
        // [Filter]
        [DataGrid]
        public float var_float { set; get; }

        /// <summary>
        /// Type : double
        /// </summary>
        [EntryForm(GroupeBox = "Primitive_Type", GroupeBoxOrder = 4)]
        // [Filter]
        [DataGrid]
        public double var_double { set; get; }
        #endregion

        #region Localized Type

        /// <summary>
        /// Type : LocalizedString
        /// </summary>
        [EntryForm(GroupeBox = "Localized_Type")]
        [Filter]
        [DataGrid]
        public LocalizedString LocalizedTitle { set; get; }
        #endregion

        #region DataSourceType

        /// <summary>
        /// Type : String Wtih DataSource
        /// </summary>
        [EntryForm(GroupeBox = " DataSourceType", GroupeBoxOrder =5)]
        [Filter ( isValeurFiltreVide = true)]
        [DataGrid]
        [ReferencesDataSource(TypeObject = typeof(ModelConfiguration),
            MethodeName = nameof(ModelConfiguration.GetAll_Entities_Type),
            DisplayName = "Name")]
        public string EntityToManimulate { set; get; }

        [Filter(WidthControl = 400, isValeurFiltreVide = true)]
        [EntryForm(WidthControl = 400, GroupeBox = " DataSourceType", GroupeBoxOrder = 5)]
        [DataGrid(WidthColonne = 400)]
        [ReferencesDataSource(TypeObject = typeof(GwinBusinessEntitiesManager),
            MethodeName = nameof(GwinBusinessEntitiesManager.GetAll),
            Param1 = typeof(AuthorizeAttribute),
            Param2 = true,
            DisplayName = "Name")]
        public string BusinessEntity { set; get; }


        #endregion

        #region Enumeration
        /// <summary>
        /// Type : Enumeration
        /// </summary>
        [EntryForm(GroupeBox = "Enumeration",GroupeBoxOrder = 1)]
        // [Filter] Enumeation in Filter not yet implmented
        [DataGrid]
        public TaskCategory Categoy { set; get; }
        #endregion

        #region RelationShip
        /// <summary>
        /// Type : ManyToOne
        /// </summary>
        [EntryForm(GroupeBox = "RelationShip",GroupeBoxOrder = 2)]
        [Filter(isValeurFiltreVide = true)]
        [DataGrid]
        [Relationship(Relation = RelationshipAttribute.Relations.ManyToOne)]
        public Project Project { set; get; }

        /// <summary>
        /// Type : ManyToManu_Creation
        /// Filter : NotImplemented yet
        /// </summary>
        [DataGrid]
        [Relationship(Relation = RelationshipAttribute.Relations.ManyToMany_Creation)]
        public virtual List<Individual> Responsibles { set; get; }

        /// <summary>
        /// Type : ManyToMany_Selection 
        /// Filter : NotImplemented yet
        /// </summary>
        [EntryForm(GroupeBox = "RelationShip", GroupeBoxOrder = 2)]
        [DataGrid]
        [Relationship(Relation = RelationshipAttribute.Relations.ManyToMany_Selection)]
        public virtual List<Individual> Peoples { set; get; }

        #endregion
    }
}
