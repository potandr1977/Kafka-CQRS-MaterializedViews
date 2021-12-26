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

        public Task<Person> GetById(Guid id);
    }
}
