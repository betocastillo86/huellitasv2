using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class ContentAttribute
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string Value { get; set; }
        public string Attribute { get; set; }

        public virtual Content Content { get; set; }
    }
}
