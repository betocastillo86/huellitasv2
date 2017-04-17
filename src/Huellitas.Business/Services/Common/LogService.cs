//-----------------------------------------------------------------------
// <copyright file="LogService.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Services
{
    using System;
    using Huellitas.Business.Helpers;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Entities.Enums;

    /// <summary>
    /// Log Service
    /// </summary>
    /// <seealso cref="Huellitas.Business.Services.ILogService" />
    public class LogService : ILogService
    {
        #region props

        /// <summary>
        /// The context helpers
        /// </summary>
        private readonly IHttpContextHelpers contextHelpers;

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
            IHttpContextHelpers contextHelpers)
        {
            this.logRepository = logRepository;
            this.contextHelpers = contextHelpers;
        }

        #endregion ctor

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
    }
}