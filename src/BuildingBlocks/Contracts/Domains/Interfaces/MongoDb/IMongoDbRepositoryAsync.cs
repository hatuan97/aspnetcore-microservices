using System.Linq.Expressions;

namespace Contracts.Domains.Interfaces;

public interface IMongoDbRepositoryAsync<T> where T : MongoEntity
{
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
    Task<T> GetAsync(string id);
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    Task CreateAsync(T item);
    Task UpdateAsync(T item);
    Task DeleteAsync(string id);
}