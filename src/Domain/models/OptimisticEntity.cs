using System;

namespace Domain.models
{
    public record OptimisticEntity
    {
        public Guid Id { get; init; }

        public DateTime? TimeStamp { get; init; }
    }
}
