using EventBus.Kafka.Abstraction;
using EventBus.Kafka.Abstraction.Messages;

namespace EventBus.Kafka
{
    public interface IKafkaPaymentConsumer : IKafkaConsumer<string,UpdatePaymentProjectionMessage>
    {
    }
}
