using Core.Interfaces;
using Core.Models;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MongoNotesRepository : INotesRepository
    {
        private readonly IMongoCollection<Notes> _notesCollection;

        public MongoNotesRepository(string connectionString, string databaseName, string collectionName)
        {
            BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _notesCollection = database.GetCollection<Notes>(collectionName);
        }

        public async Task AddAsync(Notes note)
        {
            await _notesCollection.InsertOneAsync(note);
        }

        public async Task<IEnumerable<Notes>> GetAllAsync()
        {
            return await _notesCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Notes?> GetByIdAsync(Guid id)
        {
            return await _notesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Notes note)
        {
            await _notesCollection.ReplaceOneAsync(x => x.Id == note.Id, note);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _notesCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
