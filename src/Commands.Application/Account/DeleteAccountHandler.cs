using Domain.Models;
using Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application.Commands
{
    public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand>
    {
        private readonly IAccountService _accountService;

        public DeleteAccountHandler(IAccountService accountService) => _accountService = accountService;

        public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            await _accountService.DeleteById(request.Id);
        }
    }
}
