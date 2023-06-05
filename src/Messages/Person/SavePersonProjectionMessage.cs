using System;

namespace Messages.Person
{
    public record SavePersonProjectionMessage : ProjectionMessage
    {
        public Guid PersonId { get; init; }

        public string Name { get; init; }

        public string Inn { get; init; }
    }
}
