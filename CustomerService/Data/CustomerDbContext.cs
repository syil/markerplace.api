using CustomerService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Data;

public class CustomerDbContext : DbContext
{
    private readonly IWebHostEnvironment webHostEnvironment;

    public CustomerDbContext(DbContextOptions<CustomerDbContext> dbContextOptions, IWebHostEnvironment webHostEnvironment)
		: base(dbContextOptions)
	{
        this.webHostEnvironment = webHostEnvironment;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(c => c.CustomerId);
            entity.Property(c => c.CreatedAt).HasDefaultValueSql("now()");

            if (webHostEnvironment.IsDevelopment())
            {
                entity.HasData(new Customer
                {
                    Name = "Sinan",
                    Lastname = "Yıl",
                    CustomerId = new Guid("d6ed1b92-6d18-4643-97b8-1c308eb26c2e"),
                    UserId = new Guid("70db214b-d11e-452f-9035-8e3d524f38b1"),
                    CreatedAt = new DateTimeOffset(2023, 04, 24, 09, 56, 0, TimeZoneInfo.Local.BaseUtcOffset)
                }, 
                new Customer
                {
                    Name = "Mehmet",
                    Lastname = "Demir",
                    CustomerId = new Guid("33174b3f-0062-428a-81d3-a0c99901e4d8"),
                    UserId = new Guid("69959bb2-927e-40f6-8bba-bf116f46982d"),
                    CreatedAt = new DateTimeOffset(2023, 04, 25, 22, 28, 0, TimeZoneInfo.Local.BaseUtcOffset)
                });
            }
        });

        
    }

    public DbSet<Customer> Customers { get; set; }
}
