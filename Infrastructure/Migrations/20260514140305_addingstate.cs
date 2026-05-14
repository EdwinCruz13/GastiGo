using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingstate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<Boolean>(
                name: "State",
                schema: "finances",
                table: "Accounts",
                type: "Boolean",
                nullable: true);

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("291a0339-9814-4c32-8cb1-a618e2ec3115"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("3eb48718-06e2-4ba5-9862-271a97c501b2"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("aaa3a1fb-1547-4520-879a-bb7daf4b4c9c"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("baf7a8f8-9546-4cb3-a62c-8c89c6918cdb"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("99fe52db-01cd-4e7e-af73-e966dfc91dbe"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("c5714917-7c12-4036-ba92-376116618e8d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("197abddc-0115-4af6-b979-d0a13f18dcad"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("8f3dd3cd-75ec-455f-9bcc-a5931f47e3a0"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("d998115b-4429-478a-b811-2978f7b471f7"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("23a2ec67-e025-44c2-9981-a0db823b8715"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("691d04e6-d2e4-4d5b-be23-6561f19f728f"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("4eade2cf-d16d-4f24-9b4d-2bb9ed4b31ae"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("8ae81499-f0f5-4cfa-a88e-3ee86e4a5e5a"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("cfb898f3-2122-4872-8083-915b0d5486e8"));

            migrationBuilder.RenameColumn(
                name: "State1",
                schema: "finances",
                table: "Accounts",
                newName: "State");

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0f626b5b-89ef-4985-948d-d81872af259a"), "TYPE-SAVS", new DateTime(2026, 5, 14, 14, 0, 28, 553, DateTimeKind.Utc).AddTicks(6499), "Savings" },
                    { new Guid("4e20a595-6515-4e34-b39e-ceb7b243d5d8"), "TYPE-INVS", new DateTime(2026, 5, 14, 14, 0, 28, 553, DateTimeKind.Utc).AddTicks(6500), "Investment" },
                    { new Guid("6283939b-906a-4ab1-a0af-53f330fc10cc"), "TYPE-DEBT", new DateTime(2026, 5, 14, 14, 0, 28, 553, DateTimeKind.Utc).AddTicks(6498), "Debit" },
                    { new Guid("fd711305-1a2b-4765-8b7e-a03b7db497a8"), "TYPE-CASH", new DateTime(2026, 5, 14, 14, 0, 28, 553, DateTimeKind.Utc).AddTicks(6497), "Cash" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "ImgURL", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("8e481aef-5d53-4794-bac9-2f300650356d"), "BANPRO", new DateTime(2026, 5, 14, 14, 0, 28, 553, DateTimeKind.Utc).AddTicks(5127), "", "BANCO DE LA PRODUCCION", 2.0 },
                    { new Guid("a58902ba-b7a3-404d-a1bc-f9efe85950c1"), "BAC", new DateTime(2026, 5, 14, 14, 0, 28, 553, DateTimeKind.Utc).AddTicks(5124), "", "BANCO DE AMERICA", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("16d95d76-9d7a-4be1-a057-852de4cfc362"), "NIO", new DateTime(2026, 5, 14, 14, 0, 28, 553, DateTimeKind.Utc).AddTicks(5285), "Cordoba Nicaraguense", "C$" },
                    { new Guid("77116797-5f9d-4618-a2c5-38a035e9bf89"), "USD", new DateTime(2026, 5, 14, 14, 0, 28, 553, DateTimeKind.Utc).AddTicks(5282), "Dolar Estadounidense", "$" },
                    { new Guid("edd53125-6305-4fbb-bf5f-e5a8482c49ec"), "EUR", new DateTime(2026, 5, 14, 14, 0, 28, 553, DateTimeKind.Utc).AddTicks(5285), "Euro", "€" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("5963b515-9947-40cc-99c2-67234505b4cb"), "E", new DateTime(2026, 5, 14, 14, 0, 28, 553, DateTimeKind.Utc).AddTicks(4867), "Expenses" },
                    { new Guid("609f47ee-6bdb-4129-aead-fd4713c04d94"), "I", new DateTime(2026, 5, 14, 14, 0, 28, 553, DateTimeKind.Utc).AddTicks(4862), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("25523187-98ff-4aad-97bf-ba66d44e3bd8"), "INC", new DateTime(2026, 5, 14, 14, 0, 28, 553, DateTimeKind.Utc).AddTicks(6364), 0, "Income" },
                    { new Guid("680efce6-eaab-4219-a66d-47014652b61c"), "TRF", new DateTime(2026, 5, 14, 14, 0, 28, 553, DateTimeKind.Utc).AddTicks(6368), 0, "Transfers" },
                    { new Guid("eace801a-5213-40a2-b830-0e18af9479e0"), "EXP", new DateTime(2026, 5, 14, 14, 0, 28, 553, DateTimeKind.Utc).AddTicks(6367), 0, "Expenses" }
                });
        }
    }
}
