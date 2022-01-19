using Messages;

namespace EventBus.Kafka
{
    public interface IKafkaPaymentProducer: IKafkaStrKeyProducer<UpdatePaymentProjectionMessage>
    {
    }
}
