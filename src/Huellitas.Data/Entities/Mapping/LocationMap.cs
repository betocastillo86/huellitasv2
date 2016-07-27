using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class LocationMap
    {
        public static void Map(this EntityTypeBuilder<Location> entity)
        {
            entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
        }
    }
}
