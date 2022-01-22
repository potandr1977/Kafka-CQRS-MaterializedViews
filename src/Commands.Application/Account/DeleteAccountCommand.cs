using MediatR;
using System;

namespace Commands.Application.Commands
{
    public class DeleteAccountCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
