using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class RelatedContentMap
    {
        public static void Map(this EntityTypeBuilder<RelatedContent> entity)
        {
            entity.ToTable("RelatedContent");

            entity.HasOne(d => d.Content)
                    .WithMany(p => p.RelatedContentContent)
                    .HasForeignKey(d => d.ContentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RelatedContent_Content");

            entity.HasOne(d => d.RelatedContentNavigation)
                .WithMany(p => p.RelatedContentRelatedContentNavigation)
                .HasForeignKey(d => d.RelatedContentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_RelatedContent_Content1");
        }
    }
}
