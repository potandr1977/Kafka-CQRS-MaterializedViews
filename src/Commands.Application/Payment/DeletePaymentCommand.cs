using MediatR;
using System;

namespace Commands.Application.Commands
{
    public class DeletePaymentCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
