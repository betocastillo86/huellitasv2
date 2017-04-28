//-----------------------------------------------------------------------
// <copyright file="Comment.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Comment table
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class Comment : BaseEntity
    {
        /// <summary>
        /// The comments1
        /// </summary>
        private ICollection<Comment> children;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the parent comment identifier.
        /// </summary>
        /// <value>
        /// The parent comment identifier.
        /// </value>
        public int? ParentCommentId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the content identifier.
        /// </summary>
        /// <value>
        /// The content identifier.
        /// </value>
        public int? ContentId { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public System.DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public System.DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Comment"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the number comments.
        /// </summary>
        /// <value>
        /// The number comments.
        /// </value>
        public int NumComments { get; set; }

        /// <summary>
        /// Gets or sets the <c>ip</c> address.
        /// </summary>
        /// <value>
        /// The <c>ip</c> address.
        /// </value>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the comments1.
        /// </summary>
        /// <value>
        /// The comments1.
        /// </value>
        public virtual ICollection<Comment> Children
        {
            get { return this.children ?? (this.children = new List<Comment>()); }
            protected set { this.children = value; }
        }

        /// <summary>
        /// Gets or sets the comment1.
        /// </summary>
        /// <value>
        /// The comment1.
        /// </value>
        public virtual Comment ParentComment { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public virtual Content Content { get; set; }
    }
}