using EventBus.Kafka.Abstraction;
using EventBus.Kafka.Abstraction.Messages;
using Settings;

namespace EventBus.Kafka
{
    public class KafkaPaymentProducer: KafkaProducer<string, UpdatePaymentProjectionMessage>, IKafkaPaymentProducer
    {
        public KafkaPaymentProducer():base(KafkaSettings.AccountTopicName)
        {
        }
    }
}
