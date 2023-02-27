using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalernoServer.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class neworderitemstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_OrderItem_OrderItemId",
                table: "Addons");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOptions_OrderItem_OrderItemId",
                table: "NoOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                newName: "OrderItems");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_OrderItems_OrderItemId",
                table: "Addons",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoOptions_OrderItems_OrderItemId",
                table: "NoOptions",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_OrderItems_OrderItemId",
                table: "Addons");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOptions_OrderItems_OrderItemId",
                table: "NoOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "OrderItem");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_OrderItem_OrderItemId",
                table: "Addons",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoOptions_OrderItem_OrderItemId",
                table: "NoOptions",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
