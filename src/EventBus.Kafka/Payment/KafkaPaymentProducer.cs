using EventBus.Kafka.Abstraction;
using EventBus.Kafka.Abstraction.Messages;
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
