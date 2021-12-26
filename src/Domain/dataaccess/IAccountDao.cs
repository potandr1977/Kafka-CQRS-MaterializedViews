using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.DataAccess
{
    public interface IAccountDao
    {
        public Task Save(Account author);

        public Task<List<Account>> GetAll();
        
        public Task<Account> GetById(Guid id);
    }
}
