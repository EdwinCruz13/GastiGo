using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingchangesontransactions2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                schema: "finances",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountId",
                schema: "finances",
                table: "Transactions");

            
           
          

            migrationBuilder.DropColumn(
                name: "AccountId",
                schema: "finances",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "finances",
                table: "Transactions");

            migrationBuilder.CreateTable(
                name: "TransactionDetails",
                schema: "finances",
                columns: table => new
                {
                    TransactionDetailId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    EntryType = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionDetails",
                schema: "finances");

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("0a343c59-028f-42d0-a4ac-81b37c842e59"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("78958946-a433-4c31-9c91-aa6d92e3fd9f"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("e2657d89-16e0-47c5-bc20-3f8359235cce"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("e3c53cbc-7964-42f7-a723-b9578cc5452d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("1d5c8594-4411-4f41-a2a2-620b1c89ebbd"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("890242d7-387e-4b2e-b1ac-c552f8f9248b"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("38c4f66d-a683-469e-96a5-d42f21aeb1d0"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("9edd3da8-0623-4d98-b9ce-53e50752cd51"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("df46f462-5e6b-4474-9ea9-3db2043792af"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("4ec416c4-0a67-4ae3-9cbe-be84e1820cc0"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("d3a0b213-f1dd-42bb-9a78-819acd9eb697"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("71b32477-3cc6-495f-bc3e-f872a15fb22a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("83a2c6f8-e054-4b13-8493-85af56560405"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("ea89fc8e-e1d9-4a64-bf5d-cb9040a110cd"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                schema: "finances",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                schema: "finances",
                table: "Transactions",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0fa90600-c073-4c6c-8dc8-15808ce7bb1e"), "TYPE-DEBT", new DateTime(2026, 3, 21, 14, 15, 32, 430, DateTimeKind.Utc).AddTicks(3979), "Debit" },
                    { new Guid("81f3276f-de2d-4305-9766-c08b5771806f"), "TYPE-INVS", new DateTime(2026, 3, 21, 14, 15, 32, 430, DateTimeKind.Utc).AddTicks(3982), "Investment" },
                    { new Guid("a815b325-d2ee-440f-a15d-8e370c00b91c"), "TYPE-SAVS", new DateTime(2026, 3, 21, 14, 15, 32, 430, DateTimeKind.Utc).AddTicks(3980), "Savings" },
                    { new Guid("b6fd35b1-10f2-4dfe-94b1-0974ea334d2a"), "TYPE-CASH", new DateTime(2026, 3, 21, 14, 15, 32, 430, DateTimeKind.Utc).AddTicks(3974), "Cash" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "ImgURL", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("a05aa7a9-1f4c-4f38-9c21-ed5433d32fb3"), "BAC", new DateTime(2026, 3, 21, 14, 15, 32, 430, DateTimeKind.Utc).AddTicks(2705), "", "BANCO DE AMERICA", 2.0 },
                    { new Guid("e60238dc-8eb2-4c03-bbbc-737b619e03ce"), "BANPRO", new DateTime(2026, 3, 21, 14, 15, 32, 430, DateTimeKind.Utc).AddTicks(2709), "", "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("663a6fdc-b9ef-4c04-9e11-5be2fcc8d5e4"), "EUR", new DateTime(2026, 3, 21, 14, 15, 32, 430, DateTimeKind.Utc).AddTicks(3078), "Euro", "€" },
                    { new Guid("9769c5c5-d3a6-4dd4-a478-165694bee3e8"), "USD", new DateTime(2026, 3, 21, 14, 15, 32, 430, DateTimeKind.Utc).AddTicks(3073), "Dolar Estadounidense", "$" },
                    { new Guid("b43c9029-7ee5-43fe-9992-bf044ad98498"), "NIO", new DateTime(2026, 3, 21, 14, 15, 32, 430, DateTimeKind.Utc).AddTicks(3077), "Cordoba Nicaraguense", "C$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("11884c16-39b8-4d1d-903d-c6a44cd4c685"), "I", new DateTime(2026, 3, 21, 14, 15, 32, 430, DateTimeKind.Utc).AddTicks(2091), "Income" },
                    { new Guid("506eacc7-3df8-48ab-847b-9aaa98e21c41"), "E", new DateTime(2026, 3, 21, 14, 15, 32, 430, DateTimeKind.Utc).AddTicks(2095), "Expenses" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("c528d898-8ff5-442f-bf59-fc975371b52d"), "TRF", new DateTime(2026, 3, 21, 14, 15, 32, 430, DateTimeKind.Utc).AddTicks(3578), 0, "Transfers" },
                    { new Guid("f6f92057-a792-4170-a9c0-51cfcadec9c7"), "EXP", new DateTime(2026, 3, 21, 14, 15, 32, 430, DateTimeKind.Utc).AddTicks(3576), 0, "Expenses" },
                    { new Guid("f76aecd1-bc71-4311-b4c3-68a2e3978e92"), "INC", new DateTime(2026, 3, 21, 14, 15, 32, 430, DateTimeKind.Utc).AddTicks(3570), 0, "Income" }
                });

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
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
