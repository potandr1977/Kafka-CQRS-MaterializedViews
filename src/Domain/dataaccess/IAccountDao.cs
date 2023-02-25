using Domain.dataaccess;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.DataAccess
{
    public interface IAccountDao : IDao<Account>
    {
        public Task<List<Account>> GetByPersonId(Guid personId);
    }
}
