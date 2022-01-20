using System;

namespace Domain.Models
{
    public record Person
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Inn { get; init; }
    }
}
