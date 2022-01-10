using Domain.Models;
using Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application.Commands
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand>
    {
        private readonly IAccountService _accountService;

        public CreateAccountHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Unit> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                PersonId = request.PersonId
            };

            await _accountService.Save(account);

            return Unit.Value;
        }
    }
}
