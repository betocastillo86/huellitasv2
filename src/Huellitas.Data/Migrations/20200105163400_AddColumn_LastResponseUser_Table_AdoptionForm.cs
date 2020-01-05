using Microsoft.EntityFrameworkCore.Migrations;

namespace Huellitas.Data.Migrations
{
    public partial class AddColumn_LastResponseUser_Table_AdoptionForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastResponseUserId",
                table: "AdoptionForms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionForms_LastResponseUserId",
                table: "AdoptionForms",
                column: "LastResponseUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionForm_User_LastResponse",
                table: "AdoptionForms",
                column: "LastResponseUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionForm_User_LastResponse",
                table: "AdoptionForms");

            migrationBuilder.DropIndex(
                name: "IX_AdoptionForms_LastResponseUserId",
                table: "AdoptionForms");

            migrationBuilder.DropColumn(
                name: "LastResponseUserId",
                table: "AdoptionForms");
        }
    }
}
