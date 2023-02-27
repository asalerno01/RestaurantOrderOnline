using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalernoServer.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class CascadeDelete : Migration
    {
        /// <inheritdoc />
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

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_Modifiers_ModifierId",
                table: "Addons",
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
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Modifiers_ModifierId",
                table: "Groups",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoOptions_Modifiers_ModifierId",
                table: "NoOptions",
                column: "ModifierId",
                principalTable: "Modifiers",
                principalColumn: "ModifierId");
        }
    }
}
