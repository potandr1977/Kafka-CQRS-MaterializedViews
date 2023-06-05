using System;

namespace Messages.Account
{
    public record DeleteAccountProjectionMessage : ProjectionMessage
    {
        public Guid AccountId { get; init; }
    }
}
