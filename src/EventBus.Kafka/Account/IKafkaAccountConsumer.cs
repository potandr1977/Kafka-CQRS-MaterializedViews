using EventBus.Kafka.Abstraction;
using EventBus.Kafka.Abstraction.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Kafka
{
    public interface IKafkaAccountConsumer : IKafkaConsumer<string,UpdateAccountProjectionMessage>
    {
    }
}
