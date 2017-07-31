//-----------------------------------------------------------------------
// <copyright file="AdoptionFormAttributeMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Adoption Form Attribute Mapping
    /// </summary>
    public static class AdoptionFormAttributeMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<AdoptionFormAttribute> entity)
        {
            entity.ToTable("AdoptionFormAttributes");

            entity.HasKey(c => c.Id)
                .HasName("PK_AdoptionFormAttribute");

            entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(2000);

            entity.HasOne(d => d.AdoptionForm)
                .WithMany(p => p.Attributes)
                .HasForeignKey(d => d.AdoptionFormId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_AdoptionFormAttribute_AdoptionForm");

            entity.HasOne(d => d.Attribute)
                .WithMany(p => p.AdoptionFormAttribute)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_AdoptionFormAttribute_CustomTableRow");
        }
    }
}