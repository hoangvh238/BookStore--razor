using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class InitialIdentityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account",
                schema: "book-store--dev");

            migrationBuilder.DropTable(
                name: "Order Details",
                schema: "book-store--dev");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "book-store--dev");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "book-store--dev");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "book-store--dev");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "book-store--dev");

            migrationBuilder.DropTable(
                name: "Suppliers",
                schema: "book-store--dev");
        }
    }
}
