using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace developerService.Migrations
{
    public partial class hackername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HackerName",
                table: "Developers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HackerName",
                table: "Developers");
        }
    }
}
