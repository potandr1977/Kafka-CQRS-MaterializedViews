using DataAccess.DataAccess;
using Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Mongo
{
    public class PersonDao : Dao<Person>, IPersonDao
    {

        public PersonDao(IMongoClient mongoClient) : base(mongoClient)
        {
        }

        protected override Expression<Func<Person, object>> GetSortFieldDef() => x => x.CreateDate;

    }
}
