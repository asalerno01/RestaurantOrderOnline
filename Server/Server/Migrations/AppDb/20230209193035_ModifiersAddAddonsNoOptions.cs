using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalernoServer.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class ModifiersAddAddonsNoOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ModifierId",
                table: "ModifierOption",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifierId1",
                table: "ModifierOption",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModifierOption_ModifierId",
                table: "ModifierOption",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_ModifierOption_ModifierId1",
                table: "ModifierOption",
                column: "ModifierId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifierOption_Modifiers_ModifierId",
                table: "ModifierOption",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifierOption_Modifiers_ModifierId1",
                table: "ModifierOption",
                column: "ModifierId1",
                principalTable: "Modifiers",
                principalColumn: "ModifierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModifierOption_Modifiers_ModifierId",
                table: "ModifierOption");

            migrationBuilder.DropForeignKey(
                name: "FK_ModifierOption_Modifiers_ModifierId1",
                table: "ModifierOption");

            migrationBuilder.DropIndex(
                name: "IX_ModifierOption_ModifierId",
                table: "ModifierOption");

            migrationBuilder.DropIndex(
                name: "IX_ModifierOption_ModifierId1",
                table: "ModifierOption");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                table: "ModifierOption");

            migrationBuilder.DropColumn(
                name: "ModifierId1",
                table: "ModifierOption");
        }
    }
}
