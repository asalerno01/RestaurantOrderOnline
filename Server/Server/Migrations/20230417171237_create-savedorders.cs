using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class createsavedorders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SavedOrderOrderItemId",
                table: "OrderItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SavedOrders",
                columns: table => new
                {
                    SavedOrderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerAccountId = table.Column<long>(type: "bigint", nullable: false),
                    LastOrderDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedOrders", x => x.SavedOrderId);
                    table.ForeignKey(
                        name: "FK_SavedOrders_CustomerAccounts_CustomerAccountId",
                        column: x => x.CustomerAccountId,
                        principalTable: "CustomerAccounts",
                        principalColumn: "CustomerAccountId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SavedOrderOrderItems",
                columns: table => new
                {
                    SavedOrderOrderItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SavedOrderId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedOrderOrderItems", x => x.SavedOrderOrderItemId);
                    table.ForeignKey(
                        name: "FK_SavedOrderOrderItems_SavedOrders_SavedOrderId",
                        column: x => x.SavedOrderId,
                        principalTable: "SavedOrders",
                        principalColumn: "SavedOrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_SavedOrderOrderItemId",
                table: "OrderItems",
                column: "SavedOrderOrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedOrderOrderItems_SavedOrderId",
                table: "SavedOrderOrderItems",
                column: "SavedOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedOrders_CustomerAccountId",
                table: "SavedOrders",
                column: "CustomerAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_SavedOrderOrderItems_SavedOrderOrderItemId",
                table: "OrderItems",
                column: "SavedOrderOrderItemId",
                principalTable: "SavedOrderOrderItems",
                principalColumn: "SavedOrderOrderItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_SavedOrderOrderItems_SavedOrderOrderItemId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "SavedOrderOrderItems");

            migrationBuilder.DropTable(
                name: "SavedOrders");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_SavedOrderOrderItemId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "SavedOrderOrderItemId",
                table: "OrderItems");
        }
    }
}
