using Ardalis.Result;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WeatherApi.Domain;
using WeatherApi.ExternalServices.Abstractions;
using WeatherApi.ExternalServices.OpenWeatherMap.Models;
using WeatherApp.Infrastructure.Configuration;
using WeatherApp.Infrastructure.Http;

namespace WeatherApi.ExternalServices.OpenWeatherMap;

public class OpenWeatherMapWeatherFetcher : IWeatherFetcher
{
    private readonly IHttpService _httpService;
    private readonly WeatherApiConfig _weatherApiConfig;

    public OpenWeatherMapWeatherFetcher(IOptions<WeatherApiConfig> weatherApiConfig, IHttpService httpService)
    {
        _httpService = httpService;
        _weatherApiConfig = weatherApiConfig.Value;
    }

    public async Task<Result<WeatherForecast?>> GetWeatherForecastAsync(string city)
    {
        var url = $"{_weatherApiConfig.BaseUrl}?q={city}&units=metric&appid={_weatherApiConfig.ApiKey}";

        var result = await _httpService.GetAsync<OpenWeatherMapResponse>(url);

        if (result.Status != ResultStatus.Ok)
        {
            return Result<WeatherForecast?>.Error("Failed to fetch weather forecast.");
        }

        var forecast = result.Value!;

        return new Result<WeatherForecast?>(new WeatherForecast
        {
            Id = forecast.Id,
            Location = forecast.Location,
            ForecastDate = forecast.ForecastDate,
            CurrentTemperature = forecast.WeatherForecast.CurrentTemperature,
            MinTemperature = forecast.WeatherForecast.MinTemperature,
            MaxTemperature = forecast.WeatherForecast.MaxTemperature
        });
    }
}