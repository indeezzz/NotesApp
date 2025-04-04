using Core.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class MongoRepository<TModel> : IRepository<TModel> where TModel : class
    {
        private readonly IMongoCollection<TModel> _Collection;

        public MongoRepository(string connectionString, string databaseName, string collectionName)
        {          
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _Collection = database.GetCollection<TModel>(collectionName);
        }
        public async Task DeleteAsync(Guid id)
        {
            var filter = Builders<TModel>.Filter.Eq("Id", id);
            await _Collection.DeleteOneAsync(filter);
        }

        public async Task AddAsync(TModel model)
        {
            await _Collection.InsertOneAsync(model);
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await _Collection.Find(_ => true).ToListAsync();
        }

        public async Task<TModel?> GetByIdAsync(Guid id)
        {
            var filter = Builders<TModel>.Filter.Eq("Id", id);
            return await _Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var idProperty = GetIdProperty(model);
            var filter = Builders<TModel>.Filter.Eq("Id", idProperty);
            await _Collection.ReplaceOneAsync(filter, model);
        }

        public async Task UpdateManyAsync(string idParam, Guid id)
        {
            var filter = Builders<TModel>.Filter.AnyEq(idParam, id);
            var update = Builders<TModel>.Update.Pull(idParam, id);
            await _Collection.UpdateManyAsync(filter, update);
        }

        private static object GetIdProperty(TModel model)
        {
            var idProperty = typeof(TModel).GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException($"Тип {typeof(TModel).Name} не содержит свойства Id.");

            return idProperty.GetValue(model)!;
        }
    }
}
