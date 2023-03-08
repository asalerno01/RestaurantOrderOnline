using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalernoServer.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class newtablerelationsfixnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderItemGroupOptions",
                columns: table => new
                {
                    OrderItemId = table.Column<long>(type: "bigint", nullable: false),
                    GroupOptionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemGroupOptions", x => new { x.OrderItemId, x.GroupOptionId });
                    table.ForeignKey(
                        name: "FK_OrderItemGroupOptions_GroupOptions_GroupOptionId",
                        column: x => x.GroupOptionId,
                        principalTable: "GroupOptions",
                        principalColumn: "GroupOptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemGroupOptions_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "OrderItemId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemGroupOptions_GroupOptionId",
                table: "OrderItemGroupOptions",
                column: "GroupOptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItemGroupOptions");
        }
    }
}
