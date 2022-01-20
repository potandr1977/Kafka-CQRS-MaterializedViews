using EventBus.Kafka.Abstraction;
using Messages;

namespace EventBus.Kafka
{
    public interface IKafkaAccountConsumer : IKafkaConsumer<string,UpdateAccountProjectionMessage>
    {
    }
}
