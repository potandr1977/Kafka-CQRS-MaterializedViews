using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Application.Commands
{
    public class CreateAccountCommand : IRequest
    {
        public string Name { get; set; }

        public Guid PersonId { get; set; }
    }
}
