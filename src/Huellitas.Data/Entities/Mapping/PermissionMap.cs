using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class PermissionMap
    {
        public static void Map(this EntityTypeBuilder<Permission> entity)
        {
            entity.ToTable("Permission");

            entity.Property(e => e.Description).HasColumnType("varchar(50)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");
        }
    }
}
