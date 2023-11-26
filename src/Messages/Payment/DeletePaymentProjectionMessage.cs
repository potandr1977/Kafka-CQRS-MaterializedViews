using System;

namespace Messages.Payments
{
    public record DeletePaymentProjectionMessage : ProjectionMessage
    {
        public Guid PaymentId { get; init; }
    }
}
