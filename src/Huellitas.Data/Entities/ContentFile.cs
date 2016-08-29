using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class ContentFile : BaseEntity
    {
        public int ContentId { get; set; }
        public int FileId { get; set; }
        public int DisplayOrder { get; set; }

        public virtual Content Content { get; set; }
        public virtual File File { get; set; }
    }
}
