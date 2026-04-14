using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changing_double_decimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<decimal>(
                name: "PreviousBalance",
                schema: "finances",
                table: "Transactions",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                schema: "finances",
                table: "Transactions",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "finances",
                table: "Transactions",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("322ad079-a5a9-4120-a636-0f4773e18a24"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("bdc85b59-0c7b-48b0-a6c5-eb22bdd2733c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("d1b86cbb-a883-44d4-b85f-9ccd03e48dec"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("f3cf84f4-5efb-49b7-a963-9ace0d7d0ce9"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("50385546-2999-418e-b5ae-147817aab6de"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("81e0a51d-df97-4cd3-8e50-a8cb06e0130a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("00ec3886-8c60-4874-b457-83919d536b6c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("70aa3d87-053d-42f6-80ed-eef306d7dd80"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("a0bbaeeb-5333-45bc-9884-bd7c6c035941"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("1d996037-0250-4692-8d09-60f408688511"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("4189d9f4-2557-46d1-8275-e5b5057d02e0"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("2214d4aa-0c3a-441d-bbcf-f109a5018ecd"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("4e9a838e-fb70-4a1a-aa9e-ba01dd769d6a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("91ef59fa-9482-43db-95ba-0263d4e59e4d"));

            migrationBuilder.AlterColumn<double>(
                name: "PreviousBalance",
                schema: "finances",
                table: "Transactions",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                schema: "finances",
                table: "Transactions",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                schema: "finances",
                table: "Transactions",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("343e69eb-d0b1-49a0-a13d-645c013b648f"), "TYPE-SAVS", new DateTime(2026, 4, 13, 16, 38, 27, 271, DateTimeKind.Utc).AddTicks(7482), "Savings" },
                    { new Guid("8853a480-55db-4d8e-a490-a6a2736513ea"), "TYPE-CASH", new DateTime(2026, 4, 13, 16, 38, 27, 271, DateTimeKind.Utc).AddTicks(7479), "Cash" },
                    { new Guid("e8d41ba9-82db-46b4-a535-e431dafefe2d"), "TYPE-DEBT", new DateTime(2026, 4, 13, 16, 38, 27, 271, DateTimeKind.Utc).AddTicks(7481), "Debit" },
                    { new Guid("f7339ef3-6efc-4240-a35e-3df4c838a9bc"), "TYPE-INVS", new DateTime(2026, 4, 13, 16, 38, 27, 271, DateTimeKind.Utc).AddTicks(7483), "Investment" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "ImgURL", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("00495205-4147-4a50-83fc-d3bf4eb1e975"), "BAC", new DateTime(2026, 4, 13, 16, 38, 27, 271, DateTimeKind.Utc).AddTicks(6966), "", "BANCO DE AMERICA", 2.0 },
                    { new Guid("d9c44b34-3dc9-4257-a6b1-5f19300d485c"), "BANPRO", new DateTime(2026, 4, 13, 16, 38, 27, 271, DateTimeKind.Utc).AddTicks(6972), "", "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("14d9f2f2-937a-409c-acbb-c55aac52ea53"), "NIO", new DateTime(2026, 4, 13, 16, 38, 27, 271, DateTimeKind.Utc).AddTicks(7147), "Cordoba Nicaraguense", "C$" },
                    { new Guid("37959abb-37dd-4587-a8c0-36d4c9cc785e"), "USD", new DateTime(2026, 4, 13, 16, 38, 27, 271, DateTimeKind.Utc).AddTicks(7145), "Dolar Estadounidense", "$" },
                    { new Guid("a6a341c4-84f1-46cd-82e3-41dc12d6d10a"), "EUR", new DateTime(2026, 4, 13, 16, 38, 27, 271, DateTimeKind.Utc).AddTicks(7148), "Euro", "€" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("5a9e6c3d-d7fa-4d5a-8bf1-b60dcd7027d0"), "I", new DateTime(2026, 4, 13, 16, 38, 27, 271, DateTimeKind.Utc).AddTicks(6719), "Income" },
                    { new Guid("f62b505d-6967-4169-bd32-41e7ab60eb93"), "E", new DateTime(2026, 4, 13, 16, 38, 27, 271, DateTimeKind.Utc).AddTicks(6721), "Expenses" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("42adea8a-1ce2-4159-9e31-4b8cc8c4a667"), "EXP", new DateTime(2026, 4, 13, 16, 38, 27, 271, DateTimeKind.Utc).AddTicks(7345), 0, "Expenses" },
                    { new Guid("5557730a-7ace-40d2-96ae-0fce24ed8eb7"), "TRF", new DateTime(2026, 4, 13, 16, 38, 27, 271, DateTimeKind.Utc).AddTicks(7346), 0, "Transfers" },
                    { new Guid("d19e110e-c9b3-49ce-a307-2b40536caff1"), "INC", new DateTime(2026, 4, 13, 16, 38, 27, 271, DateTimeKind.Utc).AddTicks(7341), 0, "Income" }
                });
        }
    }
}
