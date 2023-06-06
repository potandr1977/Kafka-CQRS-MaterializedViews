using DataAccess.DataAccess;
using Domain.Models;
using MongoDB.Driver;
using Settings;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Mongo
{
    public class PaymentDao : Dao<Payment>, IPaymentDao
    {
        public PaymentDao(IMongoClient mongoClient) : base(mongoClient, MongoSettings.PaymentsCollectionName)
        { 
        }

        protected override Expression<Func<Payment, object>> GetSortFieldDef() => x => x.CreateDate;

        public Task<List<Payment>> GetByAccountId(Guid accountId) => 
            Collection.Find(payment => payment.AccountId == accountId).ToListAsync();


    }
}
