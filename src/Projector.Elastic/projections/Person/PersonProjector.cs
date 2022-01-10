using DataAccess.DataAccess;
using EventBus.Kafka.Abstraction.Messages;
using Queries.Core.dataaccess;
using System;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Person
{
    public class PersonProjector : IPersonProjector
    {
        private readonly IPersonDao personDao;
        private readonly IPersonSimpleViewDao personSimpleViewDao;

        public Task ProjectOne(UpdatePersonProjectionMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
