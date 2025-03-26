using Ardalis.Result;
using Microsoft.Extensions.Options;
using WeatherApi.Domain;
using WeatherApi.ExternalServices.Abstractions;
using WeatherApi.Persistence.Abstractions;
using WeatherApi.Persistence.Entities;
using WeatherApi.Persistence.Extensions;
using WeatherApp.Application.Abstractions;
using WeatherApp.Application.Extensions;
using WeatherApp.Infrastructure.Abstractions;
using WeatherApp.Infrastructure.Configuration;

namespace WeatherApp.Application.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherFetcher _weatherFetcher;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IWeatherForecastIngressRepository _weatherForecastIngressRepository;
    private readonly WeatherApiConfig _weatherApiConfig;

    public WeatherForecastService(IWeatherFetcher weatherFetcher, IBlobStorageService blobStorageService, IWeatherForecastIngressRepository weatherForecastIngressRepository, IOptions<WeatherApiConfig> weatherApiConfig)
    {
        _weatherFetcher = weatherFetcher;
        _blobStorageService = blobStorageService;
        _weatherForecastIngressRepository = weatherForecastIngressRepository;
        _weatherApiConfig = weatherApiConfig.Value;
    }

    public async Task<Result<WeatherForecast?>> IngressWeatherForecastAsync(string city)
    {
        var weatherForecastResult = await _weatherFetcher.GetWeatherForecastAsync(city);

        var ingressId = await StoreWeatherForecastIngress(weatherForecastResult);

        if (weatherForecastResult.Status == ResultStatus.Ok)
        {
            await StoreWeatherForecast(weatherForecastResult, ingressId);
        }

        return weatherForecastResult;
    }

    public async Task<IEnumerable<WeatherForecastIngress>> GetAllWeatherForecastIngresses(DateTime fromDate, DateTime toDate)
    {
        var location = _weatherApiConfig.CityLookup;
        var weatherForecasts = await _weatherForecastIngressRepository.GetFilteredAsync(location, fromDate, toDate);
        return weatherForecasts.Select(w => w.ToModel());
    }

    public async Task<Result<WeatherForecast?>> GetWeatherForecast(string logEntryId)
    {
        return await _blobStorageService.GetJsonBlobContentAsync<WeatherForecast>(logEntryId);
    }


    private async Task<string> StoreWeatherForecastIngress(Result<WeatherForecast?> weatherForecastResult)
    {
        var weatherForecastIngressEntity = weatherForecastResult.IsSuccess ? WeatherForecastIngressEntity.CreateSuccessfulIngress(weatherForecastResult.Value!.Location, weatherForecastResult.Value.Id) : WeatherForecastIngressEntity.CreateFailedIngress(_weatherApiConfig.CityLookup);

        if (!await _weatherForecastIngressRepository.ExistsAsync(weatherForecastIngressEntity.PartitionKey, weatherForecastIngressEntity.RowKey))
        {
            await _weatherForecastIngressRepository.AddAsync(weatherForecastIngressEntity);
        }

        return weatherForecastIngressEntity.RowKey;
    }

    private async Task StoreWeatherForecast(Result<WeatherForecast?> weatherForecastResult, string ingressId)
    {
        var weatherForecastEntity = weatherForecastResult.Value!.ToEntity();

        await _blobStorageService.UploadJsonContentToBlobAsync(ingressId, weatherForecastEntity);
    }
}