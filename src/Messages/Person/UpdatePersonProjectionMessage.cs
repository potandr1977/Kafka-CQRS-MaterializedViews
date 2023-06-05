using System;

namespace Messages.Person
{
    public record UpdatePersonProjectionMessage : ProjectionMessage
    {
        public Guid PersonId { get; init; }

        public string Name { get; init; }

        public string Inn { get; init; }
    }
}
