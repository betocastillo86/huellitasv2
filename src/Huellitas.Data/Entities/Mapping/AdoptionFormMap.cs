using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class AdoptionFormMap
    {
        public static void Map(this EntityTypeBuilder<AdoptionForm> entity)
        {

            entity.ToTable("AdoptionForm");

            entity.Property(e => e.Id).ValueGeneratedNever();

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
                .WithMany(p => p.AdoptionForm)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_AdoptionForm_Location");
        }
    }
}
