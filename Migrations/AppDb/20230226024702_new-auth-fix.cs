using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalernoServer.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class newauthfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "RefreshToken");

            //migrationBuilder.DropColumn(
            //    name: "PhoneNumberConfirmed",
            //    table: "Accounts");

            //migrationBuilder.RenameColumn(
            //    name: "Role",
            //    table: "Employees",
            //    newName: "EmployeeRole");

            //migrationBuilder.RenameColumn(
            //    name: "UserName",
            //    table: "Accounts",
            //    newName: "Role");

            //migrationBuilder.AddColumn<long>(
            //    name: "EmployeeId",
            //    table: "Accounts",
            //    type: "bigint",
            //    nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "CustomerAccounts",
            //    columns: table => new
            //    {
            //        CustomerAccountId = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        Email = table.Column<string>(type: "longtext", nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        Password = table.Column<string>(type: "longtext", nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        FirstName = table.Column<string>(type: "longtext", nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        LastName = table.Column<string>(type: "longtext", nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        RefreshToken = table.Column<string>(type: "longtext", nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CustomerAccounts", x => x.CustomerAccountId);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.CreateTable(
            //    name: "EmployeeAccounts",
            //    columns: table => new
            //    {
            //        EmployeeAccountId = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        EmployeeId = table.Column<long>(type: "bigint", nullable: true),
            //        Email = table.Column<string>(type: "longtext", nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        Password = table.Column<string>(type: "longtext", nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        RefreshToken = table.Column<string>(type: "longtext", nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EmployeeAccounts", x => x.EmployeeAccountId);
            //        table.ForeignKey(
            //            name: "FK_EmployeeAccounts_Employees_EmployeeId",
            //            column: x => x.EmployeeId,
            //            principalTable: "Employees",
            //            principalColumn: "EmployeeId");
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Accounts_EmployeeId",
            //    table: "Accounts",
            //    column: "EmployeeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EmployeeAccounts_EmployeeId",
            //    table: "EmployeeAccounts",
            //    column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Employees_EmployeeId",
                table: "Accounts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Employees_EmployeeId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "CustomerAccounts");

            migrationBuilder.DropTable(
                name: "EmployeeAccounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_EmployeeId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "EmployeeRole",
                table: "Employees",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Accounts",
                newName: "UserName");

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Accounts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Token = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => new { x.AccountId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshToken_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
