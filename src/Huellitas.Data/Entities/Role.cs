using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class Role
    {
        public Role()
        {
            RolePemission = new HashSet<RolePemission>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RolePemission> RolePemission { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
