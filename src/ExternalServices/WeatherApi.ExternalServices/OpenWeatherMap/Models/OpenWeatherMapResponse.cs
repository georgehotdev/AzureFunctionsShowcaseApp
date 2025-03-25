using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WeatherApi.ExternalServices.OpenWeatherMap.Models;

internal record OpenWeatherMapResponse
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Location { get; set; }

    [JsonProperty("dt")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public long ForecastDate { get; set; }


    [JsonProperty("main")]
    public OpenWeatherMapForecastResponse WeatherForecast { get; set; }
}