using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class AdoptionFormAnswerMap
    {
        public static void Map(this EntityTypeBuilder<AdoptionFormAnswer> entity)
        {
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
