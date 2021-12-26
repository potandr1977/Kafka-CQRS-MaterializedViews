using DataAccess.DataAccess;
using Domain.Models;
using Domain.Services;
using EventBus.Kafka.Abstraction;
using Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public class PersonService : IPersonService
    {
        private readonly IPersonDao _personDao;
        

        public PersonService(IPersonDao personDao)
        {
            _personDao = personDao;
        }

        public Task<List<Person>> GetAll()
        {
            

            return _personDao.GetAll();
        }

        public Task<Person> GetById(Guid id)
        {
            return _personDao.GetById(id);
        }

        public Task Save(Person person)
        {
            return _personDao.Save(person);
        }
    }
}
