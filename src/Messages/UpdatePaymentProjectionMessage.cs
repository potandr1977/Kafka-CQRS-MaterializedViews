using System;

namespace Messages
{
    public record UpdatePaymentProjectionMessage : UpdateProjectionMessage
    {
        public Guid PaymentId { get; init; }

        public Guid AccountId { get; init; }

        public decimal Sum { get; init; }

        public int PersonType { get; init; }
    }
}
