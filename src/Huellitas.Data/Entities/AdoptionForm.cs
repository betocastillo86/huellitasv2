using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class AdoptionForm : BaseEntity
    {
        public AdoptionForm()
        {
            AdoptionFormAnswer = new HashSet<AdoptionFormAnswer>();
            AdoptionFormAttribute = new HashSet<AdoptionFormAttribute>();
        }

        public int ContentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int JobId { get; set; }
        public int LocationId { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public short FamilyMembers { get; set; }
        public Guid AutoreplyToken { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<AdoptionFormAnswer> AdoptionFormAnswer { get; set; }
        public virtual ICollection<AdoptionFormAttribute> AdoptionFormAttribute { get; set; }
        public virtual Content Content { get; set; }
        public virtual CustomTableRow Job { get; set; }
        public virtual Location Location { get; set; }
    }
}
