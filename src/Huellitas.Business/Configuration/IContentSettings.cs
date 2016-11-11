//-----------------------------------------------------------------------
// <copyright file="IContentSettings.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Configuration
{
    /// <summary>
    /// Interface of Content Settings
    /// </summary>
    public interface IContentSettings
    {
        /// <summary>
        /// Gets the default size of the page.
        /// </summary>
        /// <value>
        /// The default size of the page.
        /// </value>
        int DefaultPageSize { get; }
    }
}