using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Huellitas.Data.Entities;
using Huellitas.Data.Entities.Enums;
using Huellitas.Data.Core;
using Huellitas.Business.Helpers;

namespace Huellitas.Business.Services.Common
{
    public class LogService : ILogService
    {
        #region props
        private readonly IRepository<Log> _logRepository;
        private readonly IHttpContextHelpers _contextHelpers;
        #endregion
        #region ctor
        public LogService(IRepository<Log> logRepository,
            IHttpContextHelpers contextHelpers)
        {
            _logRepository = logRepository;
            _contextHelpers = contextHelpers;
        }
        #endregion

        public Log Insert(LogLevel logLevel, string shortMessage, string fullMessage = "", User user = null)
        {
            if (string.IsNullOrEmpty(shortMessage))
                return null;

            var log = new Log()
            {
                CreationDate = DateTime.Now,
                FullMessage = fullMessage,
                ShortMessage = shortMessage,
                IpAddress = _contextHelpers.GetCurrentIpAddress(),
                PageUrl = _contextHelpers.GetThisPageUrl(true),
                UserId = user != null ? user.Id : (int?)null,
                LogLevel = logLevel
            };

            _logRepository.Insert(log);

            return log;
        }
    }
}
