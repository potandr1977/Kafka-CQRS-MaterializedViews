using Queries.Core.Enums;
using System;

namespace Queries.Core.models
{
    public record Payment
    {
        public Guid Id { get; init; }

        public string AccountName { get; init; }

        public decimal Sum { get; init; }

        public string PaymentType { get; init; }

        public long TimeStamp { get; init; }
    }
}
