using MediatR;
using Queries.Core.dataaccess;
using Queries.Core.models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Queries.Application.Persons
{
    public class GetAllPersonHandler : IRequestHandler<GetAllPersonQuery, IReadOnlyCollection<Person>>
    {
        private readonly IPersonSimpleViewDao _personSimpleViewDao;

        public GetAllPersonHandler(IPersonSimpleViewDao personSimpleViewDao) =>
            _personSimpleViewDao = personSimpleViewDao;

        public Task<IReadOnlyCollection<Person>> Handle(GetAllPersonQuery request, CancellationToken cancellationToken) =>
            _personSimpleViewDao.GetAll();
    }
}
