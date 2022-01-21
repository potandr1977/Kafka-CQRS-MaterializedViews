using MediatR;
using System;

namespace Commands.Application.Commands
{
    public class DeletePersonCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
