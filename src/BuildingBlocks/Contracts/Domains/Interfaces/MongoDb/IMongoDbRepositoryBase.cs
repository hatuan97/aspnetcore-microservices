using MongoDB.Driver;

namespace Contracts.Domains.Interfaces;

public interface IMongoDbRepositoryBase<T> : IMongoDbRepositoryAsync<T> where T : MongoEntity
{
    IMongoCollection<T> FindAll(ReadPreference? readPreference = null);
}