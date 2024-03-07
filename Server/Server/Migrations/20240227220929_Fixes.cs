using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class Fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_ModifierSnapshots_ModifierSnapshotId",
                table: "Addons");

            migrationBuilder.DropForeignKey(
                name: "FK_AddonSnapshots_Modifiers_ModifierId",
                table: "AddonSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOptions_GroupSnapshots_GroupSnapshotId",
                table: "GroupOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOptionSnapshots_Groups_GroupId",
                table: "GroupOptionSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_ModifierSnapshots_ModifierSnapshotId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupSnapshots_Modifiers_ModifierId",
                table: "GroupSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_CategorySnapshots_CategorySnapshotId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSnapshots_Modifiers_ModifierId",
                table: "ItemSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifierSnapshots_Items_ItemId",
                table: "ModifierSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOptions_ModifierSnapshots_ModifierSnapshotId",
                table: "NoOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOptionSnapshots_Modifiers_ModifierId",
                table: "NoOptionSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_NoOptionSnapshots_ModifierId",
                table: "NoOptionSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_NoOptions_ModifierSnapshotId",
                table: "NoOptions");

            migrationBuilder.DropIndex(
                name: "IX_ModifierSnapshots_ItemId",
                table: "ModifierSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_ItemSnapshots_ModifierId",
                table: "ItemSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_Items_CategorySnapshotId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_GroupSnapshots_ModifierId",
                table: "GroupSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_Groups_ModifierSnapshotId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_GroupOptionSnapshots_GroupId",
                table: "GroupOptionSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_GroupOptions_GroupSnapshotId",
                table: "GroupOptions");

            migrationBuilder.DropIndex(
                name: "IX_AddonSnapshots_ModifierId",
                table: "AddonSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_Addons_ModifierSnapshotId",
                table: "Addons");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "NoOptionSnapshots");

            migrationBuilder.DropColumn(
                name: "ModifierSnapshotId",
                table: "NoOptions");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ModifierSnapshots");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "ItemSnapshots");

            migrationBuilder.DropColumn(
                name: "CategorySnapshotId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "GroupSnapshots");

            migrationBuilder.DropColumn(
                name: "ModifierSnapshotId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "GroupOptionSnapshots");

            migrationBuilder.DropColumn(
                name: "GroupSnapshotId",
                table: "GroupOptions");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "AddonSnapshots");

            migrationBuilder.DropColumn(
                name: "ModifierSnapshotId",
                table: "Addons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ModifierId",
                table: "NoOptionSnapshots",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ModifierSnapshotId",
                table: "NoOptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "ModifierSnapshots",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ModifierId",
                table: "ItemSnapshots",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CategorySnapshotId",
                table: "Items",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifierId",
                table: "GroupSnapshots",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ModifierSnapshotId",
                table: "Groups",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GroupId",
                table: "GroupOptionSnapshots",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "GroupSnapshotId",
                table: "GroupOptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifierId",
                table: "AddonSnapshots",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ModifierSnapshotId",
                table: "Addons",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoOptionSnapshots_ModifierId",
                table: "NoOptionSnapshots",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_NoOptions_ModifierSnapshotId",
                table: "NoOptions",
                column: "ModifierSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierSnapshots_ItemId",
                table: "ModifierSnapshots",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSnapshots_ModifierId",
                table: "ItemSnapshots",
                column: "ModifierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategorySnapshotId",
                table: "Items",
                column: "CategorySnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupSnapshots_ModifierId",
                table: "GroupSnapshots",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ModifierSnapshotId",
                table: "Groups",
                column: "ModifierSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOptionSnapshots_GroupId",
                table: "GroupOptionSnapshots",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOptions_GroupSnapshotId",
                table: "GroupOptions",
                column: "GroupSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_AddonSnapshots_ModifierId",
                table: "AddonSnapshots",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Addons_ModifierSnapshotId",
                table: "Addons",
                column: "ModifierSnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_ModifierSnapshots_ModifierSnapshotId",
                table: "Addons",
                column: "ModifierSnapshotId",
                principalTable: "ModifierSnapshots",
                principalColumn: "ModifierSnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddonSnapshots_Modifiers_ModifierId",
                table: "AddonSnapshots",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOptions_GroupSnapshots_GroupSnapshotId",
                table: "GroupOptions",
                column: "GroupSnapshotId",
                principalTable: "GroupSnapshots",
                principalColumn: "GroupSnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOptionSnapshots_Groups_GroupId",
                table: "GroupOptionSnapshots",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_ModifierSnapshots_ModifierSnapshotId",
                table: "Groups",
                column: "ModifierSnapshotId",
                principalTable: "ModifierSnapshots",
                principalColumn: "ModifierSnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSnapshots_Modifiers_ModifierId",
                table: "GroupSnapshots",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_CategorySnapshots_CategorySnapshotId",
                table: "Items",
                column: "CategorySnapshotId",
                principalTable: "CategorySnapshots",
                principalColumn: "CategorySnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSnapshots_Modifiers_ModifierId",
                table: "ItemSnapshots",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ModifierSnapshots_Items_ItemId",
                table: "ModifierSnapshots",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoOptions_ModifierSnapshots_ModifierSnapshotId",
                table: "NoOptions",
                column: "ModifierSnapshotId",
                principalTable: "ModifierSnapshots",
                principalColumn: "ModifierSnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoOptionSnapshots_Modifiers_ModifierId",
                table: "NoOptionSnapshots",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
