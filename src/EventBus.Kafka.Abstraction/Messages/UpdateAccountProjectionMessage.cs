using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Kafka.Abstraction.Messages
{
    public class UpdateAccountProjectionMessage : UpdateProjectionMessage
    {
        public int AccountId { get; set; }

        public string Name { get; set; }

        public int PersonId { get; set; }
    }
}
