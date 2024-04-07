using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class RemovedModifierObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_Modifiers_ModifierId",
                table: "Addons");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOptions_Groups_GroupId",
                table: "GroupOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Modifiers_ModifierId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOptions_Modifiers_ModifierId",
                table: "NoOptions");

            migrationBuilder.DropTable(
                name: "ModifierSnapshots");

            migrationBuilder.DropTable(
                name: "Modifiers");

            migrationBuilder.DropIndex(
                name: "IX_NoOptions_ModifierId",
                table: "NoOptions");

            migrationBuilder.DropIndex(
                name: "IX_Groups_ModifierId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Addons_ModifierId",
                table: "Addons");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "NoOptions");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "Addons");

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "NoOptions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "GroupId",
                table: "GroupOptions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "Addons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoOptions_ItemId",
                table: "NoOptions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ItemId",
                table: "Groups",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Addons_ItemId",
                table: "Addons",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_Items_ItemId",
                table: "Addons",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOptions_Groups_GroupId",
                table: "GroupOptions",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Items_ItemId",
                table: "Groups",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoOptions_Items_ItemId",
                table: "NoOptions",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_Items_ItemId",
                table: "Addons");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOptions_Groups_GroupId",
                table: "GroupOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Items_ItemId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOptions_Items_ItemId",
                table: "NoOptions");

            migrationBuilder.DropIndex(
                name: "IX_NoOptions_ItemId",
                table: "NoOptions");

            migrationBuilder.DropIndex(
                name: "IX_Groups_ItemId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Addons_ItemId",
                table: "Addons");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "NoOptions");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Addons");

            migrationBuilder.AddColumn<long>(
                name: "ModifierId",
                table: "NoOptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ModifierId",
                table: "Groups",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "GroupId",
                table: "GroupOptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifierId",
                table: "Addons",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Modifiers",
                columns: table => new
                {
                    ModifierId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modifiers", x => x.ModifierId);
                    table.ForeignKey(
                        name: "FK_Modifiers_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModifierSnapshots",
                columns: table => new
                {
                    ModifierSnapshotId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModifierId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifierSnapshots", x => x.ModifierSnapshotId);
                    table.ForeignKey(
                        name: "FK_ModifierSnapshots_Modifiers_ModifierId",
                        column: x => x.ModifierId,
                        principalTable: "Modifiers",
                        principalColumn: "ModifierId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoOptions_ModifierId",
                table: "NoOptions",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ModifierId",
                table: "Groups",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Addons_ModifierId",
                table: "Addons",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Modifiers_ItemId",
                table: "Modifiers",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModifierSnapshots_ModifierId",
                table: "ModifierSnapshots",
                column: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_Modifiers_ModifierId",
                table: "Addons",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOptions_Groups_GroupId",
                table: "GroupOptions",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Modifiers_ModifierId",
                table: "Groups",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NoOptions_Modifiers_ModifierId",
                table: "NoOptions",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
