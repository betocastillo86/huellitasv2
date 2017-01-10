//-----------------------------------------------------------------------
// <copyright file="ContentUser.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    using System;
    using Huellitas.Data.Entities.Enums;

    /// <summary>
    /// Content User Relationship
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public class ContentUser : BaseEntity
    {
        /// <summary>
        /// Gets or sets the content identifier.
        /// </summary>
        /// <value>
        /// The content identifier.
        /// </value>
        public int ContentId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the relation type identifier.
        /// </summary>
        /// <value>
        /// The relation type identifier.
        /// </value>
        public short RelationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public virtual Content Content { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the type of the relation.
        /// </summary>
        /// <value>
        /// The type of the relation.
        /// </value>
        public virtual ContentUserRelationType RelationType
        {
            get
            {
                return (ContentUserRelationType)this.RelationTypeId;
            }

            set
            {
                this.RelationTypeId = Convert.ToInt16(value);
            }
        }
    }
}