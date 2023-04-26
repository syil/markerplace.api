using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Available = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockId);
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "StockId", "Available", "ProductId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3e44d199-0bed-4213-9518-f0c5fa29867b"), 1200, new Guid("2aceae56-c4ef-4bd3-bd8f-879b93288cd1"), new DateTimeOffset(new DateTime(2023, 4, 24, 12, 14, 1, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("76d69803-9226-4b42-87a0-088f29ef6079"), 0, new Guid("c3d7bb4d-7c63-4ede-b6c9-365c362e884c"), new DateTimeOffset(new DateTime(2023, 4, 24, 12, 45, 1, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("80210541-59fb-41a3-af1b-cce95c06c829"), 10, new Guid("515718c9-4f7f-4014-9b54-8cd3c32e08d9"), new DateTimeOffset(new DateTime(2023, 4, 24, 12, 20, 1, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
