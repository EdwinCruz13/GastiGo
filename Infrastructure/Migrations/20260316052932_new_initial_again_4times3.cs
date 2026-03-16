using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class new_initial_again_4times3 : Migration
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
                        principalColumn: "UserID",
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
                        principalColumn: "UserID",
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
                        principalColumn: "UserID",
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
                        principalColumn: "UserID",
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
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0834d9ef-6110-4c43-ba8c-ec103a2e9ce3"), "TYPE-INVS", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(9071), "Investment" },
                    { new Guid("34c3d543-472f-4a73-b09d-008200273875"), "TYPE-CASH", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(9067), "Cash" },
                    { new Guid("98922c58-4aad-49cf-828a-65a7c8cf7793"), "TYPE-DEBT", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(9069), "Debit" },
                    { new Guid("e849649c-a7c2-486f-a4a5-8bb80a71b76a"), "TYPE-SAVS", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(9070), "Savings" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Banks",
                columns: new[] { "BankId", "Abbre", "CreatedAt", "Name", "TransferFee" },
                values: new object[,]
                {
                    { new Guid("7561fa08-e54a-4952-9ee0-7b96b3b75ee8"), "BAC", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8117), "BANCO DE AMERICA", 2.0 },
                    { new Guid("b25c6ee7-82c6-4760-966f-57347e01be7a"), "BANPRO", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8120), "BANCO DE LA PRODUCCION", 2.0 }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Currencies",
                columns: new[] { "CurrecyId", "Code", "CreatedAt", "Name", "Symbol" },
                values: new object[,]
                {
                    { new Guid("1282ae00-2377-4f67-8f9f-84bee5cb2ae9"), "EUR", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8451), "Euro", "€" },
                    { new Guid("3ecd8337-c6db-4a97-a563-910b00918cbf"), "USD", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8449), "Dolar Estadounidense", "$" },
                    { new Guid("f2f3e82e-3ce7-4109-a05d-527e0c5415c5"), "NIO", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8450), "Cordoba Nicaraguense", "C$" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "Natures",
                columns: new[] { "NatureId", "Abbre", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("97fbdf6d-56e8-45c8-85b4-da1d59eabc63"), "E", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(7741), "Expenses" },
                    { new Guid("b0affb6a-79e0-461e-b246-8f67b42b4c01"), "I", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(7738), "Income" }
                });

            migrationBuilder.InsertData(
                schema: "finances",
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "Code", "CreatedAt", "CurrentValue", "Name" },
                values: new object[,]
                {
                    { new Guid("80579eda-41e0-45a9-a941-a25b843f057d"), "INC", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8788), 0, "Income" },
                    { new Guid("df9dc503-6767-4a8d-b0d0-97a3c17f961e"), "TRF", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8812), 0, "Transfers" },
                    { new Guid("f7d82c20-68bf-4cb1-9ac8-4ba07260c9d0"), "EXP", new DateTime(2026, 3, 16, 5, 29, 32, 80, DateTimeKind.Utc).AddTicks(8792), 0, "Expenses" }
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
