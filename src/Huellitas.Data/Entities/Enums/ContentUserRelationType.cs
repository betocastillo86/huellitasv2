//-----------------------------------------------------------------------
// <copyright file="ContentUserRelationType.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Enums
{
    /// <summary>
    /// Content User Relation Type
    /// </summary>
    public enum ContentUserRelationType : short
    {
        /// <summary>
        /// The shelter where the user works
        /// </summary>
        Shelter = 1,

        /// <summary>
        /// The godfather
        /// </summary>
        Parent = 2
    }
}