using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class Permission : BaseEntity
    {
        public Permission()
        {
            RolePemission = new HashSet<RolePemission>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RolePemission> RolePemission { get; set; }
    }
}
