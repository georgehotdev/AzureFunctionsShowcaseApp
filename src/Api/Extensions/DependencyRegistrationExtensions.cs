using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using WeatherApi.ExternalServices.Abstractions;
using WeatherApi.ExternalServices.OpenWeatherMap;
using WeatherApp.Application.Abstractions;
using WeatherApp.Application.Services;
using WeatherApp.Infrastructure.Configuration;

namespace WeatherApp.Api.Extensions;

public static class DependencyRegistrationExtensions
{
    public static FunctionsApplicationBuilder RegisterDependencies(this FunctionsApplicationBuilder builder) =>
        builder.RegisterConfiguration()
            .RegisterExternalServices()
            .RegisterApplicationServices();

    public static FunctionsApplicationBuilder RegisterConfiguration(this FunctionsApplicationBuilder builder)
    {
        builder.Services.Configure<WeatherApiConfig>(builder.Configuration.GetSection(nameof(WeatherApiConfig)));

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
}