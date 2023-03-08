using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalernoServer.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class NewModifiersDesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModifierOption");

            migrationBuilder.CreateTable(
                name: "Addon",
                columns: table => new
                {
                    AddonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ModifierId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addon", x => x.AddonId);
                    table.ForeignKey(
                        name: "FK_Addon_Modifiers_ModifierId",
                        column: x => x.ModifierId,
                        principalTable: "Modifiers",
                        principalColumn: "ModifierId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GroupOption",
                columns: table => new
                {
                    GroupOptionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ModifierGroupId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupOption", x => x.GroupOptionId);
                    table.ForeignKey(
                        name: "FK_GroupOption_ModifierGroups_ModifierGroupId",
                        column: x => x.ModifierGroupId,
                        principalTable: "ModifierGroups",
                        principalColumn: "ModifierGroupId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NoOption",
                columns: table => new
                {
                    NoOptionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ModifierId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoOption", x => x.NoOptionId);
                    table.ForeignKey(
                        name: "FK_NoOption_Modifiers_ModifierId",
                        column: x => x.ModifierId,
                        principalTable: "Modifiers",
                        principalColumn: "ModifierId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Addon_ModifierId",
                table: "Addon",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOption_ModifierGroupId",
                table: "GroupOption",
                column: "ModifierGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_NoOption_ModifierId",
                table: "NoOption",
                column: "ModifierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addon");

            migrationBuilder.DropTable(
                name: "GroupOption");

            migrationBuilder.DropTable(
                name: "NoOption");

            migrationBuilder.CreateTable(
                name: "ModifierOption",
                columns: table => new
                {
                    ModifierOptionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ModifierGroupId = table.Column<long>(type: "bigint", nullable: true),
                    ModifierId = table.Column<long>(type: "bigint", nullable: true),
                    ModifierId1 = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PriceIncrease = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifierOption", x => x.ModifierOptionId);
                    table.ForeignKey(
                        name: "FK_ModifierOption_ModifierGroups_ModifierGroupId",
                        column: x => x.ModifierGroupId,
                        principalTable: "ModifierGroups",
                        principalColumn: "ModifierGroupId");
                    table.ForeignKey(
                        name: "FK_ModifierOption_Modifiers_ModifierId",
                        column: x => x.ModifierId,
                        principalTable: "Modifiers",
                        principalColumn: "ModifierId");
                    table.ForeignKey(
                        name: "FK_ModifierOption_Modifiers_ModifierId1",
                        column: x => x.ModifierId1,
                        principalTable: "Modifiers",
                        principalColumn: "ModifierId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierOption_ModifierGroupId",
                table: "ModifierOption",
                column: "ModifierGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierOption_ModifierId",
                table: "ModifierOption",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierOption_ModifierId1",
                table: "ModifierOption",
                column: "ModifierId1");
        }
    }
}
