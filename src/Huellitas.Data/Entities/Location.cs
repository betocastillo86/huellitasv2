//-----------------------------------------------------------------------
// <copyright file="Location.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// The class Location
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class Location : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        public Location()
        {
            this.AdoptionForm = new HashSet<AdoptionForm>();
            this.Content = new HashSet<Content>();
        }

        /// <summary>
        /// Gets or sets the adoption form.
        /// </summary>
        /// <value>
        /// The adoption form.
        /// </value>
        public virtual ICollection<AdoptionForm> AdoptionForm { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public virtual ICollection<Content> Content { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Location"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent location identifier.
        /// </summary>
        /// <value>
        /// The parent location identifier.
        /// </value>
        public int ParentLocationId { get; set; }
    }
}