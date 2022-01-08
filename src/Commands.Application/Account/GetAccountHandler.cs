using Domain.Models;
using Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application.Commands
{
    public class GetAccountHandler : IRequestHandler<GetAccountCommand, Account>
    {
        private readonly IAccountService _accountService;

        public GetAccountHandler(IAccountService accountService) =>
            _accountService = accountService;

        public Task<Account> Handle(GetAccountCommand request, CancellationToken cancellationToken) =>
            _accountService.GetById(request.Id);
    }
}
