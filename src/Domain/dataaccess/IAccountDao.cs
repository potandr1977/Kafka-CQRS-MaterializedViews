using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.DataAccess
{
    public interface IAccountDao
    {
        public Task Save(Account author);

        public Task<List<Account>> GetPage(int page, int pageSize);
        
        public Task<Account> GetById(Guid id);

        public Task<List<Account>> GetByPersonId(Guid personId);

        public Task DeleteById(Guid id);
    }
}
