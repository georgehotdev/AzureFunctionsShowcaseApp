using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using WeatherApi.ExternalServices.Abstractions;
using WeatherApi.ExternalServices.OpenWeatherMap;
using WeatherApi.Persistence.Abstractions;
using WeatherApi.Persistence.WeatherForecastIngress;
using WeatherApp.Application.Abstractions;
using WeatherApp.Application.Services;
using WeatherApp.Infrastructure.Abstractions;
using WeatherApp.Infrastructure.Configuration;
using WeatherApp.Infrastructure.Database;
using WeatherApp.Infrastructure.Http;
using WeatherApp.Infrastructure.Storage;

namespace WeatherApp.Api.Extensions;

public static class DependencyRegistrationExtensions
{
    public static FunctionsApplicationBuilder Configure(this FunctionsApplicationBuilder builder) =>
        builder.RegisterConfiguration()
            .RegisterExternalServices()
            .RegisterApplicationServices()
            .RegisterInfrastructureServices()
            .RegisterPersistenceServices();

    public static FunctionsApplicationBuilder RegisterConfiguration(this FunctionsApplicationBuilder builder)
    {
        builder.Services.Configure<WeatherApiConfig>(builder.Configuration.GetSection(nameof(WeatherApiConfig))); 
        builder.Services.Configure<TableStorageConfig>(builder.Configuration.GetSection(nameof(TableStorageConfig)));
        builder.Services.Configure<BlobStorageConfig>(builder.Configuration.GetSection(nameof(BlobStorageConfig)));
        
        builder.Services.AddHttpClient();

        return builder;
    }

    private static FunctionsApplicationBuilder RegisterPersistenceServices(this FunctionsApplicationBuilder builder)
    {
        builder.Services.AddScoped<IWeatherForecastIngressRepository, WeatherForecastIngressRepository>();

        return builder;
    }

    private static FunctionsApplicationBuilder RegisterExternalServices(this FunctionsApplicationBuilder builder)
    {
        builder.Services.AddScoped<IWeatherFetcher, OpenWeatherMapWeatherFetcher>();

        return builder;
    }

    private static FunctionsApplicationBuilder RegisterApplicationServices(this FunctionsApplicationBuilder builder)
    {
        builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();

        return builder;
    }
    public static FunctionsApplicationBuilder RegisterInfrastructureServices(this FunctionsApplicationBuilder builder)
    {
        builder.Services.AddScoped<IHttpService, HttpService>();
        builder.Services.AddScoped<ITableServiceClientProvider, TableServiceClientProvider>();
        builder.Services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
        builder.Services.AddScoped<IStorageInitializer, StorageInitializer>();
        builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

        return builder;
    }
}