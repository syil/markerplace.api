using CustomerService.Data;
using MarketplaceApi.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.AddSerilogFileLogger();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CustomerDbContext>(options =>
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
