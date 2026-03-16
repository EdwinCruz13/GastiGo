using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adding_eliminatecategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("1dffc8a4-e24f-418f-a9f5-adaccd8455b7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("3dad54c6-e5b1-4b7f-a678-efcc24a73983"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("7413b614-58ae-4922-a22d-840e7bf1d03f"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("f7e7b6f5-6999-4cf3-9ef8-518286f8112f"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("4ee53578-d602-4d37-80f2-9ac964a3e830"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("70bae19e-36f3-4a70-8783-162036b33abe"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("0e9f724a-9dc4-4b50-bf14-fc5263e1a65d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("281a09b0-8a1f-4478-89b9-06a72ca7ee37"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("4b8ba4a4-3497-41ef-bcb7-d0d3cc07fa93"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("59fd5203-4fe3-45e2-ace3-dae0ec68a99c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("e5f74b3c-2db7-447b-947d-640f676a5379"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("5bd8825b-0990-4a15-99ed-f7570513cb77"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("8e27b508-f3cd-4d04-966e-71503a8a9287"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("faa47c1b-c0de-413a-9130-7a18f58b922a"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "finances",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("39910334-952d-47df-8660-aaebda6d8ab2"), "TYPE-INVS", new DateTime(2026, 3, 16, 21, 38, 6, 72, DateTimeKind.Utc).AddTicks(5174), "Investment" },
                    { new Guid("4712c41c-b0be-4bc3-837c-764510b97344"), "TYPE-CASH", new DateTime(2026, 3, 16, 21, 38, 6, 72, DateTimeKind.Utc).AddTicks(5169), "Cash" },
                    { new Guid("a7206696-4fc5-41b3-8964-ff8dd9526ac5"), "TYPE-DEBT", new DateTime(2026, 3, 16, 21, 38, 6, 72, DateTimeKind.Utc).AddTicks(5172), "Debit" },
                    { new Guid("cc7b3f5f-0241-4541-b881-d525ef0d3bb6"), "TYPE-SAVS", new DateTime(2026, 3, 16, 21, 38, 6, 72, DateTimeKind.Utc).AddTicks(5173), "Savings" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("bdcabf75-693b-4f9e-ac67-e363d8f38230"), "BANPRO", new DateTime(2026, 3, 16, 21, 38, 6, 72, DateTimeKind.Utc).AddTicks(4591), "BANCO DE LA PRODUCCION", 2.0 },
                    { new Guid("f8413551-d55b-4455-8556-0e2aceddbe5f"), "BAC", new DateTime(2026, 3, 16, 21, 38, 6, 72, DateTimeKind.Utc).AddTicks(4589), "BANCO DE AMERICA", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("11fafee3-280c-418b-8bc1-8387d49b1bd6"), "EUR", new DateTime(2026, 3, 16, 21, 38, 6, 72, DateTimeKind.Utc).AddTicks(4779), "Euro", "€" },
                    { new Guid("385e35f0-87c3-4182-b514-12dcd0eb3b1a"), "NIO", new DateTime(2026, 3, 16, 21, 38, 6, 72, DateTimeKind.Utc).AddTicks(4778), "Cordoba Nicaraguense", "C$" },
                    { new Guid("691ce7b8-cf85-43e5-a878-185c8bbb50f7"), "USD", new DateTime(2026, 3, 16, 21, 38, 6, 72, DateTimeKind.Utc).AddTicks(4776), "Dolar Estadounidense", "$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("129a7622-4437-42d1-8e16-0e9ce1a65e2e"), "E", new DateTime(2026, 3, 16, 21, 38, 6, 72, DateTimeKind.Utc).AddTicks(4330), "Expenses" },
                    { new Guid("f1e9c692-e70c-4e5c-92a2-6908fe750f39"), "I", new DateTime(2026, 3, 16, 21, 38, 6, 72, DateTimeKind.Utc).AddTicks(4321), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("00381474-72a5-4407-bdbb-1ee93ab80609"), "TRF", new DateTime(2026, 3, 16, 21, 38, 6, 72, DateTimeKind.Utc).AddTicks(5002), 0, "Transfers" },
                    { new Guid("12ff4410-0c87-48c2-8c6e-005ab96ee155"), "EXP", new DateTime(2026, 3, 16, 21, 38, 6, 72, DateTimeKind.Utc).AddTicks(4999), 0, "Expenses" },
                    { new Guid("a819b6f9-51ae-441a-a0ad-d98430da6990"), "INC", new DateTime(2026, 3, 16, 21, 38, 6, 72, DateTimeKind.Utc).AddTicks(4996), 0, "Income" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("39910334-952d-47df-8660-aaebda6d8ab2"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("4712c41c-b0be-4bc3-837c-764510b97344"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("a7206696-4fc5-41b3-8964-ff8dd9526ac5"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("cc7b3f5f-0241-4541-b881-d525ef0d3bb6"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("bdcabf75-693b-4f9e-ac67-e363d8f38230"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("f8413551-d55b-4455-8556-0e2aceddbe5f"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("11fafee3-280c-418b-8bc1-8387d49b1bd6"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("385e35f0-87c3-4182-b514-12dcd0eb3b1a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("691ce7b8-cf85-43e5-a878-185c8bbb50f7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("129a7622-4437-42d1-8e16-0e9ce1a65e2e"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("f1e9c692-e70c-4e5c-92a2-6908fe750f39"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("00381474-72a5-4407-bdbb-1ee93ab80609"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("12ff4410-0c87-48c2-8c6e-005ab96ee155"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("a819b6f9-51ae-441a-a0ad-d98430da6990"));

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "finances",
                table: "Categories");

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("1dffc8a4-e24f-418f-a9f5-adaccd8455b7"), "TYPE-INVS", new DateTime(2026, 3, 16, 21, 2, 46, 586, DateTimeKind.Utc).AddTicks(1797), "Investment" },
                    { new Guid("3dad54c6-e5b1-4b7f-a678-efcc24a73983"), "TYPE-SAVS", new DateTime(2026, 3, 16, 21, 2, 46, 586, DateTimeKind.Utc).AddTicks(1796), "Savings" },
                    { new Guid("7413b614-58ae-4922-a22d-840e7bf1d03f"), "TYPE-DEBT", new DateTime(2026, 3, 16, 21, 2, 46, 586, DateTimeKind.Utc).AddTicks(1795), "Debit" },
                    { new Guid("f7e7b6f5-6999-4cf3-9ef8-518286f8112f"), "TYPE-CASH", new DateTime(2026, 3, 16, 21, 2, 46, 586, DateTimeKind.Utc).AddTicks(1793), "Cash" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("4ee53578-d602-4d37-80f2-9ac964a3e830"), "BAC", new DateTime(2026, 3, 16, 21, 2, 46, 586, DateTimeKind.Utc).AddTicks(1142), "BANCO DE AMERICA", 2.0 },
                    { new Guid("70bae19e-36f3-4a70-8783-162036b33abe"), "BANPRO", new DateTime(2026, 3, 16, 21, 2, 46, 586, DateTimeKind.Utc).AddTicks(1144), "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("0e9f724a-9dc4-4b50-bf14-fc5263e1a65d"), "NIO", new DateTime(2026, 3, 16, 21, 2, 46, 586, DateTimeKind.Utc).AddTicks(1377), "Cordoba Nicaraguense", "C$" },
                    { new Guid("281a09b0-8a1f-4478-89b9-06a72ca7ee37"), "EUR", new DateTime(2026, 3, 16, 21, 2, 46, 586, DateTimeKind.Utc).AddTicks(1378), "Euro", "€" },
                    { new Guid("4b8ba4a4-3497-41ef-bcb7-d0d3cc07fa93"), "USD", new DateTime(2026, 3, 16, 21, 2, 46, 586, DateTimeKind.Utc).AddTicks(1375), "Dolar Estadounidense", "$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("59fd5203-4fe3-45e2-ace3-dae0ec68a99c"), "I", new DateTime(2026, 3, 16, 21, 2, 46, 586, DateTimeKind.Utc).AddTicks(870), "Income" },
                    { new Guid("e5f74b3c-2db7-447b-947d-640f676a5379"), "E", new DateTime(2026, 3, 16, 21, 2, 46, 586, DateTimeKind.Utc).AddTicks(872), "Expenses" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("5bd8825b-0990-4a15-99ed-f7570513cb77"), "EXP", new DateTime(2026, 3, 16, 21, 2, 46, 586, DateTimeKind.Utc).AddTicks(1638), 0, "Expenses" },
                    { new Guid("8e27b508-f3cd-4d04-966e-71503a8a9287"), "TRF", new DateTime(2026, 3, 16, 21, 2, 46, 586, DateTimeKind.Utc).AddTicks(1639), 0, "Transfers" },
                    { new Guid("faa47c1b-c0de-413a-9130-7a18f58b922a"), "INC", new DateTime(2026, 3, 16, 21, 2, 46, 586, DateTimeKind.Utc).AddTicks(1633), 0, "Income" }
                });
        }
    }
}
