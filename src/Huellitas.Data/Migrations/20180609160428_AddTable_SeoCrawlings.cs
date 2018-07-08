﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Huellitas.Data.Migrations
{
    public partial class AddTable_SeoCrawlings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeoCrawlings",
                columns: table => new
                {
                    Url = table.Column<string>(type: "varchar(500)", nullable: false),
                    Html = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeoCrawling", x => x.Url);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeoCrawlings");
        }
    }
}
