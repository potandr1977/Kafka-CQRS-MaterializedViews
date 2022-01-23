using DataAccess.DataAccess;
using Domain.Models;
using Domain.Services;
using Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Domain.DataAccess;
using EventBus.Kafka.Abstraction;

namespace Business
{
    public class PersonService : IPersonService
    {
        private readonly IPersonDao _personDao;
        private readonly IKafkaProducer<UpdatePersonProjectionMessage> _kafkaPersonProducer;
        private readonly IAccountDao _accountDao;

        public PersonService(
            IPersonDao personDao,
            IAccountDao accountDao,
            IKafkaProducer<UpdatePersonProjectionMessage> kafkaPersonProducer)
        {
            _personDao = personDao;
            _accountDao = accountDao;
            _kafkaPersonProducer = kafkaPersonProducer;
        }

        public async Task DeleteById(Guid id)
        {
            var accounts = await _accountDao.GetByPersonId(id);
            if (accounts.Any())
            {
                return;
            }

            await _personDao.DeleteById(id);
        }

        public Task<List<Person>> GetAll() => _personDao.GetAll();

        public Task<Person> GetById(Guid id) => _personDao.GetById(id);

        public async Task Save(Person person)
        {
            await _personDao.Save(person);

            await _kafkaPersonProducer.ProduceAsync(new UpdatePersonProjectionMessage
            {
                Id = Guid.NewGuid().ToString(),
                PersonId = person.Id,
                Name = person.Name,
                Inn = person.Inn
            });
        }
    }
}
