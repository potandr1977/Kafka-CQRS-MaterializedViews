using Domain.Models;
using Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application.Commands
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, Guid>
    {
        private readonly IAccountService _accountService;

        public CreateAccountHandler(IAccountService accountService) => _accountService = accountService;

        public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var newGuid = Guid.NewGuid();
            var account = new Account
            {
                Id = newGuid,
                Name = request.Name,
                PersonId = request.PersonId
            };

            await _accountService.Save(account);

            return newGuid;
        }
    }
}
