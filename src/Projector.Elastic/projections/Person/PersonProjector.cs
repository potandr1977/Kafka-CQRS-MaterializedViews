using EventBus.Kafka.Abstraction;
using Messages.Persons;
using Queries.Core.dataaccess;
using System;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Person
{
    public class PersonProjector : IPersonProjector
    {
        private readonly IPersonSimpleViewDao _personSimpleViewDao;
        private readonly IKafkaProducer<UpdatePersonProjectionMessage> _kafkaUpdatePersonProducer;
        private readonly IKafkaProducer<SavePersonProjectionMessage> _kafkaSavePersonProducer;
        private readonly IKafkaProducer<DeletePersonProjectionMessage> _kafkaDeletePersonProducer;

        public PersonProjector(
            IPersonSimpleViewDao personSimpleViewDao,
            IKafkaProducer<UpdatePersonProjectionMessage> kafkaUpdatePersonProducer,
            IKafkaProducer<SavePersonProjectionMessage> kafkaSavePersonProducer,
            IKafkaProducer<DeletePersonProjectionMessage> kafkaDeletePersonProducer)
        {
            _personSimpleViewDao = personSimpleViewDao;
            _kafkaUpdatePersonProducer = kafkaUpdatePersonProducer;
            _kafkaSavePersonProducer = kafkaSavePersonProducer;
            _kafkaDeletePersonProducer = kafkaDeletePersonProducer;
        }

        public async Task ProjectOne(UpdatePersonProjectionMessage message)
        {
            var person = new Queries.Core.models.Person
            {
                Id = message.PersonId,
                Name = message.Name,
                Inn = message.Inn,
                TimeStamp = message.TimeStamp,
            };
            try
            {
                await _personSimpleViewDao.Update(person);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await _kafkaUpdatePersonProducer.ProduceAsync(message);
        }

        public async Task ProjectOne(SavePersonProjectionMessage message)
        {
            var person = new Queries.Core.models.Person
            {
                Id = message.PersonId,
                Name = message.Name,
                Inn = message.Inn,
                TimeStamp = message.TimeStamp,
            };
            try
            {
                await _personSimpleViewDao.Save(person);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await _kafkaSavePersonProducer.ProduceAsync(message);
        }

        public async Task ProjectOne(DeletePersonProjectionMessage message)
        {
            try
            {
                await _personSimpleViewDao.Delete(message.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await _kafkaDeletePersonProducer.ProduceAsync(message);
        }
    }
}
