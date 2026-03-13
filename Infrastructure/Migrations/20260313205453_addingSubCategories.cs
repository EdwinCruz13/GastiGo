using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingSubCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("5fdf49af-1cdb-4371-a82f-e81ac5ea9ea1"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("7006ffd1-a781-4e63-847e-b84878948fe7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("88e55ca7-3728-4057-b7c5-4c6be03800cc"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("b8047c91-1696-45db-8dcc-b4e6e36c849a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("15ee2ab9-de8a-4a37-8768-e484f59ada08"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("2ae15d4f-519d-4456-8a0f-7eed95e33c1e"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("3a797abe-e99e-48e0-809c-4e3b42d8709c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("492d3d09-bfc7-47bb-a540-82e8e386c0ed"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("77e2eab2-1e7c-44a3-b2b1-83803ec64abd"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("0f0d6731-48b4-4cf6-ac22-3734589b2e0a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("d8083824-04a6-4ba4-8f6a-6a63323bfac6"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("2faf81f5-af9b-4718-8747-66b26b02c840"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("6d0318eb-d2b5-41e3-9e6e-1ceb71e81094"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("9fe172a3-3d70-420b-afc5-9711d56f8839"));

            migrationBuilder.DeleteData(
                schema: "users",
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("97bffb5b-86f4-41a4-879c-5aa8151a72f8"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("5fdf49af-1cdb-4371-a82f-e81ac5ea9ea1"), "TYPE-DEBT", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7982), "Debit" },
                    { new Guid("7006ffd1-a781-4e63-847e-b84878948fe7"), "TYPE-SAVS", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7985), "Savings" },
                    { new Guid("88e55ca7-3728-4057-b7c5-4c6be03800cc"), "TYPE-INVS", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7985), "Investment" },
                    { new Guid("b8047c91-1696-45db-8dcc-b4e6e36c849a"), "TYPE-CASH", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7981), "Cash" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankID", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("15ee2ab9-de8a-4a37-8768-e484f59ada08"), "BAC", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7395), "BANCO DE AMERICA", 2.0 },
                    { new Guid("2ae15d4f-519d-4456-8a0f-7eed95e33c1e"), "BANPRO", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7397), "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyID", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("3a797abe-e99e-48e0-809c-4e3b42d8709c"), "USD", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7612), "Dolar Estadounidense", "$" },
                    { new Guid("492d3d09-bfc7-47bb-a540-82e8e386c0ed"), "EUR", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7617), "Euro", "€" },
                    { new Guid("77e2eab2-1e7c-44a3-b2b1-83803ec64abd"), "NIO", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7616), "Cordoba Nicaraguense", "C$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0f0d6731-48b4-4cf6-ac22-3734589b2e0a"), "E", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7186), "Expenses" },
                    { new Guid("d8083824-04a6-4ba4-8f6a-6a63323bfac6"), "I", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7184), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeID", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("2faf81f5-af9b-4718-8747-66b26b02c840"), "EXP", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7832), 0, "Expenses" },
                    { new Guid("6d0318eb-d2b5-41e3-9e6e-1ceb71e81094"), "TRF", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7833), 0, "Transfers" },
                    { new Guid("9fe172a3-3d70-420b-afc5-9711d56f8839"), "INC", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7830), 0, "Income" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "Users",
                columns: new[] { "UserID", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "TwoFactorEnabled", "Username" },
                values: new object[] { new Guid("97bffb5b-86f4-41a4-879c-5aa8151a72f8"), new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(6960), "edwincruz130691@gmail.com", "Edwin Cruz", true, "edwincruz130691@gmail.com", false, "Egeminis13" });
        }
    }
}
