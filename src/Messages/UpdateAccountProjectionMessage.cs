using System;

namespace Messages
{
    public record UpdateAccountProjectionMessage : UpdateProjectionMessage
    {
        public Guid AccountId { get; init; }

        public string Name { get; init; }

        public Guid PersonId { get; init; }
    }
}
