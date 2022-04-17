using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IAccountService
    {
        public Task Save(Account account);

        public Task<(int totalPages, IReadOnlyList<Account> data)> GetPage(int pageNo, int PageSize);

        public Task<Account> GetById(Guid id);

        public Task<List<Account>> GetByPersonId(Guid personId);

        public Task DeleteById(Guid id);

        public Task AddPaymentToAccount(Guid accountId, Payment payment);
    }
}
