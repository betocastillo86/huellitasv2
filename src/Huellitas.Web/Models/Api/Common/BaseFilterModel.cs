using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Models.Api.Common
{
    public class BaseFilterModel 
    {
        public string orderBy { get; set; }

        public int page { get; set; } = 0;

        public int pageSize { get; set; } = 10;
    }
}
