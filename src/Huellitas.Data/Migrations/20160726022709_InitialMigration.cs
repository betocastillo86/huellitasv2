using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Huellitas.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailNotification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    To = table.Column<string>(maxLength: 200, nullable: false),
                    ToName = table.Column<string>(maxLength: 200, nullable: true),
                    CC = table.Column<string>(maxLength: 500, nullable: true),
                    Subject = table.Column<string>(type: "varchar(300)", nullable: false),
                    Body = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SentTries = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailNotification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(type: "varchar(150)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: true),
                    MimeType = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    ParentLocationId = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomTableRow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomTableId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(type: "varchar(200)", nullable: false),
                    AdditionalInfo = table.Column<string>(maxLength: 100, nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomTableRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomTableRow_CustomTable",
                        column: x => x.CustomTableId,
                        principalTable: "CustomTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePemission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePemission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePemission_Permission",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePemission_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Body = table.Column<string>(nullable: false),
                    TypeId = table.Column<short>(nullable: false),
                    Status = table.Column<short>(nullable: false),
                    FileId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false, defaultValueSql: "0"),
                    LocationId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(type: "varchar(150)", nullable: true),
                    CommentsCount = table.Column<int>(nullable: false, defaultValueSql: "0"),
                    Views = table.Column<int>(nullable: false, defaultValueSql: "0"),
                    Featured = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Content_File",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Content_Location",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Content_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdoptionForm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Address = table.Column<string>(type: "varchar(100)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FamilyMembers = table.Column<short>(nullable: false),
                    JobId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", nullable: false),
                    Town = table.Column<string>(type: "varchar(50)", nullable: false),
                    AutoreplyToken = table.Column<Guid>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdoptionForm_Content1",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdoptionForm_CustomTableRow",
                        column: x => x.JobId,
                        principalTable: "CustomTableRow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdoptionForm_Location",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Attribute = table.Column<string>(type: "varchar(50)", nullable: false),
                    Value = table.Column<string>(type: "varchar(500)", nullable: false),
                    ContentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentAttribute_Content",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentCategory_Category",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentCategory_Content",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentId = table.Column<int>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false, defaultValueSql: "0"),
                    FileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentFile_Content",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContentFile_File",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelatedContent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentId = table.Column<int>(nullable: false),
                    RelatedContentId = table.Column<int>(nullable: false),
                    RelationType = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatedContent_Content",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelatedContent_Content1",
                        column: x => x.RelatedContentId,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdoptionFormAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdoptionFormId = table.Column<int>(nullable: false),
                    Status = table.Column<short>(nullable: false),
                    AdditionalInfo = table.Column<string>(maxLength: 2000, nullable: true),
                    Notes = table.Column<string>(maxLength: 1500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionFormAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdoptionFormAnswer_AdoptionForm",
                        column: x => x.AdoptionFormId,
                        principalTable: "AdoptionForm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdoptionFormAnswer_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdoptionFormAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdoptionFormId = table.Column<int>(nullable: false),
                    AttributeId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionFormAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdoptionFormAttribute_AdoptionForm",
                        column: x => x.AdoptionFormId,
                        principalTable: "AdoptionForm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdoptionFormAttribute_CustomTableRow",
                        column: x => x.AttributeId,
                        principalTable: "CustomTableRow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionForm_ContentId",
                table: "AdoptionForm",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionForm_JobId",
                table: "AdoptionForm",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionForm_LocationId",
                table: "AdoptionForm",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionFormAnswer_AdoptionFormId",
                table: "AdoptionFormAnswer",
                column: "AdoptionFormId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionFormAnswer_UserId",
                table: "AdoptionFormAnswer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionFormAttribute_AdoptionFormId",
                table: "AdoptionFormAttribute",
                column: "AdoptionFormId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionFormAttribute_AttributeId",
                table: "AdoptionFormAttribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Content_FileId",
                table: "Content",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Content_LocationId",
                table: "Content",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Content_UserId",
                table: "Content",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentAttribute_ContentId",
                table: "ContentAttribute",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentCategory_CategoryId",
                table: "ContentCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentCategory_ContentId",
                table: "ContentCategory",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentFile_ContentId",
                table: "ContentFile",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentFile_FileId",
                table: "ContentFile",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomTableRow_CustomTableId",
                table: "CustomTableRow",
                column: "CustomTableId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedContent_ContentId",
                table: "RelatedContent",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedContent_RelatedContentId",
                table: "RelatedContent",
                column: "RelatedContentId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePemission_PermissionId",
                table: "RolePemission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePemission_RoleId",
                table: "RolePemission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemSetting",
                table: "SystemSetting",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdoptionFormAnswer");

            migrationBuilder.DropTable(
                name: "AdoptionFormAttribute");

            migrationBuilder.DropTable(
                name: "ContentAttribute");

            migrationBuilder.DropTable(
                name: "ContentCategory");

            migrationBuilder.DropTable(
                name: "ContentFile");

            migrationBuilder.DropTable(
                name: "EmailNotification");

            migrationBuilder.DropTable(
                name: "RelatedContent");

            migrationBuilder.DropTable(
                name: "RolePemission");

            migrationBuilder.DropTable(
                name: "SystemSetting");

            migrationBuilder.DropTable(
                name: "AdoptionForm");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Content");

            migrationBuilder.DropTable(
                name: "CustomTableRow");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "CustomTable");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}