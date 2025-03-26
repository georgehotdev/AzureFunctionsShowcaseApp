using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using WeatherApi.Domain;
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
        [OpenApiOperation(operationId: "GetWeatherForecastBlob", tags: new[] { "weather" })]
        [OpenApiParameter(name: "logEntryId", In = ParameterLocation.Query, Required = true, Type = typeof(Guid))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(WeatherForecast), Description = "Weather forecast result")]
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
