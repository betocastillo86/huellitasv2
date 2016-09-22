using Huellitas.Data.Entities;
using Huellitas.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Business.Services.Common
{
    public interface ILogService
    {
        Log Insert(LogLevel logLevel, string shortMessage, string fullMessage = "", User user = null);
    }
}
