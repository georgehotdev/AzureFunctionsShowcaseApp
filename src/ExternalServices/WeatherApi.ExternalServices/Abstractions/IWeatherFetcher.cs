using Ardalis.Result;
using WeatherApi.Domain;

namespace WeatherApi.ExternalServices.Abstractions;

public interface IWeatherFetcher
{
    Task<Result<WeatherForecast?>> GetWeatherForecastAsync(string city);
}