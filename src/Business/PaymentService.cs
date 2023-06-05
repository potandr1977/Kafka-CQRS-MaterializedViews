using DataAccess.DataAccess;
using Domain.Models;
using Domain.Services;
using EventBus.Kafka.Abstraction;
using Infrastructure.Clients;
using Messages.Account;
using Messages.Payment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDao _paymentDao;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IKafkaProducer<UpdatePaymentProjectionMessage> _kafkaUpdatePaymentProducer;
        private readonly IKafkaProducer<SavePaymentProjectionMessage> _kafkaSavePaymentProducer;
        private readonly IKafkaProducer<DeletePaymentProjectionMessage> _kafkaDeletePaymentProducer;


        public PaymentService(
            IPaymentDao paymentDao,
            IExchangeRateService exchangeRateService,
            IKafkaProducer<UpdatePaymentProjectionMessage> kafkaUpdatePaymentProducer,
            IKafkaProducer<SavePaymentProjectionMessage> kafkaSavePaymentProducer,
            IKafkaProducer<DeletePaymentProjectionMessage> kafkaDeletePaymentProducer)
        {
            _paymentDao = paymentDao;
            _exchangeRateService = exchangeRateService;
            _kafkaUpdatePaymentProducer = kafkaUpdatePaymentProducer ?? throw new NullReferenceException(nameof(kafkaUpdatePaymentProducer));
            _kafkaSavePaymentProducer = kafkaSavePaymentProducer ?? throw new NullReferenceException(nameof(kafkaUpdatePaymentProducer));
            _kafkaDeletePaymentProducer = kafkaDeletePaymentProducer ?? throw new NullReferenceException(nameof(kafkaDeletePaymentProducer));
        }

        public async Task DeleteById(Guid id)
        {
            await _paymentDao.DeleteById(id);

            await _kafkaDeletePaymentProducer.ProduceAsync(new DeletePaymentProjectionMessage
            {
                Id = id.ToString(),
            });
        }

        public Task<(int totalPages, IReadOnlyList<Payment> data)> GetPage(int pageNo, int PageSize) => _paymentDao.GetPage(pageNo, PageSize);

        public Task<List<Payment>> GetByAccountId(Guid accountId) => _paymentDao.GetByAccountId(accountId);

        public Task<Payment> GetById(Guid id) => _paymentDao.GetById(id);

        public async Task Create(Payment payment)
        {
            if (payment.AccountId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(payment), "The account should not be null");
            }

            var rate = await _exchangeRateService.GetAccessRateAsync();

            var paymentRated = payment with { Sum = payment.Sum * rate };

            var updateTask = _paymentDao.CreateAsync(paymentRated);

            var notifyTask = _kafkaSavePaymentProducer.ProduceAsync(new SavePaymentProjectionMessage
            {
                Id = Guid.NewGuid().ToString(),
                PaymentId = paymentRated.Id,
                AccountId = paymentRated.AccountId,
                PaymentType = (int) paymentRated.PaymentType,
                Sum = paymentRated.Sum,
                TimeStamp = paymentRated.TimeStamp,
            });

            await Task.WhenAll(updateTask, notifyTask);
        }

        public async Task Update(Payment payment) 
        {
            if (payment.AccountId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(payment), "The account should not be null");
            }

            var rate = await _exchangeRateService.GetAccessRateAsync();

            var paymentRated = payment with { Sum = payment.Sum * rate };

            var stamped =  await _paymentDao.UpdateAsync(paymentRated);

            await _kafkaUpdatePaymentProducer.ProduceAsync(new UpdatePaymentProjectionMessage
            {
                Id = Guid.NewGuid().ToString(),
                PaymentId = stamped.Id,
                AccountId = stamped.AccountId,
                PaymentType = (int)stamped.PaymentType,
                Sum = stamped.Sum,
                TimeStamp = stamped.TimeStamp,
            });
        }
    }
}
