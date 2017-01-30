//-----------------------------------------------------------------------
// <copyright file="AdoptionFormAnswerStatus.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    /// <summary>
    /// Adoption Form Answer Status
    /// </summary>
    public enum AdoptionFormAnswerStatus : short
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,

        /// <summary>
        /// The approved
        /// </summary>
        Approved = 1,

        /// <summary>
        /// The denied
        /// </summary>
        Denied = 2,

        /// <summary>
        /// The already adopted
        /// </summary>
        AlreadyAdopted = 3
    }
}