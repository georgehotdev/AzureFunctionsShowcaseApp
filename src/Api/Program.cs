using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherApp.Api.Extensions;
using WeatherApp.Infrastructure.Abstractions;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Configure();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
    var storageInitializer = scope.ServiceProvider.GetRequiredService<IStorageInitializer>();
    await databaseInitializer.EnsureTablesExistAsync();
    await storageInitializer.EnsureContainersExistAsync();
}

app.Run();
