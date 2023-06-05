
using System;

namespace Messages
{
    public record ProjectionMessage
    {
        public string Id { get; set; }

        public long TimeStamp { get; init; }
    }
}
