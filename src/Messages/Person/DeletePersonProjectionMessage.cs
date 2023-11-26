using System;

namespace Messages.Persons
{
    public record DeletePersonProjectionMessage : ProjectionMessage
    {
        public Guid PersonId { get; init; }
    }
}
