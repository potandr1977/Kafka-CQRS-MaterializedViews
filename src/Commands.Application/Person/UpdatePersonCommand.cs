using MediatR;
using System;

namespace Commands.Application.Commands
{
    public class UpdatePersonCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Inn { get; set; }

        public DateTime TimeStamp { get; init; }
    }
}
