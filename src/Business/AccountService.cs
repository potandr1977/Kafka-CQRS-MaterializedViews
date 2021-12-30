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
        private readonly IKafkaAccountProducer _kafkaAccountProducer;

        public AccountService(
            IAccountDao accountDao,
            IKafkaAccountProducer kafkaProducer)
        {
            _accountDao = accountDao;
            _kafkaAccountProducer = kafkaProducer;
        }

        public async Task<List<Account>> GetAll()
        {
            await _kafkaAccountProducer.ProduceAsync(new UpdateAccountProjectionMessage
            {
                Id = Guid.NewGuid().ToString(),
                AccountId = 1,
                Name = "Тестовый1",
                PersonId = 1
            });

            var p = new KafkaAccountProducer();
            await p.ProduceAsync(new UpdateAccountProjectionMessage
            {
                Id = Guid.NewGuid().ToString(),
                AccountId = 2,
                Name = "Тестовый2",
                PersonId = 2
            });

            return await _accountDao.GetAll();
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
