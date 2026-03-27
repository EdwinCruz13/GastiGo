using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingforsalaryandparams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            

            migrationBuilder.AddColumn<bool>(
                name: "isSalary",
                schema: "finances",
                table: "Categories",
                type: "boolean",
                nullable: true);

           
           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("2b46c39a-c66d-4235-9a75-3be97b5dfe8d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("352a930e-4cda-4b79-872f-cbb9ec17c8b9"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("8080d638-b95c-4383-a570-bffc0814313d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("fd688223-0dd9-474a-86cd-9f9d1999102b"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("279ec543-932d-4595-ba85-df9b0b832dcf"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("3b567ebd-5215-4efb-8301-71a95db4f9e7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("446a6383-2a20-4164-b999-40dafd6925a6"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("760f984e-e3a9-4dba-bc61-89d844e3d68b"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("9f8da713-6bf5-4e60-be06-78002d41821a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("8545e9bf-da7e-4434-ba91-d93dc203f9e8"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("916c0bf9-b892-402b-ad45-00b72a396025"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("5d10195a-48a8-4eb2-b8ee-31013bc7db3b"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("5ef7faf7-178d-422d-99eb-5efcf8a72d2d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("cc5c41f6-74ed-4deb-bb89-edb993c274f2"));

            migrationBuilder.DropColumn(
                name: "isSalary",
                schema: "finances",
                table: "Categories");

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("101b9d74-dd8b-437a-aee0-e56fdfc317e2"), "TYPE-SAVS", new DateTime(2026, 3, 25, 22, 0, 33, 733, DateTimeKind.Utc).AddTicks(8254), "Savings" },
                    { new Guid("64834aac-6a60-421c-a29f-b251fe95f343"), "TYPE-CASH", new DateTime(2026, 3, 25, 22, 0, 33, 733, DateTimeKind.Utc).AddTicks(8250), "Cash" },
                    { new Guid("6fa88556-2d84-41a7-9c03-42b526340e06"), "TYPE-INVS", new DateTime(2026, 3, 25, 22, 0, 33, 733, DateTimeKind.Utc).AddTicks(8255), "Investment" },
                    { new Guid("a016e6fb-e55a-4803-aff7-c93e5cbf69e6"), "TYPE-DEBT", new DateTime(2026, 3, 25, 22, 0, 33, 733, DateTimeKind.Utc).AddTicks(8251), "Debit" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "ImgURL", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("5d4d2485-212d-4448-ab08-e2a39cb1bdb5"), "BAC", new DateTime(2026, 3, 25, 22, 0, 33, 733, DateTimeKind.Utc).AddTicks(7722), "", "BANCO DE AMERICA", 2.0 },
                    { new Guid("f0802ad9-8e7f-4f65-ba5f-10ec4e1401fb"), "BANPRO", new DateTime(2026, 3, 25, 22, 0, 33, 733, DateTimeKind.Utc).AddTicks(7724), "", "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("32ecce00-cf41-491a-8c8a-0bae0bc8ce29"), "NIO", new DateTime(2026, 3, 25, 22, 0, 33, 733, DateTimeKind.Utc).AddTicks(7908), "Cordoba Nicaraguense", "C$" },
                    { new Guid("dc5c015f-e582-443c-91e1-59eb24d2ee51"), "USD", new DateTime(2026, 3, 25, 22, 0, 33, 733, DateTimeKind.Utc).AddTicks(7906), "Dolar Estadounidense", "$" },
                    { new Guid("efd0de00-2b4b-41b3-9ac9-f53def82d6da"), "EUR", new DateTime(2026, 3, 25, 22, 0, 33, 733, DateTimeKind.Utc).AddTicks(7908), "Euro", "€" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("e8af3885-6154-478a-b76b-4ec22ab12b54"), "E", new DateTime(2026, 3, 25, 22, 0, 33, 733, DateTimeKind.Utc).AddTicks(7482), "Expenses" },
                    { new Guid("f818549a-6740-459b-bb83-d27d5ae7bc2c"), "I", new DateTime(2026, 3, 25, 22, 0, 33, 733, DateTimeKind.Utc).AddTicks(7480), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("8f96bebc-a44d-4202-a0a1-b2fb76c9e8e7"), "INC", new DateTime(2026, 3, 25, 22, 0, 33, 733, DateTimeKind.Utc).AddTicks(8101), 0, "Income" },
                    { new Guid("b75d38cb-3d6a-40e6-8d55-3ca87bef5b38"), "EXP", new DateTime(2026, 3, 25, 22, 0, 33, 733, DateTimeKind.Utc).AddTicks(8103), 0, "Expenses" },
                    { new Guid("d5ae341d-69f0-4147-8f89-9f24735f725a"), "TRF", new DateTime(2026, 3, 25, 22, 0, 33, 733, DateTimeKind.Utc).AddTicks(8104), 0, "Transfers" }
                });
        }
    }
}
