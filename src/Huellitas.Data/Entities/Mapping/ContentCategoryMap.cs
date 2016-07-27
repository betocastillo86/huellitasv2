using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class ContentCategoryMap
    {
        public static void Map(this EntityTypeBuilder<ContentCategory> entity)
        {
            entity.HasOne(d => d.Category)
                    .WithMany(p => p.ContentCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ContentCategory_Category");

            entity.HasOne(d => d.Content)
                .WithMany(p => p.ContentCategory)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ContentCategory_Content");
        }
    }
}
