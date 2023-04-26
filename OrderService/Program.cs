using MarketplaceApi.Utilities;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Services;
using OrderService.Services.CatalogService;
using OrderService.Services.CustomerService;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.AddSerilogFileLogger();

// Add services to the container.
builder.Services.AddControllers().AddExceptionFilter();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));

    if (builder.Environment.IsDevelopment())
    {
        options.EnableDetailedErrors();
    }
});

builder.Services.AddMediatRAndValidators();
builder.Services.AddApiClientService<ICatalogService, CatalogService>();
builder.Services.AddApiClientService<ICustomerService, CustomerService>();

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
