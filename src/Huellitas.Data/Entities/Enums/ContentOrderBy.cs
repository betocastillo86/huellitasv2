//-----------------------------------------------------------------------
// <copyright file="ContentOrderBy.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    /// <summary>
    /// Content Order By
    /// </summary>
    public enum ContentOrderBy
    {
        /// <summary>
        /// The display order
        /// </summary>
        DisplayOrder,

        /// <summary>
        /// The name
        /// </summary>
        Name,

        /// <summary>
        /// The created date
        /// </summary>
        CreatedDate,

        /// <summary>
        /// Featured and after created date
        /// </summary>
        Featured,

        /// <summary>
        /// Random order
        /// </summary>
        Random
    }
}