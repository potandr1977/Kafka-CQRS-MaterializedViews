using Messages;

namespace EventBus.Kafka
{
    public interface IKafkaAccountProducer: IKafkaStrKeyProducer<UpdateAccountProjectionMessage>
    {
    }
}
