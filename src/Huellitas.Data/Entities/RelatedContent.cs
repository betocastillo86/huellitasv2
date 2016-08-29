using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class RelatedContent : BaseEntity
    {
        public int ContentId { get; set; }
        public int RelatedContentId { get; set; }
        public short RelationType { get; set; }

        public virtual Content Content { get; set; }
        public virtual Content RelatedContentNavigation { get; set; }
    }
}
