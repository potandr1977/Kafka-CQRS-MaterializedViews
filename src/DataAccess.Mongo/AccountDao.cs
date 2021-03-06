using Domain.DataAccess;
using Domain.Models;
using MongoDB.Driver;
using Settings;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<List<Account>> GetAll() =>
            Accounts.Find(_ => true).ToListAsync();

        public Task<(int totalPages, IReadOnlyList<Account> data)> GetPage(int pageNo, int pageSize) => Accounts.AggregateByPage(
                Builders<Account>.Filter.Empty,
                Builders<Account>.Sort.Ascending(x => x.Name),
                page: pageNo,
                pageSize: pageSize);

        public Task<Account> GetById(Guid id) =>
            Accounts.Find(account => account.Id == id).FirstOrDefaultAsync();

        public Task DeleteById(Guid id) =>
            Accounts.DeleteOneAsync(account => account.Id == id);

        public Task<List<Account>> GetByPersonId(Guid personId) => 
            Accounts.Find(account => account.PersonId == personId).ToListAsync();
    }
}
