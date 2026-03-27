using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingcategoriesparams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                schema: "users",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true
            );

            migrationBuilder.CreateTable(
            name: "CategoryParams",
            schema: "finances",
            columns: table => new
            {
                ParamId = table.Column<Guid>(type: "uuid", nullable: false),
                CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                ApplySalary = table.Column<bool>(type: "boolean", nullable: false),
                ApplyPercentage = table.Column<bool>(type: "boolean", nullable: false),
                ApplyAmount = table.Column<bool>(type: "boolean", nullable: false),
                Value = table.Column<decimal>(type: "numeric", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CategoryParams", x => x.ParamId);
                table.ForeignKey(
                    name: "FK_CategoryParams_Categories_CategoryId",
                    column: x => x.CategoryId,
                    principalSchema: "finances",
                    principalTable: "Categories", // 👈 importante: nombre real de la tabla
                    principalColumn: "CategoryId",
                    onDelete: ReferentialAction.Cascade // 👈 comportamiento
                );
            });

                // 👇 índice (MUY recomendado)
                migrationBuilder.CreateIndex(
                    name: "IX_CategoryParams_CategoryId",
                    schema: "finances",
                    table: "CategoryParams",
                    column: "CategoryId"
                );


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("25dee4e5-95c1-4dc1-8b43-ee79d7d5b38b"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("aa877cee-077c-4679-afab-55f21fb34a93"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("e82adc98-ce8e-4d89-89e7-cb64386ba8fa"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "AccountTypes",
                keyColumn: "AccountTypeId",
                keyValue: new Guid("ff4618da-22fc-45a1-8c97-51c00a643b9d"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("4fc10980-65cb-41bc-81a8-abfdfd919255"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Banks",
                keyColumn: "BankId",
                keyValue: new Guid("f15c8c21-46bb-492e-86fd-a1378f6dc659"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("17b41995-bd83-47ae-a3c5-e209f114de3f"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("2fef2e7a-79f9-438a-a642-ac645bc5fd57"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: new Guid("941d9ec5-45ee-42c4-b9a8-dfc5b19d72a8"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("013ed610-9df5-4ec5-92eb-c1400000356e"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "Natures",
                keyColumn: "NatureId",
                keyValue: new Guid("eab6d25a-1922-4c9f-b271-ba6e6c2526da"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("e9cf1c2c-c818-47b7-a6d1-8ee2d758fa99"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("f92aa611-fc7d-40a5-8c46-57d627091279"));

            migrationBuilder.DeleteData(
                schema: "finances",
                table: "TransactionTypes",
                keyColumn: "TransactionTypeId",
                keyValue: new Guid("fbae3695-4a87-400b-b61f-49debce4948d"));

            migrationBuilder.RenameColumn(
                name: "HiresDate",
                schema: "users",
                table: "Users",
                newName: "HireDate");

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("231bd787-4a5f-4e1f-903f-36106cfedf90"), "TYPE-SAVS", new DateTime(2026, 3, 25, 21, 25, 26, 696, DateTimeKind.Utc).AddTicks(5330), "Savings" },
                    { new Guid("7c537885-864a-4565-82a0-8b03c11ac66a"), "TYPE-INVS", new DateTime(2026, 3, 25, 21, 25, 26, 696, DateTimeKind.Utc).AddTicks(5331), "Investment" },
                    { new Guid("7d8a60b1-727c-4e85-9f14-01542bad374c"), "TYPE-CASH", new DateTime(2026, 3, 25, 21, 25, 26, 696, DateTimeKind.Utc).AddTicks(5327), "Cash" },
                    { new Guid("89246423-0b09-40d2-b751-835c6c8f0ef4"), "TYPE-DEBT", new DateTime(2026, 3, 25, 21, 25, 26, 696, DateTimeKind.Utc).AddTicks(5329), "Debit" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "ImgURL", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("2b43a656-fe9c-4fd0-a073-53a888869a33"), "BANPRO", new DateTime(2026, 3, 25, 21, 25, 26, 696, DateTimeKind.Utc).AddTicks(4827), "", "BANCO DE LA PRODUCCION", 2.0 },
                    { new Guid("3b538177-12d1-4205-bc7e-3120c4a8b32d"), "BAC", new DateTime(2026, 3, 25, 21, 25, 26, 696, DateTimeKind.Utc).AddTicks(4824), "", "BANCO DE AMERICA", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrencyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("33f09d97-6bf8-4ec0-91f1-f68e1801e28d"), "NIO", new DateTime(2026, 3, 25, 21, 25, 26, 696, DateTimeKind.Utc).AddTicks(4996), "Cordoba Nicaraguense", "C$" },
                    { new Guid("49b3df29-e92b-4623-924b-c1117adbf331"), "EUR", new DateTime(2026, 3, 25, 21, 25, 26, 696, DateTimeKind.Utc).AddTicks(4997), "Euro", "€" },
                    { new Guid("b82b7d99-c641-4405-a585-44d742642c6b"), "USD", new DateTime(2026, 3, 25, 21, 25, 26, 696, DateTimeKind.Utc).AddTicks(4993), "Dolar Estadounidense", "$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("355a00a3-e58f-477b-96b6-1ffbcffc074d"), "E", new DateTime(2026, 3, 25, 21, 25, 26, 696, DateTimeKind.Utc).AddTicks(4536), "Expenses" },
                    { new Guid("e56a4329-e5a6-449b-9607-ee3f9114a98f"), "I", new DateTime(2026, 3, 25, 21, 25, 26, 696, DateTimeKind.Utc).AddTicks(4519), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("342f4257-ed64-496a-bb54-9ba59d853393"), "EXP", new DateTime(2026, 3, 25, 21, 25, 26, 696, DateTimeKind.Utc).AddTicks(5184), 0, "Expenses" },
                    { new Guid("40e8e311-7b8d-4909-814d-f11ed5af2ed2"), "INC", new DateTime(2026, 3, 25, 21, 25, 26, 696, DateTimeKind.Utc).AddTicks(5182), 0, "Income" },
                    { new Guid("c133c49e-4737-4e7f-b2ea-d6eaf4823598"), "TRF", new DateTime(2026, 3, 25, 21, 25, 26, 696, DateTimeKind.Utc).AddTicks(5188), 0, "Transfers" }
                });
        }
    }
}
