using EventBus.Kafka.Abstraction;
using Messages;
using Settings;

namespace EventBus.Kafka
{
    public class KafkaPaymentConsumer : KafkaConsumer<string, UpdatePaymentProjectionMessage>, IKafkaPaymentConsumer
    {
        public KafkaPaymentConsumer():base(KafkaSettings.BusinessGroupId,KafkaSettings.PaymentTopicName)
        {
        }
    }
}
