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
        /// The custom table rows by table
        /// </summary>
        public const string CUSTOMTABLEROWS_BY_TABLE = "cache.customtablerows.bytable.{0}";

        /// <summary>
        /// The custom table rows pattern
        /// </summary>
        public const string CUSTOMTABLEROWS_PATTERN = "cache.customtablerows";

        /// <summary>
        /// All the settings
        /// </summary>
        public const string SETTINGS_GET_ALL = "cache.settings.all";

        /// <summary>
        /// all the shelters
        /// </summary>
        public const string SHELTERS_ALL = "cache.shelters.all";

        /// <summary>
        /// The notifications all
        /// </summary>
        public const string NOTIFICATIONS_ALL = "cache.notifications.all";

        /// <summary>
        /// The locations all
        /// </summary>
        public const string LOCATIONS_ALL = "cache.locations.all";
    }
}