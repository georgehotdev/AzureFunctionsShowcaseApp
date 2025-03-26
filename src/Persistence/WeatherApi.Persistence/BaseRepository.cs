using Azure.Data.Tables;
using Azure;
using System.Linq.Expressions;
using WeatherApi.Persistence.Abstractions;
using WeatherApp.Infrastructure.Abstractions;

namespace WeatherApi.Persistence
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, ITableEntity, new()
    {
        protected readonly TableClient TableClient;

        public BaseRepository(ITableServiceClientProvider tableServiceClientProvider, string tableName)
        {
            TableClient = tableServiceClientProvider.TableServiceClient.GetTableClient(tableName);
        }

        public async Task<T?> GetAsync(string partitionKey, string rowKey)
        {
            try
            {
                var response = await TableClient.GetEntityAsync<T>(partitionKey, rowKey);
                return response.Value;
            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                return null;
            }
        }

        public async Task<bool> ExistsAsync(string partitionKey, string rowKey)
        {
            try
            {
                await TableClient.GetEntityAsync<T>(partitionKey, rowKey);
                return true;
            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                return false;
            }
        }

        public async Task AddAsync(T entity)
        {
            await TableClient.AddEntityAsync(entity);
        }

        public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate)
        {
            var results = new List<T>();

            await foreach (var entity in TableClient.QueryAsync(predicate))
            {
                results.Add(entity);
            }

            return results;
        }
    }
}
