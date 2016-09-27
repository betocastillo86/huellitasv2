//-----------------------------------------------------------------------
// <copyright file="RolePemission.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    /// <summary>
    /// Role Permission
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class RolePemission : BaseEntity
    {
        /// <summary>
        /// Gets or sets the permission.
        /// </summary>
        /// <value>
        /// The permission.
        /// </value>
        public virtual Permission Permission { get; set; }

        /// <summary>
        /// Gets or sets the permission identifier.
        /// </summary>
        /// <value>
        /// The permission identifier.
        /// </value>
        public int PermissionId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public int RoleId { get; set; }
    }
}