using Domain.DataAccess;
using Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Mongo
{
    public class AccountDao :  Dao<Account>, IAccountDao
    {
        public AccountDao(IMongoClient mongoClient) : base(mongoClient)
        { 
        }

        public Task<List<Account>> GetByPersonId(Guid personId) => 
            Collection.Find(account => account.PersonId == personId).ToListAsync();

        protected override Expression<Func<Account, object>> GetSortFieldDef() => x => x.Name;
    }
}
