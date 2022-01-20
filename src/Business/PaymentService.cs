﻿using DataAccess.DataAccess;
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
            return _paymentDao.GetAll();
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
            },
            (int) PartitionEnum.Projector);
        }
    }
}
