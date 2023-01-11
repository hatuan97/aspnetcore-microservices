using Contracts.Domains;
using Contracts.Domains.Interfaces;
using MongoDB.Driver;
using Shared.Configurations;

namespace Infrastructure.Common;

public class MongoDbRepository<T> : MongoDbRepositoryAsync<T>, IMongoDbRepositoryBase<T> where T : MongoEntity
{
    public MongoDbRepository(IMongoClient client, MongoDbSettings settings)
    {
        Database = client.GetDatabase(settings.DatabaseName)
            .WithWriteConcern(WriteConcern.Acknowledged);
    }

    protected virtual IMongoCollection<T> Collection =>
        Database.GetCollection<T>(GetCollectionName());

    public IMongoCollection<T> FindAll(ReadPreference? readPreference = null)
    {
        return Database
            .WithReadPreference(readPreference ?? ReadPreference.Primary)
            .GetCollection<T>(GetCollectionName());
    }
}