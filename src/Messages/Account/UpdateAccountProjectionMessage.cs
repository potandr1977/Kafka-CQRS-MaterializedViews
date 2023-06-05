using System;

namespace Messages.Account
{
    public record UpdateAccountProjectionMessage : ProjectionMessage
    {
        public Guid AccountId { get; init; }

        public string Name { get; init; }

        public Guid PersonId { get; init; }
    }
}
