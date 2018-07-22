//-----------------------------------------------------------------------
// <copyright file="SystemSettingServiceExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Extensions.Services
{
    using Beto.Core.Data.Configuration;
    using Huellitas.Data.Entities;

    /// <summary>
    /// System Setting Service Extensions
    /// </summary>
    public static class SystemSettingServiceExtensions
    {
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="settingService">The setting service.</param>
        /// <param name="key">The key.</param>
        /// <returns>the value</returns>
        public static TValue Get<TValue>(this ICoreSettingService settingService, string key)
        {
            return settingService.GetCachedSetting<TValue, SystemSetting>(key);
        }
    }
}