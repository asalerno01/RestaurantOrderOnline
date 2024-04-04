using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class RemoveSnapshotsFromShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartAddons_AddonSnapshots_AddonSnapshotId",
                table: "ShoppingCartAddons");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartGroups_GroupOptionSnapshots_GroupOptionSnapshotId",
                table: "ShoppingCartGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_ItemSnapshots_ItemSnapshotId",
                table: "ShoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartNoOptions_NoOptionSnapshots_NoOptionSnapshotId",
                table: "ShoppingCartNoOptions");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_ItemSnapshotId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "ItemSnapshotId",
                table: "ShoppingCartItems");

            migrationBuilder.RenameColumn(
                name: "NoOptionSnapshotId",
                table: "ShoppingCartNoOptions",
                newName: "NoOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartNoOptions_NoOptionSnapshotId",
                table: "ShoppingCartNoOptions",
                newName: "IX_ShoppingCartNoOptions_NoOptionId");

            migrationBuilder.RenameColumn(
                name: "GroupOptionSnapshotId",
                table: "ShoppingCartGroups",
                newName: "GroupOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartGroups_GroupOptionSnapshotId",
                table: "ShoppingCartGroups",
                newName: "IX_ShoppingCartGroups_GroupOptionId");

            migrationBuilder.RenameColumn(
                name: "AddonSnapshotId",
                table: "ShoppingCartAddons",
                newName: "AddonId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartAddons_AddonSnapshotId",
                table: "ShoppingCartAddons",
                newName: "IX_ShoppingCartAddons_AddonId");

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "ShoppingCartItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GroupSnapshots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ItemId",
                table: "ShoppingCartItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartAddons_Addons_AddonId",
                table: "ShoppingCartAddons",
                column: "AddonId",
                principalTable: "Addons",
                principalColumn: "AddonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartGroups_GroupOptions_GroupOptionId",
                table: "ShoppingCartGroups",
                column: "GroupOptionId",
                principalTable: "GroupOptions",
                principalColumn: "GroupOptionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Items_ItemId",
                table: "ShoppingCartItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartNoOptions_NoOptions_NoOptionId",
                table: "ShoppingCartNoOptions",
                column: "NoOptionId",
                principalTable: "NoOptions",
                principalColumn: "NoOptionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartAddons_Addons_AddonId",
                table: "ShoppingCartAddons");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartGroups_GroupOptions_GroupOptionId",
                table: "ShoppingCartGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Items_ItemId",
                table: "ShoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartNoOptions_NoOptions_NoOptionId",
                table: "ShoppingCartNoOptions");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_ItemId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "GroupSnapshots");

            migrationBuilder.RenameColumn(
                name: "NoOptionId",
                table: "ShoppingCartNoOptions",
                newName: "NoOptionSnapshotId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartNoOptions_NoOptionId",
                table: "ShoppingCartNoOptions",
                newName: "IX_ShoppingCartNoOptions_NoOptionSnapshotId");

            migrationBuilder.RenameColumn(
                name: "GroupOptionId",
                table: "ShoppingCartGroups",
                newName: "GroupOptionSnapshotId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartGroups_GroupOptionId",
                table: "ShoppingCartGroups",
                newName: "IX_ShoppingCartGroups_GroupOptionSnapshotId");

            migrationBuilder.RenameColumn(
                name: "AddonId",
                table: "ShoppingCartAddons",
                newName: "AddonSnapshotId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartAddons_AddonId",
                table: "ShoppingCartAddons",
                newName: "IX_ShoppingCartAddons_AddonSnapshotId");

            migrationBuilder.AddColumn<long>(
                name: "ItemSnapshotId",
                table: "ShoppingCartItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ItemSnapshotId",
                table: "ShoppingCartItems",
                column: "ItemSnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartAddons_AddonSnapshots_AddonSnapshotId",
                table: "ShoppingCartAddons",
                column: "AddonSnapshotId",
                principalTable: "AddonSnapshots",
                principalColumn: "AddonSnapshotId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartGroups_GroupOptionSnapshots_GroupOptionSnapshotId",
                table: "ShoppingCartGroups",
                column: "GroupOptionSnapshotId",
                principalTable: "GroupOptionSnapshots",
                principalColumn: "GroupOptionSnapshotId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_ItemSnapshots_ItemSnapshotId",
                table: "ShoppingCartItems",
                column: "ItemSnapshotId",
                principalTable: "ItemSnapshots",
                principalColumn: "ItemSnapshotId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartNoOptions_NoOptionSnapshots_NoOptionSnapshotId",
                table: "ShoppingCartNoOptions",
                column: "NoOptionSnapshotId",
                principalTable: "NoOptionSnapshots",
                principalColumn: "NoOptionSnapshotId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
