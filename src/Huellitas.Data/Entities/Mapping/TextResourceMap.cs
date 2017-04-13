using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Huellitas.Data.Entities.Mapping
{
    public static class TextResourceMap
    {
        public static void Map(this EntityTypeBuilder<TextResource> entity)
        {
            entity.ToTable("TextResources");

            entity.HasKey(c => c.Id)
                .HasName("PK_TextResource");

            entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

            entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("nvarchar(4000)");
        }
    }
}
