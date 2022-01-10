using Domain.Models;
using Domain.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application.Commands
{
    public class GetAllAccountsHandler : IRequestHandler<GetAllAccountsQuery, List<Account>>
    {
        private readonly IAccountService _accountService;

        public GetAllAccountsHandler(IAccountService accountService) =>
            _accountService = accountService;

        public Task<List<Account>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken) =>
            _accountService.GetAll();
    }
}
