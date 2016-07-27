using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class UserMap
    {
        public static void Map(this EntityTypeBuilder<User> entity)
        {
            entity.HasIndex(e => e.Email)
                    .HasName("IX_User")
                    .IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(150)");

            entity.Property(e => e.Password)
                .IsRequired()
                .HasColumnType("varchar(50)");

            entity.Property(e => e.PhoneNumber).HasColumnType("varchar(15)");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.User)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_User_Role");
        }
    }
}
