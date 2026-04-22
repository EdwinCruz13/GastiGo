using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adding_exchangeRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            

            
            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                schema: "public",
                columns: table => new
                {
                    ExchangeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    CurrencyFromId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrencyToId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.ExchangeId);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_Currencies_CurrencyFromId",
                        column: x => x.CurrencyFromId,
                        principalSchema: "finances",
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_Currencies_CurrencyToId",
                        column: x => x.CurrencyToId,
                        principalSchema: "finances",
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CurrencyFromId",
                schema: "public",
                table: "ExchangeRates",
                column: "CurrencyFromId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CurrencyToId",
                schema: "public",
                table: "ExchangeRates",
                column: "CurrencyToId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_Date",
                schema: "public",
                table: "ExchangeRates",
                column: "Date",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeRates",
                schema: "public");

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("1107bbf4-8c0e-4647-80e1-8c40eb3666ad"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("41e07a03-c4c1-438b-aac6-ce8b3991749f"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("53ba6012-128e-4566-83f2-762f41375008"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("763d98ce-b7dc-4f41-bdec-e361fad40493"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("5939910f-b8f7-4c51-8861-52dee1276a0d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("5c9dee74-2f95-4e77-892f-62fbf542cfc6"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("3fbc65ec-f626-4b44-a29d-e995c1016229"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("6adaa971-fdcb-422f-a8b9-2c5313b02581"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("ecb0fd68-e5f3-4f28-a826-994fe06452e1"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("adcebae4-6fc0-4402-a701-7a8464b47c51"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("d392c09d-5c3c-43aa-bd2d-2b8012af915c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("0605360e-a698-474b-bc46-063c9401213a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("1856e238-fc7d-4752-b5a8-4a8a1525ded9"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("73b0905f-1ad3-40aa-9939-97c4ea835616"));

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("322ad079-a5a9-4120-a636-0f4773e18a24"), "TYPE-INVS", new DateTime(2026, 4, 14, 15, 58, 13, 704, DateTimeKind.Utc).AddTicks(9013), "Investment" },
                    { new Guid("bdc85b59-0c7b-48b0-a6c5-eb22bdd2733c"), "TYPE-SAVS", new DateTime(2026, 4, 14, 15, 58, 13, 704, DateTimeKind.Utc).AddTicks(9012), "Savings" },
                    { new Guid("d1b86cbb-a883-44d4-b85f-9ccd03e48dec"), "TYPE-DEBT", new DateTime(2026, 4, 14, 15, 58, 13, 704, DateTimeKind.Utc).AddTicks(9011), "Debit" },
                    { new Guid("f3cf84f4-5efb-49b7-a963-9ace0d7d0ce9"), "TYPE-CASH", new DateTime(2026, 4, 14, 15, 58, 13, 704, DateTimeKind.Utc).AddTicks(9010), "Cash" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "ImgURL", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("50385546-2999-418e-b5ae-147817aab6de"), "BANPRO", new DateTime(2026, 4, 14, 15, 58, 13, 704, DateTimeKind.Utc).AddTicks(8478), "", "BANCO DE LA PRODUCCION", 2.0 },
                    { new Guid("81e0a51d-df97-4cd3-8e50-a8cb06e0130a"), "BAC", new DateTime(2026, 4, 14, 15, 58, 13, 704, DateTimeKind.Utc).AddTicks(8474), "", "BANCO DE AMERICA", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("00ec3886-8c60-4874-b457-83919d536b6c"), "USD", new DateTime(2026, 4, 14, 15, 58, 13, 704, DateTimeKind.Utc).AddTicks(8630), "Dolar Estadounidense", "$" },
                    { new Guid("70aa3d87-053d-42f6-80ed-eef306d7dd80"), "EUR", new DateTime(2026, 4, 14, 15, 58, 13, 704, DateTimeKind.Utc).AddTicks(8633), "Euro", "€" },
                    { new Guid("a0bbaeeb-5333-45bc-9884-bd7c6c035941"), "NIO", new DateTime(2026, 4, 14, 15, 58, 13, 704, DateTimeKind.Utc).AddTicks(8633), "Cordoba Nicaraguense", "C$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("1d996037-0250-4692-8d09-60f408688511"), "I", new DateTime(2026, 4, 14, 15, 58, 13, 704, DateTimeKind.Utc).AddTicks(8219), "Income" },
                    { new Guid("4189d9f4-2557-46d1-8275-e5b5057d02e0"), "E", new DateTime(2026, 4, 14, 15, 58, 13, 704, DateTimeKind.Utc).AddTicks(8228), "Expenses" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("2214d4aa-0c3a-441d-bbcf-f109a5018ecd"), "TRF", new DateTime(2026, 4, 14, 15, 58, 13, 704, DateTimeKind.Utc).AddTicks(8844), 0, "Transfers" },
                    { new Guid("4e9a838e-fb70-4a1a-aa9e-ba01dd769d6a"), "EXP", new DateTime(2026, 4, 14, 15, 58, 13, 704, DateTimeKind.Utc).AddTicks(8842), 0, "Expenses" },
                    { new Guid("91ef59fa-9482-43db-95ba-0263d4e59e4d"), "INC", new DateTime(2026, 4, 14, 15, 58, 13, 704, DateTimeKind.Utc).AddTicks(8840), 0, "Income" }
                });
        }
    }
}
