//-----------------------------------------------------------------------
// <copyright file="HuellitasExceptionCode.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Exceptions
{
    using System.ComponentModel;

    /// <summary>
    /// Exceptions codes
    /// </summary>
    public enum HuellitasExceptionCode
    {
        /// <summary>
        /// The shelter not found
        /// </summary>
        ShelterNotFound = 100,

        /// <summary>
        /// The user email was already used
        /// </summary>
        UserEmailAlreadyUsed = 150,

        /// <summary>
        /// The invalid foreign key
        /// </summary>
        InvalidForeignKey = 500,

        /// <summary>
        /// The row not found
        /// </summary>
        RowNotFound = 2000,

        /// <summary>
        /// The bad argument
        /// </summary>
        BadArgument = 2001
    }
}