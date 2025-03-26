namespace WeatherApi.Domain;

public class WeatherForecastIngress
{
    public string Id{ get; set; }

    public string Location { get; set; }

    public DateTime RequestedAt { get; set; }

    public WeatherForecastIngressStatus Status { get; set; }
    public long ForecastId { get; set; }
}