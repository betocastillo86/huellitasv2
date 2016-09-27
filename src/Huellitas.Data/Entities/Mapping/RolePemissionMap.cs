//-----------------------------------------------------------------------
// <copyright file="RolePemissionMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Role Permission Mapping
    /// </summary>
    public static class RolePemissionMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<RolePemission> entity)
        {
            entity.ToTable("RolePemission");

            entity.HasOne(d => d.Permission)
                   .WithMany(p => p.RolePemission)
                   .HasForeignKey(d => d.PermissionId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_RolePemission_Permission");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.RolePemissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_RolePemission_Role");
        }
    }
}