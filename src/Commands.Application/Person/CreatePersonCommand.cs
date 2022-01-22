using MediatR;
using System;

namespace Commands.Application.Commands
{
    public class CreatePersonCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public string Inn { get; set; }
    }
}
