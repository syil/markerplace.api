using CartService.Data;
using CartService.Services;
using CartService.Services.CatalogService;
using CartService.Services.OrderService;
using MarketplaceApi.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.AddSerilogFileLogger();

// Add services to the container.
builder.Services.AddControllers().AddExceptionFilter();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddDbContext<CartDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));

    if (builder.Environment.IsDevelopment())
    {
        options.EnableDetailedErrors();
    }
});

builder.Services.AddMediatRAndValidators();

builder.Services.AddApiClientService<IOrderService, OrderService>();
builder.Services.AddApiClientService<ICatalogService, CatalogService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
