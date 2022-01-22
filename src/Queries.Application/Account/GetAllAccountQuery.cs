using MediatR;
using Queries.Core.models;
using System.Collections.Generic;

namespace Queries.Application.Accounts
{
    public class GetAllAccountQuery : IRequest<IReadOnlyCollection<Account>>
    {
    }
}
