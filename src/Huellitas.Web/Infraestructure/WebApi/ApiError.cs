using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Infraestructure.WebApi
{
    public class ApiError
    {
        public string Message { get; set; }

        public string Code { get; set; }

        public string Target { get; set; }

        public IList<ApiError> Details { get; set; }
    }
}
