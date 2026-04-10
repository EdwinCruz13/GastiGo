using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addinginitialbalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<DateTime>(
                name: "InitialBalanceDate",
                schema: "finances",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("040b710c-fb3d-43ec-8913-349dd9146013"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("49c8d4bd-4827-4fbe-84df-4dc9063bd4c3"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("57c8730f-30f6-4586-8195-8f2c700e232e"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("e07ab868-b0c3-4378-809b-12a1cb0b7898"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("1b5b41c6-9fa5-4c0f-b6d0-18b8f36602e4"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("80698e0d-350e-4e0c-81eb-de9f86abe64e"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("b8388e60-eb8e-4b2e-b31a-5246eefed1ea"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("ce0f7f5f-c378-4270-a908-2a8d1cae5cc0"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("f4f56c06-5882-4aa0-a8cb-dd7d2a4480f1"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("66f39627-5e2e-48ad-af6b-75a14f177e73"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("6a979339-b49d-441c-a53a-2535c989b83a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("15f685a5-b5db-415f-9c1b-9bcb3c734a76"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("26caec2c-0507-40b2-a1dd-3a9f892b0536"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("781e86ba-7304-4d83-88f1-24f192a7ac92"));

            migrationBuilder.DropColumn(
                name: "InitialBalanceDate",
                schema: "finances",
                table: "Accounts");

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("2dcda07e-311d-4637-a308-bb2541c4b878"), "TYPE-SAVS", new DateTime(2026, 4, 1, 20, 50, 13, 611, DateTimeKind.Utc).AddTicks(3571), "Savings" },
                    { new Guid("3ce85e02-d6dd-4424-b343-fa1ca6a231da"), "TYPE-DEBT", new DateTime(2026, 4, 1, 20, 50, 13, 611, DateTimeKind.Utc).AddTicks(3568), "Debit" },
                    { new Guid("50524f59-8f96-46cb-a73e-f6c83144e6bc"), "TYPE-INVS", new DateTime(2026, 4, 1, 20, 50, 13, 611, DateTimeKind.Utc).AddTicks(3572), "Investment" },
                    { new Guid("dd9ce612-709f-41a2-8033-3d6836daf34d"), "TYPE-CASH", new DateTime(2026, 4, 1, 20, 50, 13, 611, DateTimeKind.Utc).AddTicks(3565), "Cash" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "ImgURL", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("099a8e9f-3628-4525-87a3-48a31bdf9305"), "BANPRO", new DateTime(2026, 4, 1, 20, 50, 13, 611, DateTimeKind.Utc).AddTicks(2553), "", "BANCO DE LA PRODUCCION", 2.0 },
                    { new Guid("52ff2950-1a10-438b-80b3-da79cdbddd14"), "BAC", new DateTime(2026, 4, 1, 20, 50, 13, 611, DateTimeKind.Utc).AddTicks(2549), "", "BANCO DE AMERICA", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("4f5f5f4d-4714-44f4-a8bd-c465df951ff9"), "EUR", new DateTime(2026, 4, 1, 20, 50, 13, 611, DateTimeKind.Utc).AddTicks(2949), "Euro", "€" },
                    { new Guid("d1205734-d91f-40a6-a2b3-5ff2e9dbfd14"), "NIO", new DateTime(2026, 4, 1, 20, 50, 13, 611, DateTimeKind.Utc).AddTicks(2948), "Cordoba Nicaraguense", "C$" },
                    { new Guid("e8395fef-dad2-483b-a6d0-ca476eac6d44"), "USD", new DateTime(2026, 4, 1, 20, 50, 13, 611, DateTimeKind.Utc).AddTicks(2944), "Dolar Estadounidense", "$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("683cc291-045f-4d2b-a153-7bd6cf699efc"), "E", new DateTime(2026, 4, 1, 20, 50, 13, 611, DateTimeKind.Utc).AddTicks(2105), "Expenses" },
                    { new Guid("91337c3c-b9af-4955-ace1-f8a03f71281b"), "I", new DateTime(2026, 4, 1, 20, 50, 13, 611, DateTimeKind.Utc).AddTicks(2099), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("129b7caf-cd5a-4f22-979b-b7dfe39a1d3e"), "INC", new DateTime(2026, 4, 1, 20, 50, 13, 611, DateTimeKind.Utc).AddTicks(3306), 0, "Income" },
                    { new Guid("15846a9d-e406-45ef-8481-cf377178f42c"), "TRF", new DateTime(2026, 4, 1, 20, 50, 13, 611, DateTimeKind.Utc).AddTicks(3310), 0, "Transfers" },
                    { new Guid("76619d10-5301-47ea-89b2-63edc1cfb6d0"), "EXP", new DateTime(2026, 4, 1, 20, 50, 13, 611, DateTimeKind.Utc).AddTicks(3309), 0, "Expenses" }
                });
        }
    }
}
