using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initalMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("33bd93c8-eec4-406a-9619-ff3af41bc799"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("99f16153-db8f-412e-a7af-437fe62c7f33"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("b1528456-c62e-4b45-9aa2-f9b8fa22bca0"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("e635117b-0551-40fa-afb1-5389cfcd3ce2"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("8ec5eab9-3a72-40b2-8746-b94e2ab3579b"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("90d3db5d-0c60-462a-93c9-3f0bd46c642f"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("1424bdef-0fa6-4e2a-a52b-c6d43d25df96"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("31e3c165-a6c6-4f84-9f6a-2159ba16a29e"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("f39e961f-b290-4559-91e5-2fe47e4e424d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("4477b269-2acb-4649-9560-e9d09191b588"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("b1153a41-286d-4fe7-88da-1604d4ddf8ec"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("1419b9a1-8119-418e-a679-90f4794b9b06"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("959bde9a-1eb6-47e0-9977-fb10ebd606a4"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("b158f17a-9967-434f-b5a9-3c499688966e"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("33bd93c8-eec4-406a-9619-ff3af41bc799"), "TYPE-DEBT", new DateTime(2026, 3, 16, 12, 16, 55, 0, DateTimeKind.Utc).AddTicks(5474), "Debit" },
                    { new Guid("99f16153-db8f-412e-a7af-437fe62c7f33"), "TYPE-CASH", new DateTime(2026, 3, 16, 12, 16, 55, 0, DateTimeKind.Utc).AddTicks(5471), "Cash" },
                    { new Guid("b1528456-c62e-4b45-9aa2-f9b8fa22bca0"), "TYPE-INVS", new DateTime(2026, 3, 16, 12, 16, 55, 0, DateTimeKind.Utc).AddTicks(5476), "Investment" },
                    { new Guid("e635117b-0551-40fa-afb1-5389cfcd3ce2"), "TYPE-SAVS", new DateTime(2026, 3, 16, 12, 16, 55, 0, DateTimeKind.Utc).AddTicks(5475), "Savings" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("8ec5eab9-3a72-40b2-8746-b94e2ab3579b"), "BAC", new DateTime(2026, 3, 16, 12, 16, 55, 0, DateTimeKind.Utc).AddTicks(3986), "BANCO DE AMERICA", 2.0 },
                    { new Guid("90d3db5d-0c60-462a-93c9-3f0bd46c642f"), "BANPRO", new DateTime(2026, 3, 16, 12, 16, 55, 0, DateTimeKind.Utc).AddTicks(3989), "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("1424bdef-0fa6-4e2a-a52b-c6d43d25df96"), "NIO", new DateTime(2026, 3, 16, 12, 16, 55, 0, DateTimeKind.Utc).AddTicks(4492), "Cordoba Nicaraguense", "C$" },
                    { new Guid("31e3c165-a6c6-4f84-9f6a-2159ba16a29e"), "USD", new DateTime(2026, 3, 16, 12, 16, 55, 0, DateTimeKind.Utc).AddTicks(4489), "Dolar Estadounidense", "$" },
                    { new Guid("f39e961f-b290-4559-91e5-2fe47e4e424d"), "EUR", new DateTime(2026, 3, 16, 12, 16, 55, 0, DateTimeKind.Utc).AddTicks(4493), "Euro", "€" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("4477b269-2acb-4649-9560-e9d09191b588"), "I", new DateTime(2026, 3, 16, 12, 16, 55, 0, DateTimeKind.Utc).AddTicks(3519), "Income" },
                    { new Guid("b1153a41-286d-4fe7-88da-1604d4ddf8ec"), "E", new DateTime(2026, 3, 16, 12, 16, 55, 0, DateTimeKind.Utc).AddTicks(3523), "Expenses" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("1419b9a1-8119-418e-a679-90f4794b9b06"), "INC", new DateTime(2026, 3, 16, 12, 16, 55, 0, DateTimeKind.Utc).AddTicks(5035), 0, "Income" },
                    { new Guid("959bde9a-1eb6-47e0-9977-fb10ebd606a4"), "TRF", new DateTime(2026, 3, 16, 12, 16, 55, 0, DateTimeKind.Utc).AddTicks(5041), 0, "Transfers" },
                    { new Guid("b158f17a-9967-434f-b5a9-3c499688966e"), "EXP", new DateTime(2026, 3, 16, 12, 16, 55, 0, DateTimeKind.Utc).AddTicks(5039), 0, "Expenses" }
                });
        }
    }
}
