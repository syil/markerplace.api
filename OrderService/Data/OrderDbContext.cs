using Microsoft.EntityFrameworkCore;
using OrderService.Data.Entities;

namespace OrderService.Data;

public class OrderDbContext : DbContext
{
    private readonly IWebHostEnvironment webHostEnvironment;

    public OrderDbContext(DbContextOptions<OrderDbContext> options, IWebHostEnvironment webHostEnvironment) 
        : base(options)
    {
        this.webHostEnvironment = webHostEnvironment;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Buyer>(entity =>
        {
            entity.HasKey(b => b.BuyerId);
            entity.HasMany(b => b.Orders).WithOne(o => o.Buyer).HasForeignKey(o => o.CustomerId).HasPrincipalKey(b => b.CustomerId);
            entity.Property(b => b.CreatedAt).HasDefaultValueSql("now()");

            if (webHostEnvironment.IsDevelopment())
            {
                entity.HasData(new Buyer
                {
                    BuyerId = new Guid("080ffe61-e23c-4ec3-b851-8e2ad61e45b2"),
                    CustomerId = new Guid("d6ed1b92-6d18-4643-97b8-1c308eb26c2e"),
                    Name = "Sinan",
                    Lastname = "Yıl",
                    CreatedAt = new DateTimeOffset(2023, 04, 25, 12, 03, 0, TimeZoneInfo.Local.BaseUtcOffset)
                });
            }
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.OrderId);
            entity.HasMany(o => o.OrderItems).WithOne(oi => oi.Order).HasForeignKey(o => o.OrderId);
            entity.Property(o => o.CreatedAt).HasDefaultValueSql("now()");

            if (webHostEnvironment.IsDevelopment())
            {
                entity.HasData(new Order
                {
                    OrderId = new Guid("eccaa41a-2575-4d13-a9a3-158fd2bb2bd7"),
                    Status = Enums.OrderStatus.Preparing,
                    CustomerId = new Guid("d6ed1b92-6d18-4643-97b8-1c308eb26c2e"),
                    CreatedAt = new DateTimeOffset(2023, 04, 25, 12, 03, 0, TimeZoneInfo.Local.BaseUtcOffset),
                    TotalAmount = (2 * 3.69m) + (1 * 7.99m)
                });
            }
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(oi => oi.OrderItemId);
            entity.Property(oi => oi.CreatedAt).HasDefaultValueSql("now()");

            if (webHostEnvironment.IsDevelopment())
            {
                entity.HasData(new OrderItem
                {
                    OrderItemId = new Guid("25920939-2175-4084-992a-753e162d48f0"),
                    OrderId = new Guid("eccaa41a-2575-4d13-a9a3-158fd2bb2bd7"),
                    ProductId = new Guid("515718c9-4f7f-4014-9b54-8cd3c32e08d9"),
                    ProductName = "White Eraser",
                    Quantity = 2,
                    UnitPrice = 3.69m,
                    TotalPrice = 2 * 3.69m,
                    CreatedAt = new DateTimeOffset(2023, 04, 25, 12, 03, 0, TimeZoneInfo.Local.BaseUtcOffset)
                },
                new OrderItem
                {
                    OrderItemId = new Guid("925bf9c3-890e-4c45-b80c-8f471172f7e8"),
                    OrderId = new Guid("eccaa41a-2575-4d13-a9a3-158fd2bb2bd7"),
                    ProductId = new Guid("80210541-59fb-41a3-af1b-cce95c06c829"),
                    ProductName = "Red Notepad",
                    Quantity = 1,
                    UnitPrice = 7.99m,
                    TotalPrice = 1 * 7.99m,
                    CreatedAt = new DateTimeOffset(2023, 04, 25, 12, 03, 0, TimeZoneInfo.Local.BaseUtcOffset)
                });
            }
        });
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Buyer> Buyers { get; set; }
}
