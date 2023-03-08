using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalernoServer.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class fixedmodifiercontrollerandmodelsnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Addons_Modifiers_ModifierId",
            //    table: "Addons");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_GroupOptions_Groups_GroupId",
            //    table: "GroupOptions");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Groups_Modifiers_ModifierId",
            //    table: "Groups");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_NoOptions_Modifiers_ModifierId",
            //    table: "NoOptions");

            //migrationBuilder.RenameColumn(
            //    name: "Price",
            //    table: "NoOptions",
            //    newName: "DiscountPrice");

            //migrationBuilder.AlterColumn<long>(
            //    name: "ItemId",
            //    table: "Modifiers",
            //    type: "bigint",
            //    nullable: false,
            //    defaultValue: 0L,
            //    oldClrType: typeof(long),
            //    oldType: "bigint",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "Price",
            //    table: "Items",
            //    type: "decimal(65,30)",
            //    nullable: false,
            //    oldClrType: typeof(double),
            //    oldType: "double");

            //migrationBuilder.UpdateData(
            //    table: "Items",
            //    keyColumn: "Name",
            //    keyValue: null,
            //    column: "Name",
            //    value: "");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Name",
            //    table: "Items",
            //    type: "longtext",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "longtext",
            //    oldNullable: true)
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .OldAnnotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "LastSoldDate",
            //    table: "Items",
            //    type: "datetime(6)",
            //    nullable: true,
            //    oldClrType: typeof(DateTime),
            //    oldType: "datetime(6)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "Cost",
            //    table: "Items",
            //    type: "decimal(65,30)",
            //    nullable: false,
            //    oldClrType: typeof(double),
            //    oldType: "double");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "AssignedCost",
            //    table: "Items",
            //    type: "decimal(65,30)",
            //    nullable: false,
            //    oldClrType: typeof(double),
            //    oldType: "double");

            //migrationBuilder.AlterColumn<long>(
            //    name: "GroupId",
            //    table: "GroupOptions",
            //    type: "bigint",
            //    nullable: false,
            //    defaultValue: 0L,
            //    oldClrType: typeof(long),
            //    oldType: "bigint",
            //    oldNullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Modifiers_ItemId",
            //    table: "Modifiers",
            //    column: "ItemId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Addons_Modifiers_ModifierId",
            //    table: "Addons",
            //    column: "ModifierId",
            //    principalTable: "Modifiers",
            //    principalColumn: "ModifierId",
            //    onDelete: ReferentialAction.SetNull);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_GroupOptions_Groups_GroupId",
            //    table: "GroupOptions",
            //    column: "GroupId",
            //    principalTable: "Groups",
            //    principalColumn: "GroupId",
            //    onDelete: ReferentialAction.SetNull);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Groups_Modifiers_ModifierId",
            //    table: "Groups",
            //    column: "ModifierId",
            //    principalTable: "Modifiers",
            //    principalColumn: "ModifierId",
            //    onDelete: ReferentialAction.SetNull);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Modifiers_Items_ItemId",
            //    table: "Modifiers",
            //    column: "ItemId",
            //    principalTable: "Items",
            //    principalColumn: "ItemId",
            //    onDelete: ReferentialAction.SetNull);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_NoOptions_Modifiers_ModifierId",
            //    table: "NoOptions",
            //    column: "ModifierId",
            //    principalTable: "Modifiers",
            //    principalColumn: "ModifierId",
            //    onDelete: ReferentialAction.SetNull);
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
                name: "FK_Modifiers_Items_ItemId",
                table: "Modifiers");

            migrationBuilder.DropForeignKey(
                name: "FK_NoOptions_Modifiers_ModifierId",
                table: "NoOptions");

            migrationBuilder.DropIndex(
                name: "IX_Modifiers_ItemId",
                table: "Modifiers");

            migrationBuilder.RenameColumn(
                name: "DiscountPrice",
                table: "NoOptions",
                newName: "Price");

            migrationBuilder.AlterColumn<long>(
                name: "ItemId",
                table: "Modifiers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Items",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Items",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastSoldDate",
                table: "Items",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Cost",
                table: "Items",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<double>(
                name: "AssignedCost",
                table: "Items",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<long>(
                name: "GroupId",
                table: "GroupOptions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

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
    }
}
