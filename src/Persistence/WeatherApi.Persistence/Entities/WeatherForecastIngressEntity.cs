using Azure;
using Azure.Data.Tables;
using WeatherApi.Domain;

namespace WeatherApi.Persistence.Entities
{
    public class WeatherForecastIngressEntity: ITableEntity
    {
        public WeatherForecastIngressEntity()
        {
        }

        private WeatherForecastIngressEntity(string location, WeatherForecastIngressStatus status)
        {
            PartitionKey = BuildPartitionKey(location, DateTime.UtcNow);
            RowKey = Guid.NewGuid().ToString();
            Location = location;
            Status = status;
        }

        private WeatherForecastIngressEntity(string location, WeatherForecastIngressStatus status, long forecastId) : this(location, status)
        {
            ForecastId = forecastId;
        }

        public static WeatherForecastIngressEntity CreateFailedIngress(string city)
        {
            return new WeatherForecastIngressEntity(city, WeatherForecastIngressStatus.Failure);
        }

        public static WeatherForecastIngressEntity CreateSuccessfulIngress(string city, long forecastId)
        {
            return new WeatherForecastIngressEntity(city, WeatherForecastIngressStatus.Success, forecastId);
        }

        public static string BuildPartitionKey(string location, DateTime date)
        {
            return $"{location}_{date:yyyy-MM-dd}";
        }

        public string Location { get; set; }

        public long ForecastId { get; set; }
        public WeatherForecastIngressStatus Status { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
