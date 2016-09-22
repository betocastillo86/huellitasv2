using Huellitas.Business.Services.Common;
using Huellitas.Data.Entities;
using Huellitas.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Business.Extensions.Services
{
    public static class LogExtensions
    {
        public static void Error(this ILogService log, string shortMessage, string fullMessage = null, User user = null)
        {
            log.Insert(LogLevel.Error, shortMessage, fullMessage, user);
        }

        public static void Error(this ILogService log, Exception e, User user = null)
        {
            log.Insert(LogLevel.Error, e.Message, e.ToString(), user);
        }

        public static void Information(this ILogService log, string shortMessage, string fullMessage = null, User user = null)
        {
            log.Insert(LogLevel.Information, shortMessage, fullMessage, user);
        }

        public static void Debug(this ILogService log, string shortMessage, string fullMessage = null, User user = null, bool writeConsole = false)
        {
            log.Insert(LogLevel.Debug, shortMessage, fullMessage, user);
            if (writeConsole)
                System.Console.WriteLine("{0} - {1}", DateTime.Now, shortMessage);
        }

        public static void Console(this ILogService log, string shortMessage, string fullMessage = null, User user = null)
        {
            log.Insert(LogLevel.Console, shortMessage, fullMessage, user);
            System.Console.WriteLine("{0} - {1}", DateTime.Now, shortMessage);
        }
    }
}
