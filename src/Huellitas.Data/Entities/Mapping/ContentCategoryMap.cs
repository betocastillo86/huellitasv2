//-----------------------------------------------------------------------
// <copyright file="ContentCategoryMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Content Category Mapping
    /// </summary>
    public static class ContentCategoryMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<ContentCategory> entity)
        {
            entity.ToTable("ContentCategories");

            entity.HasKey(c => c.Id)
                .HasName("PK_ContentCategory");

            entity.HasOne(d => d.Category)
                    .WithMany(p => p.ContentCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ContentCategory_Category");

            entity.HasOne(d => d.Content)
                .WithMany(p => p.ContentCategories)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ContentCategory_Content");
        }
    }
}