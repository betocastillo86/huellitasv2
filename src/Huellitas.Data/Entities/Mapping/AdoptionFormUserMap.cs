//-----------------------------------------------------------------------
// <copyright file="AdoptionFormUserMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Adoption form user map
    /// </summary>
    public static class AdoptionFormUserMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<AdoptionFormUser> entity)
        {
            entity.ToTable("AdoptionFormUsers");

            entity.HasKey(c => c.Id)
                .HasName("PK_AdoptionFormUser");

            entity.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict)
                .HasConstraintName("FK_AdoptionFormUser_User");

            entity.HasOne(c => c.AdoptionForm)
                .WithMany(c => c.Users)
                .HasForeignKey(c => c.AdoptionFormId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict)
                .HasConstraintName("FK_AdoptionFormUser_AdoptionForm");
        }
    }
}