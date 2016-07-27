using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class RolePemissionMap
    {
        public static void Map(this EntityTypeBuilder<RolePemission> entity)
        {
            entity.HasOne(d => d.Permission)
                   .WithMany(p => p.RolePemission)
                   .HasForeignKey(d => d.PermissionId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_RolePemission_Permission");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.RolePemission)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_RolePemission_Role");
        }
    }
}
