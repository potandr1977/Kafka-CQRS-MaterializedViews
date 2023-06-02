using Queries.Core.Enums;
using System;

namespace Queries.Core.models
{
    public record Payment
    {
        public Guid Id { get; init; }

        public Guid AccountId { get; init; }

        public string AccountName { get; init; }

        public decimal Sum { get; init; }

        public PaymentTypeEnum PaymentType { get; init; }

        public DateTime TimeStamp { get; init; }
    }
}
