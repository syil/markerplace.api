using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CatalogService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CreatedAt", "Name", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("2aceae56-c4ef-4bd3-bd8f-879b93288cd1"), new DateTimeOffset(new DateTime(2023, 4, 24, 12, 14, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Blue Pencil", 4.99m },
                    { new Guid("515718c9-4f7f-4014-9b54-8cd3c32e08d9"), new DateTimeOffset(new DateTime(2023, 4, 24, 12, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "Red Notepad", 7.49m },
                    { new Guid("c3d7bb4d-7c63-4ede-b6c9-365c362e884c"), new DateTimeOffset(new DateTime(2023, 4, 24, 12, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "White Eraser", 3.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
