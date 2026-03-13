using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingSubCategories2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("27c71d81-227c-451b-95c6-3c3a312b35d4"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("49468a3b-e2b1-4463-b04e-92eecd825612"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("eedc5c08-0048-44ce-993c-03597cecc30e"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("f9dda45b-ac84-45a4-bf73-5647941b92d4"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("db4a277f-3da4-47da-a91a-4fff6cca4e0f"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("fbcb44f9-d708-4a21-be67-2c747e8835a7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("98986db5-5391-4e67-87c4-306fa62eeaf3"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("b217e6cf-2f3a-4545-a15b-ffd85640c98d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("d85081a4-7224-4176-bfa6-01090b3f915c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("5f4ec114-39ed-4c01-85d5-4f9257f2e54c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("6221325d-8112-4fae-b553-a2fc8b7554ba"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("762c974b-5e8c-471e-b44f-d8826496df54"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("9dabdb42-b0a8-4c6f-a3d5-74bcc266f649"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("a82e4bb7-b706-4898-8c76-8ac05ce9462b"));

            migrationBuilder.DeleteData(
                schema: "users",
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("f2719a24-d50e-4316-8534-450d24aee1c8"));

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("15dbf584-5b68-4c29-98da-70705ca520b7"), "TYPE-SAVS", new DateTime(2026, 3, 13, 20, 59, 38, 318, DateTimeKind.Utc).AddTicks(5747), "Savings" },
                    { new Guid("31f6544c-6a13-4878-b672-7eae04271595"), "TYPE-DEBT", new DateTime(2026, 3, 13, 20, 59, 38, 318, DateTimeKind.Utc).AddTicks(5746), "Debit" },
                    { new Guid("35f4c670-de73-4866-afdb-daaf6079ef9d"), "TYPE-INVS", new DateTime(2026, 3, 13, 20, 59, 38, 318, DateTimeKind.Utc).AddTicks(5748), "Investment" },
                    { new Guid("5395837f-c426-434f-b950-9b7bf2754704"), "TYPE-CASH", new DateTime(2026, 3, 13, 20, 59, 38, 318, DateTimeKind.Utc).AddTicks(5745), "Cash" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankID", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("4e5a2179-7383-47dd-a978-fbf1abe8a2a5"), "BAC", new DateTime(2026, 3, 13, 20, 59, 38, 318, DateTimeKind.Utc).AddTicks(4964), "BANCO DE AMERICA", 2.0 },
                    { new Guid("fca0f5fe-cde2-40fd-8e25-cd4538fc4350"), "BANPRO", new DateTime(2026, 3, 13, 20, 59, 38, 318, DateTimeKind.Utc).AddTicks(4967), "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyID", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("146ad874-e132-499d-8948-be91a95743ef"), "USD", new DateTime(2026, 3, 13, 20, 59, 38, 318, DateTimeKind.Utc).AddTicks(5331), "Dolar Estadounidense", "$" },
                    { new Guid("2d36344f-1b19-4db4-a5aa-422e6968765d"), "EUR", new DateTime(2026, 3, 13, 20, 59, 38, 318, DateTimeKind.Utc).AddTicks(5334), "Euro", "€" },
                    { new Guid("a635f437-95cf-4bf2-aba7-9601ab24071d"), "NIO", new DateTime(2026, 3, 13, 20, 59, 38, 318, DateTimeKind.Utc).AddTicks(5333), "Cordoba Nicaraguense", "C$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0d9de40d-3804-4482-9fce-3c40ef4d3589"), "E", new DateTime(2026, 3, 13, 20, 59, 38, 318, DateTimeKind.Utc).AddTicks(4784), "Expenses" },
                    { new Guid("46a81ff7-09c8-4046-baf4-3c0dbaedd313"), "I", new DateTime(2026, 3, 13, 20, 59, 38, 318, DateTimeKind.Utc).AddTicks(4782), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeID", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("213797bc-94f4-47c1-bb50-56376550d5c5"), "INC", new DateTime(2026, 3, 13, 20, 59, 38, 318, DateTimeKind.Utc).AddTicks(5588), 0, "Income" },
                    { new Guid("41fdfe67-558d-466a-a91a-eeefe392a301"), "EXP", new DateTime(2026, 3, 13, 20, 59, 38, 318, DateTimeKind.Utc).AddTicks(5589), 0, "Expenses" },
                    { new Guid("7f42a917-971a-402e-bbd5-9214b8c1e343"), "TRF", new DateTime(2026, 3, 13, 20, 59, 38, 318, DateTimeKind.Utc).AddTicks(5590), 0, "Transfers" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "Users",
                columns: new[] { "UserID", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "TwoFactorEnabled", "Username" },
                values: new object[] { new Guid("3ffd23ee-2648-474a-944f-c36ee4b285a2"), new DateTime(2026, 3, 13, 20, 59, 38, 318, DateTimeKind.Utc).AddTicks(4582), "edwincruz130691@gmail.com", "Edwin Cruz", true, "\"c01327ed-2392-44d1-9b07-a8ec5cc577d1\"	\"edwincruz130691@gmail.com\"	\"Egeminis13\"	\"$2a$11$tqIgYefCWFeqdPFhHnszde9uqY9SwUCb2V8w5CWk5OCDsVp20XeQi\"	\"Edwin Cruz\"	true	false	\"2026-03-12 08:51:55.333466-06\"", false, "Egeminis13" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("15dbf584-5b68-4c29-98da-70705ca520b7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("31f6544c-6a13-4878-b672-7eae04271595"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("35f4c670-de73-4866-afdb-daaf6079ef9d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("5395837f-c426-434f-b950-9b7bf2754704"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("4e5a2179-7383-47dd-a978-fbf1abe8a2a5"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("fca0f5fe-cde2-40fd-8e25-cd4538fc4350"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("146ad874-e132-499d-8948-be91a95743ef"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("2d36344f-1b19-4db4-a5aa-422e6968765d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("a635f437-95cf-4bf2-aba7-9601ab24071d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("0d9de40d-3804-4482-9fce-3c40ef4d3589"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("46a81ff7-09c8-4046-baf4-3c0dbaedd313"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("213797bc-94f4-47c1-bb50-56376550d5c5"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("41fdfe67-558d-466a-a91a-eeefe392a301"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("7f42a917-971a-402e-bbd5-9214b8c1e343"));

            migrationBuilder.DeleteData(
                schema: "users",
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("3ffd23ee-2648-474a-944f-c36ee4b285a2"));

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("27c71d81-227c-451b-95c6-3c3a312b35d4"), "TYPE-CASH", new DateTime(2026, 3, 13, 20, 54, 52, 840, DateTimeKind.Utc).AddTicks(5414), "Cash" },
                    { new Guid("49468a3b-e2b1-4463-b04e-92eecd825612"), "TYPE-INVS", new DateTime(2026, 3, 13, 20, 54, 52, 840, DateTimeKind.Utc).AddTicks(5418), "Investment" },
                    { new Guid("eedc5c08-0048-44ce-993c-03597cecc30e"), "TYPE-DEBT", new DateTime(2026, 3, 13, 20, 54, 52, 840, DateTimeKind.Utc).AddTicks(5417), "Debit" },
                    { new Guid("f9dda45b-ac84-45a4-bf73-5647941b92d4"), "TYPE-SAVS", new DateTime(2026, 3, 13, 20, 54, 52, 840, DateTimeKind.Utc).AddTicks(5418), "Savings" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankID", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("db4a277f-3da4-47da-a91a-4fff6cca4e0f"), "BAC", new DateTime(2026, 3, 13, 20, 54, 52, 840, DateTimeKind.Utc).AddTicks(4781), "BANCO DE AMERICA", 2.0 },
                    { new Guid("fbcb44f9-d708-4a21-be67-2c747e8835a7"), "BANPRO", new DateTime(2026, 3, 13, 20, 54, 52, 840, DateTimeKind.Utc).AddTicks(4782), "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyID", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("98986db5-5391-4e67-87c4-306fa62eeaf3"), "EUR", new DateTime(2026, 3, 13, 20, 54, 52, 840, DateTimeKind.Utc).AddTicks(5017), "Euro", "€" },
                    { new Guid("b217e6cf-2f3a-4545-a15b-ffd85640c98d"), "USD", new DateTime(2026, 3, 13, 20, 54, 52, 840, DateTimeKind.Utc).AddTicks(5010), "Dolar Estadounidense", "$" },
                    { new Guid("d85081a4-7224-4176-bfa6-01090b3f915c"), "NIO", new DateTime(2026, 3, 13, 20, 54, 52, 840, DateTimeKind.Utc).AddTicks(5011), "Cordoba Nicaraguense", "C$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("5f4ec114-39ed-4c01-85d5-4f9257f2e54c"), "E", new DateTime(2026, 3, 13, 20, 54, 52, 840, DateTimeKind.Utc).AddTicks(4590), "Expenses" },
                    { new Guid("6221325d-8112-4fae-b553-a2fc8b7554ba"), "I", new DateTime(2026, 3, 13, 20, 54, 52, 840, DateTimeKind.Utc).AddTicks(4588), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeID", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("762c974b-5e8c-471e-b44f-d8826496df54"), "TRF", new DateTime(2026, 3, 13, 20, 54, 52, 840, DateTimeKind.Utc).AddTicks(5244), 0, "Transfers" },
                    { new Guid("9dabdb42-b0a8-4c6f-a3d5-74bcc266f649"), "EXP", new DateTime(2026, 3, 13, 20, 54, 52, 840, DateTimeKind.Utc).AddTicks(5243), 0, "Expenses" },
                    { new Guid("a82e4bb7-b706-4898-8c76-8ac05ce9462b"), "INC", new DateTime(2026, 3, 13, 20, 54, 52, 840, DateTimeKind.Utc).AddTicks(5239), 0, "Income" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "Users",
                columns: new[] { "UserID", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "TwoFactorEnabled", "Username" },
                values: new object[] { new Guid("f2719a24-d50e-4316-8534-450d24aee1c8"), new DateTime(2026, 3, 13, 20, 54, 52, 840, DateTimeKind.Utc).AddTicks(4357), "edwincruz130691@gmail.com", "Edwin Cruz", true, "edwincruz130691@gmail.com", false, "Egeminis13" });
        }
    }
}
