using DataAccess.DataAccess;
using Domain.Models;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDao _paymentDao;

        public PaymentService(IPaymentDao paymentDao)
        {
            _paymentDao = paymentDao;
        }

        public Task<List<Payment>> GetAll()
        {
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
