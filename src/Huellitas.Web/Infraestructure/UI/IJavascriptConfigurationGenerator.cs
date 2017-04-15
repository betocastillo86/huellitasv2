//-----------------------------------------------------------------------
// <copyright file="IJavascriptConfigurationGenerator.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Web.Infraestructure.UI
{
    /// <summary>
    /// Interface of <c>javascript</c> generator
    /// </summary>
    public interface IJavascriptConfigurationGenerator
    {
        /// <summary>
        /// Creates the <c>javascript</c> configuration file.
        /// </summary>
        void CreateJavascriptConfigurationFile();
    }
}