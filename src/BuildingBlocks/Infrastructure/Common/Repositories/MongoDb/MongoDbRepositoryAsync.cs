using System.Linq.Expressions;
using Contracts.Domains;
using Contracts.Domains.Interfaces;
using Infrastructure.Extensions;
using MongoDB.Driver;

namespace Infrastructure.Common;

public class MongoDbRepositoryAsync<T> : IMongoDbRepositoryAsync<T> where T : MongoEntity
{
    private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

    public IMongoDatabase Database { get; protected set; }

    protected virtual IMongoCollection<T> Collection =>
        Database.GetCollection<T>(GetCollectionName());


    public async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        return await Collection.Find(filterBuilder.Empty).ToListAsync();
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        return await Collection.Find(filter).ToListAsync();
    }

    public async Task<T> GetAsync(string id)
    {
        var filter = filterBuilder.Eq(x => x.Id, id);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
    {
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(T item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        await Collection.InsertOneAsync(item);
    }

    public async Task UpdateAsync(T item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        var filter = filterBuilder.Eq(existingEntity => existingEntity.Id, item.Id);
        await Collection.ReplaceOneAsync(filter, item);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = filterBuilder.Eq(existingEntity => existingEntity.Id, id);
        await Collection.DeleteOneAsync(filter);
    }

    protected static string GetCollectionName()
    {
        return (typeof(T).GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault() as
            BsonCollectionAttribute)?.CollectionName;
    }
}