using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPaymentService
    {
        public Task Save(Payment payment);

        public Task<List<Payment>> GetAll();

        public Task<Payment> GetById(Guid id);

        public Task DeleteById(Guid id);
    }
}
