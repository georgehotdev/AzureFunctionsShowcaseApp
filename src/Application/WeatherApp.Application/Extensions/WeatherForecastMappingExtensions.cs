using WeatherApi.Domain;
using WeatherApi.Persistence.Entities;

namespace WeatherApp.Application.Extensions;

public static class WeatherForecastMappingExtensions
{
    public static WeatherForecast ToModel(this WeatherForecastEntity entity)
    {
        return new WeatherForecast
        {
            Id = long.Parse(entity.RowKey),
            CurrentTemperature = entity.CurrentTemperature,
            ForecastDate = entity.ForecastDate,
            Location = entity.Location,
            MaxTemperature = entity.MaxTemperature,
            MinTemperature = entity.MinTemperature
        };
    }
}