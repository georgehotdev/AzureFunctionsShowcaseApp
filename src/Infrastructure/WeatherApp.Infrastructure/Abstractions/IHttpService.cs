using Ardalis.Result;

namespace WeatherApp.Infrastructure.Abstractions;

public interface IHttpService
{
    Task<Result<T?>> GetAsync<T>(string url);
}