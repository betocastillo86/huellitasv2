﻿//-----------------------------------------------------------------------
// <copyright file="AdoptionFormMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Adoption Form Mapping
    /// </summary>
    public static class AdoptionFormMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<AdoptionForm> entity)
        {
            entity.ToTable("AdoptionForms");

            entity.HasKey(c => c.Id)
                .HasName("PK_AdoptionForm");

            entity.Property(e => e.Id);

            entity.Property(e => e.Address)
                .IsRequired()
                .HasColumnType("varchar(100)");

            entity.Property(e => e.BirthDate).HasColumnType("datetime");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasColumnType("varchar(15)");

            entity.Property(e => e.Town)
                .IsRequired()
                .HasColumnType("varchar(50)");

            entity.Property(e => e.FamilyMembersAge)
                .HasColumnType("varchar(50)");

            entity.HasOne(d => d.Content)
                .WithMany(p => p.AdoptionForm)
                .HasForeignKey(d => d.ContentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_AdoptionForm_Content1");

            entity.HasOne(d => d.Job)
                .WithMany(p => p.AdoptionForm)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_AdoptionForm_CustomTableRow");

            entity.HasOne(d => d.Location)
                .WithMany()
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_AdoptionForm_Location");
            
            entity.HasOne(d => d.LastResponseUser)
                .WithMany()
                .HasForeignKey(d => d.LastResponseUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_AdoptionForm_User_LastResponse");
        }
    }
}