using System.Linq.Expressions;
using Azure.Data.Tables;

namespace WeatherApi.Persistence.Abstractions;

public interface IBaseRepository<T> where T : class, ITableEntity, new()
{
    Task<T?> GetAsync(string partitionKey, string rowKey);
    Task<bool> ExistsAsync(string partitionKey, string rowKey);
    Task AddAsync(T entity);
    Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate);
}