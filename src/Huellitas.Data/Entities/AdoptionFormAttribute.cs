using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class AdoptionFormAttribute
    {
        public int Id { get; set; }
        public int AdoptionFormId { get; set; }
        public int AttributeId { get; set; }
        public string Value { get; set; }

        public virtual AdoptionForm AdoptionForm { get; set; }
        public virtual CustomTableRow Attribute { get; set; }
    }
}
