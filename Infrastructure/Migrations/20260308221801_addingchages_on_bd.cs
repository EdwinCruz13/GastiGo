using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingchages_on_bd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("30c53882-3207-4d23-9a78-f11cdc7f4c78"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("7ca05246-13c5-4cb4-afbb-aed813ff36e1"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("bbaf05af-b006-4fdf-aed7-9b8460433969"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("ba7198b3-3c07-4cfb-80e7-db60fdcb34d4"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("ea210348-6d19-4f41-ae05-dbe510d27960"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("58a1f9bd-b17a-4b34-8eff-c0437a994e3a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("7dbc90de-a574-4756-b727-77037fbb2512"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("93e265d2-7f3c-4d4c-8e23-142051729afd"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("4c8f25da-52f7-4db5-88f0-85da64529c8c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("ae31bf0f-2f70-47bf-9257-d7e1c704504c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("06a7d7f9-a4fc-41e7-b86d-8e3c354db7e1"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("08906142-f635-4426-9dc9-32553fdad3b8"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("2dcfc65d-db7e-4ec2-a8ae-e34525081018"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "finances",
                table: "AccountTypes",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Abbre",
                schema: "finances",
                table: "AccountTypes",
                type: "character varying(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(8)",
                oldMaxLength: 8);

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("48655d3d-b067-4e72-b7e2-61b550e0272d"), "TYPE-CASH", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(9038), "Cash" },
                    { new Guid("7f9ec86c-76e0-4d74-939e-f67bb2f762e5"), "TYPE-DEBT", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(9041), "Debit" },
                    { new Guid("904b96c4-da75-48b9-8c15-4d92bd881d33"), "TYPE-INVS", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(9042), "Investment" },
                    { new Guid("e6344674-1ab4-46e7-92d4-cab68ff01ab3"), "TYPE-SAVS", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(9042), "Savings" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankID", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("96cfcbd5-e0dc-4f30-9e91-9bbead7454d8"), "BAC", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8044), "BANCO DE AMERICA", 2.0 },
                    { new Guid("b9be95cb-da31-4846-9297-6ef97010f7f4"), "BANPRO", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8047), "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyID", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("50df7448-0659-4520-a6fa-7605abf38030"), "NIO", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8397), "Cordoba Nicaraguense", "C$" },
                    { new Guid("7f7d602b-95a3-400f-9b56-c8e419373931"), "EUR", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8398), "Euro", "€" },
                    { new Guid("d540e523-2c02-438e-9d02-cb57737afbe0"), "USD", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8394), "Dolar Estadounidense", "$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("a99f95b3-7afe-49e1-9d03-faa35f5609f7"), "E", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(7631), "Expenses" },
                    { new Guid("e5776a01-1bf9-4c49-897e-e353bc3a285a"), "I", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(7627), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeID", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("32ac9330-eaaf-4ecf-9821-a4c2f5d58e36"), "EXP", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8746), 0, "Expenses" },
                    { new Guid("46824e6b-760c-40f6-a886-e5d6ad98b3b7"), "TRF", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8747), 0, "Transfers" },
                    { new Guid("8e56b0dc-30b7-440d-969e-3105375280a2"), "INC", new DateTime(2026, 3, 8, 22, 18, 1, 366, DateTimeKind.Utc).AddTicks(8740), 0, "Income" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("48655d3d-b067-4e72-b7e2-61b550e0272d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("7f9ec86c-76e0-4d74-939e-f67bb2f762e5"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("904b96c4-da75-48b9-8c15-4d92bd881d33"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("e6344674-1ab4-46e7-92d4-cab68ff01ab3"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("96cfcbd5-e0dc-4f30-9e91-9bbead7454d8"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("b9be95cb-da31-4846-9297-6ef97010f7f4"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("50df7448-0659-4520-a6fa-7605abf38030"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("7f7d602b-95a3-400f-9b56-c8e419373931"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("d540e523-2c02-438e-9d02-cb57737afbe0"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("a99f95b3-7afe-49e1-9d03-faa35f5609f7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("e5776a01-1bf9-4c49-897e-e353bc3a285a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("32ac9330-eaaf-4ecf-9821-a4c2f5d58e36"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("46824e6b-760c-40f6-a886-e5d6ad98b3b7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("8e56b0dc-30b7-440d-969e-3105375280a2"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "finances",
                table: "AccountTypes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Abbre",
                schema: "finances",
                table: "AccountTypes",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(9)",
                oldMaxLength: 9);

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("30c53882-3207-4d23-9a78-f11cdc7f4c78"), "TYPE-EXP", new DateTime(2026, 3, 8, 6, 15, 18, 63, DateTimeKind.Utc).AddTicks(9872), "Expense" },
                    { new Guid("7ca05246-13c5-4cb4-afbb-aed813ff36e1"), "TYPE-TRF", new DateTime(2026, 3, 8, 6, 15, 18, 63, DateTimeKind.Utc).AddTicks(9869), "Transfer" },
                    { new Guid("bbaf05af-b006-4fdf-aed7-9b8460433969"), "TYPE-INC", new DateTime(2026, 3, 8, 6, 15, 18, 63, DateTimeKind.Utc).AddTicks(9872), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankID", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("ba7198b3-3c07-4cfb-80e7-db60fdcb34d4"), "BANPRO", new DateTime(2026, 3, 8, 6, 15, 18, 63, DateTimeKind.Utc).AddTicks(8941), "BANCO DE LA PRODUCCION", 2.0 },
                    { new Guid("ea210348-6d19-4f41-ae05-dbe510d27960"), "BAC", new DateTime(2026, 3, 8, 6, 15, 18, 63, DateTimeKind.Utc).AddTicks(8937), "BANCO DE AMERICA", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyID", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("58a1f9bd-b17a-4b34-8eff-c0437a994e3a"), "NIO", new DateTime(2026, 3, 8, 6, 15, 18, 63, DateTimeKind.Utc).AddTicks(9269), "Cordoba Nicaraguense", "C$" },
                    { new Guid("7dbc90de-a574-4756-b727-77037fbb2512"), "USD", new DateTime(2026, 3, 8, 6, 15, 18, 63, DateTimeKind.Utc).AddTicks(9261), "Dolar Estadounidense", "$" },
                    { new Guid("93e265d2-7f3c-4d4c-8e23-142051729afd"), "EUR", new DateTime(2026, 3, 8, 6, 15, 18, 63, DateTimeKind.Utc).AddTicks(9270), "Euro", "€" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("4c8f25da-52f7-4db5-88f0-85da64529c8c"), "I", new DateTime(2026, 3, 8, 6, 15, 18, 63, DateTimeKind.Utc).AddTicks(8549), "Income" },
                    { new Guid("ae31bf0f-2f70-47bf-9257-d7e1c704504c"), "E", new DateTime(2026, 3, 8, 6, 15, 18, 63, DateTimeKind.Utc).AddTicks(8552), "Expenses" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeID", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("06a7d7f9-a4fc-41e7-b86d-8e3c354db7e1"), "INC", new DateTime(2026, 3, 8, 6, 15, 18, 63, DateTimeKind.Utc).AddTicks(9627), 0, "Income" },
                    { new Guid("08906142-f635-4426-9dc9-32553fdad3b8"), "EXP", new DateTime(2026, 3, 8, 6, 15, 18, 63, DateTimeKind.Utc).AddTicks(9629), 0, "Expenses" },
                    { new Guid("2dcfc65d-db7e-4ec2-a8ae-e34525081018"), "TRF", new DateTime(2026, 3, 8, 6, 15, 18, 63, DateTimeKind.Utc).AddTicks(9630), 0, "Transfers" }
                });
        }
    }
}
