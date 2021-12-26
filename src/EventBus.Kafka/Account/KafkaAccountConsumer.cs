using EventBus.Kafka.Abstraction;
using EventBus.Kafka.Abstraction.Messages;
using Settings;

namespace EventBus.Kafka
{
    public class KafkaAccountConsumer : KafkaConsumer<string, UpdateAccountProjectionMessage>, IKafkaAccountConsumer
    {
        public KafkaAccountConsumer():base(KafkaSettings.BusinessGroupId,KafkaSettings.AccountTopicName)
        {
        }
    }
}
