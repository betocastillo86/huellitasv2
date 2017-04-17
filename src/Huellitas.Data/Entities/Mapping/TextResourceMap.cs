//-----------------------------------------------------------------------
// <copyright file="TextResourceMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Text Resources Map
    /// </summary>
    public static class TextResourceMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<TextResource> entity)
        {
            entity.ToTable("TextResources");

            entity.HasKey(c => c.Id)
                .HasName("PK_TextResource");

            entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

            entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("nvarchar(4000)");
        }
    }
}