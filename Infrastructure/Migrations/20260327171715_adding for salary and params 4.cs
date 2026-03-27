using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingforsalaryandparams4 : Migration
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
                keyValue: new Guid("15110d76-6d39-420e-9a39-df5e4f8dfbb9"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("509f6e3f-77a8-4312-9660-a0df5f432685"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("746bc198-59a6-4f0d-8d6f-2703a3dbae49"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("d1d291ad-d92d-4048-a028-685e6351ed0b"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("77193825-2f2c-43d7-9dba-c624947aad65"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("f4879033-a74e-40c0-a6eb-df532505c37c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("14e1ec31-e00c-4bdf-95c5-ab65e2b7b948"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("3de417ec-e838-43e2-a354-f835049c0074"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("de0482e1-00b7-4381-89af-85ddcccaa6a7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("a1cc7a0e-1479-4148-9f0e-996fa4d46fe1"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("a954856d-391e-4fc2-9ae1-1084f9e0f276"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("6110689f-34fa-416d-ae9b-2c5ed06e0bb9"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("c76f75ab-f47f-4084-bf3f-acc9c6f4b5b3"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("cb8c5381-62e9-4693-9941-c6f07705007c"));

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("7a82b8c8-d576-45cb-af4b-981ffd976bdf"), "TYPE-DEBT", new DateTime(2026, 3, 27, 16, 23, 54, 855, DateTimeKind.Utc).AddTicks(7554), "Debit" },
                    { new Guid("8325e879-0045-4662-8c2e-0ddffe977b89"), "TYPE-CASH", new DateTime(2026, 3, 27, 16, 23, 54, 855, DateTimeKind.Utc).AddTicks(7551), "Cash" },
                    { new Guid("b102fefb-5f83-4a3f-ba33-67390fde203a"), "TYPE-SAVS", new DateTime(2026, 3, 27, 16, 23, 54, 855, DateTimeKind.Utc).AddTicks(7555), "Savings" },
                    { new Guid("e880da50-77f1-4b00-966d-66a9d8b2b3bb"), "TYPE-INVS", new DateTime(2026, 3, 27, 16, 23, 54, 855, DateTimeKind.Utc).AddTicks(7556), "Investment" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "ImgURL", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("21f9b7db-d626-46a3-bb7c-525568272062"), "BAC", new DateTime(2026, 3, 27, 16, 23, 54, 855, DateTimeKind.Utc).AddTicks(7029), "", "BANCO DE AMERICA", 2.0 },
                    { new Guid("e31863f4-88fb-4a86-9643-0271bf0d882a"), "BANPRO", new DateTime(2026, 3, 27, 16, 23, 54, 855, DateTimeKind.Utc).AddTicks(7039), "", "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("d4a101e7-82c8-4186-8834-71d9290338ca"), "NIO", new DateTime(2026, 3, 27, 16, 23, 54, 855, DateTimeKind.Utc).AddTicks(7207), "Cordoba Nicaraguense", "C$" },
                    { new Guid("e798543f-b916-4c5c-bde8-f05095098b32"), "USD", new DateTime(2026, 3, 27, 16, 23, 54, 855, DateTimeKind.Utc).AddTicks(7204), "Dolar Estadounidense", "$" },
                    { new Guid("f0d2fa8c-2b8d-4527-9c70-59f3c3b569a3"), "EUR", new DateTime(2026, 3, 27, 16, 23, 54, 855, DateTimeKind.Utc).AddTicks(7208), "Euro", "€" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("5457a35c-c329-4be5-9078-25f56378fe04"), "I", new DateTime(2026, 3, 27, 16, 23, 54, 855, DateTimeKind.Utc).AddTicks(6795), "Income" },
                    { new Guid("c22df599-78a1-493c-9141-d82b97b336bb"), "E", new DateTime(2026, 3, 27, 16, 23, 54, 855, DateTimeKind.Utc).AddTicks(6798), "Expenses" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("1d1de287-a5fa-4762-b24b-7a836712a053"), "INC", new DateTime(2026, 3, 27, 16, 23, 54, 855, DateTimeKind.Utc).AddTicks(7402), 0, "Income" },
                    { new Guid("2f69c32a-203e-4b56-86fd-6699df9387d1"), "TRF", new DateTime(2026, 3, 27, 16, 23, 54, 855, DateTimeKind.Utc).AddTicks(7406), 0, "Transfers" },
                    { new Guid("6f95d6c1-5ccc-467f-aab0-18ee047b8f58"), "EXP", new DateTime(2026, 3, 27, 16, 23, 54, 855, DateTimeKind.Utc).AddTicks(7405), 0, "Expenses" }
                });
        }
    }
}
