using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Huellitas.Data.Migrations
{
    public partial class AlterTables_ChangePluralName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RelatedContent",
                table: "RelatedContent");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "RelatedContent",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelatedContent",
                table: "RelatedContent",
                column: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "User",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePemission_RoleId",
                table: "RolePemission",
                newName: "IX_RolePemissions_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePemission_PermissionId",
                table: "RolePemission",
                newName: "IX_RolePemissions_PermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedContent_RelatedContentId",
                table: "RelatedContent",
                newName: "IX_RelatedContents_RelatedContentId");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedContent_ContentId",
                table: "RelatedContent",
                newName: "IX_RelatedContents_ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Log_UserId",
                table: "Log",
                newName: "IX_Logs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Location_ParentLocationId",
                table: "Location",
                newName: "IX_Locations_ParentLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomTableRow_CustomTableId",
                table: "CustomTableRow",
                newName: "IX_CustomTableRows_CustomTableId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentUser_UserId",
                table: "ContentUser",
                newName: "IX_ContentUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentUser_ContentId",
                table: "ContentUser",
                newName: "IX_ContentUsers_ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentFile_FileId",
                table: "ContentFile",
                newName: "IX_ContentFiles_FileId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentFile_ContentId",
                table: "ContentFile",
                newName: "IX_ContentFiles_ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentCategory_ContentId",
                table: "ContentCategory",
                newName: "IX_ContentCategories_ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentCategory_CategoryId",
                table: "ContentCategory",
                newName: "IX_ContentCategories_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentAttribute_ContentId",
                table: "ContentAttribute",
                newName: "IX_ContentAttributes_ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_UserId",
                table: "Content",
                newName: "IX_Contents_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_LocationId",
                table: "Content",
                newName: "IX_Contents_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_FriendlyName",
                table: "Content",
                newName: "IX_Contents_FriendlyName");

            migrationBuilder.RenameIndex(
                name: "IX_Content_FileId",
                table: "Content",
                newName: "IX_Contents_FileId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionFormAttribute_AttributeId",
                table: "AdoptionFormAttribute",
                newName: "IX_AdoptionFormAttributes_AttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionFormAttribute_AdoptionFormId",
                table: "AdoptionFormAttribute",
                newName: "IX_AdoptionFormAttributes_AdoptionFormId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionFormAnswer_UserId",
                table: "AdoptionFormAnswer",
                newName: "IX_AdoptionFormAnswers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionFormAnswer_AdoptionFormId",
                table: "AdoptionFormAnswer",
                newName: "IX_AdoptionFormAnswers_AdoptionFormId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionForm_LocationId",
                table: "AdoptionForm",
                newName: "IX_AdoptionForms_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionForm_JobId",
                table: "AdoptionForm",
                newName: "IX_AdoptionForms_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionForm_ContentId",
                table: "AdoptionForm",
                newName: "IX_AdoptionForms_ContentId");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "SystemSetting",
                newName: "SystemSettings");

            migrationBuilder.RenameTable(
                name: "RolePemission",
                newName: "RolePemissions");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "RelatedContent",
                newName: "RelatedContents");

            migrationBuilder.RenameTable(
                name: "Permission",
                newName: "Permissions");

            migrationBuilder.RenameTable(
                name: "Log",
                newName: "Logs");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "File",
                newName: "Files");

            migrationBuilder.RenameTable(
                name: "EmailNotification",
                newName: "EmailNotifications");

            migrationBuilder.RenameTable(
                name: "CustomTableRow",
                newName: "CustomTableRows");

            migrationBuilder.RenameTable(
                name: "CustomTable",
                newName: "CustomTables");

            migrationBuilder.RenameTable(
                name: "ContentUser",
                newName: "ContentUsers");

            migrationBuilder.RenameTable(
                name: "ContentFile",
                newName: "ContentFiles");

            migrationBuilder.RenameTable(
                name: "ContentCategory",
                newName: "ContentCategories");

            migrationBuilder.RenameTable(
                name: "ContentAttribute",
                newName: "ContentAttributes");

            migrationBuilder.RenameTable(
                name: "Content",
                newName: "Contents");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "AdoptionFormAttribute",
                newName: "AdoptionFormAttributes");

            migrationBuilder.RenameTable(
                name: "AdoptionFormAnswer",
                newName: "AdoptionFormAnswers");

            migrationBuilder.RenameTable(
                name: "AdoptionForm",
                newName: "AdoptionForms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RelatedContent",
                table: "RelatedContents");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "RelatedContents",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelatedContent",
                table: "RelatedContents",
                column: "RelatedContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                newName: "IX_User_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePemissions_RoleId",
                table: "RolePemissions",
                newName: "IX_RolePemission_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePemissions_PermissionId",
                table: "RolePemissions",
                newName: "IX_RolePemission_PermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedContents_RelatedContentId",
                table: "RelatedContents",
                newName: "IX_RelatedContent_RelatedContentId");

            migrationBuilder.RenameIndex(
                name: "IX_RelatedContents_ContentId",
                table: "RelatedContents",
                newName: "IX_RelatedContent_ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_UserId",
                table: "Logs",
                newName: "IX_Log_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_ParentLocationId",
                table: "Locations",
                newName: "IX_Location_ParentLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomTableRows_CustomTableId",
                table: "CustomTableRows",
                newName: "IX_CustomTableRow_CustomTableId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentUsers_UserId",
                table: "ContentUsers",
                newName: "IX_ContentUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentUsers_ContentId",
                table: "ContentUsers",
                newName: "IX_ContentUser_ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentFiles_FileId",
                table: "ContentFiles",
                newName: "IX_ContentFile_FileId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentFiles_ContentId",
                table: "ContentFiles",
                newName: "IX_ContentFile_ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentCategories_ContentId",
                table: "ContentCategories",
                newName: "IX_ContentCategory_ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentCategories_CategoryId",
                table: "ContentCategories",
                newName: "IX_ContentCategory_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentAttributes_ContentId",
                table: "ContentAttributes",
                newName: "IX_ContentAttribute_ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_UserId",
                table: "Contents",
                newName: "IX_Content_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_LocationId",
                table: "Contents",
                newName: "IX_Content_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_FriendlyName",
                table: "Contents",
                newName: "IX_Content_FriendlyName");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_FileId",
                table: "Contents",
                newName: "IX_Content_FileId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionFormAttributes_AttributeId",
                table: "AdoptionFormAttributes",
                newName: "IX_AdoptionFormAttribute_AttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionFormAttributes_AdoptionFormId",
                table: "AdoptionFormAttributes",
                newName: "IX_AdoptionFormAttribute_AdoptionFormId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionFormAnswers_UserId",
                table: "AdoptionFormAnswers",
                newName: "IX_AdoptionFormAnswer_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionFormAnswers_AdoptionFormId",
                table: "AdoptionFormAnswers",
                newName: "IX_AdoptionFormAnswer_AdoptionFormId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionForms_LocationId",
                table: "AdoptionForms",
                newName: "IX_AdoptionForm_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionForms_JobId",
                table: "AdoptionForms",
                newName: "IX_AdoptionForm_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_AdoptionForms_ContentId",
                table: "AdoptionForms",
                newName: "IX_AdoptionForm_ContentId");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "SystemSettings",
                newName: "SystemSetting");

            migrationBuilder.RenameTable(
                name: "RolePemissions",
                newName: "RolePemission");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "RelatedContents",
                newName: "RelatedContent");

            migrationBuilder.RenameTable(
                name: "Permissions",
                newName: "Permission");

            migrationBuilder.RenameTable(
                name: "Logs",
                newName: "Log");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "File");

            migrationBuilder.RenameTable(
                name: "EmailNotifications",
                newName: "EmailNotification");

            migrationBuilder.RenameTable(
                name: "CustomTableRows",
                newName: "CustomTableRow");

            migrationBuilder.RenameTable(
                name: "CustomTables",
                newName: "CustomTable");

            migrationBuilder.RenameTable(
                name: "ContentUsers",
                newName: "ContentUser");

            migrationBuilder.RenameTable(
                name: "ContentFiles",
                newName: "ContentFile");

            migrationBuilder.RenameTable(
                name: "ContentCategories",
                newName: "ContentCategory");

            migrationBuilder.RenameTable(
                name: "ContentAttributes",
                newName: "ContentAttribute");

            migrationBuilder.RenameTable(
                name: "Contents",
                newName: "Content");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "AdoptionFormAttributes",
                newName: "AdoptionFormAttribute");

            migrationBuilder.RenameTable(
                name: "AdoptionFormAnswers",
                newName: "AdoptionFormAnswer");

            migrationBuilder.RenameTable(
                name: "AdoptionForms",
                newName: "AdoptionForm");
        }
    }
}
