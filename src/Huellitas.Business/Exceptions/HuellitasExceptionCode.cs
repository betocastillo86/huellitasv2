//-----------------------------------------------------------------------
// <copyright file="HuellitasExceptionCode.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Exceptions
{
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
        /// When has an error trying to access to social login
        /// </summary>
        ErrorTryingExternalLogin = 151,

        /// <summary>
        /// The invalid external authentication provider
        /// </summary>
        InvalidExternalAuthenticationProvider = 152,

        /// <summary>
        /// The cant update email notification
        /// </summary>
        CantUpdateEmailNotification = 200,

        /// <summary>
        /// The invalid foreign key
        /// </summary>
        InvalidForeignKey = 500,

        /// <summary>
        /// The invalid index
        /// </summary>
        InvalidIndex = 501,

        /// <summary>
        /// The unhandled exception
        /// </summary>
        ServerError = 1000,

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