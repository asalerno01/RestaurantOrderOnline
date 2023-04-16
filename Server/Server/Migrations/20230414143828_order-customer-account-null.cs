using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ordercustomeraccountnull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CustomerAccounts_CustomerAccountId",
                table: "Orders");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerAccountId",
                table: "Orders",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CustomerAccounts_CustomerAccountId",
                table: "Orders",
                column: "CustomerAccountId",
                principalTable: "CustomerAccounts",
                principalColumn: "CustomerAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CustomerAccounts_CustomerAccountId",
                table: "Orders");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerAccountId",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CustomerAccounts_CustomerAccountId",
                table: "Orders",
                column: "CustomerAccountId",
                principalTable: "CustomerAccounts",
                principalColumn: "CustomerAccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
