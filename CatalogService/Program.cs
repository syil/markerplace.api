using CatalogService.Data;
using CatalogService.Services;
using CatalogService.Services.StockService;
using MarketplaceApi.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.AddSerilogFileLogger();

// Add services to the container.
builder.Services.AddControllers().AddExceptionFilter();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddDbContext<CatalogDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));

    if (builder.Environment.IsDevelopment())
    {
        options.EnableDetailedErrors();
    }
});

builder.Services.AddMediatRAndValidators();
builder.Services.AddApiClientService<IStockService, StockService>();

var app = builder.Build();
using var startupScope = app.Services.CreateScope();

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
