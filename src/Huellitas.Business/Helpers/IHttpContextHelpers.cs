using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huellitas.Business.Helpers
{
    public interface IHttpContextHelpers
    {
        string GetCurrentIpAddress();

        string GetThisPageUrl(bool includeQueryString);
    }
}
