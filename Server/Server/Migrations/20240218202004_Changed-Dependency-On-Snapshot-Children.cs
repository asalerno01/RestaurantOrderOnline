using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class ChangedDependencyOnSnapshotChildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_Modifiers_ModifierId",
                table: "Addons");

            migrationBuilder.DropForeignKey(
                name: "FK_AddonSnapshots_ModifierSnapshots_ModifierSnapshotId",
                table: "AddonSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOptions_Groups_GroupId",
                table: "GroupOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOptionSnapshots_GroupSnapshots_GroupSnapshotId",
                table: "GroupOptionSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Modifiers_ModifierId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupSnapshots_ModifierSnapshots_ModifierSnapshotId",
                table: "GroupSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSnapshots_CategorySnapshots_CategorySnapshotId",
                table: "ItemSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifierSnapshots_ItemSnapshots_ItemSnapshotId",
                table: "ModifierSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOptions_Modifiers_ModifierId",
                table: "NoOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOptionSnapshots_ModifierSnapshots_ModifierSnapshotId",
                table: "NoOptionSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_ModifierSnapshots_ItemSnapshotId",
                table: "ModifierSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_ItemSnapshots_CategorySnapshotId",
                table: "ItemSnapshots");

            migrationBuilder.DropColumn(
                name: "ItemSnapshotId",
                table: "ModifierSnapshots");

            migrationBuilder.RenameColumn(
                name: "ModifierSnapshotId",
                table: "NoOptionSnapshots",
                newName: "ModifierId");

            migrationBuilder.RenameIndex(
                name: "IX_NoOptionSnapshots_ModifierSnapshotId",
                table: "NoOptionSnapshots",
                newName: "IX_NoOptionSnapshots_ModifierId");

            migrationBuilder.RenameColumn(
                name: "CategorySnapshotId",
                table: "ItemSnapshots",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "ModifierSnapshotId",
                table: "GroupSnapshots",
                newName: "ModifierId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupSnapshots_ModifierSnapshotId",
                table: "GroupSnapshots",
                newName: "IX_GroupSnapshots_ModifierId");

            migrationBuilder.RenameColumn(
                name: "GroupSnapshotId",
                table: "GroupOptionSnapshots",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupOptionSnapshots_GroupSnapshotId",
                table: "GroupOptionSnapshots",
                newName: "IX_GroupOptionSnapshots_GroupId");

            migrationBuilder.RenameColumn(
                name: "ModifierSnapshotId",
                table: "AddonSnapshots",
                newName: "ModifierId");

            migrationBuilder.RenameIndex(
                name: "IX_AddonSnapshots_ModifierSnapshotId",
                table: "AddonSnapshots",
                newName: "IX_AddonSnapshots_ModifierId");

            migrationBuilder.AddColumn<long>(
                name: "ModifierSnapshotId",
                table: "NoOptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemId",
                table: "ModifierSnapshots",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
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
                name: "ModifierSnapshotId",
                table: "Groups",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GroupSnapshotId",
                table: "GroupOptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifierSnapshotId",
                table: "Addons",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoOptions_ModifierSnapshotId",
                table: "NoOptions",
                column: "ModifierSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierSnapshots_ItemId",
                table: "ModifierSnapshots",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSnapshots_CategoryId",
                table: "ItemSnapshots",
                column: "CategoryId");

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
                name: "IX_Groups_ModifierSnapshotId",
                table: "Groups",
                column: "ModifierSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOptions_GroupSnapshotId",
                table: "GroupOptions",
                column: "GroupSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_Addons_ModifierSnapshotId",
                table: "Addons",
                column: "ModifierSnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_Modifiers_ModifierId",
                table: "Addons",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_GroupOptions_Groups_GroupId",
                table: "GroupOptions",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Groups_Modifiers_ModifierId",
                table: "Groups",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_ItemSnapshots_Categories_CategoryId",
                table: "ItemSnapshots",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_NoOptions_Modifiers_ModifierId",
                table: "NoOptions",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_Modifiers_ModifierId",
                table: "Addons");

            migrationBuilder.DropForeignKey(
                name: "FK_Addons_ModifierSnapshots_ModifierSnapshotId",
                table: "Addons");

            migrationBuilder.DropForeignKey(
                name: "FK_AddonSnapshots_Modifiers_ModifierId",
                table: "AddonSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOptions_Groups_GroupId",
                table: "GroupOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOptions_GroupSnapshots_GroupSnapshotId",
                table: "GroupOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOptionSnapshots_Groups_GroupId",
                table: "GroupOptionSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Modifiers_ModifierId",
                table: "Groups");

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
                name: "FK_ItemSnapshots_Categories_CategoryId",
                table: "ItemSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSnapshots_Modifiers_ModifierId",
                table: "ItemSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifierSnapshots_Items_ItemId",
                table: "ModifierSnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOptions_Modifiers_ModifierId",
                table: "NoOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOptions_ModifierSnapshots_ModifierSnapshotId",
                table: "NoOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOptionSnapshots_Modifiers_ModifierId",
                table: "NoOptionSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_NoOptions_ModifierSnapshotId",
                table: "NoOptions");

            migrationBuilder.DropIndex(
                name: "IX_ModifierSnapshots_ItemId",
                table: "ModifierSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_ItemSnapshots_CategoryId",
                table: "ItemSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_ItemSnapshots_ModifierId",
                table: "ItemSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_Items_CategorySnapshotId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Groups_ModifierSnapshotId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_GroupOptions_GroupSnapshotId",
                table: "GroupOptions");

            migrationBuilder.DropIndex(
                name: "IX_Addons_ModifierSnapshotId",
                table: "Addons");

            migrationBuilder.DropColumn(
                name: "ModifierSnapshotId",
                table: "NoOptions");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ItemSnapshots");

            migrationBuilder.DropColumn(
                name: "CategorySnapshotId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ModifierSnapshotId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupSnapshotId",
                table: "GroupOptions");

            migrationBuilder.DropColumn(
                name: "ModifierSnapshotId",
                table: "Addons");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "NoOptionSnapshots",
                newName: "ModifierSnapshotId");

            migrationBuilder.RenameIndex(
                name: "IX_NoOptionSnapshots_ModifierId",
                table: "NoOptionSnapshots",
                newName: "IX_NoOptionSnapshots_ModifierSnapshotId");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "ItemSnapshots",
                newName: "CategorySnapshotId");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "GroupSnapshots",
                newName: "ModifierSnapshotId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupSnapshots_ModifierId",
                table: "GroupSnapshots",
                newName: "IX_GroupSnapshots_ModifierSnapshotId");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "GroupOptionSnapshots",
                newName: "GroupSnapshotId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupOptionSnapshots_GroupId",
                table: "GroupOptionSnapshots",
                newName: "IX_GroupOptionSnapshots_GroupSnapshotId");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "AddonSnapshots",
                newName: "ModifierSnapshotId");

            migrationBuilder.RenameIndex(
                name: "IX_AddonSnapshots_ModifierId",
                table: "AddonSnapshots",
                newName: "IX_AddonSnapshots_ModifierSnapshotId");

            migrationBuilder.AlterColumn<string>(
                name: "ItemId",
                table: "ModifierSnapshots",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<long>(
                name: "ItemSnapshotId",
                table: "ModifierSnapshots",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ModifierSnapshots_ItemSnapshotId",
                table: "ModifierSnapshots",
                column: "ItemSnapshotId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemSnapshots_CategorySnapshotId",
                table: "ItemSnapshots",
                column: "CategorySnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_Modifiers_ModifierId",
                table: "Addons",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddonSnapshots_ModifierSnapshots_ModifierSnapshotId",
                table: "AddonSnapshots",
                column: "ModifierSnapshotId",
                principalTable: "ModifierSnapshots",
                principalColumn: "ModifierSnapshotId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOptions_Groups_GroupId",
                table: "GroupOptions",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOptionSnapshots_GroupSnapshots_GroupSnapshotId",
                table: "GroupOptionSnapshots",
                column: "GroupSnapshotId",
                principalTable: "GroupSnapshots",
                principalColumn: "GroupSnapshotId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Modifiers_ModifierId",
                table: "Groups",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSnapshots_ModifierSnapshots_ModifierSnapshotId",
                table: "GroupSnapshots",
                column: "ModifierSnapshotId",
                principalTable: "ModifierSnapshots",
                principalColumn: "ModifierSnapshotId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSnapshots_CategorySnapshots_CategorySnapshotId",
                table: "ItemSnapshots",
                column: "CategorySnapshotId",
                principalTable: "CategorySnapshots",
                principalColumn: "CategorySnapshotId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModifierSnapshots_ItemSnapshots_ItemSnapshotId",
                table: "ModifierSnapshots",
                column: "ItemSnapshotId",
                principalTable: "ItemSnapshots",
                principalColumn: "ItemSnapshotId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoOptions_Modifiers_ModifierId",
                table: "NoOptions",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoOptionSnapshots_ModifierSnapshots_ModifierSnapshotId",
                table: "NoOptionSnapshots",
                column: "ModifierSnapshotId",
                principalTable: "ModifierSnapshots",
                principalColumn: "ModifierSnapshotId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
