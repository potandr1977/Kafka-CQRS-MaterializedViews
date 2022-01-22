using MediatR;
using Queries.Core.dataaccess;
using Queries.Core.models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Queries.Application.Accounts
{
    public class GetAllAccountHandler : IRequestHandler<GetAllAccountQuery, IReadOnlyCollection<Account>>
    {
        private readonly IAccountSimpleViewDao _accountSimpleViewDao;

        public GetAllAccountHandler(IAccountSimpleViewDao accountSimpleViewDao) =>
            _accountSimpleViewDao = accountSimpleViewDao;

        public Task<IReadOnlyCollection<Account>> Handle(GetAllAccountQuery request, CancellationToken cancellationToken) =>
            _accountSimpleViewDao.GetAll();
    }
}
