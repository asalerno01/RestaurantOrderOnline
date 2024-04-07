using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class RemoveSavedOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedOrderOrderItems");

            migrationBuilder.DropTable(
                name: "SavedOrders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedOrders",
                columns: table => new
                {
                    SavedOrderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedOrders", x => x.SavedOrderId);
                    table.ForeignKey(
                        name: "FK_SavedOrders_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavedOrderOrderItems",
                columns: table => new
                {
                    SavedOrderOrderItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderItemId = table.Column<long>(type: "bigint", nullable: true),
                    SavedOrderId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedOrderOrderItems", x => x.SavedOrderOrderItemId);
                    table.ForeignKey(
                        name: "FK_SavedOrderOrderItems_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "OrderItemId");
                    table.ForeignKey(
                        name: "FK_SavedOrderOrderItems_SavedOrders_SavedOrderId",
                        column: x => x.SavedOrderId,
                        principalTable: "SavedOrders",
                        principalColumn: "SavedOrderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedOrderOrderItems_OrderItemId",
                table: "SavedOrderOrderItems",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedOrderOrderItems_SavedOrderId",
                table: "SavedOrderOrderItems",
                column: "SavedOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedOrders_AccountId",
                table: "SavedOrders",
                column: "AccountId");
        }
    }
}
