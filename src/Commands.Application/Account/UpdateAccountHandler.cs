using Domain.Models;
using Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application.Commands
{
    public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand>
    {
        private readonly IAccountService _accountService;

        public UpdateAccountHandler(IAccountService accountService) => _accountService = accountService;

        public async Task<Unit> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account
            {
                Id = request.Id,
                Name = request.Name,
                PersonId = request.PersonId
            };

            await _accountService.Save(account);

            return Unit.Value;
        }
    }
}
