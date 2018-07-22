//-----------------------------------------------------------------------
// <copyright file="Content.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Beto.Core.Data.Common;

    /// <summary>
    /// The class Content
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class Content : BaseEntity, ISeoEntity
    {
        /// <summary>
        /// The users
        /// </summary>
        private ICollection<ContentUser> users;

        /// <summary>
        /// the comments
        /// </summary>
        private ICollection<Comment> comments;

        /// <summary>
        /// Initializes a new instance of the <see cref="Content"/> class.
        /// </summary>
        public Content()
        {
            this.AdoptionForm = new HashSet<AdoptionForm>();
            this.ContentAttributes = new HashSet<ContentAttribute>();
            this.ContentCategories = new HashSet<ContentCategory>();
            this.ContentFiles = new HashSet<ContentFile>();
            this.RelatedContentContent = new HashSet<RelatedContent>();
            this.RelatedContentRelatedContentNavigation = new HashSet<RelatedContent>();
        }

        /// <summary>
        /// Gets or sets the adoption form.
        /// </summary>
        /// <value>
        /// The adoption form.
        /// </value>
        public virtual ICollection<AdoptionForm> AdoptionForm { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the comments count.
        /// </summary>
        /// <value>
        /// The comments count.
        /// </value>
        public int CommentsCount { get; set; }

        /// <summary>
        /// Gets or sets the content attributes.
        /// </summary>
        /// <value>
        /// The content attributes.
        /// </value>
        public virtual ICollection<ContentAttribute> ContentAttributes { get; set; }

        /// <summary>
        /// Gets or sets the content category.
        /// </summary>
        /// <value>
        /// The content category.
        /// </value>
        public virtual ICollection<ContentCategory> ContentCategories { get; set; }

        /// <summary>
        /// Gets or sets the content file.
        /// </summary>
        /// <value>
        /// The content file.
        /// </value>
        public virtual ICollection<ContentFile> ContentFiles { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Content"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        /// <value>
        /// The display order.
        /// </value>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Content"/> is featured.
        /// </summary>
        /// <value>
        ///   <c>true</c> if featured; otherwise, <c>false</c>.
        /// </value>
        public bool Featured { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public virtual File File { get; set; }

        /// <summary>
        /// Gets or sets the file identifier.
        /// </summary>
        /// <value>
        /// The file identifier.
        /// </value>
        public int? FileId { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public virtual Location Location { get; set; }

        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the content of the related content.
        /// </summary>
        /// <value>
        /// The content of the related content.
        /// </value>
        public virtual ICollection<RelatedContent> RelatedContentContent { get; set; }

        /// <summary>
        /// Gets or sets the related content related content navigation.
        /// </summary>
        /// <value>
        /// The related content related content navigation.
        /// </value>
        public virtual ICollection<RelatedContent> RelatedContentRelatedContentNavigation { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual ICollection<ContentUser> Users
        {
            get
            {
                return this.users ?? (this.users = new List<ContentUser>());
            }

            set
            {
                this.users = value;
            }
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments ?? (this.comments = new List<Comment>());
            }

            set
            {
                this.comments = value;
            }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public short Status { get; set; }

        /// <summary>
        /// Gets or sets the type of the status.
        /// </summary>
        /// <value>
        /// The type of the status.
        /// </value>
        [NotMapped]
        public virtual StatusType StatusType
        {
            get
            {
                return (StatusType)this.Status;
            }

            set
            {
                this.Status = Convert.ToInt16(value);
            }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [NotMapped]
        public virtual ContentType Type
        {
            get
            {
                return (ContentType)this.TypeId;
            }

            set
            {
                this.TypeId = Convert.ToInt16(value);
            }
        }

        /// <summary>
        /// Gets or sets the type identifier.
        /// </summary>
        /// <value>
        /// The type identifier.
        /// </value>
        public short TypeId { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the closing date.
        /// </summary>
        /// <value>
        /// The closing date.
        /// </value>
        public DateTime? ClosingDate { get; set; }

        /// <summary>
        /// Gets or sets the starting date.
        /// </summary>
        /// <value>
        /// The starting date.
        /// </value>
        public DateTime? StartingDate { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the views.
        /// </summary>
        /// <value>
        /// The views.
        /// </value>
        public int Views { get; set; }

        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        public string FriendlyName { get; set; }
    }
}