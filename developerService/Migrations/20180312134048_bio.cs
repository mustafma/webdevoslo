using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace developerService.Migrations
{
    public partial class bio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Developers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Developers");
        }
    }
}
