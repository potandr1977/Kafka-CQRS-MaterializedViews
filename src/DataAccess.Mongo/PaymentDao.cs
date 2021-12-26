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
    public class PaymentDao : IPaymentDao
    {
        private readonly IMongoDatabase database;

        public PaymentDao(IMongoClient mongoClient)
        {
            database = mongoClient.GetDatabase(MongoSettings.DbName);
        }

        private IMongoCollection<Payment> Payments => database.GetCollection<Payment>(MongoSettings.PaymentsCollectionName);

        public Task Save(Payment author)
        {
            return Payments.InsertOneAsync(author);
        }

        public Task<List<Payment>> GetAll()
        {
            var builder = new FilterDefinitionBuilder<Payment>();
            var filter = builder.Empty;

            return Payments.Find(filter).ToListAsync();
        }

        public Task<Payment> GetById(Guid id)
        {
            return Payments.Find(new BsonDocument("Id", id.ToByteArray())).FirstOrDefaultAsync();
        }
    }
}
