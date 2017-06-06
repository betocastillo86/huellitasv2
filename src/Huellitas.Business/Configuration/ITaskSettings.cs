//-----------------------------------------------------------------------
// <copyright file="ITaskSettings.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Configuration
{
    /// <summary>
    /// Interface Task Settings
    /// </summary>
    public interface ITaskSettings
    {
        /// <summary>
        /// Gets the send emails interval.
        /// </summary>
        /// <value>
        /// The send emails interval.
        /// </value>
        int SendEmailsInterval { get; }
    }
}