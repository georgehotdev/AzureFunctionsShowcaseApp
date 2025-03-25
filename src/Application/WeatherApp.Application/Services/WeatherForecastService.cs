using Ardalis.Result;
using WeatherApi.Domain;
using WeatherApi.ExternalServices.Abstractions;
using WeatherApp.Application.Abstractions;

namespace WeatherApp.Application.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherFetcher _weatherFetcher;

    public WeatherForecastService(IWeatherFetcher weatherFetcher)
    {
        _weatherFetcher = weatherFetcher;
    }

    public async Task<Result<WeatherForecast?>> GetWeatherForecastAsync(string city)
    {
        var weatherForecastResult = await _weatherFetcher.GetWeatherForecastAsync(city);

        if (weatherForecastResult.Status != ResultStatus.Ok)
        {
            return Result<WeatherForecast?>.Error("Failed to fetch weather forecast.");
        }



        return weatherForecastResult;
    }
}