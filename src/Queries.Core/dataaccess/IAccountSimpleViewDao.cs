using Queries.Core.models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Queries.Core.dataaccess
{
    public interface IAccountSimpleViewDao
    {
        Task<IReadOnlyCollection<Account>> GetAll();

        Task<Account> GetById(Guid Id);

        Task Save(Account account);

        Task Update(Account account);

        Task Delete(string Id);
    }
}
