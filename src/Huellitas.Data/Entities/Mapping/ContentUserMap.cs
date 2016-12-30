//-----------------------------------------------------------------------
// <copyright file="ContentUserMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Content User Map
    /// </summary>
    public static class ContentUserMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<ContentUser> entity)
        {
            entity.ToTable("ContentUser");

            entity.Property(e => e.ContentId)
                    .HasColumnName("ContentId")
                    .IsRequired();

            entity.Property(e => e.UserId)
                    .HasColumnName("UserId")
                    .IsRequired();

            entity.Property(e => e.RelationTypeId)
                    .HasColumnName("RelationTypeId")
                    .IsRequired();

            entity.Ignore(c => c.RelationType);

            entity.HasOne(c => c.Content)
                .WithMany()
                .HasForeignKey(c => c.ContentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ContentUser_Content");

            entity.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ContentUser_User");
        }
    }
}