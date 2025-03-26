# AzureFunctionsShowcaseApp

##### 1. How to run
>  Did not manage to make it work with docker as when using the isolated process model, there is an open bug (Timer-triggered functions do not work when schedule is set to 1 min)

- Create a local.settings.json file in the WeatherApp.Api (path : AzureFunctionsShowcaseApp\src\Api) and paste in the following configuration:
```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "WeatherApiConfig__ApiKey": "03c984c85c25d46f04c25371f741cb0d",
    "WeatherApiConfig__CityLookup": "London",
    "WeatherApiConfig__BaseUrl": "https://api.openweathermap.org/data/2.5/weather",
    "TableStorageConfig__Uri": "http://127.0.0.1:10002/devstoreaccount1",
    "TableStorageConfig__WeatherForecastsTableName": "WeatherForecasts",
    "TableStorageConfig__WeatherForecastIngressesTableName": "WeatherForecastIngresses",
    "TableStorageConfig__AccountName": "devstoreaccount1",
    "TableStorageConfig__AccountKey": "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==",
    "BlobStorageConfig__ContainerName": "weather-forecasts",
    "BlobStorageConfig__ConnectionString": "UseDevelopmentStorage=true"
     }
}
```
- Run the WeatherApp.Api function app from Visual Studio and navigate to `/api/swagger/ui`
- **(Optional?) **Might need to override the "TableStorageConfig__AccountKey" to match your emulator's AccountKey