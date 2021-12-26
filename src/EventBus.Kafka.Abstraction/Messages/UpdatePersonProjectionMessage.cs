using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Kafka.Abstraction.Messages
{
    public class UpdatePersonProjectionMessage : UpdateProjectionMessage
    {
        public int PersonId { get; set; }

        public string Name {get; set; }

        public string Inn { get; set; }
    }
}
