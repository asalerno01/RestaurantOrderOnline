using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class ShoppingCartGroupFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GroupId",
                table: "ShoppingCartGroups",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartGroups_GroupId",
                table: "ShoppingCartGroups",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartGroups_Groups_GroupId",
                table: "ShoppingCartGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartGroups_Groups_GroupId",
                table: "ShoppingCartGroups");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartGroups_GroupId",
                table: "ShoppingCartGroups");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "ShoppingCartGroups");
        }
    }
}
