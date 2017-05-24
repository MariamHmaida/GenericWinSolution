using System;

namespace App.Gwin.Attributes
{
    /// <summary>
    /// The display of the entity
    /// it is required attribut, because it containe DiplayMameber config that used by ToString methode
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GwinEntityAttribute : BaseAttribute
    {
        /// <summary>
        /// The name of the property that represents the entity
        /// </summary>
      
        public string DisplayMember { get; set; }
        /// <summary>
        /// The plural name
        /// </summary>
        public string PluralName { get; set; }
        /// <summary>
        /// The singular name
        /// </summary>
        public string SingularName { get; set; }
        /// <summary>
        /// Indicates whether the name is male
        /// </summary>
        public bool isMaleName { set; get; }
    }
}