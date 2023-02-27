using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalernoServer.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ItemUUID = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Department = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UPC = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SKU = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<double>(type: "double", nullable: false),
                    Discountable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Taxable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TrackingInventory = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Cost = table.Column<double>(type: "double", nullable: false),
                    AssignedCost = table.Column<double>(type: "double", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ReorderTrigger = table.Column<int>(type: "int", nullable: false),
                    RecommendedOrder = table.Column<int>(type: "int", nullable: false),
                    LastSoldDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Supplier = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LiabilityItem = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LiabilityRedemptionTender = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaxGroupOrRate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Secret = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Modifiers",
                columns: table => new
                {
                    ModifierId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modifiers", x => x.ModifierId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "ModifierGroups",
                columns: table => new
                {
                    ModifierGroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifierId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifierGroups", x => x.ModifierGroupId);
                    table.ForeignKey(
                        name: "FK_ModifierGroups_Modifiers_ModifierId",
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

            migrationBuilder.CreateTable(
                name: "ModifierOption",
                columns: table => new
                {
                    ModifierOptionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PriceIncrease = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ModifierAddonId = table.Column<long>(type: "bigint", nullable: true),
                    ModifierGroupId = table.Column<long>(type: "bigint", nullable: true),
                    ModifierNoOptionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifierOption", x => x.ModifierOptionId);
                    table.ForeignKey(
                        name: "FK_ModifierOption_ModifierAddons_ModifierAddonId",
                        column: x => x.ModifierAddonId,
                        principalTable: "ModifierAddons",
                        principalColumn: "ModifierAddonId");
                    table.ForeignKey(
                        name: "FK_ModifierOption_ModifierGroups_ModifierGroupId",
                        column: x => x.ModifierGroupId,
                        principalTable: "ModifierGroups",
                        principalColumn: "ModifierGroupId");
                    table.ForeignKey(
                        name: "FK_ModifierOption_ModifierNoOptions_ModifierNoOptionId",
                        column: x => x.ModifierNoOptionId,
                        principalTable: "ModifierNoOptions",
                        principalColumn: "ModifierNoOptionId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierAddons_ModifierId",
                table: "ModifierAddons",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierGroups_ModifierId",
                table: "ModifierGroups",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierNoOptions_ModifierId",
                table: "ModifierNoOptions",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierOption_ModifierAddonId",
                table: "ModifierOption",
                column: "ModifierAddonId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierOption_ModifierGroupId",
                table: "ModifierOption",
                column: "ModifierGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierOption_ModifierNoOptionId",
                table: "ModifierOption",
                column: "ModifierNoOptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ModifierOption");

            migrationBuilder.DropTable(
                name: "ModifierAddons");

            migrationBuilder.DropTable(
                name: "ModifierGroups");

            migrationBuilder.DropTable(
                name: "ModifierNoOptions");

            migrationBuilder.DropTable(
                name: "Modifiers");
        }
    }
}
