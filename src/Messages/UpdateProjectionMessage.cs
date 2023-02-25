
using System;

namespace Messages
{
    public record UpdateProjectionMessage
    {
        public string Id { get; set; }

        public DateTime TimeStamp { get; init; }
    }
}
