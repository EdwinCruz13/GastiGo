using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class recreating_my_work_done_on_weekend : Migration
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
                    AccountTypeID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Abbre = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.AccountTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                schema: "finances",
                columns: table => new
                {
                    BankID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Abbre = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    TransferFee = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.BankID);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "finances",
                columns: table => new
                {
                    CurrecyID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Symbol = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrecyID);
                });

            migrationBuilder.CreateTable(
                name: "Natures",
                schema: "finances",
                columns: table => new
                {
                    NatureID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Abbre = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Natures", x => x.NatureID);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                schema: "finances",
                columns: table => new
                {
                    TransactionTypeID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    CurrentValue = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.TransactionTypeID);
                });

            migrationBuilder.CreateTable(
                name: "TwoFactorStatus",
                schema: "auth",
                columns: table => new
                {
                    TwoFactorStatusID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwoFactorStatus", x => x.TwoFactorStatusID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "finances",
                columns: table => new
                {
                    AccountID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountTypeID = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrecyID = table.Column<Guid>(type: "uuid", nullable: false),
                    BankID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Balance = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountTypes_AccountTypeID",
                        column: x => x.AccountTypeID,
                        principalSchema: "finances",
                        principalTable: "AccountTypes",
                        principalColumn: "AccountTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Banks_BankID",
                        column: x => x.BankID,
                        principalSchema: "finances",
                        principalTable: "Banks",
                        principalColumn: "BankID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Currencies_CurrecyID",
                        column: x => x.CurrecyID,
                        principalSchema: "finances",
                        principalTable: "Currencies",
                        principalColumn: "CurrecyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "users",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "finances",
                columns: table => new
                {
                    CategoryID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentID = table.Column<Guid>(type: "uuid", nullable: true),
                    NatureID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentID",
                        column: x => x.ParentID,
                        principalSchema: "finances",
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Natures_NatureID",
                        column: x => x.NatureID,
                        principalSchema: "finances",
                        principalTable: "Natures",
                        principalColumn: "NatureID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Categories_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "users",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "auth",
                columns: table => new
                {
                    RefreshTokenID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Revoked = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.RefreshTokenID);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "users",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TwoFactorCodes",
                schema: "auth",
                columns: table => new
                {
                    TwoFactorCodeID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TwoFactorStatusID = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwoFactorCodes", x => x.TwoFactorCodeID);
                    table.ForeignKey(
                        name: "FK_TwoFactorCodes_TwoFactorStatus_TwoFactorStatusID",
                        column: x => x.TwoFactorStatusID,
                        principalSchema: "auth",
                        principalTable: "TwoFactorStatus",
                        principalColumn: "TwoFactorStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TwoFactorCodes_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "users",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "finances",
                columns: table => new
                {
                    TransactionID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionTypeID = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountID = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    TransferGroupID = table.Column<Guid>(type: "uuid", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalSchema: "finances",
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalSchema: "finances",
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionTypes_TransactionTypeID",
                        column: x => x.TransactionTypeID,
                        principalSchema: "finances",
                        principalTable: "TransactionTypes",
                        principalColumn: "TransactionTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "users",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("5fdf49af-1cdb-4371-a82f-e81ac5ea9ea1"), "TYPE-DEBT", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7982), "Debit" },
                    { new Guid("7006ffd1-a781-4e63-847e-b84878948fe7"), "TYPE-SAVS", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7985), "Savings" },
                    { new Guid("88e55ca7-3728-4057-b7c5-4c6be03800cc"), "TYPE-INVS", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7985), "Investment" },
                    { new Guid("b8047c91-1696-45db-8dcc-b4e6e36c849a"), "TYPE-CASH", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7981), "Cash" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankID", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("15ee2ab9-de8a-4a37-8768-e484f59ada08"), "BAC", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7395), "BANCO DE AMERICA", 2.0 },
                    { new Guid("2ae15d4f-519d-4456-8a0f-7eed95e33c1e"), "BANPRO", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7397), "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyID", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("3a797abe-e99e-48e0-809c-4e3b42d8709c"), "USD", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7612), "Dolar Estadounidense", "$" },
                    { new Guid("492d3d09-bfc7-47bb-a540-82e8e386c0ed"), "EUR", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7617), "Euro", "€" },
                    { new Guid("77e2eab2-1e7c-44a3-b2b1-83803ec64abd"), "NIO", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7616), "Cordoba Nicaraguense", "C$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureID", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0f0d6731-48b4-4cf6-ac22-3734589b2e0a"), "E", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7186), "Expenses" },
                    { new Guid("d8083824-04a6-4ba4-8f6a-6a63323bfac6"), "I", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7184), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeID", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("2faf81f5-af9b-4718-8747-66b26b02c840"), "EXP", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7832), 0, "Expenses" },
                    { new Guid("6d0318eb-d2b5-41e3-9e6e-1ceb71e81094"), "TRF", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7833), 0, "Transfers" },
                    { new Guid("9fe172a3-3d70-420b-afc5-9711d56f8839"), "INC", new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(7830), 0, "Income" }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "TwoFactorStatus",
                columns: new[] { "TwoFactorStatusID", "Status" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Used" },
                    { 3, "Expired" },
                    { 4, "Replaced" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "Users",
                columns: new[] { "UserID", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "TwoFactorEnabled", "Username" },
                values: new object[] { new Guid("97bffb5b-86f4-41a4-879c-5aa8151a72f8"), new DateTime(2026, 3, 9, 15, 2, 41, 27, DateTimeKind.Utc).AddTicks(6960), "edwincruz130691@gmail.com", "Edwin Cruz", true, "edwincruz130691@gmail.com", false, "Egeminis13" });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountTypeID",
                schema: "finances",
                table: "Accounts",
                column: "AccountTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BankID",
                schema: "finances",
                table: "Accounts",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrecyID",
                schema: "finances",
                table: "Accounts",
                column: "CurrecyID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserID",
                schema: "finances",
                table: "Accounts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_NatureID",
                schema: "finances",
                table: "Categories",
                column: "NatureID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentID",
                schema: "finances",
                table: "Categories",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserID",
                schema: "finances",
                table: "Categories",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                schema: "auth",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserID",
                schema: "auth",
                table: "RefreshTokens",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountID",
                schema: "finances",
                table: "Transactions",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryID",
                schema: "finances",
                table: "Transactions",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Reference",
                schema: "finances",
                table: "Transactions",
                column: "Reference");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionTypeID",
                schema: "finances",
                table: "Transactions",
                column: "TransactionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserID_TransactionDate",
                schema: "finances",
                table: "Transactions",
                columns: new[] { "UserID", "TransactionDate" });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTypes_Code",
                schema: "finances",
                table: "TransactionTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TwoFactorCodes_TwoFactorStatusID",
                schema: "auth",
                table: "TwoFactorCodes",
                column: "TwoFactorStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_TwoFactorCodes_UserID_Code",
                schema: "auth",
                table: "TwoFactorCodes",
                columns: new[] { "UserID", "Code" });

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
