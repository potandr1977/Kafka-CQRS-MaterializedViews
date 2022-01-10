using Domain.Models;
using Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application.Commands
{
    public class GetAccountHandler : IRequestHandler<GetAccountQuery, Account>
    {
        private readonly IAccountService _accountService;

        public GetAccountHandler(IAccountService accountService) =>
            _accountService = accountService;

        public Task<Account> Handle(GetAccountQuery request, CancellationToken cancellationToken) =>
            _accountService.GetById(request.Id);
    }
}
