using System;

namespace Messages.Person
{
    public record DeletePersonProjectionMessage : ProjectionMessage
    {
        public Guid PersonId { get; init; }
    }
}
