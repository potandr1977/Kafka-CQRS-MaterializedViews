using System;

namespace Messages.Payment
{
    public record UpdatePaymentProjectionMessage : ProjectionMessage
    {
        public Guid PaymentId { get; init; }

        public Guid AccountId { get; init; }

        public decimal Sum { get; init; }

        public int PaymentType { get; init; }
    }
}
