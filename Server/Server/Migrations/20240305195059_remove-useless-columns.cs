using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class removeuselesscolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ModifierSnapshots");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ModifierSnapshots");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Modifiers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Modifiers");

            migrationBuilder.DropColumn(
                name: "LastSoldDate",
                table: "ItemSnapshots");

            migrationBuilder.DropColumn(
                name: "LastSoldDate",
                table: "Items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ModifierSnapshots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ModifierSnapshots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Modifiers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Modifiers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSoldDate",
                table: "ItemSnapshots",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSoldDate",
                table: "Items",
                type: "datetime2",
                nullable: true);
        }
    }
}
