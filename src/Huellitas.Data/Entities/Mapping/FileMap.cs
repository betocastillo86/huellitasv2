using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class FileMap
    {
        public static void Map(this EntityTypeBuilder<File> entity)
        {
            entity.ToTable("File");

            entity.Property(e => e.FileName)
                                .IsRequired()
                                .HasColumnType("varchar(150)");

            entity.Property(e => e.MimeType)
                .IsRequired()
                .HasColumnType("varchar(10)");

            entity.Property(e => e.Name).HasColumnType("varchar(50)");
        }
    }
}
