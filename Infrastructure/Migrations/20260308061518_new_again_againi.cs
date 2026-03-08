using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new_again_againi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Natures_CategoryID",
                schema: "finances",
                table: "Categories");

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("5da01b1c-a41d-451b-b84f-6c001113dfd1"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("852fcf59-4f4d-4482-a51d-f0ee04684467"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeID",
                keyValue: new Guid("f4f4360f-4206-4f95-90ca-30522f83c9b9"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("99cac6b1-5237-4e22-bf37-1e5b349caa03"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankID",
                keyValue: new Guid("fdbf03c6-ab92-462d-97db-868ad53ff730"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("3b82c188-6f26-449b-9919-0fcfc2e15c21"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("7417dc1e-0399-4616-bec7-982f31d3077b"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrecyID",
                keyValue: new Guid("b5518aa7-2eef-4519-91d8-6ecfcf151e83"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("bdf6f611-3d4e-4885-9f6f-51b1e2b3b7d4"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureID",
                keyValue: new Guid("d95a28af-cb51-4d19-bd16-a6c08f74bde7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("40ccf694-9137-4d85-9438-6687922c21a8"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("4b073d11-0c0d-47ff-9a90-095188f7c4f7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeID",
                keyValue: new Guid("d93e5fea-8b67-48c4-96fd-3749a2a0fcfc"));

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

            migrationBuilder.CreateIndex(
                name: "IX_Categories_NatureID",
                schema: "finances",
                table: "Categories",
                column: "NatureID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Natures_NatureID",
                schema: "finances",
                table: "Categories",
                column: "NatureID",
                principalSchema: "finances",
                principalTable: "Natures",
                principalColumn: "NatureID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Natures_NatureID",
                schema: "finances",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_NatureID",
                schema: "finances",
                table: "Categories");

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

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("5da01b1c-a41d-451b-b84f-6c001113dfd1"), "TYPE-EXP", new DateTime(2026, 3, 8, 5, 17, 38, 196, DateTimeKind.Utc).AddTicks(6218), "Expense" },
                    { new Guid("852fcf59-4f4d-4482-a51d-f0ee04684467"), "TYPE-INC", new DateTime(2026, 3, 8, 5, 17, 38, 196, DateTimeKind.Utc).AddTicks(6219), "Income" },
                    { new Guid("f4f4360f-4206-4f95-90ca-30522f83c9b9"), "TYPE-TRF", new DateTime(2026, 3, 8, 5, 17, 38, 196, DateTimeKind.Utc).AddTicks(6215), "Transfer" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankID", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("99cac6b1-5237-4e22-bf37-1e5b349caa03"), "BAC", new DateTime(2026, 3, 8, 5, 17, 38, 196, DateTimeKind.Utc).AddTicks(5185), "BANCO DE AMERICA", 2.0 },
                    { new Guid("fdbf03c6-ab92-462d-97db-868ad53ff730"), "BANPRO", new DateTime(2026, 3, 8, 5, 17, 38, 196, DateTimeKind.Utc).AddTicks(5188), "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyID", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("3b82c188-6f26-449b-9919-0fcfc2e15c21"), "USD", new DateTime(2026, 3, 8, 5, 17, 38, 196, DateTimeKind.Utc).AddTicks(5556), "Dolar Estadounidense", "$" },
                    { new Guid("7417dc1e-0399-4616-bec7-982f31d3077b"), "NIO", new DateTime(2026, 3, 8, 5, 17, 38, 196, DateTimeKind.Utc).AddTicks(5561), "Cordoba Nicaraguense", "C$" },
                    { new Guid("b5518aa7-2eef-4519-91d8-6ecfcf151e83"), "EUR", new DateTime(2026, 3, 8, 5, 17, 38, 196, DateTimeKind.Utc).AddTicks(5562), "Euro", "€" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("bdf6f611-3d4e-4885-9f6f-51b1e2b3b7d4"), "E", new DateTime(2026, 3, 8, 5, 17, 38, 196, DateTimeKind.Utc).AddTicks(4721), "Expenses" },
                    { new Guid("d95a28af-cb51-4d19-bd16-a6c08f74bde7"), "I", new DateTime(2026, 3, 8, 5, 17, 38, 196, DateTimeKind.Utc).AddTicks(4717), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeID", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("40ccf694-9137-4d85-9438-6687922c21a8"), "TRF", new DateTime(2026, 3, 8, 5, 17, 38, 196, DateTimeKind.Utc).AddTicks(5931), 0, "Transfers" },
                    { new Guid("4b073d11-0c0d-47ff-9a90-095188f7c4f7"), "INC", new DateTime(2026, 3, 8, 5, 17, 38, 196, DateTimeKind.Utc).AddTicks(5924), 0, "Income" },
                    { new Guid("d93e5fea-8b67-48c4-96fd-3749a2a0fcfc"), "EXP", new DateTime(2026, 3, 8, 5, 17, 38, 196, DateTimeKind.Utc).AddTicks(5930), 0, "Expenses" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Natures_CategoryID",
                schema: "finances",
                table: "Categories",
                column: "CategoryID",
                principalSchema: "finances",
                principalTable: "Natures",
                principalColumn: "NatureID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
