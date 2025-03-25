using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeatherApp.Application.Abstractions;
using WeatherApp.Infrastructure.Configuration;

namespace WeatherApp.Api.BackgroundJobs;

public class WeatherDataBackgroundService
{
    private readonly WeatherApiConfig _weatherApiConfig;
    private readonly IWeatherForecastService _weatherForecastService;
    private readonly ILogger _logger;

    public WeatherDataBackgroundService(ILoggerFactory loggerFactory, IOptions<WeatherApiConfig> weatherApiConfig, IWeatherForecastService weatherForecastService)
    {
        _weatherApiConfig = weatherApiConfig.Value;
        _weatherForecastService = weatherForecastService;
        _logger = loggerFactory.CreateLogger<WeatherDataBackgroundService>();
    }

    [Function("WeatherDataBackgroundService")]
    public async Task Run([TimerTrigger("0 * * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation($"Reading forecast for city {_weatherApiConfig.CityLookup} at [{DateTime.Now}]");
        
        var weatherForecast = await _weatherForecastService.GetWeatherForecastAsync(_weatherApiConfig.CityLookup);

        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation($"Next forecast read scheduled at: {myTimer.ScheduleStatus.Next}");
        }
    }
}