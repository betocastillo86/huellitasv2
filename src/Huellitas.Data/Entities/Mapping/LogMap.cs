using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class LogMap
    {
        public static void Map(this EntityTypeBuilder<Log> entity)
        {
            entity.ToTable("Log");

            entity.Property(c => c.ShortMessage)
                .HasColumnName("ShortMessage")
                .HasMaxLength(4000)
                .IsRequired();

            entity.Property(c => c.IpAddress)
                .HasMaxLength(100);

            entity.Property(c => c.PageUrl)
                .HasMaxLength(500);

            entity.Property(c => c.FullMessage)
                .HasColumnName("FullMessage")
                .HasMaxLength(4000);

            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade)
                .HasConstraintName("FK_Log_User");
        }
    }
}
