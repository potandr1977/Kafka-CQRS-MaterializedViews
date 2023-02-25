using Domain.DataAccess;
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
    public class AccountService : IAccountService
    {
        private readonly IAccountDao _accountDao;
        private readonly IPaymentService _paymentService;
        private readonly IKafkaProducer<UpdateAccountProjectionMessage> _kafkaAccountProducer;

        public AccountService(
            IAccountDao accountDao,
            IPaymentService paymentService,
            IKafkaProducer<UpdateAccountProjectionMessage> kafkaProducer)
        {
            _accountDao = accountDao;
            _paymentService = paymentService;
            _kafkaAccountProducer = kafkaProducer;
        }

        public Task AddPaymentToAccount(Guid accountId, Payment payment)
        {
            if (payment.AccountId != accountId)
            {
                throw new InvalidOperationException($"Payment {payment.Id} belongs to other account");
            }

            return _paymentService.Create(payment);
        }

        public async Task DeleteById(Guid id)
        {
            var payments = await _paymentService.GetByAccountId(id);
            if (payments.Any())
            {
                return;
            }

            await _accountDao.DeleteById(id);
        }

        public Task<(int totalPages, IReadOnlyList<Account> data)> GetPage(int pageNo, int PageSize) => _accountDao.GetPage(pageNo, PageSize);

        public Task<Account> GetById(Guid id) => _accountDao.GetById(id);

        public Task<List<Account>> GetByPersonId(Guid personId) => _accountDao.GetByPersonId(personId);

        public async Task Create(Account account)
        {
            await _accountDao.CreateAsync(account);

            await _kafkaAccountProducer.ProduceAsync(new UpdateAccountProjectionMessage
            {
                Id = Guid.NewGuid().ToString(),
                AccountId = account.Id,
                Name = account.Name,
                PersonId = account.PersonId,
                TimeStamp= account.CreateDate,
            });
        }

        public async Task Update(Account account)
        {
            var stamped = await _accountDao.UpdateAsync(account);

            await _kafkaAccountProducer.ProduceAsync(new UpdateAccountProjectionMessage
            {
                Id = Guid.NewGuid().ToString(),
                AccountId = stamped.Id,
                Name = stamped.Name,
                PersonId = stamped.PersonId,
                TimeStamp= stamped.TimeStamp,
            });
        }
    }
}
