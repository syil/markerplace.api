using CatalogService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Data;

public class CatalogDbContext : DbContext
{
    private readonly IWebHostEnvironment webHostEnvironment;

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IWebHostEnvironment webHostEnvironment) 
        : base(options)
    {
        this.webHostEnvironment = webHostEnvironment;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.ProductId);
            entity.Property(p => p.CreatedAt).ValueGeneratedOnAdd();

            if (webHostEnvironment.IsDevelopment())
            {
                entity.HasData(new Product
                {
                    ProductId = new Guid("2aceae56-c4ef-4bd3-bd8f-879b93288cd1"),
                    Name = "Blue Pencil",
                    UnitPrice = 4.99m,
                    CreatedAt = new DateTimeOffset(2023, 04, 24, 12, 14, 0, TimeZoneInfo.Local.BaseUtcOffset)
                }, new Product
                {
                    ProductId = new Guid("515718c9-4f7f-4014-9b54-8cd3c32e08d9"),
                    Name = "Red Notepad",
                    UnitPrice = 7.49m,
                    CreatedAt = new DateTimeOffset(2023, 04, 24, 12, 20, 0, TimeZoneInfo.Local.BaseUtcOffset)
                }, new Product
                {
                    ProductId = new Guid("c3d7bb4d-7c63-4ede-b6c9-365c362e884c"),
                    Name = "White Eraser",
                    UnitPrice = 3.99m,
                    CreatedAt = new DateTimeOffset(2023, 04, 24, 12, 45, 0, TimeZoneInfo.Local.BaseUtcOffset)
                });
            }
        });
    }

    public DbSet<Product> Products { get; set; }
}
