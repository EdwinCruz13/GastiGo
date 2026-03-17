using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initaignagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
