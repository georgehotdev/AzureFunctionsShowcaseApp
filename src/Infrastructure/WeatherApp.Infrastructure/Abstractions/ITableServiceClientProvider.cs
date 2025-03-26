using Azure.Data.Tables;

namespace WeatherApp.Infrastructure.Abstractions;

public interface ITableServiceClientProvider
{
    public TableServiceClient TableServiceClient { get; }
}