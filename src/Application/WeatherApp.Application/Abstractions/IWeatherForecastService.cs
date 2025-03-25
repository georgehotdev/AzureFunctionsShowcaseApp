using Ardalis.Result;
using WeatherApi.Domain;

namespace WeatherApp.Application.Abstractions;

public interface IWeatherForecastService
{
    Task<Result<WeatherForecast?>> GetWeatherForecastAsync(string city);
}