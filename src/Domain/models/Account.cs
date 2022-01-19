using System;

namespace Domain.Models
{
    public record Account
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public Guid PersonId { get; init; }

    }
}
