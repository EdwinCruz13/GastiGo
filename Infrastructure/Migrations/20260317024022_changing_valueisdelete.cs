using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changing_valueisdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("0e5800c8-4882-4eef-84ed-96305450570a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("47fd212f-5f07-4301-b1ad-f33871044849"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("e28d8af3-39a3-4fcc-8c45-7fb32c513b14"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("fcc8cdbb-8038-45f5-906c-73e78160d694"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("56f4f79a-7428-4cf6-9489-2088168eeef7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("fcaaec2d-d637-487c-a58c-33c486bcb8a9"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("08fdb445-5c5b-4dcd-9a1d-dac9b7c36094"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("508ca153-52fd-4aba-86f0-d59779da040f"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("d6324472-a6b1-4565-aade-4a813f1f5d2d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("75a985ef-bf1f-4fe5-b214-65668194cbcd"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("80d5d2e9-8bd1-44ef-b4d1-e4a3bdabfbb1"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("3480874b-ed5b-4d4d-9e9e-36c423112885"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("58c23610-132c-44dd-a2c6-2a8baa86c406"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("bf77d6b2-8814-4a14-9da2-1ed632c1c323"));

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                schema: "finances",
                table: "Categories",
                newName: "isActive");

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("525b035a-96b9-4764-9668-f99d3968ba5b"), "TYPE-SAVS", new DateTime(2026, 3, 17, 2, 40, 22, 546, DateTimeKind.Utc).AddTicks(6043), "Savings" },
                    { new Guid("990c9c2a-fade-46b4-bb6c-5f0091ba36e8"), "TYPE-DEBT", new DateTime(2026, 3, 17, 2, 40, 22, 546, DateTimeKind.Utc).AddTicks(6041), "Debit" },
                    { new Guid("c32caac8-dd61-48ec-be0e-31b0d70fdf6e"), "TYPE-INVS", new DateTime(2026, 3, 17, 2, 40, 22, 546, DateTimeKind.Utc).AddTicks(6044), "Investment" },
                    { new Guid("ca78ffaa-834e-48d1-b1ea-9162527f6a26"), "TYPE-CASH", new DateTime(2026, 3, 17, 2, 40, 22, 546, DateTimeKind.Utc).AddTicks(6034), "Cash" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("19bd6407-2608-4c57-9cb8-6b3bfc01de2d"), "BANPRO", new DateTime(2026, 3, 17, 2, 40, 22, 546, DateTimeKind.Utc).AddTicks(3507), "BANCO DE LA PRODUCCION", 2.0 },
                    { new Guid("51dd1d04-2f24-414e-b0d1-4456a42baaa8"), "BAC", new DateTime(2026, 3, 17, 2, 40, 22, 546, DateTimeKind.Utc).AddTicks(3503), "BANCO DE AMERICA", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("4bc9f34d-0b50-49d3-b2f2-b3221ae1e699"), "EUR", new DateTime(2026, 3, 17, 2, 40, 22, 546, DateTimeKind.Utc).AddTicks(4064), "Euro", "€" },
                    { new Guid("7f8e3870-d0a2-468f-b606-be41472edfc7"), "USD", new DateTime(2026, 3, 17, 2, 40, 22, 546, DateTimeKind.Utc).AddTicks(4051), "Dolar Estadounidense", "$" },
                    { new Guid("eb3b8f8f-dc19-4ee3-a4a3-315958b3d138"), "NIO", new DateTime(2026, 3, 17, 2, 40, 22, 546, DateTimeKind.Utc).AddTicks(4057), "Cordoba Nicaraguense", "C$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("23722892-10b3-40b0-b487-43227ff5bace"), "E", new DateTime(2026, 3, 17, 2, 40, 22, 546, DateTimeKind.Utc).AddTicks(2952), "Expenses" },
                    { new Guid("d8be0413-85ba-48ac-a278-152c6eaa7e0a"), "I", new DateTime(2026, 3, 17, 2, 40, 22, 546, DateTimeKind.Utc).AddTicks(2948), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("09a4a5d1-3dea-40fa-bde0-91caaf1b5eea"), "EXP", new DateTime(2026, 3, 17, 2, 40, 22, 546, DateTimeKind.Utc).AddTicks(5470), 0, "Expenses" },
                    { new Guid("2b971ff6-5cab-4f48-9858-8efa9d3d8f09"), "TRF", new DateTime(2026, 3, 17, 2, 40, 22, 546, DateTimeKind.Utc).AddTicks(5472), 0, "Transfers" },
                    { new Guid("654a18ea-be56-412f-9f8c-97c77d89eb8a"), "INC", new DateTime(2026, 3, 17, 2, 40, 22, 546, DateTimeKind.Utc).AddTicks(5465), 0, "Income" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("525b035a-96b9-4764-9668-f99d3968ba5b"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("990c9c2a-fade-46b4-bb6c-5f0091ba36e8"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("c32caac8-dd61-48ec-be0e-31b0d70fdf6e"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("ca78ffaa-834e-48d1-b1ea-9162527f6a26"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("19bd6407-2608-4c57-9cb8-6b3bfc01de2d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("51dd1d04-2f24-414e-b0d1-4456a42baaa8"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("4bc9f34d-0b50-49d3-b2f2-b3221ae1e699"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("7f8e3870-d0a2-468f-b606-be41472edfc7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("eb3b8f8f-dc19-4ee3-a4a3-315958b3d138"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("23722892-10b3-40b0-b487-43227ff5bace"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("d8be0413-85ba-48ac-a278-152c6eaa7e0a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("09a4a5d1-3dea-40fa-bde0-91caaf1b5eea"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("2b971ff6-5cab-4f48-9858-8efa9d3d8f09"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("654a18ea-be56-412f-9f8c-97c77d89eb8a"));

            migrationBuilder.RenameColumn(
                name: "isActive",
                schema: "finances",
                table: "Categories",
                newName: "IsDeleted");

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0e5800c8-4882-4eef-84ed-96305450570a"), "TYPE-SAVS", new DateTime(2026, 3, 17, 2, 12, 8, 730, DateTimeKind.Utc).AddTicks(3381), "Savings" },
                    { new Guid("47fd212f-5f07-4301-b1ad-f33871044849"), "TYPE-INVS", new DateTime(2026, 3, 17, 2, 12, 8, 730, DateTimeKind.Utc).AddTicks(3385), "Investment" },
                    { new Guid("e28d8af3-39a3-4fcc-8c45-7fb32c513b14"), "TYPE-CASH", new DateTime(2026, 3, 17, 2, 12, 8, 730, DateTimeKind.Utc).AddTicks(3377), "Cash" },
                    { new Guid("fcc8cdbb-8038-45f5-906c-73e78160d694"), "TYPE-DEBT", new DateTime(2026, 3, 17, 2, 12, 8, 730, DateTimeKind.Utc).AddTicks(3380), "Debit" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("56f4f79a-7428-4cf6-9489-2088168eeef7"), "BANPRO", new DateTime(2026, 3, 17, 2, 12, 8, 730, DateTimeKind.Utc).AddTicks(2312), "BANCO DE LA PRODUCCION", 2.0 },
                    { new Guid("fcaaec2d-d637-487c-a58c-33c486bcb8a9"), "BAC", new DateTime(2026, 3, 17, 2, 12, 8, 730, DateTimeKind.Utc).AddTicks(2308), "BANCO DE AMERICA", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("08fdb445-5c5b-4dcd-9a1d-dac9b7c36094"), "USD", new DateTime(2026, 3, 17, 2, 12, 8, 730, DateTimeKind.Utc).AddTicks(2652), "Dolar Estadounidense", "$" },
                    { new Guid("508ca153-52fd-4aba-86f0-d59779da040f"), "NIO", new DateTime(2026, 3, 17, 2, 12, 8, 730, DateTimeKind.Utc).AddTicks(2663), "Cordoba Nicaraguense", "C$" },
                    { new Guid("d6324472-a6b1-4565-aade-4a813f1f5d2d"), "EUR", new DateTime(2026, 3, 17, 2, 12, 8, 730, DateTimeKind.Utc).AddTicks(2664), "Euro", "€" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("75a985ef-bf1f-4fe5-b214-65668194cbcd"), "I", new DateTime(2026, 3, 17, 2, 12, 8, 730, DateTimeKind.Utc).AddTicks(1950), "Income" },
                    { new Guid("80d5d2e9-8bd1-44ef-b4d1-e4a3bdabfbb1"), "E", new DateTime(2026, 3, 17, 2, 12, 8, 730, DateTimeKind.Utc).AddTicks(1953), "Expenses" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("3480874b-ed5b-4d4d-9e9e-36c423112885"), "EXP", new DateTime(2026, 3, 17, 2, 12, 8, 730, DateTimeKind.Utc).AddTicks(3077), 0, "Expenses" },
                    { new Guid("58c23610-132c-44dd-a2c6-2a8baa86c406"), "TRF", new DateTime(2026, 3, 17, 2, 12, 8, 730, DateTimeKind.Utc).AddTicks(3078), 0, "Transfers" },
                    { new Guid("bf77d6b2-8814-4a14-9da2-1ed632c1c323"), "INC", new DateTime(2026, 3, 17, 2, 12, 8, 730, DateTimeKind.Utc).AddTicks(3073), 0, "Income" }
                });
        }
    }
}
