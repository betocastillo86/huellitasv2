using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Infraestructure.WebApi
{
    public class ApiHeadersList
    {
        public const string PAGINATION_HASNEXTPAGE = "X-Pagination-HasNextPage";
        public const string PAGINATION_TOTALCOUNT = "X-Pagination-TotalCount";
        public const string PAGINATION_COUNT = "X-Pagination-Count";
    }
}
