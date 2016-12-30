//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using Enums;

    /// <summary>
    /// The class User
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class User : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            this.AdoptionFormAnswer = new HashSet<AdoptionFormAnswer>();
            this.Content = new HashSet<Content>();
        }

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
        public virtual ICollection<Content> Content { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="User"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

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
        /// Gets or sets the role enum.
        /// </summary>
        /// <value>
        /// The role enum.
        /// </value>
        public virtual RoleEnum RoleEnum
        {
            get
            {
                return (Enums.RoleEnum)this.RoleId;
            }

            set
            {
                this.RoleId = Convert.ToInt32(value);
            }
        }
    }
}