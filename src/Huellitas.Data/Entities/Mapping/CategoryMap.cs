using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class CategoryMap
    {
        public static void Map(this EntityTypeBuilder<Category> entity)
        {
            entity.ToTable("Category");

            entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");
        }
    }
}
