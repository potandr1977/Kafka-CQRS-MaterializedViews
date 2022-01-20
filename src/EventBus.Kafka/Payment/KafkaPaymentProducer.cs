using EventBus.Kafka.Abstraction;
using Messages;
using Settings;

namespace EventBus.Kafka
{
    public class KafkaPaymentProducer: KafkaStrKeyProducer<UpdatePaymentProjectionMessage>, IKafkaPaymentProducer
    {
        public KafkaPaymentProducer() : base(KafkaSettings.PaymentTopicName)
        {
        }
    }
}
