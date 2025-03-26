using WeatherApi.Persistence.Entities;

namespace WeatherApi.Persistence.Abstractions;

public interface IWeatherForecastRepository : IBaseRepository<WeatherForecastEntity>
{
}