﻿//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using Beto.Core.Data.Users;

    /// <summary>
    /// The class User
    /// </summary>
    /// <seealso cref="Beto.Core.Data.Users.IUserEntity" />
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class User : BaseEntity, IUserEntity
    {
        /// <summary>
        /// The comments
        /// </summary>
        private ICollection<Comment> comments;

        /// <summary>
        /// Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        public User()
        {
            this.AdoptionFormAnswer = new HashSet<AdoptionFormAnswer>();
            this.Contents = new HashSet<Content>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the phone number2.
        /// </summary>
        /// <value>
        /// The phone number2.
        /// </value>
        public string PhoneNumber2 { get; set; }

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

        /// <summary>
        /// Gets or sets the facebook identifier.
        /// </summary>
        /// <value>
        /// The facebook identifier.
        /// </value>
        public string FacebookId { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="User" /> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>
        /// The ip address.
        /// </value>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the salt.
        /// </summary>
        /// <value>
        /// The salt.
        /// </value>
        public string Salt { get; set; }

        /// <summary>
        /// Gets or sets the password recovery token.
        /// </summary>
        /// <value>
        /// The password recovery token.
        /// </value>
        public string PasswordRecoveryToken { get; set; }

        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public virtual Location Location { get; set; }

        /// <summary>
        /// Gets or sets the adoption form answer.
        /// </summary>
        /// <value>
        /// The adoption form answer.
        /// </value>
        public virtual ICollection<AdoptionFormAnswer> AdoptionFormAnswer { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public virtual ICollection<Content> Contents { get; set; }

        /// <summary>
        /// Gets or sets the role enum.
        /// </summary>
        /// <value>
        /// The role enum.
        /// </value>
        public virtual RoleEnum RoleEnum
        {
            get
            {
                return (RoleEnum)this.RoleId;
            }

            set
            {
                this.RoleId = Convert.ToInt32(value);
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

        public Guid? DeviceId { get; set; }

        public Guid? IOsDeviceId { get; set; }
    }
}