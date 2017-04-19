//-----------------------------------------------------------------------
// <copyright file="BannerMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Banner mapping
    /// </summary>
    public static class BannerMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<Banner> entity)
        {
            entity.ToTable("Banners");

            entity.HasKey(c => c.Id)
                .HasName("PK_Banner");

            entity.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(150)");

            entity.Property(c => c.Body)
                .IsRequired();

            entity.Property(c => c.EmbedUrl)
                .HasColumnType("varchar(500)");

            entity.HasOne(c => c.File)
                .WithMany()
                .HasForeignKey(c => c.FileId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Banner_File");
        }
    }
}