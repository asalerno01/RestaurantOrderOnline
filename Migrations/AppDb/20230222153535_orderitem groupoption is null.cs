using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalernoServer.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class orderitemgroupoptionisnull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_GroupOptions_GroupOptionId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Items_ItemId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_GroupOptionId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ItemId",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Items",
                newName: "ItemId");

            migrationBuilder.AlterColumn<long>(
                name: "GroupOptionId",
                table: "OrderItem",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Items",
                newName: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "GroupOptionId",
                table: "OrderItem",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_GroupOptionId",
                table: "OrderItem",
                column: "GroupOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ItemId",
                table: "OrderItem",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_GroupOptions_GroupOptionId",
                table: "OrderItem",
                column: "GroupOptionId",
                principalTable: "GroupOptions",
                principalColumn: "GroupOptionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Items_ItemId",
                table: "OrderItem",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
