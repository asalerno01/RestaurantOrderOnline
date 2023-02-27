using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalernoServer.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class initialordersetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrderItemId",
                table: "NoOptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrderItemId",
                table: "Addons",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Subtotal = table.Column<double>(type: "double", nullable: false),
                    Tax = table.Column<double>(type: "double", nullable: false),
                    Net = table.Column<double>(type: "double", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    Subtotal = table.Column<double>(type: "double", nullable: false),
                    GroupOptionId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItem_GroupOptions_GroupOptionId",
                        column: x => x.GroupOptionId,
                        principalTable: "GroupOptions",
                        principalColumn: "GroupOptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_NoOptions_OrderItemId",
                table: "NoOptions",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Addons_OrderItemId",
                table: "Addons",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_GroupOptionId",
                table: "OrderItem",
                column: "GroupOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ItemId",
                table: "OrderItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_OrderItem_OrderItemId",
                table: "Addons");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOptions_OrderItem_OrderItemId",
                table: "NoOptions");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_NoOptions_OrderItemId",
                table: "NoOptions");

            migrationBuilder.DropIndex(
                name: "IX_Addons_OrderItemId",
                table: "Addons");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "NoOptions");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Addons");
        }
    }
}
