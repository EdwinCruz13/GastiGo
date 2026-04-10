using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class restructuring_transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionDetails",
                schema: "finances");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_Reference",
                schema: "finances",
                table: "Transactions");

            

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                schema: "finances",
                table: "Transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                schema: "finances",
                table: "Transactions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Balance",
                schema: "finances",
                table: "Transactions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "EntryType",
                schema: "finances",
                table: "Transactions",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "PreviousBalance",
                schema: "finances",
                table: "Transactions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

           

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                schema: "finances",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                schema: "finances",
                table: "Transactions",
                column: "AccountId",
                principalSchema: "finances",
                principalTable: "Accounts",
                principalColumn: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                schema: "finances",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountId",
                schema: "finances",
                table: "Transactions");

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("2dc0f2c7-d0e9-4f07-b7b2-15ac7942118b"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("af81fe7e-54ed-4f18-ba1d-ea9c3d7f6870"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("c82922c1-6275-477a-8c32-f40c983344e3"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("cf1f1faf-7d9f-491f-85ed-edb0333d81c0"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("6c1c438f-b5b2-43cd-ac13-6e4e0549a61c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("850ea20a-01ba-408a-84f6-8287d6370cb4"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("6f4bb257-2ae6-4c88-bea6-712274fd21b1"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("8b1839e7-3242-4e4c-9e1c-fe17a9a83219"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("d36adbdd-1faa-4474-82d8-a16199278c45"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("73855dc1-43c5-4ae7-b3b4-00ca1a2ca4ac"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("c25f875a-32da-451b-a6ac-6b3d82e2f0c1"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("6b516430-cee2-4a34-ba06-ab64a9c243a0"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("af9fd13e-59b2-4d3e-82e1-5f33092c9950"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("b7244678-bc1b-4d5f-a635-5c3f207171d3"));

            migrationBuilder.DropColumn(
                name: "AccountId",
                schema: "finances",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "finances",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Balance",
                schema: "finances",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "EntryType",
                schema: "finances",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PreviousBalance",
                schema: "finances",
                table: "Transactions");

            migrationBuilder.CreateTable(
                name: "TransactionDetails",
                schema: "finances",
                columns: table => new
                {
                    TransactionDetailId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EntryType = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDetails", x => x.TransactionDetailId);
                    table.CheckConstraint("CK_TransactionDetail_EntryType", "\"EntryType\" IN ('IN', 'OUT')");
                    table.ForeignKey(
                        name: "FK_TransactionDetails_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "finances",
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionDetails_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalSchema: "finances",
                        principalTable: "Transactions",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("040b710c-fb3d-43ec-8913-349dd9146013"), "TYPE-INVS", new DateTime(2026, 4, 7, 22, 46, 4, 111, DateTimeKind.Utc).AddTicks(9264), "Investment" },
                    { new Guid("49c8d4bd-4827-4fbe-84df-4dc9063bd4c3"), "TYPE-CASH", new DateTime(2026, 4, 7, 22, 46, 4, 111, DateTimeKind.Utc).AddTicks(9258), "Cash" },
                    { new Guid("57c8730f-30f6-4586-8195-8f2c700e232e"), "TYPE-DEBT", new DateTime(2026, 4, 7, 22, 46, 4, 111, DateTimeKind.Utc).AddTicks(9262), "Debit" },
                    { new Guid("e07ab868-b0c3-4378-809b-12a1cb0b7898"), "TYPE-SAVS", new DateTime(2026, 4, 7, 22, 46, 4, 111, DateTimeKind.Utc).AddTicks(9263), "Savings" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "ImgURL", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("1b5b41c6-9fa5-4c0f-b6d0-18b8f36602e4"), "BANPRO", new DateTime(2026, 4, 7, 22, 46, 4, 107, DateTimeKind.Utc).AddTicks(6995), "", "BANCO DE LA PRODUCCION", 2.0 },
                    { new Guid("80698e0d-350e-4e0c-81eb-de9f86abe64e"), "BAC", new DateTime(2026, 4, 7, 22, 46, 4, 107, DateTimeKind.Utc).AddTicks(6991), "", "BANCO DE AMERICA", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("b8388e60-eb8e-4b2e-b31a-5246eefed1ea"), "NIO", new DateTime(2026, 4, 7, 22, 46, 4, 107, DateTimeKind.Utc).AddTicks(7171), "Cordoba Nicaraguense", "C$" },
                    { new Guid("ce0f7f5f-c378-4270-a908-2a8d1cae5cc0"), "EUR", new DateTime(2026, 4, 7, 22, 46, 4, 107, DateTimeKind.Utc).AddTicks(7172), "Euro", "€" },
                    { new Guid("f4f56c06-5882-4aa0-a8cb-dd7d2a4480f1"), "USD", new DateTime(2026, 4, 7, 22, 46, 4, 107, DateTimeKind.Utc).AddTicks(7168), "Dolar Estadounidense", "$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("66f39627-5e2e-48ad-af6b-75a14f177e73"), "I", new DateTime(2026, 4, 7, 22, 46, 4, 107, DateTimeKind.Utc).AddTicks(6763), "Income" },
                    { new Guid("6a979339-b49d-441c-a53a-2535c989b83a"), "E", new DateTime(2026, 4, 7, 22, 46, 4, 107, DateTimeKind.Utc).AddTicks(6766), "Expenses" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("15f685a5-b5db-415f-9c1b-9bcb3c734a76"), "EXP", new DateTime(2026, 4, 7, 22, 46, 4, 111, DateTimeKind.Utc).AddTicks(9039), 0, "Expenses" },
                    { new Guid("26caec2c-0507-40b2-a1dd-3a9f892b0536"), "INC", new DateTime(2026, 4, 7, 22, 46, 4, 111, DateTimeKind.Utc).AddTicks(9030), 0, "Income" },
                    { new Guid("781e86ba-7304-4d83-88f1-24f192a7ac92"), "TRF", new DateTime(2026, 4, 7, 22, 46, 4, 111, DateTimeKind.Utc).AddTicks(9040), 0, "Transfers" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Reference",
                schema: "finances",
                table: "Transactions",
                column: "Reference");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_AccountId",
                schema: "finances",
                table: "TransactionDetails",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_TransactionId",
                schema: "finances",
                table: "TransactionDetails",
                column: "TransactionId");
        }
    }
}
