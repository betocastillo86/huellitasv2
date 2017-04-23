//-----------------------------------------------------------------------
// <copyright file="ILogService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using Huellitas.Data.Entities;
    using Huellitas.Data.Entities.Enums;
    using Huellitas.Data.Infraestructure;

    /// <summary>
    /// Interface for logging
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Inserts the specified log level.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="shortMessage">The short message.</param>
        /// <param name="fullMessage">The full message.</param>
        /// <param name="user">The user.</param>
        /// <returns>the value</returns>
        Log Insert(LogLevel logLevel, string shortMessage, string fullMessage = "", User user = null);

        IPagedList<Log> GetAll(string keyword, int page = 0, int pageSize = int.MaxValue);
    }
}