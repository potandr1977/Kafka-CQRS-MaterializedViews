using System;

namespace EventBus.Kafka.Abstraction.Messages
{
    public class UpdateAccountProjectionMessage : UpdateProjectionMessage
    {
        public Guid AccountId { get; set; }

        public string Name { get; set; }

        public Guid PersonId { get; set; }
    }
}
