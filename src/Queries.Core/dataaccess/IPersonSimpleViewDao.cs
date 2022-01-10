using Queries.Core.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Queries.Core.dataaccess
{
    public interface IPersonSimpleViewDao
    {
        Task<IReadOnlyCollection<Person>> GetAll();

        Task Save(Person person);
    }
}
