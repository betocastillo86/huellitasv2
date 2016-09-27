//-----------------------------------------------------------------------
// <copyright file="RelatedContent.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    /// <summary>
    /// Related Content
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class RelatedContent : BaseEntity
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public virtual Content Content { get; set; }

        /// <summary>
        /// Gets or sets the content identifier.
        /// </summary>
        /// <value>
        /// The content identifier.
        /// </value>
        public int ContentId { get; set; }

        /// <summary>
        /// Gets or sets the related content identifier.
        /// </summary>
        /// <value>
        /// The related content identifier.
        /// </value>
        public int RelatedContentId { get; set; }

        /// <summary>
        /// Gets or sets the related content navigation.
        /// </summary>
        /// <value>
        /// The related content navigation.
        /// </value>
        public virtual Content RelatedContentNavigation { get; set; }

        /// <summary>
        /// Gets or sets the type of the relation.
        /// </summary>
        /// <value>
        /// The type of the relation.
        /// </value>
        public short RelationType { get; set; }
    }
}