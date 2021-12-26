using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccess
{
    public interface IPaymentDao
    {
        public Task Save(Payment payment);

        public Task<List<Payment>> GetAll();

        public Task<Payment> GetById(Guid id);
    }
}
