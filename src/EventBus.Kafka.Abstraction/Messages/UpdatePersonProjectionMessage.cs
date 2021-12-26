using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Kafka.Abstraction.Messages
{
    public class UpdatePersonProjectionMessage
    {
        public string Id { get; set; }

        public int PersonId { get; set; }

        public string Name {get; set; }

        public string Inn { get; set; }
    }
}
