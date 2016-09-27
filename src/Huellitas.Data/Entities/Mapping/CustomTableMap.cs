//-----------------------------------------------------------------------
// <copyright file="CustomTableMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Custom Table Mapping
    /// </summary>
    public static class CustomTableMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<CustomTable> entity)
        {
            entity.ToTable("CustomTable");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Description).HasColumnType("varchar(250)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");
        }
    }
}