//-----------------------------------------------------------------------
// <copyright file="CommentModel.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Models.Api
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Content Model
    /// </summary>
    public class CommentModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Required]
        [StringLength(400, MinimumLength = 2)]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the count comments.
        /// </summary>
        /// <value>
        /// The count comments.
        /// </value>
        public int CountComments { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public BaseUserModel User { get; set; }

        /// <summary>
        /// Gets or sets the content identifier.
        /// </summary>
        /// <value>
        /// The content identifier.
        /// </value>
        public int? ContentId { get; set; }

        /// <summary>
        /// Gets or sets the parent comment identifier.
        /// </summary>
        /// <value>
        /// The parent comment identifier.
        /// </value>
        public int? ParentCommentId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can delete.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can delete; otherwise, <c>false</c>.
        /// </value>
        public bool CanDelete { get; set; }

        /// <summary>
        /// Gets or sets the first comments.
        /// </summary>
        /// <value>
        /// The first comments.
        /// </value>
        public IList<CommentModel> FirstComments { get; set; }
    }
}