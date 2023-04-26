using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemId);
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "CartItemId", "CreatedAt", "CustomerId", "ProductId", "ProductName", "Quantity", "Status", "UnitPrice" },
                values: new object[] { new Guid("07607db7-d2ee-4a48-8cfb-7d1857fa038c"), new DateTimeOffset(new DateTime(2023, 4, 22, 10, 55, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), new Guid("d6ed1b92-6d18-4643-97b8-1c308eb26c2e"), new Guid("2aceae56-c4ef-4bd3-bd8f-879b93288cd1"), "Blue Pencil", 2, 0, 4.99m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");
        }
    }
}
