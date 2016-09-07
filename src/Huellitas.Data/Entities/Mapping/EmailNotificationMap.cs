using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class EmailNotificationMap
    {
        public static void Map(this EntityTypeBuilder<EmailNotification> entity)
        {
            entity.ToTable("EmailNotification");

            entity.Property(e => e.Id);

            entity.Property(e => e.Body).IsRequired();

            entity.Property(e => e.Cc)
                .HasColumnName("CC")
                .HasMaxLength(500);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.ScheduledDate).HasColumnType("datetime");

            entity.Property(e => e.SentDate).HasColumnType("datetime");

            entity.Property(e => e.Subject)
                .IsRequired()
                .HasColumnType("varchar(300)");

            entity.Property(e => e.To)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.ToName).HasMaxLength(200);
        }
    }
}
