using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new_initial_again_4times33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("0834d9ef-6110-4c43-ba8c-ec103a2e9ce3"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("34c3d543-472f-4a73-b09d-008200273875"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("98922c58-4aad-49cf-828a-65a7c8cf7793"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("e849649c-a7c2-486f-a4a5-8bb80a71b76a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("7561fa08-e54a-4952-9ee0-7b96b3b75ee8"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("b25c6ee7-82c6-4760-966f-57347e01be7a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("1282ae00-2377-4f67-8f9f-84bee5cb2ae9"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("3ecd8337-c6db-4a97-a563-910b00918cbf"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyId",
                keyValue: new Guid("f2f3e82e-3ce7-4109-a05d-527e0c5415c5"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("97fbdf6d-56e8-45c8-85b4-da1d59eabc63"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("b0affb6a-79e0-461e-b246-8f67b42b4c01"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("80579eda-41e0-45a9-a941-a25b843f057d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("df9dc503-6767-4a8d-b0d0-97a3c17f961e"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("f7d82c20-68bf-4cb1-9ac8-4ba07260c9d0"));

            migrationBuilder.RenameColumn(
                name: "UserID",
                schema: "users",
                table: "Users",
                newName: "UserId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "users",
                table: "Users",
                newName: "UserID");

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0834d9ef-6110-4c43-ba8c-ec103a2e9ce3"), "TYPE-INVS", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(9071), "Investment" },
                    { new Guid("34c3d543-472f-4a73-b09d-008200273875"), "TYPE-CASH", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(9067), "Cash" },
                    { new Guid("98922c58-4aad-49cf-828a-65a7c8cf7793"), "TYPE-DEBT", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(9069), "Debit" },
                    { new Guid("e849649c-a7c2-486f-a4a5-8bb80a71b76a"), "TYPE-SAVS", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(9070), "Savings" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("7561fa08-e54a-4952-9ee0-7b96b3b75ee8"), "BAC", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8117), "BANCO DE AMERICA", 2.0 },
                    { new Guid("b25c6ee7-82c6-4760-966f-57347e01be7a"), "BANPRO", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8120), "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("1282ae00-2377-4f67-8f9f-84bee5cb2ae9"), "EUR", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8451), "Euro", "€" },
                    { new Guid("3ecd8337-c6db-4a97-a563-910b00918cbf"), "USD", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8449), "Dolar Estadounidense", "$" },
                    { new Guid("f2f3e82e-3ce7-4109-a05d-527e0c5415c5"), "NIO", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8450), "Cordoba Nicaraguense", "C$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("97fbdf6d-56e8-45c8-85b4-da1d59eabc63"), "E", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(7741), "Expenses" },
                    { new Guid("b0affb6a-79e0-461e-b246-8f67b42b4c01"), "I", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(7738), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("80579eda-41e0-45a9-a941-a25b843f057d"), "INC", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8788), 0, "Income" },
                    { new Guid("df9dc503-6767-4a8d-b0d0-97a3c17f961e"), "TRF", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8812), 0, "Transfers" },
                    { new Guid("f7d82c20-68bf-4cb1-9ac8-4ba07260c9d0"), "EXP", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8792), 0, "Expenses" }
                });
        }
    }
}
