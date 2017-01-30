//-----------------------------------------------------------------------
// <copyright file="CustomTableRowMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Custom table row mapping
    /// </summary>
    public static class CustomTableRowMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<CustomTableRow> entity)
        {
            entity.ToTable("CustomTableRows");

            entity.HasKey(c => c.Id)
                .HasName("PK_CustomTableRow");

            entity.Property(e => e.AdditionalInfo).HasMaxLength(100);

            entity.Property(e => e.Value)
                .IsRequired()
                .HasColumnType("varchar(200)");

            entity.HasOne(d => d.CustomTable)
                .WithMany(p => p.CustomTableRow)
                .HasForeignKey(d => d.CustomTableId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_CustomTableRow_CustomTable");
        }
    }
}