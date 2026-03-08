using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingchages_on_bdl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("48655d3d-b067-4e72-b7e2-61b550e0272d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("7f9ec86c-76e0-4d74-939e-f67bb2f762e5"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("904b96c4-da75-48b9-8c15-4d92bd881d33"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("e6344674-1ab4-46e7-92d4-cab68ff01ab3"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("96cfcbd5-e0dc-4f30-9e91-9bbead7454d8"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("b9be95cb-da31-4846-9297-6ef97010f7f4"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("50df7448-0659-4520-a6fa-7605abf38030"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("7f7d602b-95a3-400f-9b56-c8e419373931"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("d540e523-2c02-438e-9d02-cb57737afbe0"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("a99f95b3-7afe-49e1-9d03-faa35f5609f7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("e5776a01-1bf9-4c49-897e-e353bc3a285a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("32ac9330-eaaf-4ecf-9821-a4c2f5d58e36"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("46824e6b-760c-40f6-a886-e5d6ad98b3b7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("8e56b0dc-30b7-440d-969e-3105375280a2"));

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("35de156d-eaff-4eb1-bfde-b9e93d81b31c"), "TYPE-DEBT", new DateTime(2026, 3, 8, 22, 20, 1, 8, DateTimeKind.Utc).AddTicks(1656), "Debit" },
                    { new Guid("92987e45-bdcc-4151-ad5e-7a595229d89a"), "TYPE-SAVS", new DateTime(2026, 3, 8, 22, 20, 1, 8, DateTimeKind.Utc).AddTicks(1657), "Savings" },
                    { new Guid("c1d93685-ea45-45b0-a3bb-bd1b8a4f8e0b"), "TYPE-INVS", new DateTime(2026, 3, 8, 22, 20, 1, 8, DateTimeKind.Utc).AddTicks(1657), "Investment" },
                    { new Guid("c56de00f-3139-441a-ab8b-9f16d25d506c"), "TYPE-CASH", new DateTime(2026, 3, 8, 22, 20, 1, 8, DateTimeKind.Utc).AddTicks(1654), "Cash" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankID", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("142cfb66-0f58-41eb-a535-d80d09a21806"), "BANPRO", new DateTime(2026, 3, 8, 22, 20, 1, 8, DateTimeKind.Utc).AddTicks(701), "BANCO DE LA PRODUCCION", 2.0 },
                    { new Guid("7f2b947c-76bb-45a0-81a9-78842e7c995f"), "BAC", new DateTime(2026, 3, 8, 22, 20, 1, 8, DateTimeKind.Utc).AddTicks(694), "BANCO DE AMERICA", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyID", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("236534a7-b8a3-48e5-b665-61356eeffb62"), "USD", new DateTime(2026, 3, 8, 22, 20, 1, 8, DateTimeKind.Utc).AddTicks(1017), "Dolar Estadounidense", "$" },
                    { new Guid("b30f6f5e-21cf-4024-90bc-0ce29c7d67a1"), "EUR", new DateTime(2026, 3, 8, 22, 20, 1, 8, DateTimeKind.Utc).AddTicks(1027), "Euro", "€" },
                    { new Guid("f387f1bf-2189-42a7-b345-e469db0d29ad"), "NIO", new DateTime(2026, 3, 8, 22, 20, 1, 8, DateTimeKind.Utc).AddTicks(1020), "Cordoba Nicaraguense", "C$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0c3337dd-711d-4a42-a8aa-7cb11ea2a747"), "E", new DateTime(2026, 3, 8, 22, 20, 1, 8, DateTimeKind.Utc).AddTicks(394), "Expenses" },
                    { new Guid("1be08026-88e6-405d-9ff9-715660f50eba"), "I", new DateTime(2026, 3, 8, 22, 20, 1, 8, DateTimeKind.Utc).AddTicks(391), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeID", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("3f75f246-2ae7-448a-9227-7e9482ef2d21"), "TRF", new DateTime(2026, 3, 8, 22, 20, 1, 8, DateTimeKind.Utc).AddTicks(1371), 0, "Transfers" },
                    { new Guid("83373eff-bc29-4d39-8d82-b76d1d770f6b"), "EXP", new DateTime(2026, 3, 8, 22, 20, 1, 8, DateTimeKind.Utc).AddTicks(1370), 0, "Expenses" },
                    { new Guid("ae9a3009-ce0a-429a-9f78-08d3fac3e960"), "INC", new DateTime(2026, 3, 8, 22, 20, 1, 8, DateTimeKind.Utc).AddTicks(1367), 0, "Income" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "Users",
                columns: new[] { "UserID", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "TwoFactorEnabled", "Username" },
                values: new object[] { new Guid("bbee2139-5bd2-4cd3-afa3-0eec02225aae"), new DateTime(2026, 3, 8, 22, 20, 1, 7, DateTimeKind.Utc).AddTicks(9998), "edwincruz130691@gmail.com", "Edwin Cruz", true, "edwincruz130691@gmail.com", false, "Egeminis13" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("35de156d-eaff-4eb1-bfde-b9e93d81b31c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("92987e45-bdcc-4151-ad5e-7a595229d89a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("c1d93685-ea45-45b0-a3bb-bd1b8a4f8e0b"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("c56de00f-3139-441a-ab8b-9f16d25d506c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("142cfb66-0f58-41eb-a535-d80d09a21806"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("7f2b947c-76bb-45a0-81a9-78842e7c995f"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("236534a7-b8a3-48e5-b665-61356eeffb62"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("b30f6f5e-21cf-4024-90bc-0ce29c7d67a1"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("f387f1bf-2189-42a7-b345-e469db0d29ad"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("0c3337dd-711d-4a42-a8aa-7cb11ea2a747"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("1be08026-88e6-405d-9ff9-715660f50eba"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("3f75f246-2ae7-448a-9227-7e9482ef2d21"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("83373eff-bc29-4d39-8d82-b76d1d770f6b"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("ae9a3009-ce0a-429a-9f78-08d3fac3e960"));

            migrationBuilder.DeleteData(
                schema: "users",
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("bbee2139-5bd2-4cd3-afa3-0eec02225aae"));

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("48655d3d-b067-4e72-b7e2-61b550e0272d"), "TYPE-CASH", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(9038), "Cash" },
                    { new Guid("7f9ec86c-76e0-4d74-939e-f67bb2f762e5"), "TYPE-DEBT", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(9041), "Debit" },
                    { new Guid("904b96c4-da75-48b9-8c15-4d92bd881d33"), "TYPE-INVS", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(9042), "Investment" },
                    { new Guid("e6344674-1ab4-46e7-92d4-cab68ff01ab3"), "TYPE-SAVS", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(9042), "Savings" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankID", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("96cfcbd5-e0dc-4f30-9e91-9bbead7454d8"), "BAC", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8044), "BANCO DE AMERICA", 2.0 },
                    { new Guid("b9be95cb-da31-4846-9297-6ef97010f7f4"), "BANPRO", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8047), "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyID", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("50df7448-0659-4520-a6fa-7605abf38030"), "NIO", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8397), "Cordoba Nicaraguense", "C$" },
                    { new Guid("7f7d602b-95a3-400f-9b56-c8e419373931"), "EUR", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8398), "Euro", "€" },
                    { new Guid("d540e523-2c02-438e-9d02-cb57737afbe0"), "USD", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8394), "Dolar Estadounidense", "$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("a99f95b3-7afe-49e1-9d03-faa35f5609f7"), "E", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(7631), "Expenses" },
                    { new Guid("e5776a01-1bf9-4c49-897e-e353bc3a285a"), "I", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(7627), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeID", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("32ac9330-eaaf-4ecf-9821-a4c2f5d58e36"), "EXP", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8746), 0, "Expenses" },
                    { new Guid("46824e6b-760c-40f6-a886-e5d6ad98b3b7"), "TRF", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8747), 0, "Transfers" },
                    { new Guid("8e56b0dc-30b7-440d-969e-3105375280a2"), "INC", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8740), 0, "Income" }
                });
        }
    }
}
