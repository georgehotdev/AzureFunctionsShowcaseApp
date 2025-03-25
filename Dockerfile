FROM mcr.microsoft.com/dotnet/sdk:8.0 AS installer-env

WORKDIR /src
COPY . .

RUN dotnet publish src/Api/WeatherApp.Api.csproj -c Release -o /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

COPY --from=installer-env ["/home/site/wwwroot", "/home/site/wwwroot"]
