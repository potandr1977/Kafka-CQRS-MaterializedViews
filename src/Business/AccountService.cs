using Domain.DataAccess;
using Domain.Models;
using Domain.Services;
using EventBus.Kafka;
using EventBus.Kafka.Abstraction;
using EventBus.Kafka.Abstraction.Messages;
using Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDao _accountDao;
        private readonly IKafkaAccountProducer _kafkaProducer;

        public AccountService(
            IAccountDao accountDao,
            IKafkaAccountProducer kafkaProducer)
        {
            _accountDao = accountDao;
            _kafkaProducer = kafkaProducer;
        }

        public Task<List<Account>> GetAll()
        {
            _kafkaProducer.ProduceAsync(null, new UpdateAccountProjectionMessage
            {
                Id = Guid.NewGuid().ToString(),
                AccountId = 1,
                Name = "Тестовый",
                PersonId = 2
            }); ;
            return _accountDao.GetAll();
        }

        public Task<Account> GetById(Guid id)
        {
            return _accountDao.GetById(id);
        }

        public Task Save(Account account)
        {
            return _accountDao.Save(account);
        }
    }
}
