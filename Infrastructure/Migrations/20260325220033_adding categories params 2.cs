using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingcategoriesparams2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("101b9d74-dd8b-437a-aee0-e56fdfc317e2"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("64834aac-6a60-421c-a29f-b251fe95f343"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("6fa88556-2d84-41a7-9c03-42b526340e06"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("a016e6fb-e55a-4803-aff7-c93e5cbf69e6"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("5d4d2485-212d-4448-ab08-e2a39cb1bdb5"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("f0802ad9-8e7f-4f65-ba5f-10ec4e1401fb"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("32ecce00-cf41-491a-8c8a-0bae0bc8ce29"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("dc5c015f-e582-443c-91e1-59eb24d2ee51"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("efd0de00-2b4b-41b3-9ac9-f53def82d6da"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("e8af3885-6154-478a-b76b-4ec22ab12b54"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("f818549a-6740-459b-bb83-d27d5ae7bc2c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("8f96bebc-a44d-4202-a0a1-b2fb76c9e8e7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("b75d38cb-3d6a-40e6-8d55-3ca87bef5b38"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("d5ae341d-69f0-4147-8f89-9f24735f725a"));

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("29191353-65fe-4aa9-ba2f-7c4d7258fbe9"), "TYPE-CASH", new DateTime(2026, 3, 25, 21, 41, 45, 659, DateTimeKind.Utc).AddTicks(7066), "Cash" },
                    { new Guid("59990670-bf02-42f8-9085-2f0b488e8db7"), "TYPE-DEBT", new DateTime(2026, 3, 25, 21, 41, 45, 659, DateTimeKind.Utc).AddTicks(7069), "Debit" },
                    { new Guid("a766e9fa-d6f3-440f-8fb0-0d5becf0dcb2"), "TYPE-INVS", new DateTime(2026, 3, 25, 21, 41, 45, 659, DateTimeKind.Utc).AddTicks(7070), "Investment" },
                    { new Guid("fe2f6e77-b6ae-41a5-a838-5b842dda38af"), "TYPE-SAVS", new DateTime(2026, 3, 25, 21, 41, 45, 659, DateTimeKind.Utc).AddTicks(7069), "Savings" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "ImgURL", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("3a0c5228-63fe-4b29-ab19-ad5977186dfa"), "BAC", new DateTime(2026, 3, 25, 21, 41, 45, 659, DateTimeKind.Utc).AddTicks(6514), "", "BANCO DE AMERICA", 2.0 },
                    { new Guid("7d3cef9d-81af-4dc0-b9e5-16d3c4549d69"), "BANPRO", new DateTime(2026, 3, 25, 21, 41, 45, 659, DateTimeKind.Utc).AddTicks(6516), "", "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("04f476c4-730b-4674-aaf4-dea5447aa45d"), "NIO", new DateTime(2026, 3, 25, 21, 41, 45, 659, DateTimeKind.Utc).AddTicks(6691), "Cordoba Nicaraguense", "C$" },
                    { new Guid("29f46dd4-2e38-4bab-816e-aeb1d6e115c0"), "USD", new DateTime(2026, 3, 25, 21, 41, 45, 659, DateTimeKind.Utc).AddTicks(6689), "Dolar Estadounidense", "$" },
                    { new Guid("7508d3bc-1a6b-44c0-9969-102f99f63eab"), "EUR", new DateTime(2026, 3, 25, 21, 41, 45, 659, DateTimeKind.Utc).AddTicks(6693), "Euro", "€" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("51f639d8-fe97-49b6-b338-6991f244aeb3"), "I", new DateTime(2026, 3, 25, 21, 41, 45, 659, DateTimeKind.Utc).AddTicks(6230), "Income" },
                    { new Guid("ba3b527a-f459-437f-99c5-cd5d54a653c4"), "E", new DateTime(2026, 3, 25, 21, 41, 45, 659, DateTimeKind.Utc).AddTicks(6232), "Expenses" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("057b808a-54d0-4846-9cb7-7aa48d87009c"), "INC", new DateTime(2026, 3, 25, 21, 41, 45, 659, DateTimeKind.Utc).AddTicks(6895), 0, "Income" },
                    { new Guid("f8ee90bc-1ae7-4ee7-9fd6-564548b4fbda"), "TRF", new DateTime(2026, 3, 25, 21, 41, 45, 659, DateTimeKind.Utc).AddTicks(6898), 0, "Transfers" },
                    { new Guid("fc6b4572-54fc-4e0e-9ef3-377eaf7927a6"), "EXP", new DateTime(2026, 3, 25, 21, 41, 45, 659, DateTimeKind.Utc).AddTicks(6897), 0, "Expenses" }
                });
        }
    }
}
