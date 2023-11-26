using Domain.dataaccess;
using Domain.exceptions;
using Domain.models;
using MongoDB.Driver;
using Settings;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Mongo
{
    public abstract class Dao<T> : IDao<T>
        where T : OptimisticEntity, new()
    {
        protected readonly IMongoDatabase database;
        protected readonly string collectionName;

        protected abstract Expression<Func<T, object>> GetSortFieldDef();

        protected IMongoCollection<T> Collection => database.GetCollection<T>(collectionName);

        public Dao(IMongoClient mongoClient, string collectionName)
        {
            database = mongoClient.GetDatabase(MongoSettings.DbName);
            this.collectionName = collectionName;
        }


        public async Task CreateAsync(T model) => 
            await Collection.ReplaceOneAsync(
                x => x.Id == model.Id,
                model,
                new ReplaceOptions
                {
                    IsUpsert = true
                });

        public async Task<T> UpdateAsync(T model)
        {
            var all = await GetAll();

            var stampedModel = model with { TimeStamp = DateTime.Now.Ticks };

            var result = await Collection.ReplaceOneAsync(
                x => x.Id == model.Id && x.TimeStamp == model.TimeStamp, stampedModel);

            if (result.ModifiedCount == 0)
            {
                throw new ConcurentOldVersionUpdateException($"Ошибка! Попытка сохранить устаревшую версию данных {nameof(T)}.");
            }

            return stampedModel;
        }

        public Task<List<T>> GetAll() =>
            Collection.Find(_ => true).ToListAsync();

        public Task<(int totalPages, IReadOnlyList<T> data)> GetPage(int pageNo, int pageSize) => Collection.AggregateByPage(
                Builders<T>.Filter.Empty,
                Builders<T>.Sort.Ascending(GetSortFieldDef()),
                page: pageNo,
                pageSize: pageSize);

        public Task<T> GetById(Guid id) =>
            Collection.Find(account => account.Id == id).FirstOrDefaultAsync();


        public Task DeleteById(Guid id) =>
            Collection.DeleteOneAsync(model => model.Id == id);
    }
}
