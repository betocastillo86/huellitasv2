﻿using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class Location
    {
        public Location()
        {
            AdoptionForm = new HashSet<AdoptionForm>();
            Content = new HashSet<Content>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentLocationId { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<AdoptionForm> AdoptionForm { get; set; }
        public virtual ICollection<Content> Content { get; set; }
    }
}
