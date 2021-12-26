namespace EventBus.Kafka.Abstraction.Messages
{
    public class UpdatePaymentProjectionMessage
    {
        public string Id { get; set; }

        public int PaymentId { get; set; }

        public int AccountId { get; set; }

        public decimal Sum { get; set; }

        public int PersonType { get; set; }
    }
}
