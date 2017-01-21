using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Huellitas.Data.Core;

namespace Huellitas.Data.Migrations
{
    [DbContext(typeof(HuellitasContext))]
    [Migration("20170121152941_AlterTables_ChangePluralName")]
    partial class AlterTables_ChangePluralName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Huellitas.Data.Entities.AdoptionForm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

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

                    b.HasKey("Id")
                        .HasName("PK_AdoptionForm");

                    b.HasIndex("ContentId");

                    b.HasIndex("JobId");

                    b.HasIndex("LocationId");

                    b.ToTable("AdoptionForms");
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

                    b.HasKey("Id")
                        .HasName("PK_AdoptionFormAnswer");

                    b.HasIndex("AdoptionFormId");

                    b.HasIndex("UserId");

                    b.ToTable("AdoptionFormAnswers");
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

                    b.HasKey("Id")
                        .HasName("PK_AdoptionFormAttribute");

                    b.HasIndex("AdoptionFormId");

                    b.HasIndex("AttributeId");

                    b.ToTable("AdoptionFormAttributes");
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

                    b.HasKey("Id")
                        .HasName("PK_Category");

                    b.ToTable("Categories");
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

                    b.Property<string>("FriendlyName");

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

                    b.HasKey("Id")
                        .HasName("PK_Content");

                    b.HasIndex("FileId");

                    b.HasIndex("FriendlyName")
                        .IsUnique();

                    b.HasIndex("LocationId");

                    b.HasIndex("UserId");

                    b.ToTable("Contents");
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

                    b.HasKey("Id")
                        .HasName("PK_ContentAttribute");

                    b.HasIndex("ContentId");

                    b.ToTable("ContentAttributes");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.ContentCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int>("ContentId");

                    b.HasKey("Id")
                        .HasName("PK_ContentCategory");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ContentId");

                    b.ToTable("ContentCategories");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.ContentFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContentId");

                    b.Property<int>("DisplayOrder")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0");

                    b.Property<int>("FileId");

                    b.HasKey("Id")
                        .HasName("PK_ContentFile");

                    b.HasIndex("ContentId");

                    b.HasIndex("FileId");

                    b.ToTable("ContentFiles");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.ContentUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContentId")
                        .HasColumnName("ContentId");

                    b.Property<short>("RelationTypeId")
                        .HasColumnName("RelationTypeId");

                    b.Property<int>("UserId")
                        .HasColumnName("UserId");

                    b.HasKey("Id")
                        .HasName("PK_ContentUser");

                    b.HasIndex("ContentId");

                    b.HasIndex("UserId");

                    b.ToTable("ContentUsers");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.CustomTable", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id")
                        .HasName("PK_CustomTable");

                    b.ToTable("CustomTables");
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

                    b.HasKey("Id")
                        .HasName("PK_CustomTableRow");

                    b.HasIndex("CustomTableId");

                    b.ToTable("CustomTableRows");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.EmailNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

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

                    b.HasKey("Id")
                        .HasName("PK_EmailNotification");

                    b.ToTable("EmailNotifications");
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

                    b.HasKey("Id")
                        .HasName("PK_File");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("ParentLocationId");

                    b.HasKey("Id")
                        .HasName("PK_Location");

                    b.HasIndex("ParentLocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("FullMessage")
                        .HasColumnName("FullMessage")
                        .HasAnnotation("MaxLength", 4000);

                    b.Property<string>("IpAddress")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<short>("LogLevelId");

                    b.Property<string>("PageUrl")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("ShortMessage")
                        .IsRequired()
                        .HasColumnName("ShortMessage")
                        .HasAnnotation("MaxLength", 4000);

                    b.Property<int?>("UserId");

                    b.HasKey("Id")
                        .HasName("PK_Log");

                    b.HasIndex("UserId");

                    b.ToTable("Logs");
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

                    b.HasKey("Id")
                        .HasName("PK_Permission");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.RelatedContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContentId");

                    b.Property<int>("RelatedContentId");

                    b.Property<short>("RelationType");

                    b.HasKey("Id")
                        .HasName("PK_RelatedContent");

                    b.HasIndex("ContentId");

                    b.HasIndex("RelatedContentId");

                    b.ToTable("RelatedContents");
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

                    b.HasKey("Id")
                        .HasName("PK_Role");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.RolePemission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PermissionId");

                    b.Property<int>("RoleId");

                    b.HasKey("Id")
                        .HasName("PK_RolePemission");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePemissions");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.SystemSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id")
                        .HasName("PK_SystemSetting");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("IX_SystemSetting");

                    b.ToTable("SystemSettings");
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

                    b.Property<string>("PhoneNumber2")
                        .HasColumnType("varchar(15)");

                    b.Property<int>("RoleId");

                    b.HasKey("Id")
                        .HasName("PK_User");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("IX_User");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
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
                        .WithMany()
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
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .HasConstraintName("FK_Content_Location");

                    b.HasOne("Huellitas.Data.Entities.User", "User")
                        .WithMany("Contents")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Content_User");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.ContentAttribute", b =>
                {
                    b.HasOne("Huellitas.Data.Entities.Content", "Content")
                        .WithMany("ContentAttributes")
                        .HasForeignKey("ContentId")
                        .HasConstraintName("FK_ContentAttribute_Content")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Huellitas.Data.Entities.ContentCategory", b =>
                {
                    b.HasOne("Huellitas.Data.Entities.Category", "Category")
                        .WithMany("ContentCategory")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_ContentCategory_Category");

                    b.HasOne("Huellitas.Data.Entities.Content", "Content")
                        .WithMany("ContentCategories")
                        .HasForeignKey("ContentId")
                        .HasConstraintName("FK_ContentCategory_Content");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.ContentFile", b =>
                {
                    b.HasOne("Huellitas.Data.Entities.Content", "Content")
                        .WithMany("ContentFiles")
                        .HasForeignKey("ContentId")
                        .HasConstraintName("FK_ContentFile_Content");

                    b.HasOne("Huellitas.Data.Entities.File", "File")
                        .WithMany("ContentFile")
                        .HasForeignKey("FileId")
                        .HasConstraintName("FK_ContentFile_File");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.ContentUser", b =>
                {
                    b.HasOne("Huellitas.Data.Entities.Content", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .HasConstraintName("FK_ContentUser_Content");

                    b.HasOne("Huellitas.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ContentUser_User");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.CustomTableRow", b =>
                {
                    b.HasOne("Huellitas.Data.Entities.CustomTable", "CustomTable")
                        .WithMany("CustomTableRow")
                        .HasForeignKey("CustomTableId")
                        .HasConstraintName("FK_CustomTableRow_CustomTable");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.Location", b =>
                {
                    b.HasOne("Huellitas.Data.Entities.Location", "ParentLocation")
                        .WithMany()
                        .HasForeignKey("ParentLocationId")
                        .HasConstraintName("FK_Location_Location_ParentLocationId");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.Log", b =>
                {
                    b.HasOne("Huellitas.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Log_User")
                        .OnDelete(DeleteBehavior.Cascade);
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
                        .WithMany("RolePemissions")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_RolePemission_Role");
                });

            modelBuilder.Entity("Huellitas.Data.Entities.User", b =>
                {
                    b.HasOne("Huellitas.Data.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_User_Role");
                });
        }
    }
}
