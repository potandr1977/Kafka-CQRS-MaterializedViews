using DataAccess.DataAccess;
using Domain.Models;
using Domain.Services;
using EventBus.Kafka;
using EventBus.Kafka.Abstraction.Enums;
using EventBus.Kafka.Abstraction.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public class PersonService : IPersonService
    {
        private readonly IPersonDao _personDao;
        private readonly IKafkaPersonProducer _kafkaPersonProducer;

        public PersonService(
            IPersonDao personDao,
            IKafkaPersonProducer kafkaPersonProducer)
        {
            _personDao = personDao;
            _kafkaPersonProducer = kafkaPersonProducer;
        }

        public Task<List<Person>> GetAll() => _personDao.GetAll();

        public Task<Person> GetById(Guid id) => _personDao.GetById(id);

        public async Task Save(Person person)
        {
            await _kafkaPersonProducer.ProduceAsync(new UpdatePersonProjectionMessage
            {
                Id = Guid.NewGuid().ToString(),
                PersonId = person.Id,
                Name = person.Name,
                Inn = person.Inn
            },
            (int) PartitionEnum.Person);

            await _personDao.Save(person);
        }
    }
}
