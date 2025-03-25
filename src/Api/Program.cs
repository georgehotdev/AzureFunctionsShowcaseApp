using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using WeatherApp.Api.Extensions;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.RegisterDependencies();


builder.Build().Run();
