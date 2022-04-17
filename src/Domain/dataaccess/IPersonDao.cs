using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DataAccess
{
    public interface IPersonDao
    {
        public Task Save(Person payment);

        public Task<List<Person>> GetAll();

        public Task<(int totalPages, IReadOnlyList<Person> data)> GetPage(int pageNo, int PageSize);

        public Task<Person> GetById(Guid id);

        public Task DeleteById(Guid id);
    }
}
