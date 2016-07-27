using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class Permission
    {
        public Permission()
        {
            RolePemission = new HashSet<RolePemission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RolePemission> RolePemission { get; set; }
    }
}
