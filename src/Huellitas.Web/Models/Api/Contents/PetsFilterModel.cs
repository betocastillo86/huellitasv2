using Huellitas.Web.Models.Api.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Models.Api.Contents
{
    public class PetsFilterModel : BaseFilterModel
    {
        public string type { get; set; }

        public string age { get; set; }

        public string genre { get; set; }

        public string size { get; set; }

        public string shelter { get; set; }

        public string keyword { get; set; }
    }
}
