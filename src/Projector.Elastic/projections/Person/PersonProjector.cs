using DataAccess.DataAccess;
using EventBus.Kafka.Abstraction.Messages;
using Queries.Core.dataaccess;
using System;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Person
{
    public class PersonProjector : IPersonProjector
    {
        private readonly IPersonDao _personDao;
        private readonly IPersonSimpleViewDao _personSimpleViewDao;

        public PersonProjector(IPersonSimpleViewDao personSimpleViewDao)
        {
            _personSimpleViewDao = personSimpleViewDao;
        }

        public async Task ProjectOne(UpdatePersonProjectionMessage message)
        {
            var person = new Queries.Core.models.Person
            {
                Id = message.PersonId,
                Name = message.Name,
                Inn = message.Inn,
            };

            await _personSimpleViewDao.Save(person);
        }
    }
}
