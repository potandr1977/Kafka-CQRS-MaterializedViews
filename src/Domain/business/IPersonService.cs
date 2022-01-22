using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPersonService
    {
        public Task Save(Person payment);

        public Task<List<Person>> GetAll();

        public Task<Person> GetById(Guid id);

        public Task DeleteById(Guid id);
    }
}
