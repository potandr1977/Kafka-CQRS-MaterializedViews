using Queries.Core.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Queries.Core.dataaccess
{
    public interface IAccountSimpleViewDao
    {
        Task<IReadOnlyCollection<Account>> GetAll();

        Task Save(Account account);
    }
}
