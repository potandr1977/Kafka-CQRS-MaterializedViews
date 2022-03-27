using Domain.DataAccess;
using Domain.Models;
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

        public AccountDao(IMongoClient mongoClient) => database = mongoClient.GetDatabase(MongoSettings.DbName);

        private IMongoCollection<Account> Accounts => database.GetCollection<Account>(MongoSettings.AccountsCollectionName);
        
        public Task Save(Account account) => Accounts.ReplaceOneAsync(
            x => x.Id == account.Id,
            account,
            new ReplaceOptions
            {
                IsUpsert = true 
            });

        public Task<List<Account>> GetAll()
        {
            var builder = new FilterDefinitionBuilder<Account>();
            var filter = builder.Empty;

            return Accounts.Find(filter).ToListAsync();
        }

        public Task<Account> GetById(Guid id) =>
            Accounts.Find(account => account.Id == id).FirstOrDefaultAsync();

        public Task DeleteById(Guid id) =>
            Accounts.DeleteOneAsync(account => account.Id == id);

        public Task<List<Account>> GetByPersonId(Guid personId) => 
            Accounts.Find(account => account.PersonId == personId).ToListAsync();
    }
}
