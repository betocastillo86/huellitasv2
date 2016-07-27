using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class AdoptionFormAttributeMap
    {
        public static void Map(this EntityTypeBuilder<AdoptionFormAttribute> entity)
        {
            entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(1000);

            entity.HasOne(d => d.AdoptionForm)
                .WithMany(p => p.AdoptionFormAttribute)
                .HasForeignKey(d => d.AdoptionFormId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_AdoptionFormAttribute_AdoptionForm");

            entity.HasOne(d => d.Attribute)
                .WithMany(p => p.AdoptionFormAttribute)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_AdoptionFormAttribute_CustomTableRow");
        }
    }
}
