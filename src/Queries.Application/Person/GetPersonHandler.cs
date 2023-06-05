using MediatR;
using Queries.Core.dataaccess;
using Queries.Core.models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Queries.Application.Persons
{
    public class GetPersonHandler : IRequestHandler<GetPersonQuery, Person>
    {
        private readonly IPersonSimpleViewDao _personSimpleViewDao;

        public GetPersonHandler(IPersonSimpleViewDao personSimpleViewDao) =>
            _personSimpleViewDao = personSimpleViewDao;

        public Task<Person> Handle(GetPersonQuery request, CancellationToken cancellationToken) =>
            _personSimpleViewDao.GetById(request.Id);
    }
}
