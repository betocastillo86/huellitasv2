using System;
using System.Collections.Generic;

namespace Huellitas.Data.Entities
{
    public partial class EmailNotification : BaseEntity
    {
        public string To { get; set; }
        public string ToName { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public short SentTries { get; set; }
        public DateTime? SentDate { get; set; }
        public DateTime? ScheduledDate { get; set; }
    }
}
