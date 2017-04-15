//-----------------------------------------------------------------------
// <copyright file="IJavascriptConfigurationGenerator.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.UI
{
    /// <summary>
    /// Interface of javascript generator
    /// </summary>
    public interface IJavascriptConfigurationGenerator
    {
        /// <summary>
        /// Creates the javascript configuration file.
        /// </summary>
        void CreateJavascriptConfigurationFile();
    }
}