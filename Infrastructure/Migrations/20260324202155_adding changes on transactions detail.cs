using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingchangesontransactionsdetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            

            

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId1",
                schema: "finances",
                table: "TransactionDetails",
                type: "uuid",
                nullable: true);

           

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_TransactionId1",
                schema: "finances",
                table: "TransactionDetails",
                column: "TransactionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionDetails_Transactions_TransactionId1",
                schema: "finances",
                table: "TransactionDetails",
                column: "TransactionId1",
                principalSchema: "finances",
                principalTable: "Transactions",
                principalColumn: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionDetails_Transactions_TransactionId1",
                schema: "finances",
                table: "TransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_TransactionDetails_TransactionId1",
                schema: "finances",
                table: "TransactionDetails");

           
           

           

            migrationBuilder.DropColumn(
                name: "TransactionId1",
                schema: "finances",
                table: "TransactionDetails");

            
        }
    }
}
