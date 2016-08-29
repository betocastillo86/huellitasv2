using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class CustomTableMap
    {
        public static void Map(this EntityTypeBuilder<CustomTable> entity)
        {
            entity.ToTable("CustomTable");

            entity.Property(e => e.Description).HasColumnType("varchar(250)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");
        }
    }
}
