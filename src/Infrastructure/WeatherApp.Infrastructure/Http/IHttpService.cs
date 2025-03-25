using Ardalis.Result;

namespace WeatherApp.Infrastructure.Http;

public interface IHttpService
{
    Task<Result<T?>> GetAsync<T>(string url);
}