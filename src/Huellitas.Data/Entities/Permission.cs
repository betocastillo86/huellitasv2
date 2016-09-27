//-----------------------------------------------------------------------
// <copyright file="Permission.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// The Permission
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class Permission : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        public Permission()
        {
            this.RolePemission = new HashSet<RolePemission>();
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the role <c>pemission</c>.
        /// </summary>
        /// <value>
        /// The role <c>pemission</c>.
        /// </value>
        public virtual ICollection<RolePemission> RolePemission { get; set; }
    }
}