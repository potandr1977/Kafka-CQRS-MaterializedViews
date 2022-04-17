using DataAccess.DataAccess;
using Domain.Models;
using MongoDB.Driver;
using Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Mongo
{
    public class PaymentDao : IPaymentDao
    {
        private readonly IMongoDatabase database;

        public PaymentDao(IMongoClient mongoClient) => database = mongoClient.GetDatabase(MongoSettings.DbName);

        private IMongoCollection<Payment> Payments => database.GetCollection<Payment>(MongoSettings.PaymentsCollectionName);

        public Task<List<Payment>> GetAll() =>
            Payments.Find(_ => true).ToListAsync();


        public Task Save(Payment payment) => Payments.ReplaceOneAsync(
            x => x.Id == payment.Id,
            payment,
            new ReplaceOptions
            { 
                IsUpsert = true 
            });

        public Task<(int totalPages, IReadOnlyList<Payment> data)> GetPage(int pageNo, int pageSize) => Payments.AggregateByPage(
                Builders<Payment>.Filter.Empty,
                Builders<Payment>.Sort.Ascending(x => x.CreateDate),
                page: pageNo,
                pageSize: pageSize);

        public Task<Payment> GetById(Guid id) => Payments.Find(payment => payment.Id == id).FirstOrDefaultAsync();

        public Task<List<Payment>> GetByAccountId(Guid accountId) => 
            Payments.Find(payment => payment.AccountId == accountId).ToListAsync();

        public Task DeleteById(Guid id) =>
            Payments.DeleteOneAsync(account => account.Id == id);
    }
}
