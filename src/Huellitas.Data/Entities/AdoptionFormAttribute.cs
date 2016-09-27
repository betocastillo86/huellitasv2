//-----------------------------------------------------------------------
// <copyright file="AdoptionFormAttribute.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    /// <summary>
    /// Adoption Form Attribute
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class AdoptionFormAttribute : BaseEntity
    {
        /// <summary>
        /// Gets or sets the adoption form.
        /// </summary>
        /// <value>
        /// The adoption form.
        /// </value>
        public virtual AdoptionForm AdoptionForm { get; set; }

        /// <summary>
        /// Gets or sets the adoption form identifier.
        /// </summary>
        /// <value>
        /// The adoption form identifier.
        /// </value>
        public int AdoptionFormId { get; set; }

        /// <summary>
        /// Gets or sets the attribute.
        /// </summary>
        /// <value>
        /// The attribute.
        /// </value>
        public virtual CustomTableRow Attribute { get; set; }

        /// <summary>
        /// Gets or sets the attribute identifier.
        /// </summary>
        /// <value>
        /// The attribute identifier.
        /// </value>
        public int AttributeId { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }
    }
}