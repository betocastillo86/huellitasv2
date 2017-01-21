//-----------------------------------------------------------------------
// <copyright file="AdoptionFormAnswerMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Adoption Form Answer Mapping
    /// </summary>
    public static class AdoptionFormAnswerMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<AdoptionFormAnswer> entity)
        {
            entity.ToTable("AdoptionFormAnswers");

            entity.HasKey(c => c.Id)
                .HasName("PK_AdoptionFormAnswer");

            entity.Property(e => e.AdditionalInfo).HasMaxLength(2000);

            entity.Property(e => e.CreationDate).HasColumnType("datetime");

            entity.Property(e => e.Notes).HasMaxLength(1500);

            entity.HasOne(d => d.AdoptionForm)
                .WithMany(p => p.AdoptionFormAnswer)
                .HasForeignKey(d => d.AdoptionFormId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_AdoptionFormAnswer_AdoptionForm");

            entity.HasOne(d => d.User)
                .WithMany(p => p.AdoptionFormAnswer)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_AdoptionFormAnswer_User");
        }
    }
}