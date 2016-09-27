//-----------------------------------------------------------------------
// <copyright file="Role.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// The class Role
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class Role : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role()
        {
           this.RolePemissions = new HashSet<RolePemission>();
           this.Users = new HashSet<User>();
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
        /// Gets or sets the role <c>pemissions</c>.
        /// </summary>
        /// <value>
        /// The role <c>pemissions</c>.
        /// </value>
        public virtual ICollection<RolePemission> RolePemissions { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual ICollection<User> Users { get; set; }
    }
}