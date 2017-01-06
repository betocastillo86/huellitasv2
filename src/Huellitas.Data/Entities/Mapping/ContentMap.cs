//-----------------------------------------------------------------------
// <copyright file="ContentMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Content Mapping
    /// </summary>
    public static class ContentMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<Content> entity)
        {
            entity.ToTable("Content");

            entity.Property(e => e.Body).IsRequired();

            entity.Property(e => e.CommentsCount).HasDefaultValueSql("0");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.DisplayOrder).HasDefaultValueSql("0");

            entity.Property(e => e.Email).HasColumnType("varchar(150)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.Property(e => e.Views).HasDefaultValueSql("0");

            entity.HasOne(d => d.File)
                .WithMany(p => p.Content)
                .HasForeignKey(d => d.FileId)
                .HasConstraintName("FK_Content_File");

            entity.HasOne(d => d.Location)
                .WithMany()
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_Content_Location");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Contents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Content_User");

            entity.HasIndex(c => c.FriendlyName)
                .IsUnique();
        }
    }
}