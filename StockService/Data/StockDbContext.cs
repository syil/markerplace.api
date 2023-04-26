using Microsoft.EntityFrameworkCore;
using StockService.Data.Entities;
using System.Xml.Linq;

namespace StockService.Data;

public class StockDbContext : DbContext
{
    private readonly IWebHostEnvironment webHostEnvironment;

    public StockDbContext(DbContextOptions<StockDbContext> options, IWebHostEnvironment webHostEnvironment) 
        : base(options)
    {
        this.webHostEnvironment = webHostEnvironment;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(s => s.StockId);
            entity.Property(s => s.UpdatedAt).HasDefaultValueSql("now()");
            entity.Property(s => s.Version).IsRowVersion();

            if (webHostEnvironment.IsDevelopment())
            {
                entity.HasData(new Stock
                {
                    StockId = new Guid("3e44d199-0bed-4213-9518-f0c5fa29867b"),
                    ProductId = new Guid("2aceae56-c4ef-4bd3-bd8f-879b93288cd1"),
                    UpdatedAt = new DateTimeOffset(2023, 04, 24, 12, 14, 1, TimeZoneInfo.Local.BaseUtcOffset),
                    Available = 1200
                }, new Stock
                {
                    StockId = new Guid("80210541-59fb-41a3-af1b-cce95c06c829"),
                    ProductId = new Guid("515718c9-4f7f-4014-9b54-8cd3c32e08d9"),
                    UpdatedAt = new DateTimeOffset(2023, 04, 24, 12, 20, 1, TimeZoneInfo.Local.BaseUtcOffset),
                    Available = 10
                }, new Stock
                {
                    StockId = new Guid("76d69803-9226-4b42-87a0-088f29ef6079"),
                    ProductId = new Guid("c3d7bb4d-7c63-4ede-b6c9-365c362e884c"),
                    UpdatedAt = new DateTimeOffset(2023, 04, 24, 12, 45, 1, TimeZoneInfo.Local.BaseUtcOffset),
                    Available = 0
                });
            }
        });
    }

    public DbSet<Stock> Stocks { get; set; }
}
