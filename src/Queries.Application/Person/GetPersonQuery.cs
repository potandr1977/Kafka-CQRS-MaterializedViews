using MediatR;
using Queries.Core.models;
using System;

namespace Queries.Application.Persons
{
    public class GetPersonQuery : IRequest<Person>
    {
        public Guid Id { get; set; }
    }
}
