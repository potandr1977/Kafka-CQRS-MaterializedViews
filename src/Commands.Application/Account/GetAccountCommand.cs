using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Commands.Application.Commands
{
    public class GetAccountCommand : IRequest<Account>
    {
        public Guid Id { get; set; }
    }
}
