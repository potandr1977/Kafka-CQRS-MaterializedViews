using Messages;

namespace EventBus.Kafka
{
    public interface IKafkaPersonProducer: IKafkaStrKeyProducer<UpdatePersonProjectionMessage>
    {
    }
}
