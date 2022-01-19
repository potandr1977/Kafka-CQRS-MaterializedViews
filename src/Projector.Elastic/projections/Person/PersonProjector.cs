using DataAccess.DataAccess;
using EventBus.Kafka;
using EventBus.Kafka.Abstraction.Enums;
using Messages;
using Queries.Core.dataaccess;
using System;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Person
{
    public class PersonProjector : IPersonProjector
    {
        private readonly IPersonSimpleViewDao _personSimpleViewDao;
        private readonly IKafkaPersonProducer _kafkaPersonProducer;

        public PersonProjector(
            IPersonSimpleViewDao personSimpleViewDao,
            IKafkaPersonProducer kafkaPersonProducer)
        {
            _personSimpleViewDao = personSimpleViewDao;
            _kafkaPersonProducer = kafkaPersonProducer;
        }

        public async Task ProjectOne(UpdatePersonProjectionMessage message)
        {
            var person = new Queries.Core.models.Person
            {
                Id = message.PersonId,
                Name = message.Name,
                Inn = message.Inn,
            };
            try
            {
                await _personSimpleViewDao.Save(person);
                var res = await _personSimpleViewDao.GetAll();
            }
            catch (Exception e)
            {
                var m = e.Message;
            }

            await _kafkaPersonProducer.ProduceAsync(message, (int)PartitionEnum.QueriesApiFirst);
            //await _kafkaPersonProducer.ProduceAsync(message, (int)PartitionEnum.QueriesApiSecond);
        }
    }
}
