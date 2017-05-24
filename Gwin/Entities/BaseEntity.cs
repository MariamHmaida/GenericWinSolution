using App.Gwin.Attributes;
using App.Gwin.Entities.Secrurity.Authentication;
using App.Gwin.Exceptions.Gwin;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace App.Gwin.Entities
{
    /// <summary>
    /// La classe de Base de toutes les entity
    /// </summary>
    public class BaseEntity : IBaseEntity, ISerializable
    {

        #region constructor
        public BaseEntity()
        {
            // Problème de EF avec DateTime
            this.DateCreation = DateTime.Now;
            this.DateModification = DateTime.Now;
        }
        #endregion

        #region Proerties
        [Key]
        public Int64 Id { get; set; }

        /// <summary>
        /// Unique reference of Entity
        /// </summary>
        /// 

        public string Reference { get; set; }

        [DisplayProperty(isInGlossary = true)]

        public int Ordre { set; get; }

        public DateTime DateCreation { get; set; }


        [DisplayProperty(isInGlossary = true)]
        [DataGrid(Ordre = 1000, WidthColonne = 120)]
        [DisplayFormat(DataFormatString = "{0,d}")]
        public DateTime DateModification { get; set; }

        ///// <summary>
        ///// The uset that create the Entity : The Owner
        ///// </summary>
        //public virtual User Owner { set; get; }
        #endregion

        #region Equals & ToString

        public override bool Equals(Object obj)
        {
            if (obj == DBNull.Value) return false;
            if (obj == null) return false;
            BaseEntity objet = (BaseEntity)obj;

            if (this.Id == objet.Id) return true;
            else return false;

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // 
        }

        /// <summary>
        /// Généric ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string Titre = "";
            Type EntityType = ObjectContext.GetObjectType(this.GetType());

            ConfigEntity configEntity = ConfigEntity.CreateConfigEntity(EntityType);

            // Test if the object has the memeber AffichageClasse.DisplayMember
            if (this.GetType().GetProperty(configEntity.GwinEntity.DisplayMember) == null)
                throw new GwinException("The Entity " + this.GetType() + "does not have the membe  : " + configEntity.GwinEntity.DisplayMember);

            object value = this.GetType().GetProperty(configEntity.GwinEntity.DisplayMember).GetValue(this);
            if (value != null) Titre = value.ToString();
            if (Titre == string.Empty)

                if (configEntity.GwinEntity.SingularName != null)
                    return configEntity.GwinEntity.SingularName;
                else
                {
                    string msg = String.Format("Property {0} of the classe {1} must not be null", nameof(GwinEntityAttribute.SingularName), nameof(GwinEntityAttribute));
                    throw new GwinException(msg);
                }



            else return Titre;
        }
        #endregion

        #region seed 
        public virtual void Seed (DbContext context)
        {

        }
        #endregion


    }
}
