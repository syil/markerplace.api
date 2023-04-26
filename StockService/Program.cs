using MarketplaceApi.Utilities;
using Microsoft.EntityFrameworkCore;
using StockService.Data;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.AddSerilogFileLogger();

// Add services to the container.
builder.Services.AddControllers().AddExceptionFilter();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StockDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));

    if (builder.Environment.IsDevelopment())
    {
        options.EnableDetailedErrors();
    }
});

builder.Services.AddMediatRAndValidators();

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
