using System;

namespace EventBus.Kafka.Abstraction.Messages
{
    public class UpdatePaymentProjectionMessage : UpdateProjectionMessage
    {
        public Guid PaymentId { get; set; }

        public Guid AccountId { get; set; }

        public decimal Sum { get; set; }

        public int PersonType { get; set; }
    }
}
