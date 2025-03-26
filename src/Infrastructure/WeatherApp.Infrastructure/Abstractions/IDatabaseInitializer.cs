namespace WeatherApp.Infrastructure.Abstractions;

public interface IDatabaseInitializer
{
    Task EnsureTablesExistAsync();
}