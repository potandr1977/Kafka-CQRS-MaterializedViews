using System;

namespace Messages.Payment
{
    public record DeletePaymentProjectionMessage : ProjectionMessage
    {
        public Guid PaymentId { get; init; }
    }
}
