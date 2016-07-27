using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class ContentMap
    {
        public static void Map(this EntityTypeBuilder<Content> entity)
        {
            entity.Property(e => e.Body).IsRequired();

            entity.Property(e => e.CommentsCount).HasDefaultValueSql("0");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.DisplayOrder).HasDefaultValueSql("0");

            entity.Property(e => e.Email).HasColumnType("varchar(150)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.Property(e => e.Views).HasDefaultValueSql("0");

            entity.HasOne(d => d.File)
                .WithMany(p => p.Content)
                .HasForeignKey(d => d.FileId)
                .HasConstraintName("FK_Content_File");

            entity.HasOne(d => d.Location)
                .WithMany(p => p.Content)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_Content_Location");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Content)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Content_User");
        }
    }
}
