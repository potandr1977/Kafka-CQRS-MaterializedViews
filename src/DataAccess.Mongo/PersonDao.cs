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

        public Task Save(Person person) => Persons.ReplaceOneAsync(
            x => x.Id == person.Id, 
            person, 
            new ReplaceOptions
            { 
                IsUpsert = true
            });

        public async Task<List<Person>> GetPage(int pageNo, int pageSize)
        {
            var (totalPages, data) = await Persons.AggregateByPage(
                Builders<Person>.Filter.Empty,
                Builders<Person>.Sort.Ascending(x => x.CreateDate),
                page: pageNo,
                pageSize: pageSize);

            return data.ToList();
        }

        public Task<Person> GetById(Guid id) => Persons.Find(person => person.Id == id).FirstOrDefaultAsync();

        public Task DeleteById(Guid id) => Persons.DeleteOneAsync(account => account.Id == id);
    
    }
}
