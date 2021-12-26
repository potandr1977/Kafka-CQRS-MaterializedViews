using DataAccess.DataAccess;
using Domain.Models;
using Domain.Services;
using EventBus.Kafka;
using EventBus.Kafka.Abstraction.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDao _paymentDao;
        private readonly IKafkaPaymentProducer _kafkaPaymentProducer;

        public PaymentService(
            IPaymentDao paymentDao,
            IKafkaPaymentProducer kafkaPaymentProducer)
        {
            _paymentDao = paymentDao;
            _kafkaPaymentProducer = kafkaPaymentProducer;
        }

        public Task<List<Payment>> GetAll()
        {
            _kafkaPaymentProducer.ProduceAsync(new UpdatePaymentProjectionMessage
            {
                Id = Guid.NewGuid().ToString(),
                PaymentId = 1, 
                PersonType  = 2,
                AccountId = 1,
                Sum = 10
                
            }); 

            return _paymentDao.GetAll();
        }

        public Task<Payment> GetById(Guid id)
        {
            return _paymentDao.GetById(id);
        }

        public Task Save(Payment payment)
        {
            return _paymentDao.Save(payment);
        }
    }
}
