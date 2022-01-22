using DataAccess.DataAccess;
using Domain.DataAccess;
using Domain.Models;
using Domain.Services;
using EventBus.Kafka;
using EventBus.Kafka.Abstraction.Enums;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDao _accountDao;
        private readonly IPaymentDao _paymentDao;
        private readonly IKafkaAccountProducer _kafkaAccountProducer;

        public AccountService(
            IAccountDao accountDao,
            IPaymentDao paymentDao,
            IKafkaAccountProducer kafkaProducer)
        {
            _accountDao = accountDao;
            _paymentDao = paymentDao;
            _kafkaAccountProducer = kafkaProducer;
        }

        public async Task DeleteById(Guid id)
        {
            var payments = await _paymentDao.GetByAccountId(id);
            if (payments.Any())
            {
                return;
            }

            await _accountDao.DeleteById(id);
        }

        public Task<List<Account>> GetAll() => _accountDao.GetAll();

        public Task<Account> GetById(Guid id) => _accountDao.GetById(id);

        public Task<List<Account>> GetByPersonId(Guid personId) => _accountDao.GetByPersonId(personId);

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
