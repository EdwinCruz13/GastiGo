using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingtherightFKrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDetails_Transactions_TransactionId1",
                schema: "finances",
                table: "TransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_TransactionDetails_TransactionId1",
                schema: "finances",
                table: "TransactionDetails");

            

            migrationBuilder.DropColumn(
                name: "TransactionId1",
                schema: "finances",
                table: "TransactionDetails");

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("0dac5a67-eb2a-4d00-ae4e-0a3c3b30fcb7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("14bc5ea5-3bc9-438b-9c24-9bea3261037b"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("30e5e5d8-3d2f-40d2-b9cd-09c80faa58b9"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("78840d62-d873-45e6-8dda-87c7abc4ef0f"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("01fa5b19-5628-4d1f-a162-bf52b333db48"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("6b551ad0-dd1b-40ba-8c2c-d27777c6e9c7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("4cd4ccbd-6cfa-46a7-9c6b-251d1f4af205"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("cffed31c-49e9-43e8-8950-79edb28f98af"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("eae56944-7d72-4fc1-ae52-643168c5d5d5"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("084acca9-427a-465f-acb5-bc1c41754545"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("7d46458b-bb95-4621-a83e-665e313f70d4"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("25cbd561-ab61-4409-9504-73f4c2fe63b6"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("4db8084d-4a07-4576-85f9-e5773b4d44ac"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("a43e4309-1013-4e0d-8df6-ee6a6875e811"));

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId1",
                schema: "finances",
                table: "TransactionDetails",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0345ff4b-ba2b-4433-a5ff-35e2fb636a2d"), "TYPE-INVS", new DateTime(2026, 3, 24, 20, 21, 55, 729, DateTimeKind.Utc).AddTicks(7700), "Investment" },
                    { new Guid("3f4e4f3e-780a-4e3d-805d-e9afa98d314c"), "TYPE-SAVS", new DateTime(2026, 3, 24, 20, 21, 55, 729, DateTimeKind.Utc).AddTicks(7699), "Savings" },
                    { new Guid("891a7dcc-7ea1-49dc-a4e4-dbf491f2ab9d"), "TYPE-CASH", new DateTime(2026, 3, 24, 20, 21, 55, 729, DateTimeKind.Utc).AddTicks(7696), "Cash" },
                    { new Guid("d0255a8b-6812-4bda-a504-534e7c0496ce"), "TYPE-DEBT", new DateTime(2026, 3, 24, 20, 21, 55, 729, DateTimeKind.Utc).AddTicks(7698), "Debit" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "ImgURL", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("4b881212-9ba1-4990-b1b7-585779d1f861"), "BAC", new DateTime(2026, 3, 24, 20, 21, 55, 729, DateTimeKind.Utc).AddTicks(7204), "", "BANCO DE AMERICA", 2.0 },
                    { new Guid("6151d7c4-18be-4255-99a5-a61b4e97c435"), "BANPRO", new DateTime(2026, 3, 24, 20, 21, 55, 729, DateTimeKind.Utc).AddTicks(7208), "", "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("dc63bf02-0244-43ff-960b-ce5d0cbb487a"), "NIO", new DateTime(2026, 3, 24, 20, 21, 55, 729, DateTimeKind.Utc).AddTicks(7378), "Cordoba Nicaraguense", "C$" },
                    { new Guid("f4ac1418-ec25-400e-9254-ca2441f1c307"), "EUR", new DateTime(2026, 3, 24, 20, 21, 55, 729, DateTimeKind.Utc).AddTicks(7379), "Euro", "€" },
                    { new Guid("fc846e5a-e73a-4ce5-897f-5d4b3c3e9eb7"), "USD", new DateTime(2026, 3, 24, 20, 21, 55, 729, DateTimeKind.Utc).AddTicks(7376), "Dolar Estadounidense", "$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("6f16c0f8-403d-4c6b-ac78-7adbc64c1b6f"), "I", new DateTime(2026, 3, 24, 20, 21, 55, 729, DateTimeKind.Utc).AddTicks(6941), "Income" },
                    { new Guid("7ee6fbb4-1e79-4fdb-94d3-8dbd8385bbd0"), "E", new DateTime(2026, 3, 24, 20, 21, 55, 729, DateTimeKind.Utc).AddTicks(6952), "Expenses" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("98ff9ceb-5e43-4475-89df-3d288f52db15"), "EXP", new DateTime(2026, 3, 24, 20, 21, 55, 729, DateTimeKind.Utc).AddTicks(7542), 0, "Expenses" },
                    { new Guid("d1baa7b1-be60-400c-8470-ce4c312e6dac"), "TRF", new DateTime(2026, 3, 24, 20, 21, 55, 729, DateTimeKind.Utc).AddTicks(7545), 0, "Transfers" },
                    { new Guid("fc12b748-bf2a-41e6-82af-6a94e10af6f6"), "INC", new DateTime(2026, 3, 24, 20, 21, 55, 729, DateTimeKind.Utc).AddTicks(7540), 0, "Income" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_TransactionId1",
                schema: "finances",
                table: "TransactionDetails",
                column: "TransactionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDetails_Transactions_TransactionId1",
                schema: "finances",
                table: "TransactionDetails",
                column: "TransactionId1",
                principalSchema: "finances",
                principalTable: "Transactions",
                principalColumn: "TransactionId");
        }
    }
}
