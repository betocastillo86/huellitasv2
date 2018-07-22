//-----------------------------------------------------------------------
// <copyright file="LogService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Beto.Core.Exceptions;
    using Beto.Core.Helpers;
    using Huellitas.Data.Entities;
    
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Log Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.ILogService" />
    public class LogService : ILoggerService, ILogService
    {
        #region props

        /// <summary>
        /// The context helpers
        /// </summary>
        private readonly IHttpContextHelper contextHelpers;

        /// <summary>
        /// The log repository
        /// </summary>
        private readonly IRepository<Log> logRepository;

        #endregion props

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="LogService"/> class.
        /// </summary>
        /// <param name="logRepository">The log repository.</param>
        /// <param name="contextHelpers">The context helpers.</param>
        public LogService(
            IRepository<Log> logRepository,
            IHttpContextHelper contextHelpers)
        {
            this.logRepository = logRepository;
            this.contextHelpers = contextHelpers;
        }

        #endregion ctor

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Clear()
        {
            var logs = this.logRepository.Table.ToList();
            foreach (var log in logs)
            {
                await this.logRepository.DeleteAsync(log);
            }
        }

        /// <summary>
        /// Gets all the logs by filter
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the list of logs
        /// </returns>
        public IPagedList<Log> GetAll(string keyword, int page = 0, int pageSize = int.MaxValue)
        {
            var query = this.logRepository.Table
                .Include(c => c.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.FullMessage.Contains(keyword) || c.ShortMessage.Contains(keyword));
            }

            query = query.OrderByDescending(c => c.CreationDate);

            return new PagedList<Log>(query, page, pageSize);
        }

        /// <summary>
        /// Inserts the specified log level.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="shortMessage">The short message.</param>
        /// <param name="fullMessage">The full message.</param>
        /// <param name="user">The user.</param>
        /// <returns>the value</returns>
        public Log Insert(LogLevel logLevel, string shortMessage, string fullMessage = "", User user = null)
        {
            if (string.IsNullOrEmpty(shortMessage))
            {
                return null;
            }

            var log = new Log()
            {
                CreationDate = DateTime.Now,
                FullMessage = fullMessage,
                ShortMessage = shortMessage,
                IpAddress = this.contextHelpers.GetCurrentIpAddress(),
                PageUrl = this.contextHelpers.GetThisPageUrl(true),
                UserId = user != null ? user.Id : (int?)null,
                LogLevel = logLevel
            };

            this.logRepository.Insert(log);

            return log;
        }

        public void Insert(string shortMessage, string fullMessage = "")
        {
            this.Insert(LogLevel.Error, shortMessage, fullMessage, null);
        }

        public async Task InsertAsync(string shortMessage, string fullMessage = "")
        {
            if (string.IsNullOrEmpty(shortMessage))
            {
                throw new ArgumentNullException(nameof(shortMessage));
            }

            var log = new Log()
            {
                CreationDate = DateTime.Now,
                FullMessage = fullMessage,
                ShortMessage = shortMessage,
                IpAddress = this.contextHelpers.GetCurrentIpAddress(),
                PageUrl = this.contextHelpers.GetThisPageUrl(true),
                UserId = null,
                LogLevel = LogLevel.Error
            };

            await this.logRepository.InsertAsync(log);
        }
    }
}