using Ardalis.Result;

namespace WeatherApp.Infrastructure.Abstractions;

public interface IBlobStorageService
{
    Task UploadJsonContentToBlobAsync<T>(string blobName, T content);
    Task<Result<T?>> GetJsonBlobContentAsync<T>(string logEntryId);
    Task CreateContainerIfNotExistsAsync(string containerName);
}