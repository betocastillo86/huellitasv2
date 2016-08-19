using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class User : BaseEntity
    {
        public User()
        {
            AdoptionFormAnswer = new HashSet<AdoptionFormAnswer>();
            Content = new HashSet<Content>();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Deleted { get; set; }
        public int RoleId { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<AdoptionFormAnswer> AdoptionFormAnswer { get; set; }
        public virtual ICollection<Content> Content { get; set; }
        public virtual Role Role { get; set; }
    }
}
