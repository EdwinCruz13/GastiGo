using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingcategoriesparams3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            

           

          

            migrationBuilder.AlterColumn<bool>(
                name: "isSalary",
                schema: "finances",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("7a82b8c8-d576-45cb-af4b-981ffd976bdf"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("8325e879-0045-4662-8c2e-0ddffe977b89"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("b102fefb-5f83-4a3f-ba33-67390fde203a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("e880da50-77f1-4b00-966d-66a9d8b2b3bb"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("21f9b7db-d626-46a3-bb7c-525568272062"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("e31863f4-88fb-4a86-9643-0271bf0d882a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("d4a101e7-82c8-4186-8834-71d9290338ca"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("e798543f-b916-4c5c-bde8-f05095098b32"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("f0d2fa8c-2b8d-4527-9c70-59f3c3b569a3"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("5457a35c-c329-4be5-9078-25f56378fe04"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("c22df599-78a1-493c-9141-d82b97b336bb"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("1d1de287-a5fa-4762-b24b-7a836712a053"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("2f69c32a-203e-4b56-86fd-6699df9387d1"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("6f95d6c1-5ccc-467f-aab0-18ee047b8f58"));

            migrationBuilder.AlterColumn<bool>(
                name: "isSalary",
                schema: "finances",
                table: "Categories",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("2b46c39a-c66d-4235-9a75-3be97b5dfe8d"), "TYPE-INVS", new DateTime(2026, 3, 27, 16, 18, 18, 121, DateTimeKind.Utc).AddTicks(6367), "Investment" },
                    { new Guid("352a930e-4cda-4b79-872f-cbb9ec17c8b9"), "TYPE-SAVS", new DateTime(2026, 3, 27, 16, 18, 18, 121, DateTimeKind.Utc).AddTicks(6366), "Savings" },
                    { new Guid("8080d638-b95c-4383-a570-bffc0814313d"), "TYPE-DEBT", new DateTime(2026, 3, 27, 16, 18, 18, 121, DateTimeKind.Utc).AddTicks(6365), "Debit" },
                    { new Guid("fd688223-0dd9-474a-86cd-9f9d1999102b"), "TYPE-CASH", new DateTime(2026, 3, 27, 16, 18, 18, 121, DateTimeKind.Utc).AddTicks(6363), "Cash" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "ImgURL", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("279ec543-932d-4595-ba85-df9b0b832dcf"), "BANPRO", new DateTime(2026, 3, 27, 16, 18, 18, 121, DateTimeKind.Utc).AddTicks(5739), "", "BANCO DE LA PRODUCCION", 2.0 },
                    { new Guid("3b567ebd-5215-4efb-8301-71a95db4f9e7"), "BAC", new DateTime(2026, 3, 27, 16, 18, 18, 121, DateTimeKind.Utc).AddTicks(5736), "", "BANCO DE AMERICA", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("446a6383-2a20-4164-b999-40dafd6925a6"), "EUR", new DateTime(2026, 3, 27, 16, 18, 18, 121, DateTimeKind.Utc).AddTicks(5917), "Euro", "€" },
                    { new Guid("760f984e-e3a9-4dba-bc61-89d844e3d68b"), "NIO", new DateTime(2026, 3, 27, 16, 18, 18, 121, DateTimeKind.Utc).AddTicks(5913), "Cordoba Nicaraguense", "C$" },
                    { new Guid("9f8da713-6bf5-4e60-be06-78002d41821a"), "USD", new DateTime(2026, 3, 27, 16, 18, 18, 121, DateTimeKind.Utc).AddTicks(5910), "Dolar Estadounidense", "$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("8545e9bf-da7e-4434-ba91-d93dc203f9e8"), "E", new DateTime(2026, 3, 27, 16, 18, 18, 121, DateTimeKind.Utc).AddTicks(5458), "Expenses" },
                    { new Guid("916c0bf9-b892-402b-ad45-00b72a396025"), "I", new DateTime(2026, 3, 27, 16, 18, 18, 121, DateTimeKind.Utc).AddTicks(5455), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("5d10195a-48a8-4eb2-b8ee-31013bc7db3b"), "INC", new DateTime(2026, 3, 27, 16, 18, 18, 121, DateTimeKind.Utc).AddTicks(6150), 0, "Income" },
                    { new Guid("5ef7faf7-178d-422d-99eb-5efcf8a72d2d"), "EXP", new DateTime(2026, 3, 27, 16, 18, 18, 121, DateTimeKind.Utc).AddTicks(6153), 0, "Expenses" },
                    { new Guid("cc5c41f6-74ed-4deb-bb89-edb993c274f2"), "TRF", new DateTime(2026, 3, 27, 16, 18, 18, 121, DateTimeKind.Utc).AddTicks(6154), 0, "Transfers" }
                });
        }
    }
}
