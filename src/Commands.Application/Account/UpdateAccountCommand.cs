using MediatR;
using System;

namespace Commands.Application.Commands
{
    public class UpdateAccountCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid PersonId { get; set; }
    }
}
