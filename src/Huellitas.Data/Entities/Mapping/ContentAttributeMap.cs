//-----------------------------------------------------------------------
// <copyright file="ContentAttributeMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Content Attribute Mapping
    /// </summary>
    public static class ContentAttributeMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<ContentAttribute> entity)
        {
            entity.ToTable("ContentAttributes");

            entity.HasKey(c => c.Id)
                .HasName("PK_ContentAttribute");

            entity.Property(e => e.Attribute)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

            entity.Property(e => e.Value)
                .IsRequired()
                .HasColumnType("varchar(500)");

            entity.HasOne(d => d.Content)
                .WithMany(p => p.ContentAttributes)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ContentAttribute_Content");
        }
    }
}