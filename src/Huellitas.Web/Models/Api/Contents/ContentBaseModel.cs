using Huellitas.Data.Entities;
using Huellitas.Web.Models.Api.Common;
using Huellitas.Web.Models.Api.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Models.Api.Contents
{
    public class ContentBaseModel : BaseModel
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Body { get; set; }

        public ContentType TypeId { get; set; }

        public StatusType Status { get; set; }

        public string Image { get; set; }

        public int DisplayOrder { get; set; }

        public int Views { get; set; }

        public int CommentsCount { get; set; }

        public LocationModel Location { get; set; }

        public string Email { get; set; }
    }
}
