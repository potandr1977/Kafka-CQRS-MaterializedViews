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

        public async Task Save(Account account)
        {
            if (account.TimeStamp == null)
            {
                await Accounts.ReplaceOneAsync(
                x => x.Id == account.Id,
                account,
                new ReplaceOptions
                {
                    IsUpsert = true
                });

                return;
            }

            var result = await Accounts.ReplaceOneAsync(
                x => x.Id == account.Id && x.TimeStamp == account.TimeStamp,
                account,
                new ReplaceOptions
                {
                    IsUpsert = true
                });

            if (result.ModifiedCount == 0)
            {
                throw new Exception("Ошибка! Попытка сохранить устаревшую версию данных.");
            }

        }

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
