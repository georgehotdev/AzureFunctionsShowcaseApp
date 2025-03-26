namespace WeatherApp.Infrastructure.Abstractions
{
    public interface IStorageInitializer
    {
        Task EnsureContainersExistAsync();
    }
}
