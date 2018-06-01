//-----------------------------------------------------------------------
// <copyright file="SystemNotificationMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// System Notification Map
    /// </summary>
    public static class SystemNotificationMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<SystemNotification> entity)
        {
            entity.ToTable("SystemNotifications");

            entity.HasKey(c => c.Id)
                .HasName("PK_SystemNotification");

            entity.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(t => t.TargetURL)
                .IsRequired()
                .HasMaxLength(500);

            entity.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_SystemNotification_User");

            entity.HasOne(c => c.TriggerUser)
                .WithMany()
                .HasForeignKey(c => c.TriggerUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_SystemNotification_TriggerUser");
        }
    }
}