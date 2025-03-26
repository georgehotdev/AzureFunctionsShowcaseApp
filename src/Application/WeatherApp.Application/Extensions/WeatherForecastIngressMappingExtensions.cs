using WeatherApi.Domain;
using WeatherApi.Persistence.Entities;

namespace WeatherApp.Application.Extensions;

public static class WeatherForecastIngressMappingExtensions
{
    public static WeatherForecastIngress ToModel(this WeatherForecastIngressEntity weatherForecastIngressEntity)
    {
        return new WeatherForecastIngress
        {
            Id = weatherForecastIngressEntity.RowKey,
            Status = weatherForecastIngressEntity.Status,
            ForecastId = weatherForecastIngressEntity.ForecastId,
            Location = weatherForecastIngressEntity.Location,
            RequestedAt = weatherForecastIngressEntity.Timestamp?.DateTime ?? default
        };
    }
}