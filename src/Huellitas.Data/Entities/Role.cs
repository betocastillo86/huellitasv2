using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class Role : BaseEntity
    {
        public Role()
        {
            RolePemissions = new HashSet<RolePemission>();
            Users = new HashSet<User>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RolePemission> RolePemissions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
