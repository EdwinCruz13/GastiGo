using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingchangingsonaccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Banks_BankId",
                schema: "finances",
                table: "Accounts");

            

            migrationBuilder.AlterColumn<Guid>(
                name: "BankId",
                schema: "finances",
                table: "Accounts",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Banks_BankId",
                schema: "finances",
                table: "Accounts",
                column: "BankId",
                principalSchema: "finances",
                principalTable: "Banks",
                principalColumn: "BankId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Banks_BankId",
                schema: "finances",
                table: "Accounts");

            

            migrationBuilder.AlterColumn<Guid>(
                name: "BankId",
                schema: "finances",
                table: "Accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

           

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Banks_BankId",
                schema: "finances",
                table: "Accounts",
                column: "BankId",
                principalSchema: "finances",
                principalTable: "Banks",
                principalColumn: "BankId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
