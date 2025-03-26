using Azure;
using Azure.Data.Tables;

namespace WeatherApi.Persistence.Entities;

public class WeatherForecastEntity : ITableEntity
{
    public WeatherForecastEntity(long id, string location, DateTime forecastDate, double currentTemperature,
        double minTemperature, double maxTemperature)
    {
        PartitionKey = $"{location}_{DateOnly.FromDateTime(forecastDate):yyyy-MM-dd}";
        RowKey = id.ToString();
        Location = location;
        ForecastDate = forecastDate;
        CurrentTemperature = currentTemperature;
        MinTemperature = minTemperature;
        MaxTemperature = maxTemperature;
    }

    public WeatherForecastEntity()
    {
    }

    public string Location { get; init; }
    public DateTime ForecastDate { get; init; }
    public double CurrentTemperature { get; init; }
    public double MinTemperature { get; init; }
    public double MaxTemperature { get; init; }


    public string PartitionKey { get; set; }
    public string RowKey { get; set; }

    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}