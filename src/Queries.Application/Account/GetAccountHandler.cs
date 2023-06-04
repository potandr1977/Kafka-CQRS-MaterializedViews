using MediatR;
using Queries.Core.dataaccess;
using Queries.Core.models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Queries.Application.Accounts
{
    public class GetAccountHandler : IRequestHandler<GetAccountQuery, Account>
    {
        private readonly IAccountSimpleViewDao _accountSimpleViewDao;

        public GetAccountHandler(IAccountSimpleViewDao accountSimpleViewDao) =>
            _accountSimpleViewDao = accountSimpleViewDao;

        public Task<Account> Handle(GetAccountQuery request, CancellationToken _) =>
            _accountSimpleViewDao.GetById(request.Id);
    }
}
