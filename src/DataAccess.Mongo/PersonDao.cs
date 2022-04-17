using DataAccess.DataAccess;
using Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Mongo
{
    public class PersonDao : IPersonDao
    {
        private readonly IMongoDatabase database;

        public PersonDao(IMongoClient mongoClient) => database = mongoClient.GetDatabase(MongoSettings.DbName);

        private IMongoCollection<Person> Persons => database.GetCollection<Person>(MongoSettings.PersonsCollectionName);

        public Task<List<Person>> GetAll() =>
            Persons.Find(_ => true).ToListAsync();


        public Task Save(Person person) => Persons.ReplaceOneAsync(
            x => x.Id == person.Id, 
            person, 
            new ReplaceOptions
            { 
                IsUpsert = true
            });

        public Task<(int totalPages, IReadOnlyList<Person> data)> GetPage(int pageNo, int pageSize) => Persons.AggregateByPage(
                Builders<Person>.Filter.Empty,
                Builders<Person>.Sort.Ascending(x => x.CreateDate),
                page: pageNo,
                pageSize: pageSize);

        public Task<Person> GetById(Guid id) => Persons.Find(person => person.Id == id).FirstOrDefaultAsync();

        public Task DeleteById(Guid id) => Persons.DeleteOneAsync(account => account.Id == id);
    }
}
