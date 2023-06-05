using Domain.DataAccess;
using EventBus.Kafka.Abstraction;
using Messages.Payment;
using Queries.Core.dataaccess;
using SimpleViewProjector.Elastic.Extensions;
using System;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Payment
{
    public class PaymentProjector : IPaymentProjector
    {
        private readonly IAccountDao _accountDao;
        private readonly IPaymentSimpleViewDao _paymentSimpleViewDao;
        private readonly IKafkaProducer<UpdatePaymentProjectionMessage> _kafkaUpdatePaymentProducer;
        private readonly IKafkaProducer<SavePaymentProjectionMessage> _kafkaSavePaymentProducer;
        private readonly IKafkaProducer<DeletePaymentProjectionMessage> _kafkaDeletePaymentProducer;

        public PaymentProjector(
            IAccountDao accountDao,
            IPaymentSimpleViewDao paymentSimpleViewDao,
            IKafkaProducer<UpdatePaymentProjectionMessage> kafkaUpdatePaymentProducer,
            IKafkaProducer<SavePaymentProjectionMessage> kafkaSavePaymentProducer,
            IKafkaProducer<DeletePaymentProjectionMessage> kafkaDeletePaymentProducer)
        {
            _accountDao = accountDao;
            _paymentSimpleViewDao = paymentSimpleViewDao;
            _kafkaUpdatePaymentProducer = kafkaUpdatePaymentProducer;
            _kafkaSavePaymentProducer = kafkaSavePaymentProducer;
            _kafkaDeletePaymentProducer = kafkaDeletePaymentProducer;
        }

        public async Task ProjectOne(UpdatePaymentProjectionMessage message)
        {
            var accountMongo = await _accountDao.GetById(message.AccountId);

            var payment = new Queries.Core.models.Payment
            {
                Id = message.PaymentId,
                AccountName = accountMongo?.Name,
                PaymentType = message.PaymentType.GetDescription<Queries.Core.Enums.PaymentTypeEnum>(),
                Sum = message.Sum,
                TimeStamp = message.TimeStamp,
            };
            try
            {
                await _paymentSimpleViewDao.Update(payment);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await _kafkaUpdatePaymentProducer.ProduceAsync(message);
        }

        public async Task ProjectOne(SavePaymentProjectionMessage message)
        {
            var accountMongo = await _accountDao.GetById(message.AccountId);

            var payment = new Queries.Core.models.Payment
            {
                Id = message.PaymentId,
                AccountName = accountMongo?.Name,
                PaymentType = message.PaymentType.GetDescription<Queries.Core.Enums.PaymentTypeEnum>(),
                Sum = message.Sum,
                TimeStamp = message.TimeStamp,
            };
            try
            {
                await _paymentSimpleViewDao.Save(payment);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await _kafkaSavePaymentProducer.ProduceAsync(message);
        }
        public async Task ProjectOne(DeletePaymentProjectionMessage message)
        {
            try
            {
                await _paymentSimpleViewDao.Delete(message.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await _kafkaDeletePaymentProducer.ProduceAsync(message);
        }
    }
}
