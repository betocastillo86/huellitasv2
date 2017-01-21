//-----------------------------------------------------------------------
// <copyright file="UserMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// User Mapping
    /// </summary>
    public static class UserMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");

            entity.HasKey(c => c.Id)
                .HasName("PK_User");

            entity.HasIndex(e => e.Email)
                    .HasName("IX_User")
                    .IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(150)");

            entity.Property(e => e.Password)
                .IsRequired()
                .HasColumnType("varchar(50)");

            entity.Ignore(e => e.RoleEnum);

            entity.Property(e => e.PhoneNumber).HasColumnType("varchar(15)");

            entity.Property(e => e.PhoneNumber2).HasColumnType("varchar(15)");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_User_Role");
        }
    }
}