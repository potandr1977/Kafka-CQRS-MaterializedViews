using MediatR;
using Queries.Core.models;
using System;
using System.Collections.Generic;

namespace Queries.Application.Accounts
{
    public class GetAccountQuery : IRequest<Account>
    {
        public Guid Id { get; set; }
    }
}
