﻿using MediatR;
using System;

namespace Commands.Application.Commands
{
    public class CreateAccountCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public Guid PersonId { get; set; }
    }
}
