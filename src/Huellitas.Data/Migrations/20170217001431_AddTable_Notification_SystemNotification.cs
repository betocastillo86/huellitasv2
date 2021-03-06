﻿// <auto-generated/>
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Huellitas.Data.Migrations
{
    public partial class AddTable_Notification_SystemNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    IsEmail = table.Column<bool>(nullable: false),
                    IsSystem = table.Column<bool>(nullable: false),
                    EmailHtml = table.Column<string>(nullable: true),
                    EmailSubject = table.Column<string>(maxLength: 500, nullable: true),
                    SystemText = table.Column<string>(maxLength: 2000, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Tags = table.Column<string>(maxLength: 500, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 500, nullable: false),
                    TargetURL = table.Column<string>(maxLength: 500, nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Seen = table.Column<bool>(nullable: false),
                    TriggerUserId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemNotification_TriggerUser",
                        column: x => x.TriggerUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SystemNotification_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemNotifications_TriggerUserId",
                table: "SystemNotifications",
                column: "TriggerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemNotifications_UserId",
                table: "SystemNotifications",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "SystemNotifications");
        }
    }
}
