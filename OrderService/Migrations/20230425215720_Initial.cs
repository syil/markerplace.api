using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buyers",
                columns: table => new
                {
                    BuyerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyers", x => x.BuyerId);
                    table.UniqueConstraint("AK_Buyers_CustomerId", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Buyers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Buyers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Buyers",
                columns: new[] { "BuyerId", "CreatedAt", "CustomerId", "Lastname", "Name" },
                values: new object[] { new Guid("080ffe61-e23c-4ec3-b851-8e2ad61e45b2"), new DateTimeOffset(new DateTime(2023, 4, 25, 12, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), new Guid("d6ed1b92-6d18-4643-97b8-1c308eb26c2e"), "Yıl", "Sinan" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CreatedAt", "CustomerId", "Status", "TotalAmount" },
                values: new object[] { new Guid("eccaa41a-2575-4d13-a9a3-158fd2bb2bd7"), new DateTimeOffset(new DateTime(2023, 4, 25, 12, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), new Guid("d6ed1b92-6d18-4643-97b8-1c308eb26c2e"), 10, 15.37m });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "CreatedAt", "OrderId", "ProductId", "ProductName", "Quantity", "TotalPrice", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("25920939-2175-4084-992a-753e162d48f0"), new DateTimeOffset(new DateTime(2023, 4, 25, 12, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), new Guid("eccaa41a-2575-4d13-a9a3-158fd2bb2bd7"), new Guid("515718c9-4f7f-4014-9b54-8cd3c32e08d9"), "White Eraser", 2, 7.38m, 3.69m },
                    { new Guid("925bf9c3-890e-4c45-b80c-8f471172f7e8"), new DateTimeOffset(new DateTime(2023, 4, 25, 12, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), new Guid("eccaa41a-2575-4d13-a9a3-158fd2bb2bd7"), new Guid("80210541-59fb-41a3-af1b-cce95c06c829"), "Red Notepad", 1, 7.99m, 7.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Buyers");
        }
    }
}
