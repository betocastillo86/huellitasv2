//-----------------------------------------------------------------------
// <copyright file="EmailNotificationMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Email notification mapping
    /// </summary>
    public static class EmailNotificationMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<EmailNotification> entity)
        {
            entity.ToTable("EmailNotifications");

            entity.HasKey(c => c.Id)
                .HasName("PK_EmailNotification");

            entity.Property(e => e.Id);

            entity.Property(e => e.Body).IsRequired();

            entity.Property(e => e.Cc)
                .HasColumnName("CC")
                .HasMaxLength(500);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.ScheduledDate).HasColumnType("datetime");

            entity.Property(e => e.SentDate).HasColumnType("datetime");

            entity.Property(e => e.Subject)
                .IsRequired()
                .HasColumnType("varchar(300)");

            entity.Property(e => e.To)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.ToName).HasMaxLength(200);
        }
    }
}