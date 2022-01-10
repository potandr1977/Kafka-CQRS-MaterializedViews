using Domain.DataAccess;
using Domain.Models;
using Domain.Services;
using EventBus.Kafka;
using EventBus.Kafka.Abstraction.Messages;
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

        public Task Save(Account account)
        {
            return _accountDao.Save(account);
        }
    }
}
