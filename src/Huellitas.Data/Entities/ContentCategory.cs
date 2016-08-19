using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class ContentCategory : BaseEntity
    {
        public int ContentId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Content Content { get; set; }
    }
}
