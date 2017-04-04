//-----------------------------------------------------------------------
// <copyright file="ISystemSettingService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services.Configuration
{
    using System.Threading.Tasks;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;

    /// <summary>
    /// Interface System Settings Service
    /// </summary>
    public interface ISystemSettingService
    {
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the list of settings</returns>
        IPagedList<SystemSetting> Get(string key = null, string value = null, int page = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets the by key.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <returns></returns>
        SystemSetting GetByKey(string keyword);

        /// <summary>
        /// Updates the specified setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <returns>the task</returns>
        Task Update(SystemSetting setting);

        /// <summary>
        /// Gets the cached setting.
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>the value of the key typed</returns>
        T GetCachedSetting<T>(string key);
    }
}