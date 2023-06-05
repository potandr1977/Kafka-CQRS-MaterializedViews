using Queries.Core.models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Queries.Core.dataaccess
{
    public interface IPersonSimpleViewDao
    {
        Task<IReadOnlyCollection<Person>> GetAll();

        Task<Person> GetById(Guid Id);

        Task Save(Person person);

        Task Update(Person account);

        Task Delete(string Id);
    }
}
