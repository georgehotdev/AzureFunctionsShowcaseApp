using Ardalis.Result;
using Newtonsoft.Json;
using WeatherApp.Infrastructure.Abstractions;

namespace WeatherApp.Infrastructure.Http;

public class HttpService : IHttpService
{
    private readonly HttpClient _httpClient;

    public HttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<Result<T?>> GetAsync<T>(string url)
    {
        using var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return Result<T?>.CriticalError("Failed to return result from endpoint.");
        }

        var content = await response.Content.ReadAsStringAsync();
        return new Result<T?>(JsonConvert.DeserializeObject<T>(content));
    }
}