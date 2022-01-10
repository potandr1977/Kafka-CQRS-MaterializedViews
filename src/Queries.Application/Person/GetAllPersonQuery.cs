using MediatR;
using Queries.Core.models;
using System;
using System.Collections.Generic;

namespace Queries.Application.Persons
{
    public class GetAllPersonQuery : IRequest<IReadOnlyCollection<Person>>
    {
    }
}
