using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new_again_again2 : Migration
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
                    Name = table.Column<string>(type: "text", nullable: false),
                    Abbre = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
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
                        name: "FK_Categories_Natures_CategoryID",
                        column: x => x.CategoryID,
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
