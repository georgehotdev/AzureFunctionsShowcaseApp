using Azure.Storage.Blobs;
using System.Text;
using Ardalis.Result;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WeatherApp.Infrastructure.Abstractions;
using WeatherApp.Infrastructure.Configuration;

namespace WeatherApp.Infrastructure.Storage
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobContainerClient _containerClient;
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageService(IOptions<BlobStorageConfig> blobStorageConfigOptions)
        {
            var blobStorageConfig = blobStorageConfigOptions.Value;
            _blobServiceClient = new BlobServiceClient(blobStorageConfig.ConnectionString);
            _containerClient = _blobServiceClient.GetBlobContainerClient(blobStorageConfig.ContainerName);
            _containerClient.CreateIfNotExists(); // ensure the container exists
        }

        public async Task UploadJsonContentToBlobAsync<T>(string blobName, T content)
        {
            var blobClient = _containerClient.GetBlobClient(FormatFileName(blobName));
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(content)));
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        public async Task<Result<T?>> GetJsonBlobContentAsync<T>(string logEntryId)
        {
            var filename = FormatFileName(logEntryId);
            var blobClient = _containerClient.GetBlobClient(filename);

            if (await blobClient.ExistsAsync())
            {
                var downloadInfo = await blobClient.DownloadAsync();
                using var reader = new StreamReader(downloadInfo.Value.Content, Encoding.UTF8);
                var content = await reader.ReadToEndAsync();
                return Result<T?>.Success(JsonConvert.DeserializeObject<T>(content));
            }

            return Result<T?>.NotFound($"Blob '{filename}' not found.");
        }

        private string FormatFileName(string name) => $"{name}.json";


        public async Task CreateContainerIfNotExistsAsync(string containerName)
        {
            await _blobServiceClient.GetBlobContainerClient(containerName).CreateIfNotExistsAsync();
        }
    }
}
