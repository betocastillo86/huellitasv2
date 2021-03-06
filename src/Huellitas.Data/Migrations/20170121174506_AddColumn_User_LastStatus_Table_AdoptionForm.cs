﻿// <auto-generated/>
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Huellitas.Data.Migrations
{
    public partial class AddColumn_User_LastStatus_Table_AdoptionForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "LastStatus",
                table: "AdoptionForms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AdoptionForms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionForms_UserId",
                table: "AdoptionForms",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionForms_Users_UserId",
                table: "AdoptionForms",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionForms_Users_UserId",
                table: "AdoptionForms");

            migrationBuilder.DropIndex(
                name: "IX_AdoptionForms_UserId",
                table: "AdoptionForms");

            migrationBuilder.DropColumn(
                name: "LastStatus",
                table: "AdoptionForms");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AdoptionForms");
        }
    }
}
