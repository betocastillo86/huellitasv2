//-----------------------------------------------------------------------
// <copyright file="ContentFileMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Content File Mapping
    /// </summary>
    public static class ContentFileMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<ContentFile> entity)
        {
            entity.ToTable("ContentFile");

            entity.Property(e => e.Id);

            entity.Property(e => e.DisplayOrder).HasDefaultValueSql("0");

            entity.HasOne(d => d.Content)
                .WithMany(p => p.ContentFiles)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ContentFile_Content");

            entity.HasOne(d => d.File)
                .WithMany(p => p.ContentFile)
                .HasForeignKey(d => d.FileId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ContentFile_File");
        }
    }
}