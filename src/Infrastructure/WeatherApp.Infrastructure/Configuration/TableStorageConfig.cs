namespace WeatherApp.Infrastructure.Configuration;

public class TableStorageConfig
{
    public string Uri { get; set; }
    public string AccountKey { get; set; }
    public string AccountName { get; set; }
    public string WeatherForecastsTableName { get; set; }
    public string WeatherForecastIngressesTableName { get; set; }
}