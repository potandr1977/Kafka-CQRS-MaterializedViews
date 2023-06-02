using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPaymentService
    {
        public Task<(int totalPages, IReadOnlyList<Payment> data)> GetPage(int pageNo, int PageSize);

        public Task<Payment> GetById(Guid id);

        public Task DeleteById(Guid id);

        public Task<List<Payment>> GetByAccountId(Guid accountId);

        public Task Create(Payment payment);

        public Task Update(Payment payment);
    }
}
