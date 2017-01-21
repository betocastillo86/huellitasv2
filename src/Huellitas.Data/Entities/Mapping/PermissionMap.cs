//-----------------------------------------------------------------------
// <copyright file="PermissionMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Permission Mapping
    /// </summary>
    public static class PermissionMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<Permission> entity)
        {
            entity.ToTable("Permissions");

            entity.HasKey(c => c.Id)
                .HasName("PK_Permission");

            entity.Property(e => e.Description).HasColumnType("varchar(50)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");
        }
    }
}