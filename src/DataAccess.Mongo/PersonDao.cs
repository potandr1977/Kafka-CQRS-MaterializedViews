using DataAccess.DataAccess;
using Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Mongo
{
    public class PersonDao : IPersonDao
    {
        private readonly IMongoDatabase database;

        public PersonDao(IMongoClient mongoClient)
        {
            database = mongoClient.GetDatabase(MongoSettings.DbName);
        }

        private IMongoCollection<Person> Persons => database.GetCollection<Person>(MongoSettings.PersonsCollectionName);

        public Task Save(Person author)
        {
            return Persons.InsertOneAsync(author);
        }

        public Task<List<Person>> GetAll()
        {
            var builder = new FilterDefinitionBuilder<Person>();
            var filter = builder.Empty;

            return Persons.Find(filter).ToListAsync();
        }

        public Task<Person> GetById(Guid id)
        {
            return Persons.Find(new BsonDocument("Id", id.ToByteArray())).FirstOrDefaultAsync();
        }
    }
}
