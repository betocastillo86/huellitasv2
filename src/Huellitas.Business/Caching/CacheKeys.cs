//-----------------------------------------------------------------------
// <copyright file="CacheKeys.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Caching
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The Cache Keys
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Reviewed.")]
    public static class CacheKeys
    {
        /// <summary>
        /// All the settings
        /// </summary>
        public const string SETTINGS_GET_ALL = "cache.settings.all";
    }
}