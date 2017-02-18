//-----------------------------------------------------------------------
// <copyright file="INotificationSettings.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Configuration
{
    /// <summary>
    /// Interface of notification settings
    /// </summary>
    public interface INotificationSettings
    {
        /// <summary>
        /// Gets the body base HTML.
        /// </summary>
        /// <value>
        /// The body base HTML.
        /// </value>
        string BodyBaseHtml { get; }

        /// <summary>
        /// Gets the name of the email sender.
        /// </summary>
        /// <value>
        /// The name of the email sender.
        /// </value>
        string EmailSenderName { get; }
    }
}