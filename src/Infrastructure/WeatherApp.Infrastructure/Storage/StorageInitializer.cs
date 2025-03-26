using Microsoft.Extensions.Options;
using WeatherApp.Infrastructure.Abstractions;
using WeatherApp.Infrastructure.Configuration;

namespace WeatherApp.Infrastructure.Storage;

public class StorageInitializer : IStorageInitializer
{
    private readonly BlobStorageConfig _blobStorageConfig;
    private readonly IBlobStorageService _blobStorageService;

    public StorageInitializer(IBlobStorageService blobStorageService,
        IOptions<BlobStorageConfig> blobStorageConfigOptions)
    {
        _blobStorageService = blobStorageService;
        _blobStorageConfig = blobStorageConfigOptions.Value;
    }

    public async Task EnsureContainersExistAsync()
    {
        await _blobStorageService.CreateContainerIfNotExistsAsync(_blobStorageConfig.ContainerName);
    }
}