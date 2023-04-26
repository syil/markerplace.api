using CartService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CartService.Data;

public class CartDbContext : DbContext
{
    private readonly IWebHostEnvironment webHostEnvironment;

    public CartDbContext(DbContextOptions<CartDbContext> options, IWebHostEnvironment webHostEnvironment) 
        : base(options)
    {
        this.webHostEnvironment = webHostEnvironment;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(ci => ci.CartItemId);
            entity.Property(ci => ci.CreatedAt).HasDefaultValueSql("now()");

            if (webHostEnvironment.IsDevelopment())
            {
                entity.HasData(new CartItem
                {
                    CartItemId = new Guid("07607db7-d2ee-4a48-8cfb-7d1857fa038c"),
                    ProductId = new Guid("2aceae56-c4ef-4bd3-bd8f-879b93288cd1"),
                    CustomerId = new Guid("d6ed1b92-6d18-4643-97b8-1c308eb26c2e"),
                    ProductName = "Blue Pencil",
                    Quantity = 2,
                    UnitPrice = 4.99m,
                    CreatedAt = new DateTimeOffset(2023, 04, 22, 10, 55, 0, TimeZoneInfo.Local.BaseUtcOffset),
                    Status = Enums.CartItemStatus.InCart
                });
            }
        });
    }

    public DbSet<CartItem> CartItems { get; set; }
}
