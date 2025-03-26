using WeatherApi.Persistence.Entities;

namespace WeatherApi.Persistence.Abstractions;

public interface IWeatherForecastIngressRepository : IBaseRepository<WeatherForecastIngressEntity>
{
    Task<IEnumerable<WeatherForecastIngressEntity>> GetFilteredAsync(string location, DateTime fromDate, DateTime toDate);
}