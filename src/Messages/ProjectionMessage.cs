
using System;

namespace Messages
{
    public record ProjectionMessage
    {
        public string Id { get; set; }

        public DateTime TimeStamp { get; init; }
    }
}
