using DataAccess.DataAccess;
using Domain.Models;
using Domain.Services;
using EventBus.Kafka;
using EventBus.Kafka.Abstraction;
using EventBus.Kafka.Abstraction.Enums;
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

        public PaymentService(
            IPaymentDao paymentDao,
            IKafkaProducer<UpdatePaymentProjectionMessage> kafkaPaymentProducer)
        {
            _paymentDao = paymentDao;
            _kafkaPaymentProducer = kafkaPaymentProducer;
        }

        public Task DeleteById(Guid id) => _paymentDao.DeleteById(id);

        public Task<List<Payment>> GetPage(int pageNo, int PageSize) => _paymentDao.GetPage(pageNo, PageSize);

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
            await _paymentDao.Save(payment);

            await _kafkaPaymentProducer.ProduceAsync(new UpdatePaymentProjectionMessage
            {
                Id = Guid.NewGuid().ToString(),
                PaymentId = payment.Id,
                AccountId = payment.AccountId,
                PersonType = (int) payment.PaymentType,
                Sum = payment.Sum
            });
        }
    }
}
