using Domain.DataAccess;
using Domain.Models;
using Domain.Services;
using EventBus.Kafka;
using EventBus.Kafka.Abstraction.Enums;
using Messages;
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

        public Task<List<Account>> GetAll() => _accountDao.GetAll();

        public Task<Account> GetById(Guid id) => _accountDao.GetById(id);

        public async Task Save(Account account)
        {
            await _accountDao.Save(account);

            await _kafkaAccountProducer.ProduceAsync(new UpdateAccountProjectionMessage
            {

                Id = Guid.NewGuid().ToString(),
                AccountId = account.Id,
                Name = account.Name,
                PersonId = account.PersonId
            },
            (int) PartitionEnum.Projector);
        }
    }
}
