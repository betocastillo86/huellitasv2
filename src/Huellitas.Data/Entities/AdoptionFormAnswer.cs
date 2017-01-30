//-----------------------------------------------------------------------
// <copyright file="AdoptionFormAnswer.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Adoption Form Answer
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public partial class AdoptionFormAnswer : BaseEntity
    {
        /// <summary>
        /// Gets or sets the additional information.
        /// </summary>
        /// <value>
        /// The additional information.
        /// </value>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Gets or sets the adoption form.
        /// </summary>
        /// <value>
        /// The adoption form.
        /// </value>
        public virtual AdoptionForm AdoptionForm { get; set; }

        /// <summary>
        /// Gets or sets the adoption form identifier.
        /// </summary>
        /// <value>
        /// The adoption form identifier.
        /// </value>
        public int AdoptionFormId { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public short Status { get; set; }

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
        /// Gets or sets the status enum.
        /// </summary>
        /// <value>
        /// The status enum.
        /// </value>
        [NotMapped]
        public AdoptionFormAnswerStatus StatusEnum
        {
            get
            {
                return (AdoptionFormAnswerStatus)this.Status;
            }

            set
            {
                this.Status = Convert.ToInt16(value);
            }
        }
    }
}