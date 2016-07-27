using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class ContentAttributeMap
    {
        public static void Map(this EntityTypeBuilder<ContentAttribute> entity)
        {
            entity.Property(e => e.Attribute)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

            entity.Property(e => e.Value)
                .IsRequired()
                .HasColumnType("varchar(500)");

            entity.HasOne(d => d.Content)
                .WithMany(p => p.ContentAttribute)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ContentAttribute_Content");
        }
    }
}
