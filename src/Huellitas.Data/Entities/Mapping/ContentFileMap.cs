using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class ContentFileMap
    {
        public static void Map(this EntityTypeBuilder<ContentFile> entity)
        {
            entity.ToTable("ContentFile");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.DisplayOrder).HasDefaultValueSql("0");

            entity.HasOne(d => d.Content)
                .WithMany(p => p.ContentFile)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ContentFile_Content");

            entity.HasOne(d => d.File)
                .WithMany(p => p.ContentFile)
                .HasForeignKey(d => d.FileId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ContentFile_File");
        }
    }
}
