using DataAccess.DataAccess;
using Domain.Models;
using Domain.Services;
using EventBus.Kafka.Abstraction;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class PersonService : IPersonService
    {
        private readonly IPersonDao _personDao;
        private readonly IKafkaProducer<UpdatePersonProjectionMessage> _kafkaPersonProducer;
        private readonly IAccountService _accountService;

        public PersonService(
            IPersonDao personDao,
            IAccountService accountService,
            IKafkaProducer<UpdatePersonProjectionMessage> kafkaPersonProducer)
        {
            _personDao = personDao;
            _accountService = accountService;
            _kafkaPersonProducer = kafkaPersonProducer;
        }

        public async Task AddAccountToPerson(Guid personId, Account account)
        {
            if (account.PersonId != personId)
            {
                throw new InvalidOperationException($"Account {account.Id} belongs to other person");
            }

            var person = await _personDao.GetById(personId);

            if (person.AccountIds.Any())
            {
                throw new InvalidOperationException($"Only one account per person allowed. PersonId {personId}, accoutnId {account.Id}");
            }

            await _accountService.Create(account);
        }

        public async Task DeleteById(Guid id)
        {
            var accounts = await _accountService.GetByPersonId(id);
            if (accounts.Any())
            {
                return;
            }

            await _personDao.DeleteById(id);
        }

        public Task<(int totalPages, IReadOnlyList<Person> data)> GetPage(int pageNo, int PageSize) => _personDao.GetPage(pageNo, PageSize);

        public Task<Person> GetById(Guid id) => _personDao.GetById(id);

        public async Task Create(Person person)
        {
            await _personDao.CreateAsync(person);

            //We don't want wait delivery confirmation.
            _kafkaPersonProducer.Produce(new UpdatePersonProjectionMessage
            {
                Id = Guid.NewGuid().ToString(),
                PersonId = person.Id,
                Name = person.Name,
                Inn = person.Inn,
                TimeStamp = person.TimeStamp,
            });
        }
        public async Task Update(Person person)
        {
            var stampedPerson = await _personDao.UpdateAsync(person);

            //We don't want wait delivery confirmation.
            _kafkaPersonProducer.Produce(new UpdatePersonProjectionMessage
            {
                Id = Guid.NewGuid().ToString(),
                PersonId = stampedPerson.Id,
                Name = stampedPerson.Name,
                Inn = stampedPerson.Inn,
                TimeStamp = stampedPerson.TimeStamp,
            });
        }
    }
}
