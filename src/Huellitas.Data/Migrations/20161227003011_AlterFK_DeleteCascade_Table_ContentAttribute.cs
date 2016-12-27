﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Huellitas.Data.Migrations
{
    public partial class AlterFK_DeleteCascade_Table_ContentAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentAttribute_Content",
                table: "ContentAttribute");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentAttribute_Content",
                table: "ContentAttribute",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentAttribute_Content",
                table: "ContentAttribute");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentAttribute_Content",
                table: "ContentAttribute",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
