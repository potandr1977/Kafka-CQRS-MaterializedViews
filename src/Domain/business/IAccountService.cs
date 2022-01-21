using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IAccountService
    {
        public Task Save(Account author);

        public Task<List<Account>> GetAll();

        public Task<Account> GetById(Guid id);

        public Task<List<Account>> GetByPersonId(Guid personId);

        public Task DeleteById(Guid id);
    }
}
