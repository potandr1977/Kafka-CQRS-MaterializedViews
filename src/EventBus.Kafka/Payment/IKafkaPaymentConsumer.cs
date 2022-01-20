using EventBus.Kafka.Abstraction;
using Messages;

namespace EventBus.Kafka
{
    public interface IKafkaPaymentConsumer : IKafkaConsumer<string,UpdatePaymentProjectionMessage>
    {
    }
}
