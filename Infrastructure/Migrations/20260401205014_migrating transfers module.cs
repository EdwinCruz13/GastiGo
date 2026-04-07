using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migratingtransfersmodule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                schema: "finances",
                table: "Transactions");

          
            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                schema: "finances",
                table: "Transactions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

           
            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                schema: "finances",
                table: "Transactions",
                column: "CategoryId",
                principalSchema: "finances",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                schema: "finances",
                table: "Transactions");

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("2dcda07e-311d-4637-a308-bb2541c4b878"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("3ce85e02-d6dd-4424-b343-fa1ca6a231da"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("50524f59-8f96-46cb-a73e-f6c83144e6bc"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("dd9ce612-709f-41a2-8033-3d6836daf34d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("099a8e9f-3628-4525-87a3-48a31bdf9305"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("52ff2950-1a10-438b-80b3-da79cdbddd14"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("4f5f5f4d-4714-44f4-a8bd-c465df951ff9"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("d1205734-d91f-40a6-a2b3-5ff2e9dbfd14"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("e8395fef-dad2-483b-a6d0-ca476eac6d44"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("683cc291-045f-4d2b-a153-7bd6cf699efc"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("91337c3c-b9af-4955-ace1-f8a03f71281b"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("129b7caf-cd5a-4f22-979b-b7dfe39a1d3e"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("15846a9d-e406-45ef-8481-cf377178f42c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("76619d10-5301-47ea-89b2-63edc1cfb6d0"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                schema: "finances",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("15110d76-6d39-420e-9a39-df5e4f8dfbb9"), "TYPE-DEBT", new DateTime(2026, 3, 27, 17, 17, 14, 820, DateTimeKind.Utc).AddTicks(3063), "Debit" },
                    { new Guid("509f6e3f-77a8-4312-9660-a0df5f432685"), "TYPE-SAVS", new DateTime(2026, 3, 27, 17, 17, 14, 820, DateTimeKind.Utc).AddTicks(3064), "Savings" },
                    { new Guid("746bc198-59a6-4f0d-8d6f-2703a3dbae49"), "TYPE-INVS", new DateTime(2026, 3, 27, 17, 17, 14, 820, DateTimeKind.Utc).AddTicks(3065), "Investment" },
                    { new Guid("d1d291ad-d92d-4048-a028-685e6351ed0b"), "TYPE-CASH", new DateTime(2026, 3, 27, 17, 17, 14, 820, DateTimeKind.Utc).AddTicks(3062), "Cash" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "ImgURL", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("77193825-2f2c-43d7-9dba-c624947aad65"), "BANPRO", new DateTime(2026, 3, 27, 17, 17, 14, 820, DateTimeKind.Utc).AddTicks(2586), "", "BANCO DE LA PRODUCCION", 2.0 },
                    { new Guid("f4879033-a74e-40c0-a6eb-df532505c37c"), "BAC", new DateTime(2026, 3, 27, 17, 17, 14, 820, DateTimeKind.Utc).AddTicks(2583), "", "BANCO DE AMERICA", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("14e1ec31-e00c-4bdf-95c5-ab65e2b7b948"), "USD", new DateTime(2026, 3, 27, 17, 17, 14, 820, DateTimeKind.Utc).AddTicks(2731), "Dolar Estadounidense", "$" },
                    { new Guid("3de417ec-e838-43e2-a354-f835049c0074"), "NIO", new DateTime(2026, 3, 27, 17, 17, 14, 820, DateTimeKind.Utc).AddTicks(2733), "Cordoba Nicaraguense", "C$" },
                    { new Guid("de0482e1-00b7-4381-89af-85ddcccaa6a7"), "EUR", new DateTime(2026, 3, 27, 17, 17, 14, 820, DateTimeKind.Utc).AddTicks(2741), "Euro", "€" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("a1cc7a0e-1479-4148-9f0e-996fa4d46fe1"), "E", new DateTime(2026, 3, 27, 17, 17, 14, 820, DateTimeKind.Utc).AddTicks(2335), "Expenses" },
                    { new Guid("a954856d-391e-4fc2-9ae1-1084f9e0f276"), "I", new DateTime(2026, 3, 27, 17, 17, 14, 820, DateTimeKind.Utc).AddTicks(2332), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("6110689f-34fa-416d-ae9b-2c5ed06e0bb9"), "TRF", new DateTime(2026, 3, 27, 17, 17, 14, 820, DateTimeKind.Utc).AddTicks(2930), 0, "Transfers" },
                    { new Guid("c76f75ab-f47f-4084-bf3f-acc9c6f4b5b3"), "EXP", new DateTime(2026, 3, 27, 17, 17, 14, 820, DateTimeKind.Utc).AddTicks(2929), 0, "Expenses" },
                    { new Guid("cb8c5381-62e9-4693-9941-c6f07705007c"), "INC", new DateTime(2026, 3, 27, 17, 17, 14, 820, DateTimeKind.Utc).AddTicks(2927), 0, "Income" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                schema: "finances",
                table: "Transactions",
                column: "CategoryId",
                principalSchema: "finances",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
