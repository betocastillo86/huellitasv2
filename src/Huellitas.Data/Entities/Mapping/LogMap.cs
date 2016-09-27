//-----------------------------------------------------------------------
// <copyright file="LogMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Log Mapping
    /// </summary>
    public static class LogMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<Log> entity)
        {
            entity.ToTable("Log");

            entity.Property(c => c.ShortMessage)
                .HasColumnName("ShortMessage")
                .HasMaxLength(4000)
                .IsRequired();

            entity.Property(c => c.IpAddress)
                .HasMaxLength(100);

            entity.Property(c => c.PageUrl)
                .HasMaxLength(500);

            entity.Property(c => c.FullMessage)
                .HasColumnName("FullMessage")
                .HasMaxLength(4000);

            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade)
                .HasConstraintName("FK_Log_User");
        }
    }
}