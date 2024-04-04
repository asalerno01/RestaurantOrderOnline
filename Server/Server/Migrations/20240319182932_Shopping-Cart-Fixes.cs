using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class ShoppingCartFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartAddons_ShoppingCartItems_ShoppingCartItemId",
                table: "ShoppingCartAddons");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartGroups_ShoppingCartItems_ShoppingCartItemId",
                table: "ShoppingCartGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartNoOptions_ShoppingCartItems_ShoppingCartItemId",
                table: "ShoppingCartNoOptions");

            migrationBuilder.DropColumn(
                name: "SessionToken",
                table: "ShoppingCarts");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "ShoppingCarts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "ShoppingCartItemId",
                table: "ShoppingCartNoOptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ShoppingCartItemId",
                table: "ShoppingCartGroups",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ShoppingCartItemId",
                table: "ShoppingCartAddons",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_RefreshToken",
                table: "ShoppingCarts",
                column: "RefreshToken");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartAddons_ShoppingCartItems_ShoppingCartItemId",
                table: "ShoppingCartAddons",
                column: "ShoppingCartItemId",
                principalTable: "ShoppingCartItems",
                principalColumn: "ShoppingCartItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartGroups_ShoppingCartItems_ShoppingCartItemId",
                table: "ShoppingCartGroups",
                column: "ShoppingCartItemId",
                principalTable: "ShoppingCartItems",
                principalColumn: "ShoppingCartItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartNoOptions_ShoppingCartItems_ShoppingCartItemId",
                table: "ShoppingCartNoOptions",
                column: "ShoppingCartItemId",
                principalTable: "ShoppingCartItems",
                principalColumn: "ShoppingCartItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartAddons_ShoppingCartItems_ShoppingCartItemId",
                table: "ShoppingCartAddons");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartGroups_ShoppingCartItems_ShoppingCartItemId",
                table: "ShoppingCartGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartNoOptions_ShoppingCartItems_ShoppingCartItemId",
                table: "ShoppingCartNoOptions");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_RefreshToken",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "ShoppingCarts");

            migrationBuilder.AddColumn<string>(
                name: "SessionToken",
                table: "ShoppingCarts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ShoppingCartItemId",
                table: "ShoppingCartNoOptions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ShoppingCartItemId",
                table: "ShoppingCartGroups",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ShoppingCartItemId",
                table: "ShoppingCartAddons",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartAddons_ShoppingCartItems_ShoppingCartItemId",
                table: "ShoppingCartAddons",
                column: "ShoppingCartItemId",
                principalTable: "ShoppingCartItems",
                principalColumn: "ShoppingCartItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartGroups_ShoppingCartItems_ShoppingCartItemId",
                table: "ShoppingCartGroups",
                column: "ShoppingCartItemId",
                principalTable: "ShoppingCartItems",
                principalColumn: "ShoppingCartItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartNoOptions_ShoppingCartItems_ShoppingCartItemId",
                table: "ShoppingCartNoOptions",
                column: "ShoppingCartItemId",
                principalTable: "ShoppingCartItems",
                principalColumn: "ShoppingCartItemId");
        }
    }
}
