using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class ShoppingCartInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropColumn(
                name: "OrderCount",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    ShoppingCartId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.ShoppingCartId);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    ShoppingCartItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ItemSnapshotId = table.Column<long>(type: "bigint", nullable: false),
                    ShoppingCartId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.ShoppingCartItemId);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_ItemSnapshots_ItemSnapshotId",
                        column: x => x.ItemSnapshotId,
                        principalTable: "ItemSnapshots",
                        principalColumn: "ItemSnapshotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "ShoppingCartId");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartAddons",
                columns: table => new
                {
                    ShoppingCartAddonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddonSnapshotId = table.Column<long>(type: "bigint", nullable: false),
                    ShoppingCartItemId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartAddons", x => x.ShoppingCartAddonId);
                    table.ForeignKey(
                        name: "FK_ShoppingCartAddons_AddonSnapshots_AddonSnapshotId",
                        column: x => x.AddonSnapshotId,
                        principalTable: "AddonSnapshots",
                        principalColumn: "AddonSnapshotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartAddons_ShoppingCartItems_ShoppingCartItemId",
                        column: x => x.ShoppingCartItemId,
                        principalTable: "ShoppingCartItems",
                        principalColumn: "ShoppingCartItemId");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartGroups",
                columns: table => new
                {
                    ShoppingCartGroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupOptionSnapshotId = table.Column<long>(type: "bigint", nullable: false),
                    ShoppingCartItemId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartGroups", x => x.ShoppingCartGroupId);
                    table.ForeignKey(
                        name: "FK_ShoppingCartGroups_GroupOptionSnapshots_GroupOptionSnapshotId",
                        column: x => x.GroupOptionSnapshotId,
                        principalTable: "GroupOptionSnapshots",
                        principalColumn: "GroupOptionSnapshotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartGroups_ShoppingCartItems_ShoppingCartItemId",
                        column: x => x.ShoppingCartItemId,
                        principalTable: "ShoppingCartItems",
                        principalColumn: "ShoppingCartItemId");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartNoOptions",
                columns: table => new
                {
                    ShoppingCartNoOptionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoOptionSnapshotId = table.Column<long>(type: "bigint", nullable: false),
                    ShoppingCartItemId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartNoOptions", x => x.ShoppingCartNoOptionId);
                    table.ForeignKey(
                        name: "FK_ShoppingCartNoOptions_NoOptionSnapshots_NoOptionSnapshotId",
                        column: x => x.NoOptionSnapshotId,
                        principalTable: "NoOptionSnapshots",
                        principalColumn: "NoOptionSnapshotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartNoOptions_ShoppingCartItems_ShoppingCartItemId",
                        column: x => x.ShoppingCartItemId,
                        principalTable: "ShoppingCartItems",
                        principalColumn: "ShoppingCartItemId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartAddons_AddonSnapshotId",
                table: "ShoppingCartAddons",
                column: "AddonSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartAddons_ShoppingCartItemId",
                table: "ShoppingCartAddons",
                column: "ShoppingCartItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartGroups_GroupOptionSnapshotId",
                table: "ShoppingCartGroups",
                column: "GroupOptionSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartGroups_ShoppingCartItemId",
                table: "ShoppingCartGroups",
                column: "ShoppingCartItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ItemSnapshotId",
                table: "ShoppingCartItems",
                column: "ItemSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                table: "ShoppingCartItems",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartNoOptions_NoOptionSnapshotId",
                table: "ShoppingCartNoOptions",
                column: "NoOptionSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartNoOptions_ShoppingCartItemId",
                table: "ShoppingCartNoOptions",
                column: "ShoppingCartItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_AccountId",
                table: "ShoppingCarts",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartAddons");

            migrationBuilder.DropTable(
                name: "ShoppingCartGroups");

            migrationBuilder.DropTable(
                name: "ShoppingCartNoOptions");

            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderCount",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Review_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_AccountId",
                table: "Review",
                column: "AccountId");
        }
    }
}
