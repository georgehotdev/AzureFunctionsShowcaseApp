using Ardalis.Result;
using WeatherApi.Domain;

namespace WeatherApp.Application.Abstractions;

public interface IWeatherForecastService
{
    Task<Result<WeatherForecast?>> IngressWeatherForecastAsync(string city);
    Task<IEnumerable<WeatherForecastIngress>> GetAllWeatherForecastIngresses(DateTime fromDate, DateTime toDate);
    Task<Result<WeatherForecast?>> GetWeatherForecast(string logEntryId);
}