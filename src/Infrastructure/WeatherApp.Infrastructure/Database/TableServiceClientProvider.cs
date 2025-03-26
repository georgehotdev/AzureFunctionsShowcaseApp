using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using WeatherApp.Infrastructure.Abstractions;
using WeatherApp.Infrastructure.Configuration;

namespace WeatherApp.Infrastructure.Database;

public class TableServiceClientProvider : ITableServiceClientProvider
{
    public TableServiceClientProvider(IOptions<TableStorageConfig> tableStorageConfigOptions)
    {
        var tableStorageConfig = tableStorageConfigOptions.Value;
        TableServiceClient = new TableServiceClient(
        new Uri(tableStorageConfig.Uri),
        new TableSharedKeyCredential(tableStorageConfig.AccountName, tableStorageConfig.AccountKey)
        );
    }

    public TableServiceClient TableServiceClient { get; }
}