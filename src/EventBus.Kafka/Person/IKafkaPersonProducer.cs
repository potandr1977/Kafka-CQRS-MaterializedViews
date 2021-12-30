using EventBus.Kafka.Abstraction.Messages;

namespace EventBus.Kafka
{
    public interface IKafkaPersonProducer: IKafkaStrKeyProducer<UpdatePersonProjectionMessage>
    {
    }
}
