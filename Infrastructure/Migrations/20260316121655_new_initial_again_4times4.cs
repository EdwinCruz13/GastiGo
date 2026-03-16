using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new_initial_again_4times4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("039f1e70-0699-4ff5-83ee-5dc373a514cd"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("28089ecd-e7dc-439e-9e74-dc12b6d0adf1"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("6c1021dd-d156-4698-92d7-43b28a3c5252"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("bb11dc74-1e50-459f-bfdb-8caca9e1f39e"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("221a1b17-c281-4b92-94ea-716be28509bc"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("d4439eac-6e85-4453-a185-4085d0ae1422"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("8b8915c6-dcb9-476a-852c-c5d302a6a468"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("b9a9f974-a217-4bac-912f-a28dec574ef3"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("bc01ffc7-c7a5-4c4c-8181-f4179da8acd1"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("3a3986e0-4054-48a0-a65c-42e25b174009"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("e34d3e16-9b3b-45e8-b121-8737fb05b808"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("56e9d347-931e-4608-b8fa-a9d7b88f713c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("903813e3-80ee-4be0-9c0a-49568421da8f"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("960f529a-ff29-4a1e-9eab-35023509c815"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("039f1e70-0699-4ff5-83ee-5dc373a514cd"), "TYPE-CASH", new DateTime(2026, 3, 16, 5, 30, 33, 158, DateTimeKind.Utc).AddTicks(7391), "Cash" },
                    { new Guid("28089ecd-e7dc-439e-9e74-dc12b6d0adf1"), "TYPE-DEBT", new DateTime(2026, 3, 16, 5, 30, 33, 158, DateTimeKind.Utc).AddTicks(7393), "Debit" },
                    { new Guid("6c1021dd-d156-4698-92d7-43b28a3c5252"), "TYPE-INVS", new DateTime(2026, 3, 16, 5, 30, 33, 158, DateTimeKind.Utc).AddTicks(7395), "Investment" },
                    { new Guid("bb11dc74-1e50-459f-bfdb-8caca9e1f39e"), "TYPE-SAVS", new DateTime(2026, 3, 16, 5, 30, 33, 158, DateTimeKind.Utc).AddTicks(7394), "Savings" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("221a1b17-c281-4b92-94ea-716be28509bc"), "BANPRO", new DateTime(2026, 3, 16, 5, 30, 33, 158, DateTimeKind.Utc).AddTicks(6417), "BANCO DE LA PRODUCCION", 2.0 },
                    { new Guid("d4439eac-6e85-4453-a185-4085d0ae1422"), "BAC", new DateTime(2026, 3, 16, 5, 30, 33, 158, DateTimeKind.Utc).AddTicks(6414), "BANCO DE AMERICA", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("8b8915c6-dcb9-476a-852c-c5d302a6a468"), "NIO", new DateTime(2026, 3, 16, 5, 30, 33, 158, DateTimeKind.Utc).AddTicks(6751), "Cordoba Nicaraguense", "C$" },
                    { new Guid("b9a9f974-a217-4bac-912f-a28dec574ef3"), "EUR", new DateTime(2026, 3, 16, 5, 30, 33, 158, DateTimeKind.Utc).AddTicks(6760), "Euro", "€" },
                    { new Guid("bc01ffc7-c7a5-4c4c-8181-f4179da8acd1"), "USD", new DateTime(2026, 3, 16, 5, 30, 33, 158, DateTimeKind.Utc).AddTicks(6748), "Dolar Estadounidense", "$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("3a3986e0-4054-48a0-a65c-42e25b174009"), "E", new DateTime(2026, 3, 16, 5, 30, 33, 158, DateTimeKind.Utc).AddTicks(6055), "Expenses" },
                    { new Guid("e34d3e16-9b3b-45e8-b121-8737fb05b808"), "I", new DateTime(2026, 3, 16, 5, 30, 33, 158, DateTimeKind.Utc).AddTicks(6052), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("56e9d347-931e-4608-b8fa-a9d7b88f713c"), "TRF", new DateTime(2026, 3, 16, 5, 30, 33, 158, DateTimeKind.Utc).AddTicks(7124), 0, "Transfers" },
                    { new Guid("903813e3-80ee-4be0-9c0a-49568421da8f"), "EXP", new DateTime(2026, 3, 16, 5, 30, 33, 158, DateTimeKind.Utc).AddTicks(7123), 0, "Expenses" },
                    { new Guid("960f529a-ff29-4a1e-9eab-35023509c815"), "INC", new DateTime(2026, 3, 16, 5, 30, 33, 158, DateTimeKind.Utc).AddTicks(7120), 0, "Income" }
                });
        }
    }
}
