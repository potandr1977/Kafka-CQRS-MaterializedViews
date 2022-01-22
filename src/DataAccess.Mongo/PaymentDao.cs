using DataAccess.DataAccess;
using Domain.Models;
using MongoDB.Driver;
using Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Mongo
{
    public class PaymentDao : IPaymentDao
    {
        private readonly IMongoDatabase database;

        public PaymentDao(IMongoClient mongoClient) => database = mongoClient.GetDatabase(MongoSettings.DbName);

        private IMongoCollection<Payment> Payments => database.GetCollection<Payment>(MongoSettings.PaymentsCollectionName);

        public Task Save(Payment author) => Payments.InsertOneAsync(author);

        public Task<List<Payment>> GetAll()
        {
            var builder = new FilterDefinitionBuilder<Payment>();
            var filter = builder.Empty;

            return Payments.Find(filter).ToListAsync();
        }

        public Task<Payment> GetById(Guid id) => Payments.Find(payment => payment.Id == id).FirstOrDefaultAsync();

        public Task<List<Payment>> GetByAccountId(Guid accountId) => Payments.Find(payment => payment.AccountId == accountId).ToListAsync();

        public Task DeleteById(Guid id) =>
            Payments.DeleteOneAsync(account => account.Id == id);
    }
}
