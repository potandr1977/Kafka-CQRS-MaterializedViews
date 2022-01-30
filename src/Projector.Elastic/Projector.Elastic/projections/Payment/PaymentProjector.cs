using Domain.DataAccess;
using EventBus.Kafka;
using EventBus.Kafka.Abstraction;
using EventBus.Kafka.Abstraction.Enums;
using Messages;
using Queries.Core.dataaccess;
using System;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Payment
{
    public class PaymentProjector : IPaymentProjector
    {
        private readonly IAccountDao _accountDao;
        private readonly IPaymentSimpleViewDao _paymentSimpleViewDao;
        private readonly IKafkaProducer<UpdatePaymentProjectionMessage> _kafkaPaymentProducer;

        public PaymentProjector(
            IAccountDao accountDao,
            IPaymentSimpleViewDao paymentSimpleViewDao,
            IKafkaProducer<UpdatePaymentProjectionMessage> kafkaPaymentProducer)
        {
            _accountDao = accountDao;
            _paymentSimpleViewDao = paymentSimpleViewDao;
            _kafkaPaymentProducer = kafkaPaymentProducer;
        }

        public async Task ProjectOne(UpdatePaymentProjectionMessage message)
        {
            var accountMongo = await _accountDao.GetById(message.AccountId);

            var payment = new Queries.Core.models.Payment
            {
                Id = message.PaymentId,
                AccountId = message.AccountId,
                AccountName = accountMongo?.Name,
                PaymentType = (Queries.Core.Enums.PaymentTypeEnum) message.PersonType,
                Sum = message.Sum
            };
            try
            {
                await _paymentSimpleViewDao.Save(payment);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await _kafkaPaymentProducer.ProduceAsync(message);
        }
    }
}
