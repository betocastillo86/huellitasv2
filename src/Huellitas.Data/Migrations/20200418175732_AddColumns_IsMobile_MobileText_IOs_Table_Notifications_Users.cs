using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Huellitas.Data.Migrations
{
    public partial class AddColumns_IsMobile_MobileText_IOs_Table_Notifications_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeviceId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IOsDeviceId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMobile",
                table: "Notifications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MobileText",
                table: "Notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IOsDeviceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsMobile",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "MobileText",
                table: "Notifications");
        }
    }
}
