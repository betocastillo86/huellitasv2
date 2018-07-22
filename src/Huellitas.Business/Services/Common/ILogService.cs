//-----------------------------------------------------------------------
// <copyright file="ILogService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Huellitas.Data.Entities;

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

        /// <summary>
        /// Gets all the logs by filter
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the list of logs</returns>
        IPagedList<Log> GetAll(string keyword, int page = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns>the task</returns>
        Task Clear();
    }
}