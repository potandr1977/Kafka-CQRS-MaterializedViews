using DataAccess.DataAccess;
using Domain.Models;
using MongoDB.Driver;
using Settings;
using System;
using System.Linq.Expressions;

namespace DataAccess.Mongo
{
    public class PersonDao : Dao<Person>, IPersonDao
    {

        public PersonDao(IMongoClient mongoClient) : base(mongoClient, MongoSettings.PersonsCollectionName)
        {
        }

        protected override Expression<Func<Person, object>> GetSortFieldDef() => x => x.CreateDate;

    }
}
