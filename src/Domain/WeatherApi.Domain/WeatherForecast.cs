namespace WeatherApi.Domain;

public record WeatherForecast
{
    public long Id { get; set; }
    public string Location { get; set; }
    public long ForecastDate { get; set; }
    public double CurrentTemperature { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
}