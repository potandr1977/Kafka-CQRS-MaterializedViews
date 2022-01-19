using EventBus.Kafka.Abstraction;
using Messages;

namespace EventBus.Kafka
{
    public interface IKafkaPersonConsumer : IKafkaConsumer<string,UpdatePersonProjectionMessage>
    {
    }
}
