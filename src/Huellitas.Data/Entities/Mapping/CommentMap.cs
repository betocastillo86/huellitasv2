using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities.Mapping
{
    public static class CommentMap
    {
        public static void Map(this EntityTypeBuilder<Comment> entity)
        {
            entity.ToTable("Comments");

            entity.HasKey(c => c.Id)
                .HasName("PK_Comment");

            entity.Property(c => c.IpAddress)
                .HasColumnType("varchar(20)");

            entity.Property(c => c.Value)
                .HasColumnType("nvarchar(1500)")
                .IsRequired();

            entity.HasOne(c => c.Content)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.ContentId)
                .HasConstraintName("FK_Comment_Content")
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.UserId)
                .IsRequired()
                .HasConstraintName("FK_Comment_User")
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.ParentComment)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentCommentId)
                .IsRequired(false)
                .HasConstraintName("FK_Comment_ParentComment")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
