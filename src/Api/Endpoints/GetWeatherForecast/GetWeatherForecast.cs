using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using WeatherApp.Application.Abstractions;

namespace WeatherApp.Api.Endpoints.GetWeatherForecast
{
    public class GetWeatherForecast
    {
        private readonly ILogger<GetWeatherForecast> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public GetWeatherForecast(ILogger<GetWeatherForecast> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        [Function("GetWeatherForecastBlob")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            if (!req.QueryString.HasValue || !req.Query.TryGetValue("logEntryId", out _))
            {
                return new BadRequestObjectResult("LogEntryId parameter is required");
            }

            var logEntryId = req.Query["logEntryId"].ToString();

            var data = await _weatherForecastService.GetWeatherForecast(logEntryId);

            if (!data.IsSuccess)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(data.Value);
        }
    }
}
