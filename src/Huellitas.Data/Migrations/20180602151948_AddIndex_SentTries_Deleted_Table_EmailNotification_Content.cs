using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Huellitas.Data.Migrations
{
    public partial class AddIndex_SentTries_Deleted_Table_EmailNotification_Content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionFormAnswer_AdoptionForm",
                table: "AdoptionFormAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionFormAnswer_User",
                table: "AdoptionFormAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionFormAttribute_AdoptionForm",
                table: "AdoptionFormAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionFormAttribute_CustomTableRow",
                table: "AdoptionFormAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionForm_Content1",
                table: "AdoptionForms");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionForm_CustomTableRow",
                table: "AdoptionForms");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionForm_Location",
                table: "AdoptionForms");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionFormUser_AdoptionForm",
                table: "AdoptionFormUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionFormUser_User",
                table: "AdoptionFormUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Banner_File",
                table: "Banners");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Content",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_ParentComment",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentCategory_Category",
                table: "ContentCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentCategory_Content",
                table: "ContentCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentFile_Content",
                table: "ContentFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentFile_File",
                table: "ContentFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Content_User",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentUser_Content",
                table: "ContentUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentUser_User",
                table: "ContentUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomTableRow_CustomTable",
                table: "CustomTableRows");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Location_ParentLocationId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedContent_Content",
                table: "RelatedContents");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedContent_Content1",
                table: "RelatedContents");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePemission_Permission",
                table: "RolePemissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePemission_Role",
                table: "RolePemissions");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemNotification_TriggerUser",
                table: "SystemNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemNotification_User",
                table: "SystemNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Location",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Contents_FriendlyName",
                table: "Contents");

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotifications_SentDate_SentTries",
                table: "EmailNotifications",
                columns: new[] { "SentDate", "SentTries" });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_FriendlyName",
                table: "Contents",
                column: "FriendlyName",
                unique: true,
                filter: "[FriendlyName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_Deleted_TypeId_Status_ClosingDate",
                table: "Contents",
                columns: new[] { "Deleted", "TypeId", "Status", "ClosingDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionFormAnswer_AdoptionForm",
                table: "AdoptionFormAnswers",
                column: "AdoptionFormId",
                principalTable: "AdoptionForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionFormAnswer_User",
                table: "AdoptionFormAnswers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionFormAttribute_AdoptionForm",
                table: "AdoptionFormAttributes",
                column: "AdoptionFormId",
                principalTable: "AdoptionForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionFormAttribute_CustomTableRow",
                table: "AdoptionFormAttributes",
                column: "AttributeId",
                principalTable: "CustomTableRows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionForm_Content1",
                table: "AdoptionForms",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionForm_CustomTableRow",
                table: "AdoptionForms",
                column: "JobId",
                principalTable: "CustomTableRows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionForm_Location",
                table: "AdoptionForms",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionFormUser_AdoptionForm",
                table: "AdoptionFormUsers",
                column: "AdoptionFormId",
                principalTable: "AdoptionForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionFormUser_User",
                table: "AdoptionFormUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Banner_File",
                table: "Banners",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Content",
                table: "Comments",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_ParentComment",
                table: "Comments",
                column: "ParentCommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentCategory_Category",
                table: "ContentCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentCategory_Content",
                table: "ContentCategories",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentFile_Content",
                table: "ContentFiles",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentFile_File",
                table: "ContentFiles",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Content_User",
                table: "Contents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentUser_Content",
                table: "ContentUsers",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentUser_User",
                table: "ContentUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomTableRow_CustomTable",
                table: "CustomTableRows",
                column: "CustomTableId",
                principalTable: "CustomTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Location_ParentLocationId",
                table: "Locations",
                column: "ParentLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedContent_Content",
                table: "RelatedContents",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedContent_Content1",
                table: "RelatedContents",
                column: "RelatedContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePemission_Permission",
                table: "RolePemissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePemission_Role",
                table: "RolePemissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemNotification_TriggerUser",
                table: "SystemNotifications",
                column: "TriggerUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemNotification_User",
                table: "SystemNotifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Location",
                table: "Users",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionFormAnswer_AdoptionForm",
                table: "AdoptionFormAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionFormAnswer_User",
                table: "AdoptionFormAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionFormAttribute_AdoptionForm",
                table: "AdoptionFormAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionFormAttribute_CustomTableRow",
                table: "AdoptionFormAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionForm_Content1",
                table: "AdoptionForms");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionForm_CustomTableRow",
                table: "AdoptionForms");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionForm_Location",
                table: "AdoptionForms");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionFormUser_AdoptionForm",
                table: "AdoptionFormUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionFormUser_User",
                table: "AdoptionFormUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Banner_File",
                table: "Banners");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Content",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_ParentComment",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentCategory_Category",
                table: "ContentCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentCategory_Content",
                table: "ContentCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentFile_Content",
                table: "ContentFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentFile_File",
                table: "ContentFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Content_User",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentUser_Content",
                table: "ContentUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentUser_User",
                table: "ContentUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomTableRow_CustomTable",
                table: "CustomTableRows");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Location_ParentLocationId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedContent_Content",
                table: "RelatedContents");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedContent_Content1",
                table: "RelatedContents");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePemission_Permission",
                table: "RolePemissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePemission_Role",
                table: "RolePemissions");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemNotification_TriggerUser",
                table: "SystemNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemNotification_User",
                table: "SystemNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Location",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_EmailNotifications_SentDate_SentTries",
                table: "EmailNotifications");

            migrationBuilder.DropIndex(
                name: "IX_Contents_FriendlyName",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_Deleted_TypeId_Status_ClosingDate",
                table: "Contents");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_FriendlyName",
                table: "Contents",
                column: "FriendlyName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionFormAnswer_AdoptionForm",
                table: "AdoptionFormAnswers",
                column: "AdoptionFormId",
                principalTable: "AdoptionForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionFormAnswer_User",
                table: "AdoptionFormAnswers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionFormAttribute_AdoptionForm",
                table: "AdoptionFormAttributes",
                column: "AdoptionFormId",
                principalTable: "AdoptionForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionFormAttribute_CustomTableRow",
                table: "AdoptionFormAttributes",
                column: "AttributeId",
                principalTable: "CustomTableRows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionForm_Content1",
                table: "AdoptionForms",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionForm_CustomTableRow",
                table: "AdoptionForms",
                column: "JobId",
                principalTable: "CustomTableRows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionForm_Location",
                table: "AdoptionForms",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionFormUser_AdoptionForm",
                table: "AdoptionFormUsers",
                column: "AdoptionFormId",
                principalTable: "AdoptionForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionFormUser_User",
                table: "AdoptionFormUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Banner_File",
                table: "Banners",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Content",
                table: "Comments",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_ParentComment",
                table: "Comments",
                column: "ParentCommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentCategory_Category",
                table: "ContentCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentCategory_Content",
                table: "ContentCategories",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentFile_Content",
                table: "ContentFiles",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentFile_File",
                table: "ContentFiles",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Content_User",
                table: "Contents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentUser_Content",
                table: "ContentUsers",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentUser_User",
                table: "ContentUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomTableRow_CustomTable",
                table: "CustomTableRows",
                column: "CustomTableId",
                principalTable: "CustomTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Location_ParentLocationId",
                table: "Locations",
                column: "ParentLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedContent_Content",
                table: "RelatedContents",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedContent_Content1",
                table: "RelatedContents",
                column: "RelatedContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePemission_Permission",
                table: "RolePemissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePemission_Role",
                table: "RolePemissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemNotification_TriggerUser",
                table: "SystemNotifications",
                column: "TriggerUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemNotification_User",
                table: "SystemNotifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Location",
                table: "Users",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
