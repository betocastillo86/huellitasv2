﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Huellitas.Data.Core;

namespace Huellitas.Data.Migrations
{
    [DbContext(typeof(HuellitasContext))]
    [Migration("20160726022709_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Huellitas.Data.Entities.AdoptionForm", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd();

                b.Property<string>("Address")
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                b.Property<Guid>("AutoreplyToken");

                b.Property<DateTime>("BirthDate")
                    .HasColumnType("datetime");

                b.Property<int>("ContentId");

                b.Property<DateTime>("CreationDate")
                    .HasColumnType("datetime");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                b.Property<short>("FamilyMembers");

                b.Property<int>("JobId");

                b.Property<int>("LocationId");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                b.Property<string>("PhoneNumber")
                    .IsRequired()
                    .HasColumnType("varchar(15)");

                b.Property<string>("Town")
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                b.HasKey("Id");

                b.HasIndex("ContentId");

                b.HasIndex("JobId");

                b.HasIndex("LocationId");

                b.ToTable("AdoptionForm");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.AdoptionFormAnswer", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("AdditionalInfo")
                    .HasAnnotation("MaxLength", 2000);

                b.Property<int>("AdoptionFormId");

                b.Property<DateTime>("CreationDate")
                    .HasColumnType("datetime");

                b.Property<string>("Notes")
                    .HasAnnotation("MaxLength", 1500);

                b.Property<short>("Status");

                b.Property<int>("UserId");

                b.HasKey("Id");

                b.HasIndex("AdoptionFormId");

                b.HasIndex("UserId");

                b.ToTable("AdoptionFormAnswer");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.AdoptionFormAttribute", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("AdoptionFormId");

                b.Property<int>("AttributeId");

                b.Property<string>("Value")
                    .IsRequired()
                    .HasAnnotation("MaxLength", 1000);

                b.HasKey("Id");

                b.HasIndex("AdoptionFormId");

                b.HasIndex("AttributeId");

                b.ToTable("AdoptionFormAttribute");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.Category", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                b.HasKey("Id");

                b.ToTable("Category");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.Content", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Body")
                    .IsRequired();

                b.Property<int>("CommentsCount")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("0");

                b.Property<DateTime>("CreatedDate")
                    .HasColumnType("datetime");

                b.Property<bool>("Deleted");

                b.Property<int>("DisplayOrder")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("0");

                b.Property<string>("Email")
                    .HasColumnType("varchar(150)");

                b.Property<bool>("Featured");

                b.Property<int?>("FileId");

                b.Property<int?>("LocationId");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                b.Property<short>("Status");

                b.Property<short>("TypeId");

                b.Property<DateTime?>("UpdatedDate")
                    .HasColumnType("datetime");

                b.Property<int>("UserId");

                b.Property<int>("Views")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("0");

                b.HasKey("Id");

                b.HasIndex("FileId");

                b.HasIndex("LocationId");

                b.HasIndex("UserId");

                b.ToTable("Content");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.ContentAttribute", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Attribute")
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                b.Property<int>("ContentId");

                b.Property<string>("Value")
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                b.HasKey("Id");

                b.HasIndex("ContentId");

                b.ToTable("ContentAttribute");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.ContentCategory", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("CategoryId");

                b.Property<int>("ContentId");

                b.HasKey("Id");

                b.HasIndex("CategoryId");

                b.HasIndex("ContentId");

                b.ToTable("ContentCategory");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.ContentFile", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd();

                b.Property<int>("ContentId");

                b.Property<int>("DisplayOrder")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("0");

                b.Property<int>("FileId");

                b.HasKey("Id");

                b.HasIndex("ContentId");

                b.HasIndex("FileId");

                b.ToTable("ContentFile");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.CustomTable", b =>
            {
                b.Property<int>("Id");

                b.Property<string>("Description")
                    .HasColumnType("varchar(250)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                b.HasKey("Id");

                b.ToTable("CustomTable");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.CustomTableRow", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("AdditionalInfo")
                    .HasAnnotation("MaxLength", 100);

                b.Property<int>("CustomTableId");

                b.Property<bool>("Deleted");

                b.Property<string>("Value")
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                b.HasKey("Id");

                b.HasIndex("CustomTableId");

                b.ToTable("CustomTableRow");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.EmailNotification", b =>
            {
                b.Property<int>("Id");

                b.Property<string>("Body")
                    .IsRequired();

                b.Property<string>("Cc")
                    .HasColumnName("CC")
                    .HasAnnotation("MaxLength", 500);

                b.Property<DateTime>("CreatedDate")
                    .HasColumnType("datetime");

                b.Property<DateTime?>("ScheduledDate")
                    .HasColumnType("datetime");

                b.Property<DateTime?>("SentDate")
                    .HasColumnType("datetime");

                b.Property<short>("SentTries");

                b.Property<string>("Subject")
                    .IsRequired()
                    .HasColumnType("varchar(300)");

                b.Property<string>("To")
                    .IsRequired()
                    .HasAnnotation("MaxLength", 200);

                b.Property<string>("ToName")
                    .HasAnnotation("MaxLength", 200);

                b.HasKey("Id");

                b.ToTable("EmailNotification");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.File", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("FileName")
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                b.Property<string>("MimeType")
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                b.Property<string>("Name")
                    .HasColumnType("varchar(50)");

                b.HasKey("Id");

                b.ToTable("File");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.Location", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<bool>("Deleted");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                b.Property<int>("ParentLocationId");

                b.HasKey("Id");

                b.ToTable("Location");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.Permission", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Description")
                    .HasColumnType("varchar(50)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                b.HasKey("Id");

                b.ToTable("Permission");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.RelatedContent", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("ContentId");

                b.Property<int>("RelatedContentId");

                b.Property<short>("RelationType");

                b.HasKey("Id");

                b.HasIndex("ContentId");

                b.HasIndex("RelatedContentId");

                b.ToTable("RelatedContent");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.Role", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Description")
                    .HasColumnType("varchar(200)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                b.HasKey("Id");

                b.ToTable("Role");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.RolePemission", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("PermissionId");

                b.Property<int>("RoleId");

                b.HasKey("Id");

                b.HasIndex("PermissionId");

                b.HasIndex("RoleId");

                b.ToTable("RolePemission");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.SystemSetting", b =>
            {
                b.Property<int>("Id");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                b.Property<string>("Value")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("Name")
                    .IsUnique()
                    .HasName("IX_SystemSetting");

                b.ToTable("SystemSetting");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.User", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("CreatedDate")
                    .HasColumnType("datetime");

                b.Property<bool>("Deleted");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                b.Property<string>("Password")
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                b.Property<string>("PhoneNumber")
                    .HasColumnType("varchar(15)");

                b.Property<int>("RoleId");

                b.HasKey("Id");

                b.HasIndex("Email")
                    .IsUnique()
                    .HasName("IX_User");

                b.HasIndex("RoleId");

                b.ToTable("User");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.AdoptionForm", b =>
            {
                b.HasOne("Huellitas.Data.Entities.Content", "Content")
                    .WithMany("AdoptionForm")
                    .HasForeignKey("ContentId")
                    .HasConstraintName("FK_AdoptionForm_Content1");

                b.HasOne("Huellitas.Data.Entities.CustomTableRow", "Job")
                    .WithMany("AdoptionForm")
                    .HasForeignKey("JobId")
                    .HasConstraintName("FK_AdoptionForm_CustomTableRow");

                b.HasOne("Huellitas.Data.Entities.Location", "Location")
                    .WithMany("AdoptionForm")
                    .HasForeignKey("LocationId")
                    .HasConstraintName("FK_AdoptionForm_Location");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.AdoptionFormAnswer", b =>
            {
                b.HasOne("Huellitas.Data.Entities.AdoptionForm", "AdoptionForm")
                    .WithMany("AdoptionFormAnswer")
                    .HasForeignKey("AdoptionFormId")
                    .HasConstraintName("FK_AdoptionFormAnswer_AdoptionForm");

                b.HasOne("Huellitas.Data.Entities.User", "User")
                    .WithMany("AdoptionFormAnswer")
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_AdoptionFormAnswer_User");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.AdoptionFormAttribute", b =>
            {
                b.HasOne("Huellitas.Data.Entities.AdoptionForm", "AdoptionForm")
                    .WithMany("AdoptionFormAttribute")
                    .HasForeignKey("AdoptionFormId")
                    .HasConstraintName("FK_AdoptionFormAttribute_AdoptionForm");

                b.HasOne("Huellitas.Data.Entities.CustomTableRow", "Attribute")
                    .WithMany("AdoptionFormAttribute")
                    .HasForeignKey("AttributeId")
                    .HasConstraintName("FK_AdoptionFormAttribute_CustomTableRow");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.Content", b =>
            {
                b.HasOne("Huellitas.Data.Entities.File", "File")
                    .WithMany("Content")
                    .HasForeignKey("FileId")
                    .HasConstraintName("FK_Content_File");

                b.HasOne("Huellitas.Data.Entities.Location", "Location")
                    .WithMany("Content")
                    .HasForeignKey("LocationId")
                    .HasConstraintName("FK_Content_Location");

                b.HasOne("Huellitas.Data.Entities.User", "User")
                    .WithMany("Content")
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_Content_User");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.ContentAttribute", b =>
            {
                b.HasOne("Huellitas.Data.Entities.Content", "Content")
                    .WithMany("ContentAttribute")
                    .HasForeignKey("ContentId")
                    .HasConstraintName("FK_ContentAttribute_Content");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.ContentCategory", b =>
            {
                b.HasOne("Huellitas.Data.Entities.Category", "Category")
                    .WithMany("ContentCategory")
                    .HasForeignKey("CategoryId")
                    .HasConstraintName("FK_ContentCategory_Category");

                b.HasOne("Huellitas.Data.Entities.Content", "Content")
                    .WithMany("ContentCategory")
                    .HasForeignKey("ContentId")
                    .HasConstraintName("FK_ContentCategory_Content");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.ContentFile", b =>
            {
                b.HasOne("Huellitas.Data.Entities.Content", "Content")
                    .WithMany("ContentFile")
                    .HasForeignKey("ContentId")
                    .HasConstraintName("FK_ContentFile_Content");

                b.HasOne("Huellitas.Data.Entities.File", "File")
                    .WithMany("ContentFile")
                    .HasForeignKey("FileId")
                    .HasConstraintName("FK_ContentFile_File");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.CustomTableRow", b =>
            {
                b.HasOne("Huellitas.Data.Entities.CustomTable", "CustomTable")
                    .WithMany("CustomTableRow")
                    .HasForeignKey("CustomTableId")
                    .HasConstraintName("FK_CustomTableRow_CustomTable");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.RelatedContent", b =>
            {
                b.HasOne("Huellitas.Data.Entities.Content", "Content")
                    .WithMany("RelatedContentContent")
                    .HasForeignKey("ContentId")
                    .HasConstraintName("FK_RelatedContent_Content");

                b.HasOne("Huellitas.Data.Entities.Content", "RelatedContentNavigation")
                    .WithMany("RelatedContentRelatedContentNavigation")
                    .HasForeignKey("RelatedContentId")
                    .HasConstraintName("FK_RelatedContent_Content1");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.RolePemission", b =>
            {
                b.HasOne("Huellitas.Data.Entities.Permission", "Permission")
                    .WithMany("RolePemission")
                    .HasForeignKey("PermissionId")
                    .HasConstraintName("FK_RolePemission_Permission");

                b.HasOne("Huellitas.Data.Entities.Role", "Role")
                    .WithMany("RolePemission")
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_RolePemission_Role");
            });

            modelBuilder.Entity("Huellitas.Data.Entities.User", b =>
            {
                b.HasOne("Huellitas.Data.Entities.Role", "Role")
                    .WithMany("User")
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_User_Role");
            });
        }
    }
}