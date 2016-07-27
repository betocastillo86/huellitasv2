using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class SystemSettingMap
    {
        public static void Map(this EntityTypeBuilder<SystemSetting> entity)
        {
            entity.HasIndex(e => e.Name)
                   .HasName("IX_SystemSetting")
                   .IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");

            entity.Property(e => e.Value).IsRequired();
        }
    }
}
