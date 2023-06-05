using System;

namespace Domain.models
{
    public record OptimisticEntity
    {
        public Guid Id { get; init; }

        public long TimeStamp { get; init; } = DateTime.Now.Ticks;
    }
}
