using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalernoServer.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class RelationshipUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addon_Modifiers_ModifierId",
                table: "Addon");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOption_ModifierGroups_ModifierGroupId",
                table: "GroupOption");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOption_Modifiers_ModifierId",
                table: "NoOption");

            migrationBuilder.DropTable(
                name: "ModifierGroups");

            migrationBuilder.DropIndex(
                name: "IX_GroupOption_ModifierGroupId",
                table: "GroupOption");

            migrationBuilder.DropColumn(
                name: "ModifierGroupId",
                table: "GroupOption");

            migrationBuilder.AlterColumn<long>(
                name: "ModifierId",
                table: "NoOption",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GroupId",
                table: "GroupOption",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "ModifierId",
                table: "Addon",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifierId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Group_Modifiers_ModifierId",
                        column: x => x.ModifierId,
                        principalTable: "Modifiers",
                        principalColumn: "ModifierId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOption_GroupId",
                table: "GroupOption",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_ModifierId",
                table: "Group",
                column: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addon_Modifiers_ModifierId",
                table: "Addon",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addon_Modifiers_ModifierId",
                table: "Addon");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOption_Group_GroupId",
                table: "GroupOption");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOption_Modifiers_ModifierId",
                table: "NoOption");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropIndex(
                name: "IX_GroupOption_GroupId",
                table: "GroupOption");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "GroupOption");

            migrationBuilder.AlterColumn<long>(
                name: "ModifierId",
                table: "NoOption",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ModifierGroupId",
                table: "GroupOption",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ModifierId",
                table: "Addon",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "ModifierGroups",
                columns: table => new
                {
                    ModifierGroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ModifierId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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

            migrationBuilder.CreateIndex(
                name: "IX_GroupOption_ModifierGroupId",
                table: "GroupOption",
                column: "ModifierGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierGroups_ModifierId",
                table: "ModifierGroups",
                column: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addon_Modifiers_ModifierId",
                table: "Addon",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOption_ModifierGroups_ModifierGroupId",
                table: "GroupOption",
                column: "ModifierGroupId",
                principalTable: "ModifierGroups",
                principalColumn: "ModifierGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoOption_Modifiers_ModifierId",
                table: "NoOption",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId");
        }
    }
}
