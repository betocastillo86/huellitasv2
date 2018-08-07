﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Huellitas.Data.Migrations
{
    public partial class AddIndex_TypeId_Attribute_Table_Content_Attribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Contents_TypeId",
                table: "Contents",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentAttributes_Attribute",
                table: "ContentAttributes",
                column: "Attribute");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contents_TypeId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_ContentAttributes_Attribute",
                table: "ContentAttributes");
        }
    }
}
