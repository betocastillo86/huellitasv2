//-----------------------------------------------------------------------
// <copyright file="SystemSettingService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Caching;
    using Beto.Core.Data;
    using Beto.Core.EventPublisher;
    using Huellitas.Business.Caching;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;

    /// <summary>
    /// System Setting Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.ISystemSettingService" />
    public class SystemSettingService : ISystemSettingService
    {
        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager cacheManager;

        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// The system setting repository
        /// </summary>
        private readonly IRepository<SystemSetting> systemSettingRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemSettingService"/> class.
        /// </summary>
        /// <param name="systemSettingRepository">The system setting repository.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="publisher">The publisher.</param>
        public SystemSettingService(
            IRepository<SystemSetting> systemSettingRepository,
            ICacheManager cacheManager,
            IPublisher publisher)
        {
            this.systemSettingRepository = systemSettingRepository;
            this.cacheManager = cacheManager;
            this.publisher = publisher;
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the list of settings
        /// </returns>
        public IPagedList<SystemSetting> Get(string key = null, string value = null, int page = 0, int pageSize = int.MaxValue)
        {
            var query = this.systemSettingRepository.Table;

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(c => c.Name.Contains(key));
            }

            if (!string.IsNullOrEmpty(value))
            {
                query = query.Where(c => c.Value.Contains(value));
            }

            return new PagedList<SystemSetting>(query, page, pageSize);
        }

        /// <summary>
        /// Gets the by key.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <returns>the setting</returns>
        public SystemSetting GetByKey(string keyword)
        {
            return this.systemSettingRepository.Table.FirstOrDefault(c => c.Name.Equals(keyword));
        }

        /// <summary>
        /// Gets the cached setting.
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>
        /// the value of the key typed
        /// </returns>
        public T GetCachedSetting<T>(string key)
        {
            string value = string.Empty;
            if (this.GetAllCachedSettings().TryGetValue(key, out value))
            {
                TypeConverter destinationConverter = TypeDescriptor.GetConverter(typeof(T));
                return (T)destinationConverter.ConvertFrom(null, System.Globalization.CultureInfo.InvariantCulture, value);
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Updates the specified setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Update(SystemSetting setting)
        {
            await this.systemSettingRepository.UpdateAsync(setting);

            await this.publisher.EntityUpdated(setting);
        }

        /// <summary>
        /// Gets all cached settings.
        /// </summary>
        /// <returns>The list of settings</returns>
        protected IDictionary<string, string> GetAllCachedSettings()
        {
            var allKeys = this.cacheManager.Get(
                CacheKeys.SETTINGS_GET_ALL,
                () =>
            {
                var dictionarySettings = new Dictionary<string, string>();
                foreach (var setting in this.Get())
                {
                    dictionarySettings.Add(setting.Name, setting.Value);
                }

                return dictionarySettings;
            });
            return allKeys;
        }
    }
}