using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using WeatherApp.Application.Abstractions;

namespace WeatherApp.Api.Endpoints.GetAllWeatherForecasts
{
    public class GetAllWeatherForecastIngresses
    {
        private readonly ILogger<GetAllWeatherForecastIngresses> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public GetAllWeatherForecastIngresses(ILogger<GetAllWeatherForecastIngresses> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        [Function(nameof(GetAllWeatherForecastIngresses))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            if (!req.QueryString.HasValue || !req.Query.TryGetValue("fromDate", out _) || !req.Query.TryGetValue("toDate", out _))
            {
                return new BadRequestObjectResult("FromDate and ToDate parameters are required");
            }

            var fromDate = DateTime.Parse(req.Query["fromDate"]!);
            var toDate = DateTime.Parse(req.Query["toDate"]!);
            var weatherForecastIngresses = await _weatherForecastService.GetAllWeatherForecastIngresses(fromDate, toDate);
            return new OkObjectResult(weatherForecastIngresses);
        }
    }
}
