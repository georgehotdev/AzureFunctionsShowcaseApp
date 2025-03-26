using WeatherApi.Persistence.Entities;

namespace WeatherApi.Persistence.Extensions;

public static class WeatherForecastEntityMappingExtensions
{
    public static WeatherForecastEntity ToEntity(this Domain.WeatherForecast weatherForecast) =>
        new(weatherForecast.Id, weatherForecast.Location, weatherForecast.ForecastDate,
            weatherForecast.CurrentTemperature, weatherForecast.MinTemperature, weatherForecast.MaxTemperature);
}