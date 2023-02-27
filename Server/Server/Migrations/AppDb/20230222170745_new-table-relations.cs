using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalernoServer.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class newtablerelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_OrderItems_OrderItemId",
                table: "Addons");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOptions_OrderItems_OrderItemId",
                table: "NoOptions");

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

            migrationBuilder.CreateTable(
                name: "OrderItemAddons",
                columns: table => new
                {
                    OrderItemId = table.Column<long>(type: "bigint", nullable: false),
                    AddonId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemAddons", x => new { x.OrderItemId, x.AddonId });
                    table.ForeignKey(
                        name: "FK_OrderItemAddons_Addons_AddonId",
                        column: x => x.AddonId,
                        principalTable: "Addons",
                        principalColumn: "AddonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemAddons_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "OrderItemId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderItemNoOptions",
                columns: table => new
                {
                    OrderItemId = table.Column<long>(type: "bigint", nullable: false),
                    NoOptionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemNoOptions", x => new { x.OrderItemId, x.NoOptionId });
                    table.ForeignKey(
                        name: "FK_OrderItemNoOptions_NoOptions_NoOptionId",
                        column: x => x.NoOptionId,
                        principalTable: "NoOptions",
                        principalColumn: "NoOptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemNoOptions_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "OrderItemId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemAddons_AddonId",
                table: "OrderItemAddons",
                column: "AddonId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemNoOptions_NoOptionId",
                table: "OrderItemNoOptions",
                column: "NoOptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItemAddons");

            migrationBuilder.DropTable(
                name: "OrderItemNoOptions");

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

            migrationBuilder.CreateIndex(
                name: "IX_NoOptions_OrderItemId",
                table: "NoOptions",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Addons_OrderItemId",
                table: "Addons",
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
        }
    }
}
