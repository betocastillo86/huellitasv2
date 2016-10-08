//-----------------------------------------------------------------------
// <copyright file="LocationMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Location Mapping
    /// </summary>
    public static class LocationMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<Location> entity)
        {
            entity.ToTable("Location");

            entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

            entity.Property(e => e.ParentLocationId)
                .IsRequired(false);
        }
    }
}