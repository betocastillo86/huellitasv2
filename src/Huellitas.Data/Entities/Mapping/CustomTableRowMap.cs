using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class CustomTableRowMap
    {
        public static void Map(this EntityTypeBuilder<CustomTableRow> entity)
        {
            entity.Property(e => e.AdditionalInfo).HasMaxLength(100);

            entity.Property(e => e.Value)
                .IsRequired()
                .HasColumnType("varchar(200)");

            entity.HasOne(d => d.CustomTable)
                .WithMany(p => p.CustomTableRow)
                .HasForeignKey(d => d.CustomTableId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_CustomTableRow_CustomTable");
        }
    }
}
