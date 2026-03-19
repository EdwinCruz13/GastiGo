using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adding_bankIMGURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "finances");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.CreateTable(
                name: "AccountTypes",
                schema: "finances",
                columns: table => new
                {
                    AccountTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Abbre = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.AccountTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                schema: "finances",
                columns: table => new
                {
                    BankId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Abbre = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    TransferFee = table.Column<double>(type: "double precision", nullable: false),
                    ImgURL = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.BankId);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "finances",
                columns: table => new
                {
                    CurrecyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Symbol = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrecyId);
                });

            migrationBuilder.CreateTable(
                name: "Natures",
                schema: "finances",
                columns: table => new
                {
                    NatureId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Abbre = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Natures", x => x.NatureId);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                schema: "finances",
                columns: table => new
                {
                    TransactionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    CurrentValue = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.TransactionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TwoFactorStatus",
                schema: "auth",
                columns: table => new
                {
                    TwoFactorStatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwoFactorStatus", x => x.TwoFactorStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "finances",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrecyId = table.Column<Guid>(type: "uuid", nullable: false),
                    BankId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Balance = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountTypes_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalSchema: "finances",
                        principalTable: "AccountTypes",
                        principalColumn: "AccountTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Banks_BankId",
                        column: x => x.BankId,
                        principalSchema: "finances",
                        principalTable: "Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Currencies_CurrecyId",
                        column: x => x.CurrecyId,
                        principalSchema: "finances",
                        principalTable: "Currencies",
                        principalColumn: "CurrecyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "users",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "finances",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    NatureId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "finances",
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Natures_NatureId",
                        column: x => x.NatureId,
                        principalSchema: "finances",
                        principalTable: "Natures",
                        principalColumn: "NatureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Categories_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "users",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "auth",
                columns: table => new
                {
                    RefreshTokenId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Revoked = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "users",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TwoFactorCodes",
                schema: "auth",
                columns: table => new
                {
                    TwoFactorCodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TwoFactorStatusId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwoFactorCodes", x => x.TwoFactorCodeId);
                    table.ForeignKey(
                        name: "FK_TwoFactorCodes_TwoFactorStatus_TwoFactorStatusId",
                        column: x => x.TwoFactorStatusId,
                        principalSchema: "auth",
                        principalTable: "TwoFactorStatus",
                        principalColumn: "TwoFactorStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TwoFactorCodes_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "users",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "finances",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    TransferGroupID = table.Column<Guid>(type: "uuid", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "finances",
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "finances",
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalSchema: "finances",
                        principalTable: "TransactionTypes",
                        principalColumn: "TransactionTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "users",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("3bec05ec-78ff-40f8-a2a6-4ac29343a89d"), "TYPE-CASH", new DateTime(2026, 3, 19, 15, 43, 39, 889, DateTimeKind.Utc).AddTicks(8), "Cash" },
                    { new Guid("4212447d-201e-4f41-b3c7-1b9381f2adc4"), "TYPE-SAVS", new DateTime(2026, 3, 19, 15, 43, 39, 889, DateTimeKind.Utc).AddTicks(11), "Savings" },
                    { new Guid("9fc537e9-e19a-4fbc-9bf6-8b352f6e422b"), "TYPE-DEBT", new DateTime(2026, 3, 19, 15, 43, 39, 889, DateTimeKind.Utc).AddTicks(10), "Debit" },
                    { new Guid("d408c707-19da-49ae-be14-6adff41d86f7"), "TYPE-INVS", new DateTime(2026, 3, 19, 15, 43, 39, 889, DateTimeKind.Utc).AddTicks(12), "Investment" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "ImgURL", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("047b6804-9ec6-4a89-aff8-89b5dfd6aee6"), "BAC", new DateTime(2026, 3, 19, 15, 43, 39, 888, DateTimeKind.Utc).AddTicks(9437), "", "BANCO DE AMERICA", 2.0 },
                    { new Guid("9d69c4ce-92b9-4d89-95da-e7c0980429ec"), "BANPRO", new DateTime(2026, 3, 19, 15, 43, 39, 888, DateTimeKind.Utc).AddTicks(9441), "", "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("22d4a305-7eff-443c-b489-04f3d91537c7"), "EUR", new DateTime(2026, 3, 19, 15, 43, 39, 888, DateTimeKind.Utc).AddTicks(9646), "Euro", "€" },
                    { new Guid("85b72c97-1e67-4494-83b5-caa8865f0d98"), "NIO", new DateTime(2026, 3, 19, 15, 43, 39, 888, DateTimeKind.Utc).AddTicks(9645), "Cordoba Nicaraguense", "C$" },
                    { new Guid("8e57c7e1-6994-4107-b468-7cf991f82248"), "USD", new DateTime(2026, 3, 19, 15, 43, 39, 888, DateTimeKind.Utc).AddTicks(9643), "Dolar Estadounidense", "$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("85183be1-d77e-4f0e-b1ad-1456a093c76b"), "I", new DateTime(2026, 3, 19, 15, 43, 39, 888, DateTimeKind.Utc).AddTicks(9210), "Income" },
                    { new Guid("8e29b285-17dc-4c6a-8aeb-fd782fba09d3"), "E", new DateTime(2026, 3, 19, 15, 43, 39, 888, DateTimeKind.Utc).AddTicks(9212), "Expenses" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("8eab2ce2-994f-41a7-825f-08821fa1429a"), "EXP", new DateTime(2026, 3, 19, 15, 43, 39, 888, DateTimeKind.Utc).AddTicks(9851), 0, "Expenses" },
                    { new Guid("c20d8bc6-252f-4a2b-b145-01419ec5be12"), "TRF", new DateTime(2026, 3, 19, 15, 43, 39, 888, DateTimeKind.Utc).AddTicks(9852), 0, "Transfers" },
                    { new Guid("f47d09d9-bfee-4b9b-a26b-0830a01a56c8"), "INC", new DateTime(2026, 3, 19, 15, 43, 39, 888, DateTimeKind.Utc).AddTicks(9849), 0, "Income" }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "TwoFactorStatus",
                columns: new[] { "TwoFactorStatusId", "Status" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Used" },
                    { 3, "Expired" },
                    { 4, "Replaced" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountTypeId",
                schema: "finances",
                table: "Accounts",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BankId",
                schema: "finances",
                table: "Accounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrecyId",
                schema: "finances",
                table: "Accounts",
                column: "CurrecyId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                schema: "finances",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_NatureId",
                schema: "finances",
                table: "Categories",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                schema: "finances",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                schema: "finances",
                table: "Categories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                schema: "auth",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                schema: "auth",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                schema: "finances",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                schema: "finances",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Reference",
                schema: "finances",
                table: "Transactions",
                column: "Reference");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionTypeId",
                schema: "finances",
                table: "Transactions",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId_TransactionDate",
                schema: "finances",
                table: "Transactions",
                columns: new[] { "UserId", "TransactionDate" });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTypes_Code",
                schema: "finances",
                table: "TransactionTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TwoFactorCodes_TwoFactorStatusId",
                schema: "auth",
                table: "TwoFactorCodes",
                column: "TwoFactorStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TwoFactorCodes_UserId_Code",
                schema: "auth",
                table: "TwoFactorCodes",
                columns: new[] { "UserId", "Code" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "users",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "TwoFactorCodes",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "TransactionTypes",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "TwoFactorStatus",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AccountTypes",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "Banks",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "Natures",
                schema: "finances");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "users");
        }
    }
}
