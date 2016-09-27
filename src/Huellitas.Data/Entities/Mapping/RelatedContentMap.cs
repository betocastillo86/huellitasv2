//-----------------------------------------------------------------------
// <copyright file="RelatedContentMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Related Content Mapping
    /// </summary>
    public static class RelatedContentMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<RelatedContent> entity)
        {
            entity.ToTable("RelatedContent");

            entity.HasOne(d => d.Content)
                    .WithMany(p => p.RelatedContentContent)
                    .HasForeignKey(d => d.ContentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RelatedContent_Content");

            entity.HasOne(d => d.RelatedContentNavigation)
                .WithMany(p => p.RelatedContentRelatedContentNavigation)
                .HasForeignKey(d => d.RelatedContentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_RelatedContent_Content1");
        }
    }
}