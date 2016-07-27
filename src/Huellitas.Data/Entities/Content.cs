using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class Content
    {
        public Content()
        {
            AdoptionForm = new HashSet<AdoptionForm>();
            ContentAttribute = new HashSet<ContentAttribute>();
            ContentCategory = new HashSet<ContentCategory>();
            ContentFile = new HashSet<ContentFile>();
            RelatedContentContent = new HashSet<RelatedContent>();
            RelatedContentRelatedContentNavigation = new HashSet<RelatedContent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public short TypeId { get; set; }
        public short Status { get; set; }
        public int? FileId { get; set; }
        public int? LocationId { get; set; }
        public string Email { get; set; }
        public int DisplayOrder { get; set; }
        public int Views { get; set; }
        public int CommentsCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int UserId { get; set; }
        public bool Deleted { get; set; }
        public bool Featured { get; set; }

        public virtual ICollection<AdoptionForm> AdoptionForm { get; set; }
        public virtual ICollection<ContentAttribute> ContentAttribute { get; set; }
        public virtual ICollection<ContentCategory> ContentCategory { get; set; }
        public virtual ICollection<ContentFile> ContentFile { get; set; }
        public virtual ICollection<RelatedContent> RelatedContentContent { get; set; }
        public virtual ICollection<RelatedContent> RelatedContentRelatedContentNavigation { get; set; }
        public virtual File File { get; set; }
        public virtual Location Location { get; set; }
        public virtual User User { get; set; }
    }
}
