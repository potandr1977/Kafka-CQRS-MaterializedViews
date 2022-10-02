using DataAccess.DataAccess;
using Domain.Models;
using Domain.Services;
using EventBus.Kafka;
using EventBus.Kafka.Abstraction;
using EventBus.Kafka.Abstraction.Enums;
using Infrastructure.Clients;
using Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDao _paymentDao;
        private readonly IKafkaProducer<UpdatePaymentProjectionMessage> _kafkaPaymentProducer;
        private readonly IExchangeRateService _exchangeRateService;

        public PaymentService(
            IPaymentDao paymentDao,
            IKafkaProducer<UpdatePaymentProjectionMessage> kafkaPaymentProducer,
            IExchangeRateService exchangeRateService)
        {
            _paymentDao = paymentDao;
            _kafkaPaymentProducer = kafkaPaymentProducer;
            _exchangeRateService = exchangeRateService;
        }

        public Task DeleteById(Guid id) => _paymentDao.DeleteById(id);

        public Task<(int totalPages, IReadOnlyList<Payment> data)> GetPage(int pageNo, int PageSize) => _paymentDao.GetPage(pageNo, PageSize);

        public Task<List<Payment>> GetByAccountId(Guid accountId)
        {
            return _paymentDao.GetByAccountId(accountId);
        }

        public Task<Payment> GetById(Guid id)
        {
            return _paymentDao.GetById(id);
        }

        public async Task Save(Payment payment)
        {
            if (payment.AccountId == Guid.Empty)
            { 
                throw new ArgumentNullException(nameof(payment),"The account should not be null");
            }

            var rate = await _exchangeRateService.GetAccessRateAsync();

            var paymentRated = payment with { Sum = payment.Sum * rate };

            await _paymentDao.Save(paymentRated);

            await _kafkaPaymentProducer.ProduceAsync(new UpdatePaymentProjectionMessage
            {
                Id = Guid.NewGuid().ToString(),
                PaymentId = paymentRated.Id,
                AccountId = paymentRated.AccountId,
                PersonType = (int) paymentRated.PaymentType,
                Sum = paymentRated.Sum
            });
        }
    }
}
