using FluentValidation;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketplaceApi.Utilities;

public static class StartupExtensions
{
    public static void AddSerilogFileLogger(this WebApplicationBuilder builder)
    {
        var seriLogger = new LoggerConfiguration()
            .MinimumLevel.Information().WriteTo.File("logs/log_.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        builder.Logging.AddSerilog(seriLogger, dispose: true);
    }

    public static IServiceCollection AddApiClientService<TService, TImplementation>(this IServiceCollection services)
        where TService : class 
        where TImplementation : class, TService
    {
        services.AddScoped<TService, TImplementation>().AddHttpClient(typeof(TService).Name);

        return services;
    }

    public static IServiceCollection AddMediatRAndValidators(this IServiceCollection services)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<Program>());
        services.AddValidatorsFromAssemblyContaining<Program>();

        return services;
    }

    public static IMvcBuilder AddExceptionFilter(this IMvcBuilder builder)
    {
        builder.Services.AddScoped<ExceptionToProblemFilterAttribute>();
        builder.AddMvcOptions(setup => setup.Filters.AddService<ExceptionToProblemFilterAttribute>(order: 0));

        return builder;
    }
}
