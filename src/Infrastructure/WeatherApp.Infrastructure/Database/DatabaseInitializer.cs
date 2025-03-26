using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using WeatherApp.Infrastructure.Abstractions;
using WeatherApp.Infrastructure.Configuration;

namespace WeatherApp.Infrastructure.Database;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly TableStorageConfig _tableStorageConfig;
    private readonly TableServiceClient _tableServiceClient;

    public DatabaseInitializer(ITableServiceClientProvider tableServiceClientProvider, IOptions<TableStorageConfig> tableStorageConfig)
    {
        _tableStorageConfig = tableStorageConfig.Value;
        _tableServiceClient = tableServiceClientProvider.TableServiceClient;
    }

    public async Task EnsureTablesExistAsync()
    {
        await EnsureWeatherForecastsTableExistsAsync();
        await EnsureWeatherForecastsIngressesTableExistsAsync();
    }

    private async Task EnsureWeatherForecastsTableExistsAsync()
    {
        var tableClient = _tableServiceClient.GetTableClient(_tableStorageConfig.WeatherForecastsTableName);
        await tableClient.CreateIfNotExistsAsync();
    }
    private async Task EnsureWeatherForecastsIngressesTableExistsAsync()
    {
        var tableClient = _tableServiceClient.GetTableClient(_tableStorageConfig.WeatherForecastIngressesTableName);
        await tableClient.CreateIfNotExistsAsync();
    }
}