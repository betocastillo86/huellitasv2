using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class CustomTableRow
    {
        public CustomTableRow()
        {
            AdoptionForm = new HashSet<AdoptionForm>();
            AdoptionFormAttribute = new HashSet<AdoptionFormAttribute>();
        }

        public int Id { get; set; }
        public int CustomTableId { get; set; }
        public string Value { get; set; }
        public string AdditionalInfo { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<AdoptionForm> AdoptionForm { get; set; }
        public virtual ICollection<AdoptionFormAttribute> AdoptionFormAttribute { get; set; }
        public virtual CustomTable CustomTable { get; set; }
    }
}
