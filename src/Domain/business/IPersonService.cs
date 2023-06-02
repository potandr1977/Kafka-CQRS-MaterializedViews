using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPersonService
    {
        public Task<(int totalPages, IReadOnlyList<Person> data)> GetPage(int pageNo, int PageSize);

        public Task<Person> GetById(Guid id);

        public Task DeleteById(Guid id);

        public Task AddAccountToPerson(Guid personId, Account account);

        public Task Create(Person person);

        public Task Update(Person person);

    }
}
