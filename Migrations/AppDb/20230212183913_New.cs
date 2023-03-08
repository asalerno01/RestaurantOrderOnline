using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalernoServer.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addon_Modifiers_ModifierId",
                table: "Addon");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Modifiers_ModifierId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOption_Group_GroupId",
                table: "GroupOption");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOption_Modifiers_ModifierId",
                table: "NoOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoOption",
                table: "NoOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupOption",
                table: "GroupOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Group",
                table: "Group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addon",
                table: "Addon");

            migrationBuilder.RenameTable(
                name: "NoOption",
                newName: "NoOptions");

            migrationBuilder.RenameTable(
                name: "GroupOption",
                newName: "GroupOptions");

            migrationBuilder.RenameTable(
                name: "Group",
                newName: "Groups");

            migrationBuilder.RenameTable(
                name: "Addon",
                newName: "Addons");

            migrationBuilder.RenameIndex(
                name: "IX_NoOption_ModifierId",
                table: "NoOptions",
                newName: "IX_NoOptions_ModifierId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupOption_GroupId",
                table: "GroupOptions",
                newName: "IX_GroupOptions_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Group_ModifierId",
                table: "Groups",
                newName: "IX_Groups_ModifierId");

            migrationBuilder.RenameIndex(
                name: "IX_Addon_ModifierId",
                table: "Addons",
                newName: "IX_Addons_ModifierId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "NoOptions",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "GroupOptions",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Addons",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<long>(
                name: "ModifierId",
                table: "Addons",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoOptions",
                table: "NoOptions",
                column: "NoOptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupOptions",
                table: "GroupOptions",
                column: "GroupOptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addons",
                table: "Addons",
                column: "AddonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_Modifiers_ModifierId",
                table: "Addons",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOptions_Groups_GroupId",
                table: "GroupOptions",
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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoOptions_Modifiers_ModifierId",
                table: "NoOptions",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoOptions",
                table: "NoOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupOptions",
                table: "GroupOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addons",
                table: "Addons");

            migrationBuilder.RenameTable(
                name: "NoOptions",
                newName: "NoOption");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "Group");

            migrationBuilder.RenameTable(
                name: "GroupOptions",
                newName: "GroupOption");

            migrationBuilder.RenameTable(
                name: "Addons",
                newName: "Addon");

            migrationBuilder.RenameIndex(
                name: "IX_NoOptions_ModifierId",
                table: "NoOption",
                newName: "IX_NoOption_ModifierId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_ModifierId",
                table: "Group",
                newName: "IX_Group_ModifierId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupOptions_GroupId",
                table: "GroupOption",
                newName: "IX_GroupOption_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Addons_ModifierId",
                table: "Addon",
                newName: "IX_Addon_ModifierId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "NoOption",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "GroupOption",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Addon",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<long>(
                name: "ModifierId",
                table: "Addon",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoOption",
                table: "NoOption",
                column: "NoOptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Group",
                table: "Group",
                column: "GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupOption",
                table: "GroupOption",
                column: "GroupOptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addon",
                table: "Addon",
                column: "AddonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addon_Modifiers_ModifierId",
                table: "Addon",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Modifiers_ModifierId",
                table: "Group",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOption_Group_GroupId",
                table: "GroupOption",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoOption_Modifiers_ModifierId",
                table: "NoOption",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
