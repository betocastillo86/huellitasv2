using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class AdoptionFormAnswer
    {
        public int Id { get; set; }
        public int AdoptionFormId { get; set; }
        public short Status { get; set; }
        public string AdditionalInfo { get; set; }
        public string Notes { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual AdoptionForm AdoptionForm { get; set; }
        public virtual User User { get; set; }
    }
}
