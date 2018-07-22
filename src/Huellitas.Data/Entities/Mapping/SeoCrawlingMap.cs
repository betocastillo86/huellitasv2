//-----------------------------------------------------------------------
// <copyright file="SeoCrawlingMap.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// SEO Crawling map
    /// </summary>
    public static class SeoCrawlingMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<SeoCrawling> entity)
        {
            entity.ToTable("SeoCrawlings");

            entity.HasKey(c => c.Url)
                .HasName("PK_SeoCrawling");

            entity.Property(c => c.Url)
                .HasColumnType("varchar(500)");

            entity.Property(c => c.Html)
                .IsRequired()
                .HasColumnType("nvarchar(MAX)");
        }
    }
}