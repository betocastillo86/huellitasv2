//-----------------------------------------------------------------------
// <copyright file="AdoptionForm.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The class Adoption Form
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class AdoptionForm : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdoptionForm"/> class.
        /// </summary>
        public AdoptionForm()
        {
            this.Answers = new HashSet<AdoptionFormAnswer>();
            this.Attributes = new HashSet<AdoptionFormAttribute>();
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
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the content identifier.
        /// </summary>
        /// <value>
        /// The content identifier.
        /// </value>
        public int ContentId { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the family members.
        /// </summary>
        /// <value>
        /// The family members.
        /// </value>
        public short FamilyMembers { get; set; }

        /// <summary>
        /// Gets or sets the family members age.
        /// </summary>
        /// <value>
        /// The family members age.
        /// </value>
        public string FamilyMembersAge { get; set; }

        /// <summary>
        /// Gets or sets the job.
        /// </summary>
        /// <value>
        /// The job.
        /// </value>
        public virtual CustomTableRow Job { get; set; }

        /// <summary>
        /// Gets or sets the job identifier.
        /// </summary>
        /// <value>
        /// The job identifier.
        /// </value>
        public int JobId { get; set; }

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
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the town.
        /// </summary>
        /// <value>
        /// The town.
        /// </value>
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets the last status.
        /// </summary>
        /// <value>
        /// The last status.
        /// </value>
        public short? LastStatus { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the autoreply token.
        /// </summary>
        /// <value>
        /// The autoreply token.
        /// </value>
        public Guid AutoreplyToken { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        public DateTime BirthDate { get; set; }

        public int? LastResponseUserId { get; set; }

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


        public virtual User LastResponseUser { get; set; }

        /// <summary>
        /// Gets or sets the adoption form answer.
        /// </summary>
        /// <value>
        /// The adoption form answer.
        /// </value>
        public virtual ICollection<AdoptionFormAnswer> Answers { get; set; }

        /// <summary>
        /// Gets or sets the adoption form attribute.
        /// </summary>
        /// <value>
        /// The adoption form attribute.
        /// </value>
        public virtual ICollection<AdoptionFormAttribute> Attributes { get; set; }

        /// <summary>
        /// Gets or sets the uses.
        /// </summary>
        /// <value>
        /// The uses.
        /// </value>
        public virtual ICollection<AdoptionFormUser> Users { get; set; }

        public DateTime? ReponseDate { get; set; }

        /// <summary>
        /// Gets or sets the status enum.
        /// </summary>
        /// <value>
        /// The status enum.
        /// </value>
        [NotMapped]
        public AdoptionFormAnswerStatus LastStatusEnum
        {
            get
            {
                return (AdoptionFormAnswerStatus)this.LastStatus;
            }

            set
            {
                this.LastStatus = Convert.ToInt16(value);
            }
        }
    }
}