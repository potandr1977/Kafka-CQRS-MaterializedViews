using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Commands.Application.Commands
{
    public class GetAccountQuery : IRequest<Account>
    {
        public Guid Id { get; set; }
    }
}
