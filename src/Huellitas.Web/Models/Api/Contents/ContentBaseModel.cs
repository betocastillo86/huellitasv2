using Huellitas.Web.Models.Api.Common;
using Huellitas.Web.Models.Api.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Models.Api.Contents
{
    public class ContentBaseModel : BaseModel
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public int TypeId { get; set; }

        public int Status { get; set; }

        public string Image { get; set; }

        public int DisplayOrder { get; set; }

        public int Views { get; set; }

        public int CommentsCount { get; set; }
    }
}
