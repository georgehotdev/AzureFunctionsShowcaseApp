using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using WeatherApi.Persistence.Abstractions;
using WeatherApi.Persistence.Entities;
using WeatherApp.Infrastructure.Abstractions;
using WeatherApp.Infrastructure.Configuration;

namespace WeatherApi.Persistence.WeatherForecastIngress;

public class WeatherForecastIngressRepository : BaseRepository<WeatherForecastIngressEntity>, IWeatherForecastIngressRepository
{
    public WeatherForecastIngressRepository(ITableServiceClientProvider tableServiceClientProvider,
        IOptions<TableStorageConfig> tableStorageConfig) : base(tableServiceClientProvider,
        tableStorageConfig.Value.WeatherForecastIngressesTableName)
    {
    }

    public async Task<IEnumerable<WeatherForecastIngressEntity>> GetFilteredAsync(string location, DateTime fromDate, DateTime toDate)
    {
        var minPartitionKeyValue = WeatherForecastIngressEntity.BuildPartitionKey(location, fromDate);
        var maxPartitionKeyValue = WeatherForecastIngressEntity.BuildPartitionKey(location, toDate);
        var filter = TableClient.CreateQueryFilter($"PartitionKey ge {minPartitionKeyValue} and PartitionKey le {maxPartitionKeyValue}");

        var data = new List<WeatherForecastIngressEntity>();

        await foreach (var entity in TableClient.QueryAsync<WeatherForecastIngressEntity>(filter))
        {
            data.Add(entity);
        }

        return data;
    }
}