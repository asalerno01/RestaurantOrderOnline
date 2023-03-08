using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalernoServer.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class NewModifierUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModifierOption_ModifierAddons_ModifierAddonId",
                table: "ModifierOption");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifierOption_ModifierNoOptions_ModifierNoOptionId",
                table: "ModifierOption");

            migrationBuilder.DropTable(
                name: "ModifierAddons");

            migrationBuilder.DropTable(
                name: "ModifierNoOptions");

            migrationBuilder.DropIndex(
                name: "IX_ModifierOption_ModifierAddonId",
                table: "ModifierOption");

            migrationBuilder.DropIndex(
                name: "IX_ModifierOption_ModifierNoOptionId",
                table: "ModifierOption");

            migrationBuilder.DropColumn(
                name: "ModifierAddonId",
                table: "ModifierOption");

            migrationBuilder.DropColumn(
                name: "ModifierNoOptionId",
                table: "ModifierOption");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ModifierOption",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ModifierOption");

            migrationBuilder.AddColumn<long>(
                name: "ModifierAddonId",
                table: "ModifierOption",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifierNoOptionId",
                table: "ModifierOption",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ModifierAddons",
                columns: table => new
                {
                    ModifierAddonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ModifierId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifierAddons", x => x.ModifierAddonId);
                    table.ForeignKey(
                        name: "FK_ModifierAddons_Modifiers_ModifierId",
                        column: x => x.ModifierId,
                        principalTable: "Modifiers",
                        principalColumn: "ModifierId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ModifierNoOptions",
                columns: table => new
                {
                    ModifierNoOptionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ModifierId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifierNoOptions", x => x.ModifierNoOptionId);
                    table.ForeignKey(
                        name: "FK_ModifierNoOptions_Modifiers_ModifierId",
                        column: x => x.ModifierId,
                        principalTable: "Modifiers",
                        principalColumn: "ModifierId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierOption_ModifierAddonId",
                table: "ModifierOption",
                column: "ModifierAddonId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierOption_ModifierNoOptionId",
                table: "ModifierOption",
                column: "ModifierNoOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierAddons_ModifierId",
                table: "ModifierAddons",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierNoOptions_ModifierId",
                table: "ModifierNoOptions",
                column: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifierOption_ModifierAddons_ModifierAddonId",
                table: "ModifierOption",
                column: "ModifierAddonId",
                principalTable: "ModifierAddons",
                principalColumn: "ModifierAddonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifierOption_ModifierNoOptions_ModifierNoOptionId",
                table: "ModifierOption",
                column: "ModifierNoOptionId",
                principalTable: "ModifierNoOptions",
                principalColumn: "ModifierNoOptionId");
        }
    }
}
