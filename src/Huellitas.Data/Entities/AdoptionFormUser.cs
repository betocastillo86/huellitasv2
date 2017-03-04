//-----------------------------------------------------------------------
// <copyright file="AdoptionFormUser.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    /// <summary>
    /// Users allowed to answer adoption forms
    /// </summary>
    /// <seealso cref="Huellitas.Data.Entities.BaseEntity" />
    public class AdoptionFormUser : BaseEntity
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the adoption form identifier.
        /// </summary>
        /// <value>
        /// The adoption form identifier.
        /// </value>
        public int AdoptionFormId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the adoption form.
        /// </summary>
        /// <value>
        /// The adoption form.
        /// </value>
        public virtual AdoptionForm AdoptionForm { get; set; }
    }
}