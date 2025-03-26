using WeatherApi.Persistence.Entities;

namespace WeatherApi.Persistence.Extensions;

public static class WeatherForecastIngressEntityMappingExtensions
{
    public static WeatherForecastIngressEntity ToEntity(this Domain.WeatherForecastIngress weatherForecastIngress)
    {
        return new WeatherForecastIngressEntity
        {
            Location = weatherForecastIngress.Location,
            ForecastId = weatherForecastIngress.ForecastId,
            Status = weatherForecastIngress.Status,
        };
    }
}