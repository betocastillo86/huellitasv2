//-----------------------------------------------------------------------
// <copyright file="FileMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// File Mapping
    /// </summary>
    public static class FileMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<File> entity)
        {
            entity.ToTable("Files");

            entity.HasKey(c => c.Id)
                .HasName("PK_File");

            entity.Property(e => e.FileName)
                                .IsRequired()
                                .HasColumnType("varchar(150)");

            entity.Property(e => e.MimeType)
                .IsRequired()
                .HasColumnType("varchar(10)");

            entity.Property(e => e.Name).HasColumnType("varchar(150)");
        }
    }
}