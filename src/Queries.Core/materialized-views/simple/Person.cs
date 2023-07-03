using System;

namespace Queries.Core.models
{
    public record Person
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Inn { get; init; }

        public long TimeStamp { get; init; }
    }
}
