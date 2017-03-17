//-----------------------------------------------------------------------
// <copyright file="NotificationMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Notification Map
    /// </summary>
    public static class NotificationMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<Notification> entity)
        {
            entity.ToTable("Notifications");

            entity.HasKey(c => c.Id)
                .HasName("PK_Notification");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(t => t.SystemText)
                .HasMaxLength(2000);

            entity.Property(t => t.EmailSubject)
                .HasMaxLength(500);

            entity.Property(t => t.Tags)
                .HasMaxLength(3000);
        }
    }
}