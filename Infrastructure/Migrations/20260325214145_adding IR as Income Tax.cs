using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingIRasIncomeTax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "IncomeTax",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Min = table.Column<double>(type: "double precision", nullable: false),
                    Max = table.Column<double>(type: "double precision", nullable: false),
                    Percentage = table.Column<double>(type: "double precision", nullable: false),
                    Base = table.Column<double>(type: "double precision", nullable: false),
                    Excess = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeTax", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncomeTax",
                schema: "public");

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("29191353-65fe-4aa9-ba2f-7c4d7258fbe9"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("59990670-bf02-42f8-9085-2f0b488e8db7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("a766e9fa-d6f3-440f-8fb0-0d5becf0dcb2"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("fe2f6e77-b6ae-41a5-a838-5b842dda38af"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("3a0c5228-63fe-4b29-ab19-ad5977186dfa"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("7d3cef9d-81af-4dc0-b9e5-16d3c4549d69"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("04f476c4-730b-4674-aaf4-dea5447aa45d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("29f46dd4-2e38-4bab-816e-aeb1d6e115c0"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("7508d3bc-1a6b-44c0-9969-102f99f63eab"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("51f639d8-fe97-49b6-b338-6991f244aeb3"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("ba3b527a-f459-437f-99c5-cd5d54a653c4"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("057b808a-54d0-4846-9cb7-7aa48d87009c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("f8ee90bc-1ae7-4ee7-9fd6-564548b4fbda"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("fc6b4572-54fc-4e0e-9ef3-377eaf7927a6"));

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("25dee4e5-95c1-4dc1-8b43-ee79d7d5b38b"), "TYPE-CASH", new DateTime(2026, 3, 25, 21, 31, 45, 667, DateTimeKind.Utc).AddTicks(830), "Cash" },
                    { new Guid("aa877cee-077c-4679-afab-55f21fb34a93"), "TYPE-DEBT", new DateTime(2026, 3, 25, 21, 31, 45, 667, DateTimeKind.Utc).AddTicks(832), "Debit" },
                    { new Guid("e82adc98-ce8e-4d89-89e7-cb64386ba8fa"), "TYPE-INVS", new DateTime(2026, 3, 25, 21, 31, 45, 667, DateTimeKind.Utc).AddTicks(835), "Investment" },
                    { new Guid("ff4618da-22fc-45a1-8c97-51c00a643b9d"), "TYPE-SAVS", new DateTime(2026, 3, 25, 21, 31, 45, 667, DateTimeKind.Utc).AddTicks(833), "Savings" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "ImgURL", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("4fc10980-65cb-41bc-81a8-abfdfd919255"), "BAC", new DateTime(2026, 3, 25, 21, 31, 45, 667, DateTimeKind.Utc).AddTicks(343), "", "BANCO DE AMERICA", 2.0 },
                    { new Guid("f15c8c21-46bb-492e-86fd-a1378f6dc659"), "BANPRO", new DateTime(2026, 3, 25, 21, 31, 45, 667, DateTimeKind.Utc).AddTicks(345), "", "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("17b41995-bd83-47ae-a3c5-e209f114de3f"), "EUR", new DateTime(2026, 3, 25, 21, 31, 45, 667, DateTimeKind.Utc).AddTicks(512), "Euro", "€" },
                    { new Guid("2fef2e7a-79f9-438a-a642-ac645bc5fd57"), "NIO", new DateTime(2026, 3, 25, 21, 31, 45, 667, DateTimeKind.Utc).AddTicks(511), "Cordoba Nicaraguense", "C$" },
                    { new Guid("941d9ec5-45ee-42c4-b9a8-dfc5b19d72a8"), "USD", new DateTime(2026, 3, 25, 21, 31, 45, 667, DateTimeKind.Utc).AddTicks(502), "Dolar Estadounidense", "$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("013ed610-9df5-4ec5-92eb-c1400000356e"), "E", new DateTime(2026, 3, 25, 21, 31, 45, 667, DateTimeKind.Utc).AddTicks(71), "Expenses" },
                    { new Guid("eab6d25a-1922-4c9f-b271-ba6e6c2526da"), "I", new DateTime(2026, 3, 25, 21, 31, 45, 667, DateTimeKind.Utc).AddTicks(70), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("e9cf1c2c-c818-47b7-a6d1-8ee2d758fa99"), "EXP", new DateTime(2026, 3, 25, 21, 31, 45, 667, DateTimeKind.Utc).AddTicks(712), 0, "Expenses" },
                    { new Guid("f92aa611-fc7d-40a5-8c46-57d627091279"), "TRF", new DateTime(2026, 3, 25, 21, 31, 45, 667, DateTimeKind.Utc).AddTicks(713), 0, "Transfers" },
                    { new Guid("fbae3695-4a87-400b-b61f-49debce4948d"), "INC", new DateTime(2026, 3, 25, 21, 31, 45, 667, DateTimeKind.Utc).AddTicks(710), 0, "Income" }
                });
        }
    }
}
