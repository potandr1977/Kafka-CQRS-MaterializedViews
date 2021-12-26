using Domain.DataAccess;
using Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Mongo
{
    public class AccountDao : IAccountDao
    {
        private readonly IMongoDatabase database;

        public AccountDao(IMongoClient mongoClient)
        {
            database = mongoClient.GetDatabase(MongoSettings.DbName);
        }

        private IMongoCollection<Account> Accounts => database.GetCollection<Account>(MongoSettings.AccountsCollectionName);

        public Task Save(Account author)
        {
            return Accounts.InsertOneAsync(author);
        }

        public Task<List<Account>> GetAll()
        {
            var builder = new FilterDefinitionBuilder<Account>();
            var filter = builder.Empty;

            return Accounts.Find(filter).ToListAsync();
        }

        public Task<Account> GetById(Guid id)
        {
            return Accounts.Find(new BsonDocument("Id", id.ToByteArray())).FirstOrDefaultAsync();
        }
    }
}
