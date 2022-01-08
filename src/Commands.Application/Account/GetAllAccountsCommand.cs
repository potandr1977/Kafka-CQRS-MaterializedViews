using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Commands.Application.Commands
{
    public class GetAllAccountsCommand : IRequest<List<Account>>
    {

    }
}
