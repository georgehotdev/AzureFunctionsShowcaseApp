using System.Text.Json;
using Ardalis.Result;

namespace WeatherApp.Infrastructure.Http;

public class HttpService : IHttpService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Result<T?>> GetAsync<T>(string url)
    {
        using var client = _httpClientFactory.CreateClient();
        using var response = await client.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<T?>.CriticalError("Failed to return result from endpoint.");
        }

        var content = await response.Content.ReadAsStringAsync();
        return new Result<T?>(JsonSerializer.Deserialize<T>(content));
    }
}